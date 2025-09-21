using ECommerce.Configuration;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Repository
{
    public class IdentityRoleRepo : IIdentityRoleRepo
    {
        private readonly ECommerceDbContext context;
        RoleManager<IdentityRole> roleManager;
        public IdentityRoleRepo(ECommerceDbContext _context, RoleManager<IdentityRole> roleManager)
        {
            context = _context;
            this.roleManager = roleManager;
        }


        public async Task<IdentityResult> Delete(IdentityRole entity)
        {
            return await roleManager.DeleteAsync(entity);
           
        }

        public List<IdentityRole> GetAll()
        {
            return context.Roles.ToList();
        }

        public IdentityRole GetById(string id)
        {
            return context.Roles.SingleOrDefault(r => r.Id == id);
        }

        public void Insert(IdentityRole entity)
        {
            context.Roles.Add(entity);
        }

        public async Task<IdentityResult> Update(string id, IdentityRole entity)
        {
            var role = GetById(id);
          
            role.Name = entity.Name;

            return await roleManager.UpdateAsync(role);
            
        }
    }
}
