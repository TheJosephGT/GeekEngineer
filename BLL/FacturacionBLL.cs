using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GeekEngineer.Data;

public class FacturacionBLL
{
    private ApplicationDbContext contexto;
    public FacturacionBLL(ApplicationDbContext _contexto)
    {
        contexto = _contexto;
    }

    public bool Existe(int facturaId)
    {
        return contexto.Facturacion.Any(p => p.FacturaId == facturaId);
    }

    private bool Insertar(Facturacion factura)
    {
        contexto.Facturacion.Add(factura);
        bool salida = contexto.SaveChanges() > 0;
        contexto.Entry(factura).State = EntityState.Detached;
        return salida;
    }

    private bool Modificar(Facturacion factura)
    {
        var existe = contexto.Facturacion.Find(factura.FacturaId);

        if (existe != null)
        {
            contexto.Entry(existe).CurrentValues.SetValues(factura);
            bool salida = contexto.SaveChanges() > 0;
            contexto.Entry(factura).State = EntityState.Detached;
            return salida;
        }

        return false;
    }

    public bool Guardar(Facturacion factura)
    {
        if (!Existe(factura.FacturaId))
            return Insertar(factura);
        else
            return Modificar(factura);
    }

    public bool Eliminar(int facturaId)
    {
        var eliminado = contexto.Facturacion
        .Where(p => p.FacturaId == facturaId)
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

    public Facturacion? Buscar(int facturaId)
    {
        return contexto.Facturacion.Include(o => o.facturaDetalle).Where(o => o.FacturaId == facturaId).SingleOrDefault();
    }

    public List<Facturacion> GetList(Expression<Func<Facturacion, bool>> criterio)
    {
        return contexto.Facturacion.AsNoTracking().Where(criterio).ToList();
    }
}