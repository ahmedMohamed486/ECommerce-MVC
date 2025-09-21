using ECommerce.Attributes;
using ECommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.View_Model
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [MaxLength(200)]
        [UniqueProductName]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }

        [MaxLength(50)]
        [Display(Name = "Product Color")]
        public string? ProductColor { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Display(Name ="Product Type")]
        public string? ProductTypeName { get; set; }

    }
}
