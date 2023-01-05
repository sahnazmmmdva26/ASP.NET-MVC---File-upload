using Microsoft.EntityFrameworkCore;
using ProniaSite.Models;

namespace ProniaSite.DAL
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-59QHG10;Database=Pronia;Trusted_Connection=True;Integrated Security=true;Encrypt=false;");
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<ClientSlide> ClientSlides { get; set; }
        public DbSet<IndexMainSlide> IndexMainSlides { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Banner> Banners { get; set; }
    }
}
