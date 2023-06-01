
public class Proveedores
{
    [Key]
    public int ProveedorId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string NombreEmpresa { get; set; } = string.Empty;
    public string RNCEmpresa { get; set; } = string.Empty;
    public string NCFEmpresa { get; set; } = string.Empty;
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public int ProductoId { get; set; } 
    public string Direccion { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}