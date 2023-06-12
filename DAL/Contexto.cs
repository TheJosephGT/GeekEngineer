using Microsoft.EntityFrameworkCore;

public class Contexto : DbContext
{
    #nullable disable
    public DbSet<Clientes> cliente { get; set; }
    public DbSet<Categorias> categoria { get; set; }
    public DbSet<Inventarios> inventario { get; set; }
    public DbSet<Productos> producto { get; set; }
    public DbSet<Proveedores> proveedor { get; set; }
    public DbSet<Ventas> venta { get; set; }

    public Contexto(DbContextOptions <Contexto> options) : base(options){}

}