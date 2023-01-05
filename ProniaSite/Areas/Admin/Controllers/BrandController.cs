using Microsoft.AspNetCore.Mvc;
using ProniaSite.DAL;
using ProniaSite.Models;
using ProniaSite.ViewModels;

namespace ProniaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            List<Brand> brands = new List<Brand>();
            using (AppDbContext context = new AppDbContext())
            {
                brands = context.Brands.ToList();
            }
            return View(brands);
        }
        public IActionResult Delete(int id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                Brand brand = context.Brands.Find(id);
                if (brand is null) return NotFound();
                context.Brands.Remove(brand);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return BadRequest();
            Brand brand = new Brand();
            using (AppDbContext context = new AppDbContext())
            {
                brand = context.Brands.Find(id);
                if (brand is null) return NotFound();
            }
            return View(brand);
        }
        [HttpPost]
        public IActionResult Update(int? id, Brand brand)
        {
            if (id == null) return BadRequest();
            Brand existBrand = new Brand();
            if (brand is null) return NotFound();
            using (AppDbContext context = new AppDbContext())
            {
                existBrand = context.Brands.Find(id);
                existBrand.Image = brand.Image;
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBrandVM brandVM)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            IFormFile file = brandVM.image;
            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Yuklediyiniz fayl shekil deyil");
                return View();
            }
            string filename = Guid.NewGuid() + file.FileName;
            using (FileStream stream = new FileStream("C:\\Users\\HP\\Desktop\\c# files\\ProniaSite\\wwwroot\\assets\\images\\" + filename, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            string fileName = Guid.NewGuid() + (file.FileName.Length > 64 ? file.FileName.Substring(0, 64) : file.FileName);

            using (var stream = new FileStream("C:\\Users\\HP\\Desktop\\c# files\\ProniaSite\\wwwroot\\assets\\images\\" + fileName, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Brand brand = new Brand() { Image = fileName };
            using (AppDbContext context = new AppDbContext())
            {
                context.Brands.Add(brand);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
