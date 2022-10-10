using NeuLibrary.Application.DTO;
using NeuLibrary.Domain.Entity;

namespace NeuLibrary.Application.Services.Interfaces
{
    public interface IUserRolePermissionService
    {
        public Task<string> AddAdmin(int userId);
        public Task<Boolean> VerifyAdmin(int userId);
        Task<IEnumerable<UserRolePermission>> GetAllAdmins();
        Task<IEnumerable<UserRolePermission>> GetAllNonAdmins();
        public Task<string> RemoveAdmin(int id);
    }
}
