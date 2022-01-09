using System.Threading.Tasks;
using BookReview.Domain;

namespace BookReview.Persistence.Interfaces
{
    public interface IUserPersist
    {
         Task<User[]> GetAllUsersAsync();
         Task<User[]> GetAllUsersByNameAsync(string name);
         Task<User> GetAllUsersByEmailAsync(string email);
         Task<User> GetUserByIdAsync(int id); 
    }
}