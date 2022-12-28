using AutoMapper;
using BookManagement.Core.Domain.Queries.PublisherQueries.Inputs;
using BookManagement.Core.Domain.Queries.PublisherQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.PublisherRepositories.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.PublisherQueries;

public class PublisherQueries : IPublisherQueries
{
    private readonly IMapper _mapper;
    private readonly IPublisherRepository _publisherRepository;

    public PublisherQueries(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<PublisherQueryResult>> GetAllAsync()
    {
        var publisher = await _publisherRepository.GetAllAsync();

        return _mapper.Map<ICollection<PublisherQueryResult>>(publisher);
    }

    public async Task<PublisherQueryResult> GetByIdAsync(Guid id)
    {
        var publisher = await _publisherRepository.GetByIdAsync(id);

        return _mapper.Map<PublisherQueryResult>(publisher);
    }

    public async Task<bool> PublisherExistsAsync(string name)
    {
        return await _publisherRepository.PublisherExistsAsync(x => x.Name == name);
    }

    public async Task<IPaginateQueryResult<PublisherQueryResult>> GetPaginatedAsync(FilterPublisherQueries filter)
    {
        var publishersPaginated = await _publisherRepository.GetPaginatedAsync(filter);

        return publishersPaginated;
    }
}