public class Inventarios
{
    [Key]
    public int InventarioId { get; set; }
    [Required(ErrorMessage = "El campo de producto ID es necesario.")]
    public int ProductoId { get; set; }
    public int CantidadAumentada { get; set; }
    [Required(ErrorMessage = "Seleccione el código de barra.")]
    public string CodigoBarra { get; set; } = string.Empty;
    [Required(ErrorMessage = "El campo fecha es requerido.")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool Status { get; set; } = true;
    public int Existencia { get; set; }
}