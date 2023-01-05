using Microsoft.AspNetCore.Mvc;
using ProniaSite.DAL;
using ProniaSite.Models;

namespace ProniaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        public IActionResult Index()
        {
            List<Color> colors = new List<Color>();
            using (AppDbContext context = new AppDbContext())
            {
                colors = context.Colors.ToList();
            }
            return View(colors);
        }
        public IActionResult Delete(int id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                Color color = context.Colors.Find(id);
                if (color is null) return NotFound();
                context.Colors.Remove(color);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return BadRequest();
            Color color = new Color();
            using (AppDbContext context = new AppDbContext())
            {
                color = context.Colors.Find(id);
                if (color is null) return NotFound();
            }
            return View(color);
        }
        [HttpPost]
        public IActionResult Update(int? id, Color color)
        {
            if (id == null) return BadRequest();
            Color color1 = new Color();
            if (color is null) return NotFound();
            using (AppDbContext context = new AppDbContext())
            {
                color1 = context.Colors.Find(id);
                color1.Name = color.Name;
                color1.Count = color.Count;
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Color color)
        {
            using (AppDbContext context = new AppDbContext())
            {
                context.Colors.Add(color);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
