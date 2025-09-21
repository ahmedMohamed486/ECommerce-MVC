using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.View_Model
{
    public class LoginViewModel
    {
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
    }
}
