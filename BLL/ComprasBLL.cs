using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GeekEngineer.Data;

public class ComprasBLL
{
    private ApplicationDbContext contexto;
    public ComprasBLL(ApplicationDbContext _contexto)
    {
        contexto = _contexto;
    }

    public bool Existe(int compraId)
    {
        return contexto.Compras.Any(p => p.CompraId == compraId);
    }

    private bool Insertar(Compras compra)
    {
        InsertarDetalle(compra);
        contexto.Compras.Add(compra);
        bool salida = contexto.SaveChanges() > 0;
        contexto.Entry(compra).State = EntityState.Detached;
        return salida;
    }

    private bool Modificar(Compras compra)
    {
        ModificarDetalle(compra);
        contexto.Entry(compra).State = EntityState.Modified;
        bool salida = contexto.SaveChanges() > 0;
        contexto.Entry(compra).State = EntityState.Detached;
        return salida;
    }

    public bool Guardar(Compras compra)
    {
        if (!Existe(compra.CompraId))
            return Insertar(compra);
        else
            return Modificar(compra);
    }

    private void InsertarDetalle(Compras compra)
    {
        if (compra.ComprasDetalle != null)
        {
            foreach (var item in compra.ComprasDetalle)
            {
                var producto = contexto.Productos.Find(item.ProductoId);

                if (producto != null)
                {
                    producto.Existencia += item.Cantidad;
                    contexto.Entry(producto).State = EntityState.Modified;
                    
                }
            }
        }
    }

    private void ModificarDetalle(Compras compra)
    {
        var DetalleAnterior = contexto.Compras.Where(o => o.CompraId == compra.CompraId).Include(o => o.ComprasDetalle).AsNoTracking().SingleOrDefault();

        if (DetalleAnterior != null)
        {
            foreach (var item in DetalleAnterior.ComprasDetalle)
            {
                var producto = contexto.Productos.Find(item.ProductoId);

                if (producto != null)
                {
                    producto.Existencia -= item.Cantidad;
                    contexto.Entry(producto).State = EntityState.Modified;
                }
            }

            foreach (var item in compra.ComprasDetalle)
            {
                var producto = contexto.Productos.Find(item.ProductoId);

                if (producto != null)
                {
                    producto.Existencia += item.Cantidad;
                    contexto.Entry(producto).State = EntityState.Modified;
                }
            }
        }
    }

    public bool Eliminar(int compraId)
    {
        var eliminado = contexto.Compras
        .Where(p => p.CompraId == compraId)
        .SingleOrDefault();

        if (eliminado != null)
        {
            foreach (var item in eliminado.ComprasDetalle)
            {
                var producto = contexto.Productos.Find(item.ProductoId);

                if (producto != null)
                {
                    producto.Existencia -= item.Cantidad;
                    contexto.Entry(producto).State = EntityState.Modified;
                }
            }

            contexto.RemoveRange(eliminado.ComprasDetalle);
            eliminado.Status = false;
            bool salida = contexto.SaveChanges() > 0;
            contexto.Entry(eliminado).State = EntityState.Detached;
            return salida;
        }
        return false;
    }

    public Compras? Buscar(int compraId)
    {
        return contexto.Compras
        .Include(o => o.ComprasDetalle)
        .Where(o => o.CompraId == compraId)
        .SingleOrDefault();
    }

    public List<Compras> GetList(Expression<Func<Compras, bool>> criterio)
    {
        return contexto.Compras
        .AsNoTracking()
        .Where(criterio)
        .ToList();
    }
}