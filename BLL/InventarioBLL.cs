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

    public string BuscarCodigoDeBarra(int productoId)
    {
        var productoBuscado = contexto.Productos.Find(productoId);
        if(productoBuscado != null)
        {
            string CodigoBarra = productoBuscado.CodigoBarra;
            return CodigoBarra;
        }
        else
        {
            return string.Empty;
        }
    }

    public bool Existe(int inventarioId)
    {
        return contexto.Inventarios.Any(p => p.InventarioId == inventarioId);
    }

    private bool Insertar(Inventarios inventario)
    {
        InsertarDetalle(inventario);
        contexto.Inventarios.Add(inventario);
        bool salida = contexto.SaveChanges() > 0;
        contexto.Entry(inventario).State = EntityState.Detached;
        return salida;
    }

    private bool Modificar(Inventarios inventario)
    {
        ModificarDetalle(inventario);
        contexto.Entry(inventario).State = EntityState.Modified;
        return contexto.SaveChanges() > 0;
    }

    public bool Guardar(Inventarios inventario)
    {
        if (!Existe(inventario.InventarioId))
            return Insertar(inventario);
        else
            return Modificar(inventario);
    }

    private bool InsertarDetalle(Inventarios inventario)
    {
        if (inventario.InventariosDetalle != null)
        {
            foreach (var item in inventario.InventariosDetalle)
            {
                var producto = contexto.Productos.Find(item.ProductoId);

                if (producto != null)
                {
                    producto.Existencia += item.Cantidad;
                    contexto.Entry(producto).State = EntityState.Modified;
                }
            }
        }

        bool salida = contexto.SaveChanges() > 0;
        contexto.Entry(inventario).State = EntityState.Detached;
        return salida;
    }

    private bool ModificarDetalle(Inventarios inventario)
    {
        var DetalleAnterior = contexto.Inventarios.Where(o => o.InventarioId == inventario.InventarioId).Include(o => o.InventariosDetalle).AsNoTracking().SingleOrDefault();

        if (DetalleAnterior != null)
        {
            foreach (var item in inventario.InventariosDetalle)
            {
                var producto = contexto.Productos.Find(item.ProductoId);

                if (producto != null)
                {
                    producto.Existencia -= item.Cantidad;
                    contexto.Entry(producto).State = EntityState.Modified;
                }
            }
        }

        contexto.Database.ExecuteSqlRaw($"DELETE FROM EntradaDetalle where EntradaId = {inventario.InventarioId}");

        //Se deshacen los cambios al detalle anterior.

        foreach (var item in inventario.InventariosDetalle)
        {
            var producto = contexto.Productos.Find(item.ProductoId);

            if (producto != null)
            {
                producto.Existencia += item.Cantidad;
                contexto.Entry(producto).State = EntityState.Modified;

            }
        }

        contexto.Inventarios.Update(inventario);
        bool salida = contexto.SaveChanges() > 0;
        contexto.Entry(inventario).State = EntityState.Detached;
        return salida;
    }

    public bool Eliminar(int inventarioId)
    {
        var eliminado = contexto.Inventarios
        .Where(p => p.InventarioId == inventarioId)
        .SingleOrDefault();

        if (eliminado != null)
        {
            foreach (var item in eliminado.InventariosDetalle)
            {
                var producto = contexto.Productos.Find(item.ProductoId);

                if (producto != null)
                {
                    producto.Existencia -= item.Cantidad;
                    contexto.Entry(producto).State = EntityState.Modified;
                }
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