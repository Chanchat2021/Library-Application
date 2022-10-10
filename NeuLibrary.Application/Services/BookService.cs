using NeuLibrary.Application.DTO;
using NeuLibrary.Application.Exceptions;
using NeuLibrary.Application.Services.Interfaces;
using NeuLibrary.Domain.Entity;
using NeuLibrary.Infrastructure.Repositories.Interfaces;

namespace NeuLibrary.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _genericRepositoryBook;
        private readonly IGenericRepository<Reserve> _genericRepositoryReserve;
        public BookService(IGenericRepository<Book> genericRepositoryBook, IGenericRepository<Reserve> genericRepositoryReserve)
        {
            _genericRepositoryBook = genericRepositoryBook;
            _genericRepositoryReserve = genericRepositoryReserve;
        }
        public async Task<string> AddBook(CreateBookDTO addBook)
        {
            var data = new Book
            {
                Author = addBook.Author,
                Publisher = addBook.Publisher,
                Title = addBook.Title,
                Abstract = addBook.Abstract,
                Genre = addBook.Genre,
                Thumbnails = addBook.Thumbnails
            };
            await _genericRepositoryBook.Create(data);
            return ($"Book Added Successfully");
        }
        //enlist= returnedbook+newbook
        public async Task<string> EnlistBook(int id)
        {
            var query = _genericRepositoryBook.GetQuery();
            var result = query.Where(x => x.Id == id).FirstOrDefault();
            if (result != null)
            {
                result.IsAvailable = true;
                result.IsReserved = false;
                await _genericRepositoryBook.Update(result);
                return ($"Book Enlisted Successfully");
            }
            else
            {
                throw new KeyNotFoundException("Book Id Not Found");
            }
        }
        public async Task<IEnumerable<GetBookDTO>> GetBook()
        {
            var result = await _genericRepositoryBook.GetAll();
            var response = new List<GetBookDTO>();
            foreach (var item in result)
            {
                var data = new GetBookDTO();
                data.Id = item.Id;
                data.Author = item.Author;
                data.Publisher = item.Publisher;
                data.Title = item.Title;
                data.Abstract = item.Abstract;
                data.Genre = item.Genre;
                data.IsAvailable = item.IsAvailable;
                data.IsReserved=item.IsReserved;
                data.Thumbnails = item.Thumbnails;
                response.Add(data);
            }
            return response;
        }

        public async Task<IEnumerable<GetBookDTO>> GetAllAvailableBooks()
        {
            var result = await _genericRepositoryBook.GetAll();
            var response = new List<GetBookDTO>();
            foreach (var item in result)
            {
                if(item.IsAvailable==true && item.IsReserved == false)
                {
                    var data = new GetBookDTO();
                    data.Id = item.Id;
                    data.Author = item.Author;
                    data.Publisher = item.Publisher;
                    data.Title = item.Title;
                    data.Abstract = item.Abstract;
                    data.Genre = item.Genre;
                    data.IsAvailable = item.IsAvailable;
                    data.IsReserved = item.IsReserved;
                    data.Thumbnails = item.Thumbnails;
                    response.Add(data);
                }
                
            }
            return response;
        }

        public async Task<IEnumerable<GetBookDTO>> GetAllReservedBooks()
        {
            var result = await _genericRepositoryBook.GetAll();
            var response = new List<GetBookDTO>();
            foreach (var item in result)
            {
                if (item.IsAvailable == false && item.IsReserved == true)
                {
                    var data = new GetBookDTO();
                    data.Id = item.Id;
                    data.Author = item.Author;
                    data.Publisher = item.Publisher;
                    data.Title = item.Title;
                    data.Abstract = item.Abstract;
                    data.Genre = item.Genre;
                    data.IsAvailable = item.IsAvailable;
                    data.IsReserved = item.IsReserved;
                    data.Thumbnails = item.Thumbnails;
                    response.Add(data);
                }

            }
            return response;
        }

        public async Task<IEnumerable<GetBookDTO>> GetAllNewBooks()
        {
            var result = await _genericRepositoryBook.GetAll();
            var response = new List<GetBookDTO>();
            foreach (var item in result)
            {
                if (item.IsAvailable == null && item.IsReserved == false)
                {
                    var data = new GetBookDTO();
                    data.Id = item.Id;
                    data.Author = item.Author;
                    data.Publisher = item.Publisher;
                    data.Title = item.Title;
                    data.Abstract = item.Abstract;
                    data.Genre = item.Genre;
                    data.IsAvailable = item.IsAvailable;
                    data.IsReserved = item.IsReserved;
                    data.Thumbnails = item.Thumbnails;
                    response.Add(data);
                }

            }
            return response;
        }

        public async Task<string> UpdateBook(UpdateBookDTO updateBook)
        {
            var query = _genericRepositoryBook.GetQuery();
            var response = query.Where(e => e.Id == updateBook.Id).FirstOrDefault();
            if (response != null)
            {
                response.Id = updateBook.Id;
                response.Author = updateBook.Author;
                response.Publisher = updateBook.Publisher;
                response.Title = updateBook.Title;
                response.Abstract = updateBook.Abstract;
                response.Genre = updateBook.Genre;
                response.IsAvailable = updateBook.IsAvailable;
                response.IsReserved = updateBook.IsReserved;
                response.Thumbnails = updateBook.Thumbnails;
                await _genericRepositoryBook.Update(response);
                return "Book Updated Successfully";
            }
            else
            {
                throw new KeyNotFoundException($"Book Id: {updateBook.Id} does not exist");
            }
        }
        public async Task<string> DelistBook(int id)
        {
            var query = _genericRepositoryBook.GetQuery();
            var result = query.Where(x => x.Id == id).FirstOrDefault();
            if (result != null)
            {
                //var queryForDelistedBook = _genericRepositoryBook.GetQuery();
                //var response = queryForDelistedBook.Where(x => x.IsReserved== true).FirstOrDefault();
                if (result.IsReserved == false)
                {
                    result.IsAvailable = false;
                    await _genericRepositoryBook.Update(result);
                    return ($"Book Delisted Successfully");
                }
                else
                {
                    throw new MethodNotAllowedException("A Reserved Book can't Be Delisted");
                }
            }
            else
            {
                throw new KeyNotFoundException("Book Id Not Found");
            }
           
        }
        public async Task<IEnumerable<Book>> SearchBooks(string search)
        {
            var query = _genericRepositoryBook.GetQuery();
            var result = query.Where(x => x.Title.Contains(search) || x.Author.Contains(search) || x.Genre.Contains(search));

            if (result.Length() == 0)
            {
                throw new KeyNotFoundException($"No Book Found");
            }
            return result;
        }
    }
}
