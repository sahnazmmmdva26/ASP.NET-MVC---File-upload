using Microsoft.AspNetCore.Mvc;
using ProniaSite.DAL;
using ProniaSite.Models;

namespace ProniaSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShippingController : Controller
    {
        public IActionResult Index()
        {
            List<Shipping> shippings = new List<Shipping>();
            using (AppDbContext context = new AppDbContext())
            {
                shippings = context.Shippings.ToList();
            }
            return View(shippings);
        }
        public IActionResult Delete(int id)
        {
            using (AppDbContext context = new AppDbContext())
            {
                Shipping shipping = context.Shippings.Find(id);
                if (shipping is null) return NotFound();
                context.Shippings.Remove(shipping);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id == null) return BadRequest();
            Shipping shipping = new Shipping();
            using (AppDbContext context = new AppDbContext())
            {
                shipping = context.Shippings.Find(id);
                if (shipping is null) return NotFound();
            }
            return View(shipping);
        }
        [HttpPost]
        public IActionResult Update(int? id, Shipping shipping)
        {
            if (id == null) return BadRequest();
            Shipping existShipping = new Shipping();
            if (shipping is null) return NotFound();
            using (AppDbContext context = new AppDbContext())
            {
                existShipping = context.Shippings.Find(id);
                existShipping.Name = shipping.Name;
                existShipping.Image = shipping.Image;
                existShipping.Description=shipping.Description;
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Shipping shipping)
        {
            using (AppDbContext context = new AppDbContext())
            {
                context.Shippings.Add(shipping);
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
