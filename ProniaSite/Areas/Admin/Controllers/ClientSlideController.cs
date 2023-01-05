using Microsoft.AspNetCore.Mvc;
using ProniaSite.DAL;
using ProniaSite.Models;

namespace ProniaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClientSlideController : Controller
    {
        public IActionResult Index()
        {
            List<ClientSlide> clientSlides = new List<ClientSlide>();
            using (AppDbContext context = new AppDbContext())
            {
                clientSlides = context.ClientSlides.ToList();
            }
            return View(clientSlides);
        }
        public IActionResult Delete(int id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                ClientSlide clientSlide = context.ClientSlides.Find(id);
                if (clientSlide is null) return NotFound();
                context.ClientSlides.Remove(clientSlide);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return BadRequest();
            ClientSlide clientSlide = new ClientSlide();
            using (AppDbContext context = new AppDbContext())
            {
                clientSlide = context.ClientSlides.Find(id);
                if (clientSlide is null) return NotFound();
            }
            return View(clientSlide);
        }
        [HttpPost]
        public IActionResult Update(int? id, ClientSlide clientSlide)
        {
            if (id == null) return BadRequest();
            ClientSlide clientSlide1 = new ClientSlide();
            if (clientSlide is null) return NotFound();
            using (AppDbContext context = new AppDbContext())
            {
                clientSlide1 = context.ClientSlides.Find(id);
                clientSlide1.Fullname = clientSlide.Fullname;
                clientSlide1.Image = clientSlide.Image;
                clientSlide1.Comment = clientSlide1.Comment;
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ClientSlide clientSlide)
        {
            using (AppDbContext context = new AppDbContext())
            {
                context.ClientSlides.Add(clientSlide);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
