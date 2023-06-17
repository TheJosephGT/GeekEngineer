public class Inventarios
{
    [Key]
    public int InventarioId { get; set; }
    [Required(ErrorMessage = "Debe seleccionar el producto.")]
    public int ProductoId { get; set; }
    [Required]
    [Range(minimum: 1, maximum: double.MaxValue, ErrorMessage = "Ingrese una cantidad válida.")]
    public int Cantidad { get; set; }
    [Required(ErrorMessage = "Debe seleccionar el proveedor del producto.")]
    public int ProveedorId { get; set; }
    [Required(ErrorMessage = "Ingrese el código de barra del producto.")]
    public string CodigoBarra { get; set; } = string.Empty;
    [Required(ErrorMessage = "El campo fecha es requerido.")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool Status { get; set; } = true;
}