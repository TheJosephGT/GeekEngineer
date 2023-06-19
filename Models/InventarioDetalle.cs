public class InventarioDetalle
{
    [Key]
    public int DetalleId { get; set; }
    public int InventarioId { get; set; }
    [Required(ErrorMessage = "Debe seleccionar el producto.")]
    public int ProductoId { get; set; }
    [Required]
    [Range(minimum: 1, maximum: double.MaxValue, ErrorMessage = "Ingrese una cantidad válida.")]
    public int Cantidad { get; set; }
    [Required(ErrorMessage = "Ingrese el código de barra del producto.")]
    public string CodigoBarra { get; set; } = string.Empty;
}