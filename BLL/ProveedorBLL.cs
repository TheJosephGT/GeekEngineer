using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class ProveedorBLL
{
    private Contexto contexto;

    public ProveedorBLL(Contexto _contexto)
    {
        contexto = _contexto;
    }

    private bool Existe(int proveedorId)
    {
        return contexto.Proveedores.Any(p => p.ProveedorId == proveedorId);
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

        if (eliminado != null)
        {
            contexto.Entry(eliminado).State = EntityState.Deleted;
            return contexto.SaveChanges() > 0;
        }

        return false;
    }

    public Proveedores? Buscar(int proveedorId)
    {
        return contexto.Proveedores.Where(p => p.ProveedorId == proveedorId).AsNoTracking().SingleOrDefault();
    }

    public List<Proveedores> GetList(Expression<Func<Proveedores, bool>> criterio)
    {
        return contexto.Proveedores.AsNoTracking().Where(criterio).ToList();
    }
}