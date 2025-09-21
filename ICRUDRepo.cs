namespace ECommerce.Repository
{
    public interface ICRUDRepo<T>
    {
        T GetById(int id);
        List<T> GetAll();
        void Insert (T entity);
        void Update (int id , T entity);
        void Delete (T entity);
    }
}
