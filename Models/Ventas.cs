using System.ComponentModel.DataAnnotations.Schema;
public class Ventas
{
    [Key]
    public int VentaId { get; set; }
    [Required(ErrorMessage = "El campo cliente es necesario")]
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "El campo producto es necesario")]
    public int ProductoId { get; set; }
    [DataType(DataType.Date)] 
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public double ITBIS { get; set; }
    public double SubTotal { get; set; }
    public double Total { get; set; }
    public bool EsVisible { get; set; } = false;
    

    [ForeignKey("VentaId")]
    public virtual List<VentasDetalle> ventasDetalle { get; set; } = new List<VentasDetalle>();


}