public class Compras 
{
    [Key]
    public int CompraId { get; set; }
    [Required(ErrorMessage = "El campo cliente es necesario")]
    public int ProveedorId { get; set; }
    [Required(ErrorMessage = "El campo producto es necesario")]
    public int ProductoId { get; set; }
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "El campo fecha es necesario")]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [Required(ErrorMessage = "El campo sub total es necesario")]
    public double SubTotal { get; set; }
    [Required(ErrorMessage = "El campo total es necesario")]
    public double Total { get; set; }
    public bool Status { get; set; } = true;
}