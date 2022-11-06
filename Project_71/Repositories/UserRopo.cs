using Project_71.Contexts;
using Project_71_Library.Models;
using Project_71_Library.Repositories;
using System.Xml.Linq;

namespace Project_71.Repositories
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
            context.Users.Add(user);
            context.SaveChanges();
            return user.Id;
        }
        public bool CheckName(string name)
        {
            User? user = context.Users.FirstOrDefault(item=>item.Name == name);
            if (user != null) return true;
            else return false;
        }

        public bool CheckUser(string name, string password)
        {
            User? user = context.Users.FirstOrDefault(item => item.Name == name && item.Password == password);
            if (user != null) return true;
            else return false;
        }
        public bool Update(string name, string password)
        {
            User? user = context.Users.FirstOrDefault(item => item.Name == name);
            if (user != null) {
                user.Password = password;
                context.SaveChanges();
                return true;
            }
            else return false;
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
