
public class Productos
{
    [Key]
    public int ProductoId { get; set; }
    [Required(ErrorMessage = "Ingrese el nombre del producto.")]
    public string Nombre { get; set; } = string.Empty;
    [DataType(DataType.Date)]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [Required]
    [Range(minimum: 1, maximum: double.MaxValue, ErrorMessage = "Ingrese un costo mayor a 0.")]
    public double Costo { get; set; }
    [Required]
    [Range(minimum: 1, maximum: double.MaxValue, ErrorMessage = "Ingrese un precio mayor a 0.")]
    public double Precio { get; set; }
    [Required]
    [Range(minimum: 1, maximum: double.MaxValue, ErrorMessage = "Ingrese un % de ITBIS válido.")]
    public double ITBIS { get; set; }
    [Required(ErrorMessage = "Seleccione la categoría")]
    public int CategoriaId { get; set; }
    [Required(ErrorMessage = "Seleccione si esta empacado.")]
    public bool EstaEmpacado { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Ingrese la existencia del producto.")]
    public int Existencia { get; set; }
    [Required(ErrorMessage = "Seleccione el proveedor del producto.")]
    public int ProveedorId { get; set; }
    [Required(ErrorMessage = "Ingrese la ubicación del producto.")]
    public string Ubicacion { get; set; } = string.Empty;
    public double Descuento { get; set; }
    [Required(ErrorMessage = "Ingrese el código de barra.")]
    public string CodigoBarra { get; set; } = string.Empty; 
    public bool EsVisible { get; set; } = true;
    
}