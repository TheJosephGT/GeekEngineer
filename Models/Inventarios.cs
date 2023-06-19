public class Inventarios
{
    [Key]
    public int InventarioId { get; set; }
    [Required(ErrorMessage = "El campo de producto ID es necesario.")]
    public int ProductoId { get; set; }
    [Required]
    [Range(minimum: 1, maximum: double.MaxValue, ErrorMessage = "Ingrese una cantidad válida.")]
    public int Cantidad { get; set; }
    [Required(ErrorMessage = "Seleccione el código de barra.")]
    public string CodigoBarra { get; set; } = string.Empty;
    [Required(ErrorMessage = "El campo fecha es requerido.")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool Status { get; set; } = true;
}