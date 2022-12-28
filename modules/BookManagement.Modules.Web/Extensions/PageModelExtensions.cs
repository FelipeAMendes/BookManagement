using BookManagement.Core.Shared.Extensions.EnumerableExtensions;
using BookManagement.Modules.Web.Models;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace BookManagement.Modules.Web.Extensions;

public static class PageModelExtensions
{
    public static void AddNotification(this PageModel pageModel, NotificationType type, string message, IEnumerable<ValidationFailure> errors)
    {
        var validationResult = errors.ToList();
        var notification = new DefaultNotificationModel
        {
            Type = type,
            Messages = validationResult.Any() ? validationResult.Select(x => x.ErrorMessage) : message.ToGenericList()
        };

        pageModel?.TempData.Put("DefaultNotification", notification);
    }
}