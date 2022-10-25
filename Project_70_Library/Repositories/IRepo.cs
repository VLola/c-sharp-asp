namespace Project_70_Library.Repositories
{
    public interface IRepo<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
