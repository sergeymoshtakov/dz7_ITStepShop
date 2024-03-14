using Microsoft.AspNetCore.Identity;

namespace ITStepShop.Models
{
    public class ShopUser : IdentityUser
    {
        public string FullName { get; set; }
        public override string UserName { get; set; }
        public string AddressDelivery { get; set; }
    }
}
