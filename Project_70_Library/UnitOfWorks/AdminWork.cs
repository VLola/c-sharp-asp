using Project_70_Library.Contexts;
using Project_70_Library.Repositories;

namespace Project_70_Library.UnitOfWorks
{
    public class AdminWork
    {
        private LocalHostContext _context = new LocalHostContext();

        private AdminRepo? _adminRepo;

        public AdminRepo AdminRepo
        {
            get
            {
                if (_adminRepo == null) _adminRepo = new AdminRepo(_context);
                return _adminRepo;
            }
            set { _adminRepo = value; }
        }
        public void Dispose() => GC.SuppressFinalize(this);
    }
}
