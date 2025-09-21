using ECommerce.Models;
using ECommerce.Repository;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Attributes
{
    public class UniqueProductNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            
            var context = validationContext.GetService<IProductRepo>();

            if(value==null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            Product? pDb = context.GetAll().FirstOrDefault(p => p.Name == value.ToString());
            var pRequest = (Product)validationContext.ObjectInstance;

            if(pRequest.Id == 0 && pDb != null)
            {
                return new ValidationResult("this name already exist");
            }

            return ValidationResult.Success;
        }
    }
}
