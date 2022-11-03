namespace Project_71_Library.Repositories
{
    public interface IRepo<T> where T : class
    {
        public T? Get(int id);
        public IEnumerable<T> GetAll();
    }
}
