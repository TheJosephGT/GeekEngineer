using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GeekEngineer.Data;

public class VentasBLL
{
    private ApplicationDbContext contexto;
    public VentasBLL(ApplicationDbContext _contexto)
    {
        contexto = _contexto;
    }

    public bool Existe(int ventaId)
    {
        return contexto.Ventas.Any(p => p.VentaId == ventaId);
    }

    private bool Insertar(Ventas venta)
    {
        InsertarDetalle(venta);
        // var cliente = contexto.Clientes.Find(venta.ClienteId);
        // if (cliente != null)
        // {
        //     cliente.TotalVentas += venta.Total;
        //     contexto.Entry(cliente).State = EntityState.Modified;
        // }
        contexto.Ventas.Add(venta);
        bool salida = contexto.SaveChanges() > 0;
        contexto.Entry(venta).State = EntityState.Detached;
        return salida;
    }

    private bool Modificar(Ventas venta)
    {
        ModificarDetalle(venta);

        // var clienteAnterior = contexto.Clientes.Find(venta.ClienteId);
        // if (clienteAnterior != null)
        // {
        //     clienteAnterior.TotalVentas -= venta.Total;
        //     contexto.Entry(clienteAnterior).State = EntityState.Modified;
        // }

        // var cliente = contexto.Clientes.Find(venta.ClienteId);
        // if (cliente != null)
        // {
        //     cliente.TotalVentas += venta.Total;
        //     contexto.Entry(cliente).State = EntityState.Modified;
        // }

        contexto.Entry(venta).State = EntityState.Modified;
        bool salida = contexto.SaveChanges() > 0;
        contexto.Entry(venta).State = EntityState.Detached;
        return salida;
    }

    public bool Guardar(Ventas venta)
    {
        if (!Existe(venta.VentaId))
            return Insertar(venta);
        else
            return Modificar(venta);
    }

    private void InsertarDetalle(Ventas venta)
    {
        if (venta.ventasDetalle != null)
        {
            foreach (var item in venta.ventasDetalle)
            {
                var producto = contexto.Productos.Find(item.ProductoId);


                if (producto != null)
                {
                    producto.Existencia -= item.Cantidad;
                    producto.Ganancias += item.Importe;
                    producto.TotalVentas += item.Cantidad;
                    contexto.Entry(producto).State = EntityState.Modified;

                }
            }
        }
    }

    private void ModificarDetalle(Ventas venta)
    {
        var DetalleAnterior = contexto.Ventas.Where(o => o.VentaId == venta.VentaId).Include(o => o.ventasDetalle).AsNoTracking().SingleOrDefault();

        if (DetalleAnterior != null)
        {
            foreach (var item in DetalleAnterior.ventasDetalle)
            {
                var producto = contexto.Productos.Find(item.ProductoId);

                if (producto != null)
                {
                    producto.Existencia += item.Cantidad;
                    producto.Ganancias -= item.Importe;
                    producto.TotalVentas -= item.Cantidad;
                    contexto.Entry(producto).State = EntityState.Modified;
                }
            }

            foreach (var item in venta.ventasDetalle)
            {
                var producto = contexto.Productos.Find(item.ProductoId);

                if (producto != null)
                {
                    producto.Existencia -= item.Cantidad;
                    producto.Ganancias += item.Importe;
                    producto.TotalVentas += item.Cantidad;
                    contexto.Entry(producto).State = EntityState.Modified;
                }
            }
        }
    }

    public bool Eliminar(int ventaId)
    {
        var eliminado = contexto.Ventas
        .Where(p => p.VentaId == ventaId)
        .SingleOrDefault();


        if (eliminado != null)
        {
            // var cliente = contexto.Clientes.Find(eliminado.ClienteId);
            // if (cliente != null)
            // {
            //     cliente.TotalVentas -= eliminado.Total;
            //     contexto.Entry(cliente).State = EntityState.Modified;
            // }

            foreach (var item in eliminado.ventasDetalle)
            {
                var producto = contexto.Productos.Find(item.ProductoId);

                if (producto != null)
                {
                    producto.Existencia += item.Cantidad;
                    producto.Ganancias -= item.Importe;
                    producto.TotalVentas -= item.Cantidad;
                    contexto.Entry(producto).State = EntityState.Modified;
                }
            }

            contexto.RemoveRange(eliminado.ventasDetalle);
            eliminado.Status = false;
            bool salida = contexto.SaveChanges() > 0;
            contexto.Entry(eliminado).State = EntityState.Detached;
            return salida;
        }
        return false;
    }

    public Ventas? Buscar(int ventaId)
    {
        return contexto.Ventas
        .Include(o => o.ventasDetalle)
        .Where(o => o.VentaId == ventaId)
        .SingleOrDefault();
    }

    public List<Ventas> GetList(Expression<Func<Ventas, bool>> criterio)
    {
        return contexto.Ventas
        .AsNoTracking()
        .Where(criterio)
        .ToList();
    }
}