using AutoMapper;
using BookManagement.Core.Domain.Queries.AuthorQueries.Inputs;
using BookManagement.Core.Domain.Queries.AuthorQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.AuthorRepositories.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.AuthorQueries;

public class AuthorQueries : IAuthorQueries
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public AuthorQueries(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<AuthorQueryResult>> GetAllAsync()
    {
        var authors = await _authorRepository.GetAllAsync();

        return _mapper.Map<ICollection<AuthorQueryResult>>(authors);
    }

    public async Task<AuthorQueryResult> GetByIdAsync(Guid id)
    {
        var author = await _authorRepository.GetByIdAsync(id);

        return _mapper.Map<AuthorQueryResult>(author);
    }

    public async Task<bool> AuthorExistsAsync(string name)
    {
        return await _authorRepository.AuthorExistsAsync(x =>
            string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase));
    }

    public async Task<IPaginateQueryResult<AuthorQueryResult>> GetPaginatedAsync(FilterAuthorQueries filter)
    {
        var authorsPaginated = await _authorRepository.GetPaginatedAsync(filter);

        return authorsPaginated;
    }
}