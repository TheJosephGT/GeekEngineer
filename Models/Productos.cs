
public class Productos
{
    [Key]
    public int ProductoId { get; set; }
    [Required(ErrorMessage = "Ingrese el nombre del producto.")]
    public string Nombre { get; set; } = string.Empty;
    [DataType(DataType.Date)]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [Required]
    [Range(minimum: 1, maximum: float.MaxValue, ErrorMessage = "Ingrese un costo mayor a 0.")]
    public double Costo { get; set; }
    [Required]
    [Range(minimum: 1, maximum: float.MaxValue, ErrorMessage = "Ingrese un precio mayor a 0.")]
    public double Precio { get; set; }
    [Required(ErrorMessage = "Campo ITBIS es obligatorio.")]
    [Range(minimum: 1, maximum: float.MaxValue, ErrorMessage = "Seleccione el % de ITBIS.")]
    public double ITBIS { get; set; }
    public int CategoriaId { get; set; }
    [Required(ErrorMessage = "Seleccione si esta empacado.")]
    public string EstaEmpacado { get; set; } = string.Empty;
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Ingrese la cantidad del producto.")]
    public int Existencia { get; set; }
    public int ProveedorId { get; set; }
     [Required(ErrorMessage = "Seleccione la posición del producto.")]
    public string PosicionProducto { get; set; } = string.Empty;
    [Required(ErrorMessage = "Campo descuento es obligatorio.")]
    [Range(minimum: 1, maximum: float.MaxValue, ErrorMessage = "Seleccione el % del descuento.")]
    public double Descuento { get; set; }
    [Required(ErrorMessage = "Ingrese el código de barra.")]
    [RegularExpression(@"^\d{3}[- ]?\d{7}[- ]?\d{1}$", ErrorMessage = "Formato inválido 0000000")]
    public string CodigoBarra { get; set; } = string.Empty; 
    public bool Status { get; set; } = false;
    
}