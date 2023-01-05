using Microsoft.AspNetCore.Mvc;
using ProniaSite.DAL;
using ProniaSite.Models;

namespace ProniaSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            List<IndexMainSlide> IndexMainSlides = new List<IndexMainSlide>();
            using (AppDbContext context = new AppDbContext())
            {
                IndexMainSlides = context.IndexMainSlides.ToList();
            }
            List<Brand> Brands = new List<Brand>();
            using (AppDbContext context = new AppDbContext())
            {
                Brands = context.Brands.ToList();
            }
            List<ClientSlide> ClientSlides = new List<ClientSlide>();
            using (AppDbContext context = new AppDbContext())
            {
                ClientSlides=context.ClientSlides.ToList();
            }
            List<Shipping> Shippings = new List<Shipping>();
            using (AppDbContext context = new AppDbContext())
            {
                Shippings=context.Shippings.ToList();   
            }
            List<Banner> banners = new List<Banner>();
            using (AppDbContext context= new AppDbContext())
            {
                banners = context.Banners.ToList();
            }
            ViewBag.Banners=banners;
            ViewBag.Shippings=Shippings;
            ViewBag.ClientSlides = ClientSlides;
            ViewBag.Brands = Brands;
            return View(IndexMainSlides);
        }
        public IActionResult SingleProduct()
        {
            return View();
        }
        public IActionResult Shop()
        {
            List<Category> Categories = new List<Category>();
            using (AppDbContext context = new AppDbContext())
            {
                Categories = context.Categories.ToList();
            }
            List<Color> Colors = new List<Color>();
            using (AppDbContext context = new AppDbContext())
            {
                Colors = context.Colors.ToList();
            }
            ViewBag.Colors=Colors;
            return View(Categories);
        }
        public IActionResult Card()
        {
            return View();
        }
        public IActionResult LoginRegister()
        {
            return View();
        }
         
    }
}
