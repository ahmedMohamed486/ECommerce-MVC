using ECommerce.Models;
using ECommerce.View_Model;

namespace ECommerce.Repository
{
    public interface IApplicationUserRepo  
    {
        ApplicationUser GetById(string id);
        List<ApplicationUser> GetAll();
        void Insert(ApplicationUser entity);
        void Update(string id, ApplicationUser entity);
        int Lockout(ApplicationUser entity);
        int Delete(ApplicationUser entity);
        int Active(ApplicationUser entity);
        List<AllRolesWithUsersViewModel> GetUsersWithRoles();
    }
}
