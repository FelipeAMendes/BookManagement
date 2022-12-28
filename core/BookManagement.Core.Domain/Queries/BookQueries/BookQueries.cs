using AutoMapper;
using BookManagement.Core.Domain.Queries.BookQueries.Inputs;
using BookManagement.Core.Domain.Queries.BookQueries.QueryResults;
using BookManagement.Core.Domain.Repositories.BookRepositories.Interfaces;
using BookManagement.Core.Shared.Queries.Interfaces.QueryResults;

namespace BookManagement.Core.Domain.Queries.BookQueries;

public class BookQueries : IBookQueries
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookQueries(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<BookQueryResult>> GetAllAsync()
    {
        var books = await _bookRepository.GetAllAsync(true);

        return _mapper.Map<ICollection<BookQueryResult>>(books);
    }

    public async Task<BookQueryResult> GetByIdAsync(Guid id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        return _mapper.Map<BookQueryResult>(book);
    }

    public async Task<bool> BookExistsAsync(string isbn10, string isbn13)
    {
        return await _bookRepository.BookExistsAsync(x => x.Isbn10 == isbn10 || x.Isbn13 == isbn13);
    }

    public async Task<IPaginateQueryResult<BookQueryResult>> GetPaginatedAsync(FilterBookQueries filterBookCommand)
    {
        return await _bookRepository.GetPaginatedAsync(filterBookCommand);
    }
}