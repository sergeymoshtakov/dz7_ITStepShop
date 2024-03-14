using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ITStepShop.Models.ViewModel
{
    public class ManageRoleViewModel
    {
        public string UserId { get; set; }
        public List<RoleSelectionViewModel> Roles { get; set; }
        public List<ShopUser> Users { get; set; }
    }

    public class RoleSelectionViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
