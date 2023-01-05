using Microsoft.AspNetCore.Mvc;
using ProniaSite.DAL;
using ProniaSite.Models;

namespace ProniaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            List<Category> categories = new List<Category>();
            using (AppDbContext context= new AppDbContext())
            {
                categories=context.Categories.ToList();
            }
            return View(categories);
        }
        public IActionResult Delete(int id)
        {
            using (AppDbContext context= new AppDbContext())
            {
                Category category = context.Categories.Find(id);
                if (category is null) return NotFound();
                context.Categories.Remove(category);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if(id== null) return BadRequest();
            Category category = new Category();
            using (AppDbContext context = new AppDbContext())
            {
               category = context.Categories.Find(id);
                if (category is null) return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(int? id, Category category)
        {
            if(id==null) return BadRequest();
            Category existCategory = new Category();
            if(category is null) return NotFound();
            using (AppDbContext context = new AppDbContext())
            {
                existCategory= context.Categories.Find(id);
                existCategory.Name=category.Name;
                existCategory.Count = category.Count;
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            using(AppDbContext context= new AppDbContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
