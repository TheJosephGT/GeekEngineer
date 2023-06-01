public class Ventas
{
    [Key]
    public int VentaId { get; set; }
    public int ClienteId { get; set; }
    public int ProductoId { get; set; }
    public string ListaProducto { get; set; } = string.Empty; //Falta poner el tipo de dato que sea una lista.
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public double Precio { get; set; }
    
    //Esto se puede hacer perfectamente en un metodo.
    public double Importe { get; set; }
    public double SubTotal { get; set; }
    public double Total { get; set; }


}