public class ComprasDetalle
{
    [Key]
    public int DetalleId { get; set; }
    public int CompraId { get; set; }
    public int ProductoId { get; set; } 
    public int Cantidad { get; set; }
    public double PrecioProducto { get; set; }
    public double Importe { get; set; }
    public double SubTotal { get; set; } = 0;
    public string? Llegada { get; set; }
    public bool Status { get; set; } = true;
}