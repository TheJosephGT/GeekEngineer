using System.ComponentModel.DataAnnotations.Schema;

public class Facturacion
{
    [Key]
    public int FacturaId { get; set; }
    [Required(ErrorMessage = "El campo cliente es necesario")]
    public int ClienteId { get; set; }
    [DataType(DataType.Date)] 
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public double Total { get; set; }
    public bool Status { get; set; } = true;

    [ForeignKey("FacturaId")]
    public List<FacturacionDetalle> facturaDetalle { get; set; } = new List<FacturacionDetalle>();
}