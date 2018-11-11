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

            foreach (var item in basket)
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);

                item.Product = product;
            }

            return View(basket);
        }
        
        [HttpGet]
        [Route("delFromBasket/{id:int}")]
        public async Task<IActionResult> Del(int? id)
        {
            var BasketItem = await _db.BasketItems.FirstOrDefaultAsync(x => x.Id == id);
            _db.BasketItems.Remove(BasketItem);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("PlaceOrder")]
        public async Task<IActionResult> PlaceOrder()
        {
            var order = new Order();

            _db.Orders.Add(order);

            await _db.SaveChangesAsync();//запишем в базу для формирования Id заказа

            var items = await _db.BasketItems.ToListAsync();

            foreach (var item in items)
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == item.ProductId);

                var OrderItem = new OrderItem(order, product, item.Count);

                _db.OrderItems.Add(OrderItem);
            }

            _db.BasketItems.RemoveRange(_db.BasketItems);
            //foreach (var item in items)
            //{ _db.BasketItems.Remove(item); }
            await _db.SaveChangesAsync();

            return RedirectToAction("Details", "Orders", new {id=order.Id});
        }

    }
}
