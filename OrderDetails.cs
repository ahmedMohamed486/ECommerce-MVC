using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        [ForeignKey("Order")]
        [Display(Name ="Order")]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        [Display(Name ="Product")]
        public int ProductId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
