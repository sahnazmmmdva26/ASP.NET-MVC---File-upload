using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaSite.DAL;
using ProniaSite.Models;
using ProniaSite.ViewModels;
using System.Reflection;

namespace ProniaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IndexMainSlideController : Controller
    {
        public IActionResult Index()
        {
            List<IndexMainSlide> indexMainSlides = new List<IndexMainSlide>();
            using (AppDbContext context = new AppDbContext())
            {
                indexMainSlides = context.IndexMainSlides.ToList();
            }
            return View(indexMainSlides);
        }
        public IActionResult Delete(int id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                IndexMainSlide indexMainSlide = context.IndexMainSlides.Find(id);
                if (indexMainSlide is null) return NotFound();
                context.IndexMainSlides.Remove(indexMainSlide);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return BadRequest();
            IndexMainSlide indexMainSlide = new IndexMainSlide();
            using (AppDbContext context = new AppDbContext())
            {
                indexMainSlide = context.IndexMainSlides.Find(id);
                if (indexMainSlide is null) return NotFound();
            }
            return View(indexMainSlide);
        }
        [HttpPost]
        public IActionResult Update(int? id, IndexMainSlide indexMainSlide)
        {
            if (id == null) return BadRequest();
            IndexMainSlide indexMainSlide1 = new IndexMainSlide();
            if (indexMainSlide is null) return NotFound();
            using (AppDbContext context = new AppDbContext())
            {
                indexMainSlide1 = context.IndexMainSlides.Find(id);
                indexMainSlide1.Name = indexMainSlide.Name;
                indexMainSlide1.Image = indexMainSlide.Image;
                indexMainSlide1.Description=indexMainSlide.Description;
                indexMainSlide1.Discount=indexMainSlide.Discount;
                
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateIndexMainSlideVM ındexMainSlideVM)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            IFormFile file = ındexMainSlideVM.image;
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
            IndexMainSlide indexMainSlide = new IndexMainSlide { Description = ındexMainSlideVM.Description, Name = ındexMainSlideVM.Name, Image = fileName,Discount= ındexMainSlideVM.Discount};
            using (AppDbContext context = new AppDbContext())
            {
                context.IndexMainSlides.Add(indexMainSlide);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
           

        }


 
    }
}
