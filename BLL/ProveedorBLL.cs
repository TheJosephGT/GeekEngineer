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

    public bool Existe(int proveedorId)
    {
        return contexto.Proveedores.Any(p => p.ProveedorId == proveedorId);
    }
    
    public bool ExisteNombreProveedor(Proveedores proveedor)
    {
        var modificado = contexto.Proveedores.Find(proveedor.ProveedorId);

        if(modificado == null)
        {
            var existe = contexto.Proveedores.Any(p => p.Nombre.ToLower() == proveedor.Nombre.ToLower());
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

    private bool Insertar(Proveedores proveedor)
    {
        contexto.Proveedores.Add(proveedor);
        return contexto.SaveChanges() > 0;
    }

    private bool Modificar(Proveedores proveedor)
    {
        var existe = contexto.Proveedores.Find(proveedor.ProveedorId);

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
        var eliminado = contexto.Proveedores.Where(p => p.ProveedorId == proveedorId).SingleOrDefault();
        if(eliminado != null)
        {
            eliminado.EsVisible = false;
            return contexto.SaveChanges() > 0;
        }
        
        return false;
    }

    public Proveedores Buscar(int proveedorId)
    {
        if(contexto.Proveedores.Any(p => p.EsVisible == true))
            return contexto.Proveedores.Where(p => p.ProveedorId == proveedorId).AsNoTracking().SingleOrDefault();
        else
            return null;
    }

    public List<Proveedores> GetList(Expression<Func<Proveedores, bool>> criterio)
    {
        return contexto.Proveedores.AsNoTracking().Where(criterio).ToList();
    }
}