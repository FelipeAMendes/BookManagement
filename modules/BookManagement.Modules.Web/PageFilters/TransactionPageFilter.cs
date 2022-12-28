using BookManagement.Core.Infra.Data.Interfaces;
using BookManagement.Core.Shared.Commands;
using BookManagement.Modules.Web.Extensions;
using BookManagement.Modules.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.PageFilters;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class TransactionPageFilter : Attribute, IAsyncPageFilter
{
    private readonly IBookContext _bookContext;

    private readonly ICollection<string> _httpMethodsToHandle = new List<string>
    {
        HttpMethod.Post.Method.ToLower(),
        HttpMethod.Put.Method.ToLower(),
        HttpMethod.Delete.Method.ToLower(),
        HttpMethod.Patch.Method.ToLower()
    };

    private readonly IServiceScopeFactory _serviceProvider;

    public TransactionPageFilter(IBookContext bookContext, IServiceScopeFactory serviceProvider)
    {
        _bookContext = bookContext;
        _serviceProvider = serviceProvider;
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        var httpMethodsToHandle = _httpMethodsToHandle.Contains(context.HandlerMethod?.HttpMethod.ToLower());
        if (httpMethodsToHandle)
        {
            await BeginTransactionAsync();

            var resultContext = await next();

            await CommitTransactionAsync(resultContext);
        }
        else
        {
            await next.Invoke();
        }
    }

    private async Task BeginTransactionAsync()
    {
        await _bookContext.BeginTransactionAsync();
    }

    private async Task CommitTransactionAsync(PageHandlerExecutedContext resultContext)
    {
        try
        {
            var basePageModel = resultContext.HandlerInstance as BasePageModel;
            var commandResult = basePageModel?.CommandResult;
            string message;

            if (commandResult is null)
                return;

            if (commandResult.Success)
            {
                await _bookContext.CommitAsync();
                message = commandResult.GetMessage("Successful operation");
                basePageModel.AddNotification(NotificationType.Success, message, commandResult.Errors);
            }
            else
            {
                await _bookContext.RollbackAsync();
                message = commandResult.GetMessage("An error has occurred");
                basePageModel.AddNotification(NotificationType.Error, message, commandResult.Errors);
            }
        }
        catch (Exception ex)
        {
            var message = GetEnvironmentErrorMessage(ex);

            var commandResultRollback = CommandResult<CommandNoneResult>.CreateResult(false, message);

            resultContext.Result = new BadRequestObjectResult(commandResultRollback);

            await _bookContext.RollbackAsync();
        }
    }

    private string GetEnvironmentErrorMessage(Exception ex)
    {
        using var scope = _serviceProvider.CreateScope();

        var webHostEnvironment = scope.ServiceProvider.GetService(typeof(IWebHostEnvironment)) as IWebHostEnvironment;

        return webHostEnvironment.IsDevelopment()
            ? $"'{ex.Message}' {ex.StackTrace}"
            : "An unexpected error occurred";
    }
}