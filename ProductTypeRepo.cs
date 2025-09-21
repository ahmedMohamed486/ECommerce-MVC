using ECommerce.Configuration;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repository
{
    public class ProductTypeRepo : IProductTypeRepo
    {
        private readonly ECommerceDbContext context;
        public ProductTypeRepo(ECommerceDbContext _context)
        {
            context = _context;
        }


        public void Delete(ProductType entity)
        {
            context.productTypes.Remove(entity);
            context.SaveChanges();
        }

        public List<ProductType> GetAll()
        {
            return context.productTypes.Include(pt=>pt.Products).ToList();
        }

        public ProductType GetById(int id)
        {
            return context.productTypes.SingleOrDefault(pt => pt.Id == id);
        }

        public void Insert(ProductType entity)
        {
            context.productTypes.Add(entity);
            context.SaveChanges();
        }

        public void Update(int id, ProductType entity)
        {
            var pType = context.productTypes.FirstOrDefault(p=>p.Id == entity.Id);
            pType.PType = entity.PType;
            pType.Products = entity.Products;

            context.SaveChanges();
        }
    }
}
