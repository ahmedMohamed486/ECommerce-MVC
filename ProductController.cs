using ECommerce.Models;
using ECommerce.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepo repo;
        private readonly IWebHostEnvironment he;
        private readonly IProductTypeRepo productTypeRepo;
        public ProductController(IProductRepo _repo, IWebHostEnvironment he, IProductTypeRepo productTypeRepo)
        {
            repo = _repo;
            this.he = he;
            this.productTypeRepo = productTypeRepo;
        }


        public IActionResult Index()
        {
            return View(repo.GetAll());
        }

        [HttpPost]
        public IActionResult Index(decimal? lowAmount,decimal? largeAmount)
        {
            var products = repo.GetAll()
                .Where(p => p.Price >= lowAmount && p.Price <= largeAmount).ToList();
            if (lowAmount == null || largeAmount == null)
            {
                products = repo.GetAll();
            }
            return View(products);
        }

        public IActionResult Create()
        {
            SelectList list = new SelectList(productTypeRepo.GetAll(), "Id", "PType");
            ViewBag.productTypes = list;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product/*,IFormFile? Image*/) 
        {
            if (ModelState.IsValid)
            {
                #region MyRegion
                //if (Image != null)
                //{
                //    var name = Path.Combine(he.WebRootPath + "/Images", Path.GetFileName(Image.FileName));
                //    Image.CopyTo(new FileStream(name, FileMode.Create));
                //    product.Image = "Images/" + Image.FileName;
                //}
                //if(Image == null)
                //{
                //    product.Image = "Images/notFound.png";
                //} 
                #endregion
                if (product.ImageFile != null)
                {
                    string uploadPath = Path.Combine(he.WebRootPath, "Images");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);
                    
                    string fileName = Path.GetFileName(product.ImageFile.FileName);
                    string filePath = Path.Combine(uploadPath, fileName);
                    
                    using var stream = new FileStream(filePath, FileMode.Create);
                    product.ImageFile.CopyTo(stream);
                    
                    product.Image = "/Images/" + fileName;
                }
                else
                {
                    product.Image = "/Images/notFound.png";
                }

                repo.Insert(product);
                TempData["save"] = "Product Added successfully";
                return RedirectToAction("Index");
            }
            SelectList list = new SelectList(productTypeRepo.GetAll(), "Id", "PType");
            ViewBag.productTypes = list;
            return View(product);
        }


        public IActionResult Edit(int id)
        {
            SelectList list = new SelectList(productTypeRepo.GetAll(), "Id", "PType");
            ViewBag.productTypes = list;
            return View(repo.GetById(id));
        }
        [HttpPost]
        public IActionResult Edit(int id , Product product/*, IFormFile? Image*/)
        {
            if(ModelState.IsValid)
            {
                #region MyRegion
                //if(Image != null)
                //{
                //    var name = Path.Combine(he.WebRootPath + "/Images", Path.GetFileName(Image.FileName));
                //    Image.CopyTo(new FileStream(name, FileMode.Create));
                //    product.Image = "Images/" + Image.FileName;
                //}
                //if (Image == null)
                //{
                //    product.Image = "Images/notFound.png";
                //}  
                #endregion
                if (product.ImageFile != null)
                {
                    string uploadPath = Path.Combine(he.WebRootPath, "Images");
                    if (!Directory.Exists(uploadPath))
                        Directory.CreateDirectory(uploadPath);

                    string fileName = Path.GetFileName(product.ImageFile.FileName);
                    string filePath = Path.Combine(uploadPath, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    product.ImageFile.CopyTo(stream);

                    product.Image = "/Images/" + fileName;
                }
                else
                {
                    product.Image = "/Images/notFound.png";
                }

                repo.Update(id , product);
                TempData["save"] = "product updated successfully";
                return RedirectToAction("Index");
            }
            SelectList list = new SelectList(productTypeRepo.GetAll(), "Id", "PType");
            ViewBag.productTypes = list;
            return View(product);
        }

        public IActionResult Details(int id) 
        {
            var pTypes = productTypeRepo.GetAll();
            SelectList list = new SelectList(pTypes, "Id", "PType");
            return View(repo.GetById(id));
        }

        public IActionResult Delete(int id) 
        {
            var pTypes = productTypeRepo.GetAll();
            SelectList list = new SelectList(pTypes, "Id", "PType");
            return View(repo.GetById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            repo.Delete(product);
                TempData["save"] = "product deleted successfully";
                return RedirectToAction("Index");
        
        }
    }
}
