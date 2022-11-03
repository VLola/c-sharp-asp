using Project_71_Library.Contexts;
using Project_71_Library.Models;

namespace Project_71_Library.Repositories
{
    public class UserRepo : IRepo<User>
    {
        public SmarterContext context;
        public UserRepo(SmarterContext context)
        {
            this.context = context;
        }
        public User? Get(int id) => context.Users.Find(id);
        public IEnumerable<User> GetAll() => context.Users;
    }
}
