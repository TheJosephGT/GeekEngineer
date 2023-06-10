public class Inventario
{
    [Key]
    public int InventarioId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public int ProveedorId { get; set; }

}