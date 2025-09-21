using ECommerce.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.View_Model
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Order Number")]
        public string? OrderNo { get; set; }
        public string Name { get; set; }

        [Display(Name = "Phone")]
        public string PhoneNo { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<string>? OrderDetailsNames { get; set; } = new List<string>();
        //public virtual List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
