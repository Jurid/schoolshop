using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("catalog")]
    public class CatalogController : Controller
    {
        private readonly ShopContext _db;

        public CatalogController(ShopContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var products = await _db.Products.ToListAsync();

            return View(products);
        }

        [HttpGet]
        [Route("AddInBasket/{id:int}")]
        public async Task<IActionResult> AddInBasket(int? id)
        {
            if (!id.HasValue)
                return View();

            var BasketItem = await _db.BasketItems.FirstOrDefaultAsync(x => x.ProductId == id);

            if (BasketItem is null)
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
                var NewBasketItem = new BasketItem(product, 1);
                _db.BasketItems.Add(NewBasketItem);
            }
            else { BasketItem.Count = BasketItem.Count + 1; }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(Product product)
        {
            if (product.Id == default(int))
            {
                _db.Products.Add(product);
            }
            else
            {
                _db.Products.Attach(product);
                _db.Entry(product).State = EntityState.Modified;
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("del_from_basket/{id:int}")]
        public async Task<IActionResult> Del(int? id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            _db.Products.Remove(product);
            //_db.Entry(product).State = EntityState.Modified;

            await _db.SaveChangesAsync();

            //return View();
            return RedirectToAction("Index");
        }
    }
}
