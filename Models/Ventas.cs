using System.ComponentModel.DataAnnotations.Schema;
public class Ventas
{
    [Key]
    public int VentaId { get; set; }
    public int ClienteId { get; set; }
    public int ProductoId { get; set; }
    public string ListaProducto { get; set; } = string.Empty; //Falta poner el tipo de dato que sea una lista.
    [DataType(DataType.Date)] 
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public double SubTotal { get; set; }
    public double Total { get; set; }
    

    [ForeignKey("VentaId")]
    public virtual List<VentasDetalle> ventasDetalle { get; set; } = new List<VentasDetalle>();


}