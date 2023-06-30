public class FacturacionDetalle{

    [Key]
    public int DetalleId { get; set; }
    public int FacturaId { get; set; }
    public string? Concepto { get; set; } 
    public double Precio { get; set; }
    public double Importe { get; set; }
    public int Cantidad { get; set; }
}