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

        if(modificado == null)
        {
            var existe = contexto.Clientes.Any(p => p.Cedula == cliente.Cedula && p.Status == true);
            if(existe == true)
                return false;
            else
                return true;
            
        }
        else
        {
            return true;
        }
    }

    public bool ExisteEmail(Clientes cliente)
    {
        var modificado = contexto.Clientes.Find(cliente.ClienteId);

        if(modificado == null)
        {
            var existe = contexto.Clientes.Any(p => p.Email == cliente.Email && p.Status == true);
            if(existe == true)
                return false;
            else
                return true;
            
        }
        else
        {
            return true;
        }
    }

    public bool ExisteTelefono(Clientes cliente)
    {
        var modificado = contexto.Clientes.Find(cliente.ClienteId);

        if(modificado == null)
        {
            var existe = contexto.Clientes.Any(p => p.Telefono == cliente.Telefono && p.Status == true);
            if(existe == true)
                return false;
            else
                return true;
            
        }
        else
        {
            return true;
        }
    }
    
    private bool Insertar(Clientes cliente)
    {
        contexto.Clientes.Add(cliente);
        return contexto.SaveChanges() > 0;
    }

    private bool Modificar(Clientes cliente)
    {
        var existe = contexto.Clientes.Find(cliente.ClienteId);

        if (existe != null)
        {
            contexto.Entry(existe).CurrentValues.SetValues(cliente);
            return contexto.SaveChanges() > 0;
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

        if(eliminado != null)
        {
            eliminado.Status = false;
            return contexto.SaveChanges() > 0;
        }
        
        return false;
    }

    public Clientes? Buscar(Clientes cliente)
    {
        
        var valor =  contexto.Clientes.Find(cliente.ClienteId);

        if(valor != null)
        {
            if(valor.Status == true)
            return contexto.Clientes
            .Where(p => p.ClienteId == valor.ClienteId)
            .AsNoTracking()
            .SingleOrDefault();
        else
            return null;
        }
        
        return null;
        
    }

    public List<Clientes> GetList(Expression<Func<Clientes, bool>> criterio)
    {
        return contexto.Clientes
        .AsNoTracking()
        .Where(criterio)
        .ToList();
    }
}