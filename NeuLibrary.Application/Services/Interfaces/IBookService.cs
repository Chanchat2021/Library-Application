using NeuLibrary.Application.DTO;
using NeuLibrary.Domain.Entity;

namespace NeuLibrary.Application.Services.Interfaces
{
    public interface IBookService
    {
        public Task<string> AddBook(CreateBookDTO addBook);
        public Task<string> EnlistBook(int id);
        Task<IEnumerable<GetBookDTO>> GetBook();

        Task<IEnumerable<GetBookDTO>> GetAllAvailableBooks();
        Task<IEnumerable<GetBookDTO>> GetAllNewBooks();
        Task<IEnumerable<GetBookDTO>> GetAllReservedBooks();
        public Task<string> UpdateBook(UpdateBookDTO updateBook);
        public Task<string> DelistBook(int id);
        Task<IEnumerable<Book>> SearchBooks(string search);
    }
}
