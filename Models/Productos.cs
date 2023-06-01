
public class Productos
{
    [Key]
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public double PrecioCompra { get; set; }
    public double ITBIS { get; set; }
    public int CategoriaId { get; set; }
    public string EstaEmpacado { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public int ProveedorId { get; set; }
}