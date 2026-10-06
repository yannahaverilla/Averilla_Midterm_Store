using Microsoft.AspNetCore.Mvc;
using Averilla_Midterm_Store.Data;
using Averilla_Midterm_Store.Models;

namespace Averilla_Midterm_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var items = _db.CartItems.ToList();
            return View(items);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _db.Products.Find(id);

            if (product != null)
            {
                var item = _db.CartItems.FirstOrDefault(x => x.ProductId == id);

                if (item == null)
                {
                    _db.CartItems.Add(new CartItem
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = 1
                    });
                }
                else
                {
                    item.Quantity += 1;
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index", "Products");
        }

        [HttpPost]
        public IActionResult Update(int id, int quantity)
        {
            var item = _db.CartItems.Find(id);

            if (item != null)
            {
                item.Quantity = quantity;
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var item = _db.CartItems.Find(id);

            if (item != null)
            {
                _db.CartItems.Remove(item);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}