using Project_70_Library.Contexts;
using Project_70_Library.Models;

namespace Project_70_Library.Repositories
{
    public class UserRepo : IRepo<Note>
    {
        public LocalHostContext context;
        public UserRepo(LocalHostContext context)
        {
            this.context = context;
        }
        public Note Get(int id) => context.Notes.Find(id);
        public IEnumerable<Note> GetAll() => context.Notes;
    }
}
