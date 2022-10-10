using NeuLibrary.Application.DTO;
using NeuLibrary.Application.Services.Interfaces;
using NeuLibrary.Domain.Entity;
using NeuLibrary.Infrastructure.Repositories.Interfaces;

namespace NeuLibrary.Application.Services
{
    public class UserRolePermissionService : IUserRolePermissionService
    {
        private readonly IGenericRepository<UserRolePermission> _genericRepositoryUserRolePermission;
        private readonly IGenericRepository<User> _genericRepositoryUser;

        public UserRolePermissionService(IGenericRepository<UserRolePermission> genericRepositoryUserRolePermission, IGenericRepository<User> genericRepositoryUser)
        {
            _genericRepositoryUserRolePermission = genericRepositoryUserRolePermission;
            _genericRepositoryUser = genericRepositoryUser;
    }

        public async Task<string> AddAdmin(int userId)
        {
            var query = _genericRepositoryUserRolePermission.GetQuery();
            var result = query.Where(e => e.UserId == userId).FirstOrDefault();
            if (result != null)
            {
                result.IsAdmin = true;
                await _genericRepositoryUserRolePermission.Update(result);
                return ($"Admin Added Successfully");
            }
            
            else
            {
                throw new KeyNotFoundException("User Id Not Found");
            }
        }
        public async Task<IEnumerable<UserRolePermission>> GetAllAdmins()
        {
            var query = _genericRepositoryUserRolePermission.GetQuery();
            var result = query.Where(e => e.IsAdmin==true);
            
            return result;
        }

        public async Task<IEnumerable<UserRolePermission>> GetAllNonAdmins()
        {
            var query = _genericRepositoryUserRolePermission.GetQuery();
            var result = query.Where(e => e.IsAdmin == false);  
            return result;
        }
        public async Task<string> RemoveAdmin(int userId)
        {
            var query = _genericRepositoryUserRolePermission.GetQuery();
            var result = query.Where(e => e.UserId == userId).FirstOrDefault();
            if (result != null)
            {
                result.IsAdmin = false;
                await _genericRepositoryUserRolePermission.Update(result);
                return ($"Admin Removed Successfully");
            }

            else
            {
                throw new KeyNotFoundException("User Id Not Found");
            }
        }

        public async Task<Boolean> VerifyAdmin(int userId)
        {
            var query = _genericRepositoryUserRolePermission.GetQuery();
            var result = query.Where(e => e.UserId == userId).FirstOrDefault();
            if (result == null)
            {
                throw new KeyNotFoundException("User Id Not Found");
            }
            else
            {
                return (result.IsAdmin);
            }
        }



    }
}
