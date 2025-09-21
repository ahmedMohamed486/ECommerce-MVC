using ECommerce.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ECommerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        [MaxLength(200)]
        [UniqueProductName]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Image {  get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [MaxLength(50)]
        [Display(Name ="Product Color")]
        public string? ProductColor { get; set; }

        [Display(Name ="Available")]
        public bool IsAvailable { get; set; }

        [ForeignKey("ProductType")]
        [Display(Name ="Product Type")]
        public int PTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }
    }
}
