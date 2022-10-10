using NeuLibrary.Application.DTO;
using NeuLibrary.Application.Exceptions;
using NeuLibrary.Application.Services.Interfaces;
using NeuLibrary.Domain.Entity;
using NeuLibrary.Infrastructure.Repositories.Interfaces;

namespace NeuLibrary.Application.Services
{
    public class ReserveService : IReserveService
    {
        private readonly IGenericRepository<Reserve> _genericRepositoryReserve;
        private readonly IGenericRepository<Book> _genericRepositoryBook;
        public ReserveService(IGenericRepository<Reserve> genericRepositoryReserve,IGenericRepository<Book> genericRepositoryBook)
        {
            _genericRepositoryReserve = genericRepositoryReserve;
            _genericRepositoryBook = genericRepositoryBook;
        }
        public async Task<string> ReserveBook(ReserveBookDTO reserveBook)
        {
            var data = new Reserve
            {
                UserId = reserveBook.UserId,
                BookId = reserveBook.BookId,
                ReservedDate=DateTime.UtcNow
            };
            var query = _genericRepositoryBook.GetQuery();
            var response = query.Where(e => e.Id == reserveBook.BookId).FirstOrDefault();
            if (response != null)
            {
                response.IsAvailable = false;
                response.IsReserved = true;
                await _genericRepositoryBook.Update(response);
            }
            await _genericRepositoryReserve.Create(data);
            return ($"Book Reserved Successfully");
        }
        public async Task<string> ReturnBook(int bookId)
        {
            var query = _genericRepositoryReserve.GetQuery();
            var id = query.Where(e => e.BookId == bookId).FirstOrDefault().Id;
            var response = await _genericRepositoryReserve.Delete(id);
            if (response != null)
            {
                var queryForBookTable = _genericRepositoryBook.GetQuery();
                var result = queryForBookTable.Where(e => e.Id == bookId).FirstOrDefault();
                if (result != null)
                {
                    result.IsAvailable = null;
                    result.IsReserved = false;
                    await _genericRepositoryBook.Update(result);
                    return ($"Book Returned Successfully");
                }
                else
                {
                    throw new MethodNotAllowedException("Book is not Reserved at the moment");
                }
            }
            else
            {
                throw new KeyNotFoundException("Book Id Not Found");
            }
            
        }
        public async Task<IEnumerable<GetReserveBooksDTO>> GetReserveBooks()
        {
            var result = await _genericRepositoryReserve.GetAll();
            var response = new List<GetReserveBooksDTO>();
            foreach (var item in result)
            {
                var data = new GetReserveBooksDTO();
                data.Id = item.Id;
                data.UserId = item.UserId;
                data.BookId = item.BookId;
                data.ReservedDate = item.ReservedDate;
                response.Add(data);
            }
            return response;
        }
    }
}
