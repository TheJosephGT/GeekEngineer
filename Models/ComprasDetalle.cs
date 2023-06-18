public class ComprasDetalle
{
    [Key]
    public int DetalleId { get; set; }
    public int CompraId { get; set; }
    public int ProductoId { get; set; } 
    public double Cantidad { get; set; }
    public float PrecioProducto { get; set; }
    public float Importe { get; set; }
    public bool Status { get; set; } = true;
}