using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("categories")]
    public class CategoriesController : Controller
    {
        private readonly ShopContext _db;

        public CategoriesController(ShopContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var categories = await _db.Categories.ToListAsync();

            return View(categories);
        }

        [HttpGet]
        [Route("add")]
        [Route("edit/{id:int}")]
        public async Task<IActionResult> Category(int? id)
        {
            if (!id.HasValue)
                return View();

            var category = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            return View(category);
        }

        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(Category category)
        {
            if (category.Id == default(int))
            {
                _db.Categories.Add(category);
            }
            else
            {
                _db.Categories.Attach(category);
                _db.Entry(category).State = EntityState.Modified;
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
