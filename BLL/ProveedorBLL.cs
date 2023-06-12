using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GeekEngineer.Data;

public class ProveedorBLL
{
    #nullable disable
    private ApplicationDbContext contexto;
    public ProveedorBLL(ApplicationDbContext _contexto)
    {
        contexto = _contexto;
    }

    private bool Existe(int proveedorId)
    {
        return contexto.proveedor.Any(p => p.ProveedorId == proveedorId);
    }
    public Proveedores ExisteNombreProveedor(string Nombre)
    {
        Proveedores existe;

        try
        {
            existe = contexto.proveedor              
            .Where( p => p.Nombre
            .ToLower() == Nombre.ToLower())
            .AsNoTracking()
            .SingleOrDefault();

        }
        catch (Exception)
        {
            throw;
        }
        return existe;
    }

    private bool Insertar(Proveedores proveedor)
    {
        contexto.proveedor.Add(proveedor);
        return contexto.SaveChanges() > 0;
    }

    private bool Modificar(Proveedores proveedor)
    {
        var existe = contexto.proveedor.Find(proveedor.ProveedorId);

        if (existe != null)
        {
            contexto.Entry(existe).CurrentValues.SetValues(proveedor);
            return contexto.SaveChanges() > 0;
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
        var eliminado = contexto.proveedor.Where(p => p.ProveedorId == proveedorId).SingleOrDefault();

        if (eliminado != null)
        {
            contexto.Entry(eliminado).State = EntityState.Deleted;
            return contexto.SaveChanges() > 0;
        }

        return false;
    }

    public Proveedores Buscar(int proveedorId)
    {
        return contexto.proveedor.Where(p => p.ProveedorId == proveedorId).AsNoTracking().SingleOrDefault();
    }

    public List<Proveedores> GetList(Expression<Func<Proveedores, bool>> criterio)
    {
        return contexto.proveedor.AsNoTracking().Where(criterio).ToList();
    }
}