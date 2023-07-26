using System.ComponentModel.DataAnnotations.Schema;
public class Categorias
{
    [Key]
    public int CategoriaId { get; set; }
    [Required(ErrorMessage = "Campo nombre es obligatorio.")]
    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "Campo fecha es obligatorio.")]
    [NotMapped]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool Status { get; set; } = true;
}