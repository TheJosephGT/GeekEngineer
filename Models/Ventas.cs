using System.ComponentModel.DataAnnotations.Schema;
public class Ventas
{
    [Key]
    public int VentaId { get; set; }
    [Required(ErrorMessage = "El campo cliente es necesario")]
    public int ClienteId { get; set; }
    [DataType(DataType.Date)] 
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public double Total { get; set; }
    [Required(ErrorMessage = "Favor especifique si su venta es a crédito.")]
    public string? EsCredito { get; set; }
    public int Cuotas { get; set; }
    public double ValorCuotas { get; set; }
    public string? TipoCredito { get; set; }
    public string? CuotaInicial { get; set; }
    public bool Status { get; set; } = true;

    [ForeignKey("VentaId")]
    public List<VentasDetalle> ventasDetalle { get; set; } = new List<VentasDetalle>();


}