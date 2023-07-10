using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GeekEngineer.Data;

public class ProveedorBLL
{
    private ApplicationDbContext contexto;
    public ProveedorBLL(ApplicationDbContext _contexto)
    {
        contexto = _contexto;
    }

    public bool Existe(int proveedorId)
    {
        return contexto.Proveedores.Any(p => p.ProveedorId == proveedorId);
    }

    public bool ExisteNombreProveedor(Proveedores proveedor)
    {
        var modificado = contexto.Proveedores.Find(proveedor.ProveedorId);

        if (modificado == null)
        {
            var existe = contexto.Proveedores.Any(p => p.Nombre.ToLower() == proveedor.Nombre.ToLower() && p.Status == true);
            if (existe == true)
                return false;
            else
                return true;

        }
        else
        {

            var existe = contexto.Proveedores.Any(p => p.Nombre.ToLower() == proveedor.Nombre.ToLower() && p.Status == true && p.ProveedorId != modificado.ProveedorId);
            if (existe == true)
                return false;
            else
                return true;
        }
    }

    public bool ExisteRNC(Proveedores proveedor)
    {
        var modificado = contexto.Proveedores.Find(proveedor.ProveedorId);

        if (modificado == null)
        {
            var existe = contexto.Proveedores.Any(p => p.RNC.ToLower() == proveedor.RNC.ToLower() && p.Status == true);
            if (existe == true)
                return false;
            else
                return true;

        }
        else
        {

            var existe = contexto.Proveedores.Any(p => p.RNC.ToLower() == proveedor.RNC.ToLower() && p.Status == true && p.ProveedorId != modificado.ProveedorId);
            if (existe == true)
                return false;
            else
                return true;
        }
    }

    public bool ExisteEmail(Proveedores proveedor)
    {
        var modificado = contexto.Proveedores.Find(proveedor.ProveedorId);

        if (modificado == null)
        {
            var existe = contexto.Proveedores.Any(p => p.Email.ToLower() == proveedor.Email.ToLower() && p.Status == true);
            if (existe == true)
                return false;
            else
                return true;

        }
        else
        {

            var existe = contexto.Proveedores.Any(p => p.Email.ToLower() == proveedor.Email.ToLower() && p.Status == true && p.ProveedorId != modificado.ProveedorId);
            if (existe == true)
                return false;
            else
                return true;
        }
    }

    public bool ExisteTelefono(Proveedores proveedor)
    {
        var modificado = contexto.Proveedores.Find(proveedor.ProveedorId);

        if (modificado == null)
        {
            var existe = contexto.Proveedores.Any(p => p.Telefono.ToLower() == proveedor.Telefono.ToLower() && p.Status == true);
            if (existe == true)
                return false;
            else
                return true;

        }
        else
        {

            var existe = contexto.Proveedores.Any(p => p.Telefono.ToLower() == proveedor.Telefono.ToLower() && p.Status == true && p.ProveedorId != modificado.ProveedorId);
            if (existe == true)
                return false;
            else
                return true;
        }
    }


    private bool Insertar(Proveedores proveedor)
    {
        contexto.Proveedores.Add(proveedor);
        bool salida = contexto.SaveChanges() > 0;
        contexto.Entry(proveedor).State = EntityState.Detached;
        return salida;
    }

    private bool Modificar(Proveedores proveedor)
    {
        var existe = contexto.Proveedores.Find(proveedor.ProveedorId);

        if (existe != null)
        {
            contexto.Entry(existe).CurrentValues.SetValues(proveedor);
            bool salida = contexto.SaveChanges() > 0;
            contexto.Entry(proveedor).State = EntityState.Detached;
            return salida;
        }

        return false;
    }

    public bool Guardar(Proveedores proveedor)
    {
        if (!Existe(proveedor.ProveedorId))
            return Insertar(proveedor);
        else
            return Modificar(proveedor);
    }

    public bool Eliminar(int proveedorId)
    {
        var eliminado = contexto.Proveedores
        .Where(p => p.ProveedorId == proveedorId)
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

    public Proveedores? Buscar(Proveedores proveedor)
    {
        var valor = contexto.Proveedores.Find(proveedor.ProveedorId);

        if (valor != null)
        {
            if (valor.Status == true)
                return contexto.Proveedores
                .Where(p => p.ProveedorId == valor.ProveedorId)
                .AsNoTracking()
                .SingleOrDefault();
            else
                return null;
        }

        return null;
    }

    public List<Proveedores> GetList(Expression<Func<Proveedores, bool>> criterio)
    {
        return contexto.Proveedores
        .AsNoTracking()
        .Where(criterio)
        .ToList();
    }
}