using BookManagement.Core.Domain.Queries.BookQueries.Inputs;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models.BookModels;
using System;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Services.BookServices.Interfaces;

public interface IBookPageService
{
    Task<BookQueryResult> GetByIdAsync(Guid id);
    Task<bool> BookExistsAsync(string isbn10, string isbn13);
    Task<IPaginateQueryResult<BookQueryResult>> GetPaginatedAsync(FilterBookQueries filter);
    Task<ICommandResult<CommandNoneResult>> CreateAsync(BookModel bookModel);
    Task<ICommandResult<CommandNoneResult>> UpdateAsync(BookModel bookModel);
    Task<ICommandResult<CommandNoneResult>> DeleteAsync(Guid id);
}