﻿using System.ComponentModel;

namespace ECommerce.View_Model
{
    public class RoleUserViewModel
    {
        [DisplayName("User")]
        public string UserId { get; set; }

        [DisplayName("Role")]
        public string RoleId { get; set; }
    }
}
