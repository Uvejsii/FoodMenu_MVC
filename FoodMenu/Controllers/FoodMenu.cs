using Microsoft.AspNetCore.Mvc;
using FoodMenu.Data;
using FoodMenu.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodMenu.Controllers
{
    public class FoodMenu : Controller
    {
        private readonly FoodMenuContext _context;
        public FoodMenu(FoodMenuContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Dishes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var dish = await _context.Dishes
                .Include(di => di.DishIngredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }
    }
}
