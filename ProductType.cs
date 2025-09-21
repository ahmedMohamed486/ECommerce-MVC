using ECommerce.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        [Display(Name ="Product Type")]
        [MaxLength(255)]
        [UniqueProductTypeName]
        public string PType { get; set; }

        public virtual List<Product>? Products { get; set; } = new List<Product>();
    }
}
