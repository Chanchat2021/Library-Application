using NeuLibrary.Application.DTO;
using NeuLibrary.Application.Exceptions;
using NeuLibrary.Application.Services.Interfaces;
using NeuLibrary.Domain.Entity;
using NeuLibrary.Infrastructure.Repositories.Interfaces;

namespace NeuLibrary.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepositoryUser;
        private readonly IGenericRepository<UserRolePermission> _genericRepositoryUserRolePermission;
        public UserService(IGenericRepository<User> genericRepositoryUser, IGenericRepository<UserRolePermission> genericRepositoryUserRolePermission)
        {
            _genericRepositoryUser = genericRepositoryUser;
            _genericRepositoryUserRolePermission = genericRepositoryUserRolePermission;
        }
        public async Task<string> CreateUser(UserDTO createUser)
        {
            var query = _genericRepositoryUser.GetQuery();
            var result = query.Where(e => e.Email == createUser.Email).FirstOrDefault();
            if (result == null)
            {
                var userData = new User
                {
                    Email = createUser.Email,
                    Password = createUser.Password
                };
                await _genericRepositoryUser.Create(userData);

                var response = query.Where(e => e.Email == createUser.Email).FirstOrDefault().Id;
                var data = new UserRolePermission
                {
                    UserId = response,
                    IsAdmin = false,
                };
                await _genericRepositoryUserRolePermission.Create(data);
                return ($"User Created Successfully");
            }
            else
            {
                throw new DuplicateException("User Already Exist");
            }
        }
        public async Task<IEnumerable<GetUserDTO>> GetUsers()
        {
            var result = await _genericRepositoryUser.GetAll();
            var response = new List<GetUserDTO>();
            foreach (var item in result)
            {
                var data = new GetUserDTO();
                data.Id = item.Id;
                data.Email = item.Email;
                response.Add(data);
            }
            return response;
        }
        public async Task<int> LoginCheck(string Email, string Password)
        {
            var query = _genericRepositoryUser.GetQuery();
            var result = query.Where(e => e.Email == Email && e.Password == Password).FirstOrDefault();
            if (result == null)
            {
                throw new UnauthorizedAccessException("Unathorized User");
            }
            else
            {
                return (result.Id);
            }
        }
    }
}
