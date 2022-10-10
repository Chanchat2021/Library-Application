using NeuLibrary.Application.DTO;

namespace NeuLibrary.Application.Services.Interfaces
{
    public interface IReserveService
    {
        public Task<string> ReserveBook(ReserveBookDTO reserveBook);
        Task<IEnumerable<GetReserveBooksDTO>> GetReserveBooks();
        public Task<string> ReturnBook(int bookId);
    }
}
