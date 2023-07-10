using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GeekEngineer.Data;

public class ProductoBLL
{
    private ApplicationDbContext contexto;

    public ProductoBLL(ApplicationDbContext _contexto)
    {
        contexto = _contexto;
    }

    public bool Existe(int productoId)
    {
        return contexto.Productos.Any(p => p.ProductoId == productoId);
    }

    public bool ExisteCodigoBarra(Productos producto)
    {
        var modificado = contexto.Productos.Find(producto.ProductoId);

        if (modificado == null)
        {
            var existe = contexto.Productos.Any(p => p.CodigoBarra.ToLower() == producto.CodigoBarra.ToLower() && p.Status == true);
            if (existe == true)
                return false;
            else
                return true;

        }
        else
        {

            var existe = contexto.Productos.Any(p => p.CodigoBarra.ToLower() == producto.CodigoBarra.ToLower() && p.Status == true && p.ProductoId != modificado.ProductoId);
            if (existe == true)
                return false;
            else
                return true;
        }
    }

    public bool ExisteNombre(Productos producto)
    {
        var modificado = contexto.Productos.Find(producto.ProductoId);

        if (modificado == null)
        {
            var existe = contexto.Productos.Any(p => p.Nombre.ToLower() == producto.Nombre.ToLower() && p.Status == true && p.ProductoId != producto.ProductoId);
            if (existe == true)
                return false;
            else
                return true;

        }
        else
        {
            var existe = contexto.Productos.Any(p => p.Nombre.ToLower() == producto.Nombre.ToLower() && p.Status == true && p.ProductoId != modificado.ProductoId);
            if (existe == true)
                return false;
            else
                return true;
        }
    }
    private bool Insertar(Productos producto)
    {
        contexto.Productos.Add(producto);
        bool salida = contexto.SaveChanges() > 0;
        contexto.Entry(producto).State = EntityState.Detached;
        return salida;
    }

    private bool Modificar(Productos producto)
    {
        var existe = contexto.Productos.Find(producto.ProductoId);

        if (existe != null)
        {
            contexto.Entry(existe).CurrentValues.SetValues(producto);
            bool salida = contexto.SaveChanges() > 0;
            contexto.Entry(producto).State = EntityState.Detached;
            return salida;
        }

        return false;
    }

    public bool Guardar(Productos producto)
    {
        if (!Existe(producto.ProductoId))
            return Insertar(producto);
        else
            return Modificar(producto);
    }

    public bool Eliminar(int productoId)
    {
        var eliminado = contexto.Productos
        .Where(p => p.ProductoId == productoId)
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

    public Productos? Buscar(int productoId)
    {
        return contexto.Productos
        .Where(p => p.ProductoId == productoId && p.Status == true)
        .AsNoTracking()
        .SingleOrDefault();
    }

    public List<Productos> GetList(Expression<Func<Productos, bool>> criterio)
    {
        return contexto.Productos
        .AsNoTracking()
        .Where(criterio)
        .ToList();
    }
}