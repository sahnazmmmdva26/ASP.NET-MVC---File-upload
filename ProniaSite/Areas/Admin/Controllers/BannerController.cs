using Microsoft.AspNetCore.Mvc;
using ProniaSite.DAL;
using ProniaSite.Models;
using ProniaSite.ViewModels;

namespace ProniaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        public IActionResult Index()
        {
            List<Banner> banners = new List<Banner>();
            using (AppDbContext context = new AppDbContext())
            {
                banners = context.Banners.ToList();
            }
            return View(banners);
        }
        public IActionResult Delete(int id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                Banner banner = context.Banners.Find(id);
                if (banner is null) return NotFound();
                context.Banners.Remove(banner);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return BadRequest();
            Banner banner = new Banner();
            using (AppDbContext context = new AppDbContext())
            {
                banner = context.Banners.Find(id);
                if (banner is null) return NotFound();
            }
            return View(banner);
        }
        [HttpPost]
        public IActionResult Update(int? id, Banner banner)
        {
            if (id == null) return BadRequest();
            Banner exist = new Banner();
            if (banner is null) return NotFound();
            using (AppDbContext context = new AppDbContext())
            {
                exist = context.Banners.Find(id);
                exist.Title = banner.Title;
                exist.Description = banner.Description;
                exist.Image=banner.Image;
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        public IActionResult Create(CreateBannerVM banner)
        {
            if (ModelState.IsValid==false)
            {
                return View();
            }
            IFormFile file = banner.Image;
            if (!file.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Image", "Yuklediyiniz fayl shekil deyil");
                return View();
            }
            string filename=Guid.NewGuid()+file.FileName;
            using (FileStream stream = new FileStream("CC:\\Users\\HP\\Desktop\\c# files\\ProniaSite\\wwwroot\\assets\\images\\" + filename,FileMode.Create))
            {
                file.CopyTo(stream);
            }
            Banner banner1 = new Banner() { Description = banner.Description,ImageUrl=filename,Title=banner.Title};
            using (AppDbContext context = new AppDbContext())
            {
                context.Banners.Add(banner1);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
