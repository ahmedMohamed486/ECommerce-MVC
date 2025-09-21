using ECommerce.Configuration;
using ECommerce.Models;

namespace ECommerce.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ECommerceDbContext context;
        public OrderRepo(ECommerceDbContext _context)
        {
            context = _context;
        }


        public void Delete(Order entity)
        {
            context.orders.Remove(entity);
            context.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return context.orders.ToList();
        }

        public Order GetById(int id)
        {
            return context.orders.SingleOrDefault(o => o.Id == id);
        }

        public void Insert(Order entity)
        {
            context.orders.Add(entity);
            context.SaveChanges();
        }

        public void Update(int id, Order entity)
        {
            var orderDb = context.orders.SingleOrDefault(o => o.Id == id);

            orderDb.Name = entity.Name;
            orderDb.Address = entity.Address;
            orderDb.OrderDate = entity.OrderDate;
            orderDb.OrderNo = entity.OrderNo;
            orderDb.Email = entity.Email;
            orderDb.PhoneNo = entity.PhoneNo;
            orderDb.OrderDetails = entity.OrderDetails;

            context.SaveChanges();
        }
    }
}
