public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public bool Status { get; set; } = false;
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}