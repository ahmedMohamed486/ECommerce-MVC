using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.View_Model
{
    public class RegisterViewModel
    {
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
