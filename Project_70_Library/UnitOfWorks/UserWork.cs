using Project_70_Library.Contexts;
using Project_70_Library.Repositories;

namespace Project_70_Library.UnitOfWorks
{
    public class UserWork
    {
        private LocalHostContext _context = new LocalHostContext();

        private UserRepo? _userRepo;

        public UserRepo UserRepo
        {
            get
            {
                if (_userRepo == null) _userRepo = new AdminRepo(_context);
                return _userRepo;
            }
            set { _userRepo = value; }
        }
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
