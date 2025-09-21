using ECommerce.Configuration;
using ECommerce.Models;

namespace ECommerce.Repository
{
    public class OrderDetailsRepo : IOrderDetailsRepo
    {
        private readonly ECommerceDbContext context;
        public OrderDetailsRepo(ECommerceDbContext _context)
        {
            context = _context;
        }

        public void Delete(OrderDetails entity)
        {
            context.ordersDetails.Remove(entity);
            context.SaveChanges();
        }

        public List<OrderDetails> GetAll()
        {
            return context.ordersDetails.ToList();
        }

        public OrderDetails GetById(int id)
        {
            return context.ordersDetails.Single(od => od.Id == id);
        }

        public void Insert(OrderDetails entity)
        {
            context.ordersDetails.Add(entity);
            context.SaveChanges();
        }

        public void Update(int id, OrderDetails entity)
        {
            var orderDetailsDB = context.ordersDetails.SingleOrDefault(od=>od.Id == id);

            orderDetailsDB.OrderId = entity.OrderId;
            orderDetailsDB.ProductId = entity.ProductId;

            context.SaveChanges();
        }
    }
}
