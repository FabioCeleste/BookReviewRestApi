using System.Threading.Tasks;
using BookReview.Application.Dtos;

namespace BookReview.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> AddUser(UserDto model);
        Task<UserDto> UpdateUser(UserDto model, int userId);
        Task<bool> DeleteUser(int userId);
        Task<UserNoPasswordDto[]> GetAllUsersAsync();
        Task<UserNoPasswordDto[]> GetAllUsersByNameAsync(string name);
        Task<UserDto> GetAllUsersByEmailAsync(string email);
        Task<UserNoPasswordDto> GetUserByIdAsync(int id);
    }
}