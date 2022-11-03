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

        public int Add(User user)
        {
            SmarterContext db = new SmarterContext();
            db.Users.Add(user);
            db.SaveChanges();
            return user.Id;
        }
        public bool Delete(int id)
        {
            User? user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}
