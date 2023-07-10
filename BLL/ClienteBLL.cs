using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GeekEngineer.Data;

public class ClienteBLL
{
    private ApplicationDbContext contexto;

    public ClienteBLL(ApplicationDbContext _contexto)
    {
        contexto = _contexto;
    }

    public bool Existe(int clienteId)
    {
        return contexto.Clientes.Any(p => p.ClienteId == clienteId);
    }

    public bool ExisteCedula(Clientes cliente)
    {
        var modificado = contexto.Clientes.Find(cliente.ClienteId);

        if (modificado == null)
        {
            var existe = contexto.Clientes.Any(p => p.Cedula.ToLower() == cliente.Cedula.ToLower() && p.Status == true);
            if (existe == true)
                return false;
            else
                return true;

        }
        else
        {

            var existe = contexto.Clientes.Any(p => p.Cedula.ToLower() == cliente.Cedula.ToLower() && p.Status == true && p.ClienteId != modificado.ClienteId);
            if (existe == true)
                return false;
            else
                return true;
        }
    }

    public bool ExisteEmail(Clientes cliente)
    {
        var modificado = contexto.Clientes.Find(cliente.ClienteId);

        if (modificado == null)
        {
            var existe = contexto.Clientes.Any(p => p.Email.ToLower() == cliente.Email.ToLower() && p.Status == true);
            if (existe == true)
                return false;
            else
                return true;

        }
        else
        {

            var existe = contexto.Clientes.Any(p => p.Email.ToLower() == cliente.Email.ToLower() && p.Status == true && p.ClienteId != modificado.ClienteId);
            if (existe == true)
                return false;
            else
                return true;
        }
    }

    public bool ExisteTelefono(Clientes cliente)
    {
        var modificado = contexto.Clientes.Find(cliente.ClienteId);

        if (modificado == null)
        {
            var existe = contexto.Clientes.Any(p => p.Telefono.ToLower() == cliente.Telefono.ToLower() && p.Status == true);
            if (existe == true)
                return false;
            else
                return true;

        }
        else
        {

            var existe = contexto.Clientes.Any(p => p.Telefono.ToLower() == cliente.Telefono.ToLower() && p.Status == true && p.ClienteId != modificado.ClienteId);
            if (existe == true)
                return false;
            else
                return true;
        }
    }

    private bool Insertar(Clientes cliente)
    {
        contexto.Clientes.Add(cliente);
        bool salida = contexto.SaveChanges() > 0;
        contexto.Entry(cliente).State = EntityState.Detached;
        return salida;
    }

    private bool Modificar(Clientes cliente)
    {
        var existe = contexto.Clientes.Find(cliente.ClienteId);

        if (existe != null)
        {
            contexto.Entry(existe).CurrentValues.SetValues(cliente);
            bool salida = contexto.SaveChanges() > 0;
            contexto.Entry(cliente).State = EntityState.Detached;
            return salida;
        }

        return false;
    }

    public bool Guardar(Clientes cliente)
    {
        if (!Existe(cliente.ClienteId))
            return Insertar(cliente);
        else
            return Modificar(cliente);
    }

    public bool Eliminar(int clienteId)
    {
        var eliminado = contexto.Clientes
        .Where(p => p.ClienteId == clienteId)
        .SingleOrDefault();

        if (eliminado != null)
        {
            eliminado.Status = false;
            bool salida = contexto.SaveChanges() > 0;
            contexto.Entry(eliminado).State = EntityState.Detached;
            return salida;
        }

        return false;
    }

    public Clientes? Buscar(int clienteId)
    {
        return contexto.Clientes
        .Where(p => p.ClienteId == clienteId && p.Status == true)
        .AsNoTracking()
        .SingleOrDefault();
    }

    public List<Clientes> GetList(Expression<Func<Clientes, bool>> criterio)
    {
        return contexto.Clientes
        .AsNoTracking()
        .Where(criterio)
        .ToList();
    }
}