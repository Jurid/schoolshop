using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using System.Linq;

namespace Shop.Controllers
{
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly ShopContext _db;

        public BasketController(ShopContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var basket = await _db.BasketItems.ToListAsync();

            return View(basket);
        }
        
        [HttpGet]
        [Route("del_from_basket/{id:int}")]
        public async Task<IActionResult> Del(int? id)
        {
            var basket_item = await _db.BasketItems.FirstOrDefaultAsync(x => x.Id == id);
            _db.BasketItems.Remove(basket_item);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("place_order")]
        public async Task<IActionResult> PlaceOrder()
        {
            var order = new Order();

            _db.Orders.Add(order);

            await _db.SaveChangesAsync();

            var items = await _db.BasketItems.ToListAsync();

            foreach (var item in items)
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);

                var order_item = new OrderItem(product, item.Count);

                order_item.OrderId = order.Id;
                order_item.ProductId = product.Id;

                _db.OrderItems.Add(order_item);
                
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("orders/Index");
        }

    }
}
