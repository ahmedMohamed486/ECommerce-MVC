using System.Diagnostics;
using ECommerce.Models;
using ECommerce.Repository;
using ECommerce.View_Model;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Extensions;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepo productRepo;
        private readonly IProductTypeRepo productTypeRepo;
        public HomeController(ILogger<HomeController> logger, 
            IProductRepo _productRepo, IProductTypeRepo _productTypeRepo)
        {
            _logger = logger;
            productRepo = _productRepo;
            productTypeRepo = _productTypeRepo;
        }

        public IActionResult Index(int? page)
        {
            return View(productRepo.GetAll().ToPagedList(pageNumber:page??1,pageSize:6));
        }

        #region MyRegion
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        } 
        #endregion

        public IActionResult Details(int id)
        {
            return View(productRepo.GetById(id));
        }

        [HttpPost]
        [ActionName("Details")]
        [ValidateAntiForgeryToken]
        public IActionResult ProductDetails(int id)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();

            Product product = productRepo.GetById(id);


            products = HttpContext.Session.Get<List<ProductViewModel>>("products");
            if (products == null)
            {
                products = new List<ProductViewModel>();
            }

            products.Add(new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Image = product.Image,
                IsAvailable = product.IsAvailable,
                ProductTypeName = product.ProductType.PType
            });
            HttpContext.Session.Set("products", products);
            return View(product);
            
        }

        [ActionName("Remove")]
        public IActionResult RemoveToCart(int id)
        {
            List<ProductViewModel> productsVM =
                HttpContext.Session.Get<List<ProductViewModel>>("products");

            if (productsVM != null)
            {
                var product = productsVM.FirstOrDefault(p => p.Id == id);
                if (product != null)
                {
                    productsVM.Remove(product);
                    HttpContext.Session.Set("products", productsVM);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int id)
        {
            List<ProductViewModel> productsVM =
                HttpContext.Session.Get<List<ProductViewModel>>("products");

            if (productsVM != null)
            {
                var product = productsVM.FirstOrDefault(p => p.Id == id);
                if(product != null)
                {
                    productsVM.Remove(product);
                    HttpContext.Session.Set("products", productsVM);
                }
            }

            return RedirectToAction("Index");
        }


        public IActionResult Cart()
        {
            List<ProductViewModel> productsVM = 
                HttpContext.Session.Get<List<ProductViewModel>>("products");

            if(productsVM == null)
                productsVM = new List<ProductViewModel>();

            return View(productsVM);
        }
    }
}
