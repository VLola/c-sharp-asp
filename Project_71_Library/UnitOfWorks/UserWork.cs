using Project_71_Library.Contexts;
using Project_71_Library.Repositories;

namespace Project_71_Library.UnitOfWorks
{
    public class UserWork
    {
        private SmarterContext _context = new SmarterContext();

        private UserRepo? _userRepo;

        public UserRepo UserRepo
        {
            get
            {
                if (_userRepo == null) _userRepo = new UserRepo(_context);
                return _userRepo;
            }
            set { _userRepo = value; }
        }
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
