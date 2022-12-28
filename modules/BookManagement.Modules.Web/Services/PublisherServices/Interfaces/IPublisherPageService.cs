using BookManagement.Core.Domain.Queries.PublisherQueries.Inputs;
using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;
using BookManagement.Core.Shared.Commands;
using BookManagement.Core.Shared.Commands.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;
using BookManagement.Modules.Web.Models.PublisherModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagement.Modules.Web.Services.PublisherServices.Interfaces;

public interface IPublisherPageService
{
    Task<ICollection<PublisherQueryResult>> GetAllAsync();
    Task<PublisherQueryResult> GetByIdAsync(Guid id);
    Task<IPaginateQueryResult<PublisherQueryResult>> GetPaginatedAsync(FilterPublisherQueries filter);
    Task<bool> PublisherExistsAsync(string name);
    Task<ICommandResult<CommandNoneResult>> CreateAsync(PublisherModel publisherModel);
    Task<ICommandResult<CommandNoneResult>> UpdateAsync(PublisherModel publisherModel);
    Task<ICommandResult<CommandNoneResult>> DeleteAsync(Guid id);
}