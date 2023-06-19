using System.ComponentModel.DataAnnotations.Schema;

public class Inventarios
{
    [Key]
    public int InventarioId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
    public string CodigoBarra { get; set; } = string.Empty;
    [Required(ErrorMessage = "El campo fecha es requerido.")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool Status { get; set; } = true;

    [ForeignKey("InventarioId")]
    public virtual List<InventarioDetalle> InventariosDetalle { get; set; } = new List<InventarioDetalle>();
}