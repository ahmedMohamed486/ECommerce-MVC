using ECommerce.Configuration;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly ECommerceDbContext context;
        public ProductRepo(ECommerceDbContext _context)
        {
            context = _context;
        }


        public void Delete(Product entity)
        {
            context.products.Remove(entity);
            context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return context.products.Include(p=>p.ProductType).ToList();
        }

        public Product GetById(int id)
        {
            return context.products.Include(p=>p.ProductType).SingleOrDefault(p => p.Id == id);
        }

        public void Insert(Product entity)
        {
            context.products.Add(entity);
            context.SaveChanges();
        }

        public void Update(int id , Product entity)
        {
            
            var pDb = context.products.Include(p=>p.ProductType).FirstOrDefault(p=>p.Id == id);

            pDb.Name = entity.Name;
            pDb.Price = entity.Price;
            //pDb.ProductType = entity.ProductType;
            pDb.Image = entity.Image;
            pDb.ProductColor = entity.ProductColor;
            pDb.IsAvailable = entity.IsAvailable;
            pDb.PTypeId = entity.PTypeId;
            
            //context.products.Update(pDb);
            context.SaveChanges();
        }
    }
}
