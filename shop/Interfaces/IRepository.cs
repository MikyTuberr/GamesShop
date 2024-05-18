namespace shop.Interfaces
{
    public interface IRepository
    {
        bool Add<T>(T entity);
        bool Update<T>(T entity);
        bool Delete<T>(T entity);
        bool Save();
    }
}


