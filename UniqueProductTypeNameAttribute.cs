using ECommerce.Models;
using ECommerce.Repository;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Attributes
{
    public class UniqueProductTypeNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var context = validationContext.GetService<IProductTypeRepo>();

            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            ProductType? pDb = context.GetAll().FirstOrDefault(p => p.PType == value.ToString());
            var pRequest = (ProductType)validationContext.ObjectInstance;

            if (pRequest.Id == 0 && pDb != null)
                return new ValidationResult("this name already exist");

            return ValidationResult.Success;
        }
    }
}
