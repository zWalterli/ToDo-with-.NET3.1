using System.Threading.Tasks;
using Todo.Domain.ViewModels;

namespace Todo.Service.Interfaces.Interfaces
{
    public interface IUserService
    {
        Task<TokenViewModel> Login(UserViewModel user);
        Task<UserViewModel> Register(UserViewModel user);
        Task<UserViewModel> Update(UserViewModel user);
        Task<UserViewModel> GetById(int UserId, bool includePassword);
        Task<UserViewModel> GetByEmail(string email);
    }
}