using ECommerce.Models;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Repository
{
    public interface IIdentityRoleRepo
    {
        IdentityRole GetById(string id);
        List<IdentityRole> GetAll();
        void Insert(IdentityRole entity);
        Task<IdentityResult> Update(string id, IdentityRole entity);
        //int Lockout(IdentityRole entity);
        Task<IdentityResult> Delete(IdentityRole entity);
        //int Active(IdentityRole entity);
        
    }
}
