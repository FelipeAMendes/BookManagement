using BookManagement.Core.Domain.Queries.AuthorQueries.Inputs;
using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models.AuthorModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Services.AuthorServices.Interfaces;

public interface IAuthorPageService
{
    Task<ICollection<AuthorQueryResult>> GetAllAsync();
    Task<AuthorQueryResult> GetByIdAsync(Guid id);
    Task<bool> AuthorExistsAsync(string name);
    Task<IPaginateQueryResult<AuthorQueryResult>> GetPaginatedAsync(FilterAuthorQueries filter);
    Task<ICommandResult<CommandNoneResult>> CreateAsync(AuthorModel authorModel);
    Task<ICommandResult<CommandNoneResult>> UpdateAsync(AuthorModel authorModel);
    Task<ICommandResult<CommandNoneResult>> DeleteAsync(Guid id);
}