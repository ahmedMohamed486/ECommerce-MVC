using ECommerce.Models;
using ECommerce.Repository;
using ECommerce.View_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepo orderRepo;
        private readonly IOrderDetailsRepo orderDetailsRepo;
        public OrderController(IOrderRepo _orderRepo, IOrderDetailsRepo _orderDetailsRepo)
        {
            orderDetailsRepo = _orderDetailsRepo;
            orderRepo = _orderRepo;
        }


        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Checkout() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(Order anOrder)
        {
            List<ProductViewModel> productsVM =
                HttpContext.Session.Get<List<ProductViewModel>>("products");

            if (productsVM != null)
            {
                foreach (var p in productsVM)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = p.Id;

                    anOrder.OrderDetails.Add(orderDetails);
                }
            }
            anOrder.OrderNo = GetOrderNo();
            orderRepo.Insert(anOrder);

            HttpContext.Session.Set("products", new List<ProductViewModel>()); 
            return View();
        }

        public string GetOrderNo()
        {
            int rowCount = orderRepo.GetAll().Count()+1;
            return rowCount.ToString("000");
        }
    }
}
