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

        if(modificado == null)
        {
            var existe = contexto.Productos.Any(p => p.CodigoBarra == producto.CodigoBarra);
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
    
     public bool ExisteNombre(Productos producto)
    {
        var modificado = contexto.Productos.Find(producto.ProductoId);

        if(modificado == null)
        {
            var existe = contexto.Productos.Any(p => p.Nombre == producto.Nombre);
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
    private bool Insertar(Productos producto)
    {
        contexto.Productos.Add(producto);
        return contexto.SaveChanges() > 0;
    }

    private bool Modificar(Productos producto)
    {
        var existe = contexto.Productos.Find(producto.ProductoId);

        if (existe != null)
        {
            contexto.Entry(existe).CurrentValues.SetValues(producto);
            return contexto.SaveChanges() > 0;
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

        if(eliminado != null)
        {
            eliminado.EsVisible = false;
            return contexto.SaveChanges() > 0;
        }
        
        return false;
    }

    public Productos? Buscar(int productoId)
    {
        if(contexto.Productos.Any(p => p.EsVisible == true))
            return contexto.Productos
            .Where(p => p.ProductoId == productoId)
            .AsNoTracking()
            .SingleOrDefault();
        else
            return null;
    }

    public List<Productos> GetList(Expression<Func<Productos, bool>> criterio)
    {
        return contexto.Productos
        .AsNoTracking()
        .Where(criterio)
        .ToList();
    }
}