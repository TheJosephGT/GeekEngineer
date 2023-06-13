using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekEngineer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        #nullable disable
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Inventarios> Inventarios { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}