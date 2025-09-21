using ECommerce.Models;
using ECommerce.Repository;
using ECommerce.View_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IIdentityRoleRepo identityRoleRepo;
        private readonly IApplicationUserRepo applicationUserRepo;
        public RoleController(RoleManager<IdentityRole> _roleManager, 
            IIdentityRoleRepo _identityRoleRepo,IApplicationUserRepo _applicationUserRepo,
            UserManager<ApplicationUser> _userManager)
        {
            roleManager = _roleManager;
            identityRoleRepo = _identityRoleRepo;
            applicationUserRepo = _applicationUserRepo;
            userManager = _userManager;
        }


        public IActionResult Index()
        {
            List<IdentityRole> roles = identityRoleRepo.GetAll();
            ViewBag.Roles = roles;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            IdentityRole newRole = new IdentityRole();
            newRole.Name = name;

            IdentityResult result = await roleManager.CreateAsync(newRole);

            if (result.Succeeded) 
            {
                TempData["save"] = "Role Added successfully";
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        
            return View();
        }


        public IActionResult Edit(string id)
        {
            if (id == null)
                return NotFound();
            var role = identityRoleRepo.GetById(id);
            if (role == null)
                return NotFound();
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id , IdentityRole roleUpdated)
        {
            if (ModelState.IsValid)
            {
                var role = identityRoleRepo.GetById(id);
                if (role == null)
                    return NotFound();

                var result = await identityRoleRepo.Update(role.Id, roleUpdated);
                if (result.Succeeded)
                {
                    TempData["save"] = "Role Updated successfully";
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(roleUpdated);
        }


        public IActionResult Delete(string id)
        {
            if (id == null)
                return NotFound();
            var role = identityRoleRepo.GetById(id);
            if (role == null)
                return NotFound();
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, IdentityRole roleDeleted)
        {
            var role = identityRoleRepo.GetById(id);
            if (role == null)
                return NotFound();
            var result = await identityRoleRepo.Delete(role);
            if (result.Succeeded) 
            {
                TempData["save"] = "Role Deleted successfully";
                return RedirectToAction("Index");
            } 
            foreach(var error in result.Errors)
                ModelState.AddModelError(string.Empty,error.Description);

            return View(roleDeleted);
        }


        public async Task<IActionResult> Assign()
        {
            ViewBag.UserId = 
                new SelectList(applicationUserRepo.GetAll().Where(u=>u.LockoutEnd<DateTime.Now||u.LockoutEnd==null).ToList(), "Id", "UserName");
            ViewBag.RoleId =
                new SelectList(identityRoleRepo.GetAll(), "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(RoleUserViewModel roleUserVm)
        {
            var role = identityRoleRepo.GetById(roleUserVm.RoleId);
            var user = applicationUserRepo.GetById(roleUserVm.UserId);
            if (user == null || role == null)
                return NotFound();

            var isCheckedRoleAssign = await userManager.IsInRoleAsync(user, role.Name);
            if (isCheckedRoleAssign)
            {
                ViewBag.msg = "this user already assign this role";
                ViewBag.UserId =
                new SelectList(applicationUserRepo.GetAll().Where(u => u.LockoutEnd < DateTime.Now || u.LockoutEnd == null).ToList(), "Id", "UserName");
                ViewBag.RoleId =
                    new SelectList(identityRoleRepo.GetAll(), "Id", "Name");
                return View();
            }
                

            var result = await userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                TempData["save"] = "User role assigned successfully";
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            ViewBag.UserId =
                new SelectList(applicationUserRepo.GetAll().Where(u => u.LockoutEnd < DateTime.Now || u.LockoutEnd == null).ToList(), "Id", "UserName");
            ViewBag.RoleId =
                new SelectList(identityRoleRepo.GetAll(), "Id", "Name");
            return View();
        }

       
        public ActionResult AssignUserRole()
        {
            ViewBag.UserRoles = applicationUserRepo.GetUsersWithRoles();
            
            return View();
        }
    }
}



