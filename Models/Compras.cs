using System.ComponentModel.DataAnnotations.Schema;

public class Compras 
{
    [Key]
    public int CompraId { get; set; }
    [Required(ErrorMessage = "El campo cliente es necesario")]
    public int ProveedorId { get; set; }
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "El campo fecha es necesario")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly FechaEntrada { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public double Total { get; set; }
    public bool Status { get; set; } = true;

    [ForeignKey("CompraId")]
    public List<ComprasDetalle> ComprasDetalle { get; set; } = new List<ComprasDetalle>();
}