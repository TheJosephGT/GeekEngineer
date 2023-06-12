using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekEngineer.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        #nullable disable
        public DbSet<Clientes> cliente { get; set; }
        public DbSet<Categorias> categoria { get; set; }
        public DbSet<Inventarios> inventario { get; set; }
        public DbSet<Productos> producto { get; set; }
        public DbSet<Proveedores> proveedor { get; set; }
        public DbSet<Ventas> venta { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}