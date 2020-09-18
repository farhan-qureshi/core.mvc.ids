using core.mvc.ids.Data.EF;
using core.mvc.ids.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace core.mvc.ids.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _dbContext;
        public UserRepository(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
