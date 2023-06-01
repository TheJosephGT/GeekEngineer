
public class Productos
{
    [Key]
    public int ProductoId { get; set; }
    [Required(ErrorMessage = "Ingrese el nombre del producto.")]
    public string Nombre { get; set; } = string.Empty;
    [DataType(DataType.Date)]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [Required]
    [Range(minimum: 1, maximum: float.MaxValue, ErrorMessage = "Ingrese un precio mayor a 0.")]
    public double PrecioCompra { get; set; }
    [Required(ErrorMessage = "Campo ITBIS es obligatorio.")]
    [Range(minimum: 1, maximum: float.MaxValue, ErrorMessage = "Seleccione el % de ITBIS.")]
    public double ITBIS { get; set; }
    public int CategoriaId { get; set; }
    [Required(ErrorMessage = "Seleccione si esta empacado.")]
    public string EstaEmpacado { get; set; } = string.Empty;
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Ingrese la cantidad del producto.")]
    public int Cantidad { get; set; }
    public int ProveedorId { get; set; }
}