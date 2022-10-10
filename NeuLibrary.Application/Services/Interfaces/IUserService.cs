using NeuLibrary.Application.DTO;

namespace NeuLibrary.Application.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> CreateUser(UserDTO user);
        Task<IEnumerable<GetUserDTO>> GetUsers();
        public Task<int> LoginCheck(string Email, string Password);
    }
}
