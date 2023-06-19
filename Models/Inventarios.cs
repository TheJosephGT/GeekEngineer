using System.ComponentModel.DataAnnotations.Schema;

public class Inventarios
{
    [Key]
    public int InventarioId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    [Required(ErrorMessage = "Seleccione el código de barra.")]
    [RegularExpression(@"^\d{3}[- ]?\d{3}[- ]?\d{4}$",ErrorMessage = "Formato inválido. 000-000-000")]
    public string CodigoBarra { get; set; } = string.Empty;
    [Required(ErrorMessage = "El campo fecha es requerido.")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool Status { get; set; } = true;

    [ForeignKey("InventarioId")]
    public virtual List<InventarioDetalle> inventariosDetalle { get; set; } = new List<InventarioDetalle>();
}