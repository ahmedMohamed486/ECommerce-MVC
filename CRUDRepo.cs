
//using ECommerce.Configuration;

//namespace ECommerce.Repository
//{
//    public class CRUDRepo<T> : ICRUDRepo<T> where T : class
//    {
//        private readonly ECommerceDbContext context;
//        public CRUDRepo(ECommerceDbContext _context)
//        {
//            context = _context;
//        }


//        public void Delete(T entity)
//        {
//            context.Set<T>().Remove(entity);
//            context.SaveChanges();
//        }

//        public List<T> GetAll()
//        {
//            return context.Set<T>().ToList();
//        }

//        public T GetById(int id)
//        {
//            return context.Set<T>().Find(id);
//        }

//        public void Insert(T entity)
//        {
//            context.Set<T>().Add(entity);
//            context.SaveChanges();
//        }

//        public void Update(T entity)
//        {
//            context.Set<T>().Update(entity);
//            context.SaveChanges();
//        }
//    }
//}
