public class VentasDetalle{

    [Key]
    
    public int DetalleId { get; set; }
    public int VentaId { get; set; }
    public int ProductoId { get; set; } 
    public double Cantidad { get; set; }
    public float Importe { get; set; }
}