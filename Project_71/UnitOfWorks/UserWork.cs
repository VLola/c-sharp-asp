using Project_71.Contexts;
using Project_71.Repositories;

namespace Project_71.UnitOfWorks
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
