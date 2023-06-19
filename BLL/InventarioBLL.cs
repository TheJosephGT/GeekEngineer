using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GeekEngineer.Data;

public class InventarioBLL
{
    private ApplicationDbContext contexto;
    public InventarioBLL(ApplicationDbContext _contexto)
    {
        contexto = _contexto;
    }

    public bool Existe(int inventarioId)
    {
        return contexto.Inventarios.Any(p => p.InventarioId == inventarioId);
    }

    private bool Insertar(Inventarios inventario)
    {

        var producto = contexto.Productos.Find(inventario.ProductoId);
        if (producto != null)
        {
            producto.Existencia += inventario.Cantidad;
            contexto.Entry(producto).State = EntityState.Modified;
            contexto.Inventarios.Add(inventario);
            bool salida = contexto.SaveChanges() > 0;
            contexto.Entry(inventario).State = EntityState.Detached;
            return salida;
        }
        return false;
    }

    private bool Modificar(Inventarios inventario)
    {
        var existe = contexto.Inventarios.FirstOrDefault(p => p.InventarioId == inventario.InventarioId);

        if (existe != null)
        {
            var producto = contexto.Productos.Find(inventario.ProductoId);
            if (producto != null)
            {
                producto.Existencia -= inventario.Cantidad;
                producto.Existencia += inventario.Cantidad;
                contexto.Entry(producto).State = EntityState.Modified;
                contexto.SaveChanges();
                contexto.Entry(producto).State = EntityState.Detached;
            }
            contexto.Entry(existe).CurrentValues.SetValues(inventario);
            bool salida = contexto.SaveChanges() > 0;
            contexto.Entry(inventario).State = EntityState.Detached;
            return salida;
        }

        return false;
    }

    public bool Guardar(Inventarios inventario)
    {
        if (!Existe(inventario.InventarioId))
            return Insertar(inventario);
        else
            return Modificar(inventario);
    }

    public bool Eliminar(int inventarioId)
    {
        var eliminado = contexto.Inventarios
        .Where(p => p.InventarioId == inventarioId)
        .SingleOrDefault();

        if (eliminado != null)
        {
            var producto = contexto.Productos.Find(eliminado.ProductoId);
            if (producto != null)
            {
                producto.Existencia -= eliminado.Cantidad;
                contexto.Entry(producto).State = EntityState.Modified;
            }
            eliminado.Status = false;
            bool salida = contexto.SaveChanges() > 0;
            contexto.Entry(eliminado).State = EntityState.Detached;
            return salida;
        }

        return false;
    }

    public Inventarios? Buscar(Inventarios inventario)
    {
        var valor = contexto.Inventarios.Find(inventario.InventarioId);

        if (valor != null)
        {
            if (valor.Status == true)
                return contexto.Inventarios
                .Where(p => p.InventarioId == valor.InventarioId)
                .AsNoTracking()
                .SingleOrDefault();
            else
                return null;
        }

        return null;
    }

    public List<Inventarios> GetList(Expression<Func<Inventarios, bool>> criterio)
    {
        return contexto.Inventarios.AsNoTracking().Where(criterio).ToList();
    }
}