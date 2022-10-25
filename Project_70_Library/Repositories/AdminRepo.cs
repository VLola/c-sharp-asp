using Project_70_Library.Contexts;
using Project_70_Library.Models;

namespace Project_70_Library.Repositories
{
    public class AdminRepo: UserRepo
    {
        public AdminRepo(LocalHostContext context) : base(context) { }

        public int Add(Note note)
        {
            LocalHostContext db = new LocalHostContext();
            db.Notes.Add(note);
            db.SaveChanges();
            return note.Id;
        }
        public bool Delete(int id)
        {
            Note? note = context.Notes.Find(id);
            if (note != null)
            {
                context.Notes.Remove(note);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}
