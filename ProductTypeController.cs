using ECommerce.Configuration;
using ECommerce.Models;
using ECommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductTypeController : Controller
    {
        private readonly IProductTypeRepo repo;
        public ProductTypeController(IProductTypeRepo _repo)
        {
            repo = _repo;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(repo.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                repo.Insert(productType);
                TempData["save"] = "Product type has been saved successfully";
                return RedirectToAction("Index"); 
            }
            return View(productType);
        }


        public IActionResult Edit(int id)
        {
            var pType = repo.GetById(id);
            return View(pType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id , ProductType productType)
        {
            if (ModelState.IsValid)
            { 
                repo.Update(id, productType);
                TempData["save"] = "Product type has been Updated successfully";
                return RedirectToAction("Index");
            }
            return View(productType);
        }

        public IActionResult Details(int id)
        {
            var pType = repo.GetById(id);
            return View(pType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(ProductType productType)
        { 
                return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var pType = repo.GetById(id);
            return View(pType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(ProductType productType)
        {
            repo.Delete(productType);
            TempData["save"] = "Product type has been deleted";
            return RedirectToAction("Index");
           
        }
    }
}
