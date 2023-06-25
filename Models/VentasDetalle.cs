public class VentasDetalle{

    [Key]
    public int DetalleId { get; set; }
    public int VentaId { get; set; }
    public int ProductoId { get; set; } 
    public double PrecioProducto { get; set; }
    public double Importe { get; set; }
    public int Cantidad { get; set; }
    public double SubTotal { get; set; } = 0;
    public double ITBIS { get; set; }
}