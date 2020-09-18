using core.mvc.ids.Models;
using System.Threading.Tasks;

namespace core.mvc.ids
{
    public interface IUserRepository
    {
        public Task<User> GetUserAsync(string username, string password);
    }
}