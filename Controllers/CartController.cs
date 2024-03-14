using ITStepShop.Data;
using ITStepShop.Models.ViewModel;
using ITStepShop.Models;
using Microsoft.AspNetCore.Mvc;
using ITStepShop.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ITStepShop.Controllers
{
    [Authorize(Roles = $"{WC.AdminRole}")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        [Authorize(Roles = $"{WC.AdminRole}, {WC.ManagerRole}")]
        public IActionResult Index()
        {
            List<ShopingCart> shopingCartList = new List<ShopingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart).Count() > 0)
            {
                shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart);
            }

            List<int> prodInCart = shopingCartList.Select(x => x.ProductId).ToList();

            IEnumerable<Product> prodList = _db.Product.Where(x => prodInCart.Contains(x.Id));

            return View(prodList);
        }
        [Authorize(Roles = $"{WC.AdminRole}, {WC.ManagerRole}")]
        public IActionResult Remove(int id)
        {
            List<ShopingCart> shopingCartList = new List<ShopingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart).Count() > 0)
            {
                shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart);
            }
            shopingCartList.Remove(shopingCartList.FirstOrDefault(x => x.ProductId==id));
            HttpContext.Session.Set(WC.SessionCart, shopingCartList);
            //return RedirectToAction("Index");
            return RedirectToAction(nameof(Index));
        }
    }
}
