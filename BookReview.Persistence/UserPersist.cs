using System.Linq;
using System.Threading.Tasks;
using BookReview.Domain;
using BookReview.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Persistence
{
    public class UserPersist : IUserPersist
    {
        public readonly DataContext _context;

        public UserPersist(DataContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public async Task<User> GetAllUsersByEmailAsync(string email)
        {
            IQueryable<User> query = _context.Users;
            query = query.OrderBy(u => u.Id).Where(u => u.Email == email);
            var user = await query.FirstOrDefaultAsync();
            return user;
        }

        public async Task<User[]> GetAllUsersByNameAsync(string name)
        {
            IQueryable<User> query = _context.Users;
            query = query.OrderBy(u => u.Id).Where(u => u.UserName == name);
            return await query.ToArrayAsync();
        }

        public async Task<User[]> GetAllUsersAsync()
        {
            IQueryable<User> query = _context.Users;
            query = query.OrderBy(u => u.Id);
            return await query.ToArrayAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            IQueryable<User> query = _context.Users;
            query = query.OrderBy(u => u.Id).Where(u => u.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}