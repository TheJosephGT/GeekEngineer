using System.ComponentModel.DataAnnotations.Schema;
public class Clientes
{
    [Key]
    public int ClienteId { get; set; }
    [Required(ErrorMessage = "Ingrese un número de cédula.")]
    [RegularExpression(@"^\d{3}[- ]?\d{7}[- ]?\d{1}$", ErrorMessage = "Formato inválido 000-0000000-0")]
    public string Cedula { get; set; } = string.Empty;
    [Required(ErrorMessage = "Ingrese un nombre.")]
    public string Nombre { get; set; } = string.Empty;
    [Required(ErrorMessage = "Ingrese un número teléfonico.")]
    [RegularExpression(@"^\d{3}[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Formato inválido. 000-000-0000")]
    public string Telefono { get; set; } = string.Empty;
    [Required(ErrorMessage = "Ingrese una dirección.")]
    public string Direccion { get; set; } = string.Empty;
    [Required(ErrorMessage = "Campo email es requerido.")]
    [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "Formato inválido. name@gmail.com")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "La fecha es un campo requerido.")]
    [NotMapped]
    public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool Status { get; set; } = true;
}