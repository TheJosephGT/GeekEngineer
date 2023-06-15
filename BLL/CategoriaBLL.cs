using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using GeekEngineer.Data;

public class CategoriaBLL
{
    private ApplicationDbContext contexto;
    public CategoriaBLL(ApplicationDbContext _contexto)
    {
        contexto = _contexto;
    }

    public bool Existe(int categoriaId)
    {
        return contexto.Categorias.Any(p => p.CategoriaId == categoriaId);
    }
    
    public bool ExisteNombreCategoria(Categorias categoria)
    {
        var modificado = contexto.Categorias.Find(categoria.CategoriaId);

        if(modificado == null)
        {
            var existe = contexto.Categorias.Any(p => p.Nombre.ToLower() == categoria.Nombre.ToLower() && p.EsVisible == true);
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

    private bool Insertar(Categorias categoria)
    {
        contexto.Categorias.Add(categoria);
        return contexto.SaveChanges() > 0;
    }

    private bool Modificar(Categorias categoria)
    {
        var existe = contexto.Categorias.Find(categoria.CategoriaId);

        if (existe != null)
        {
            contexto.Entry(existe).CurrentValues.SetValues(categoria);
            return contexto.SaveChanges() > 0;
        }

        return false;
    }

    public bool Guardar(Categorias categoria)
    {
        if (!Existe(categoria.CategoriaId))
            return Insertar(categoria);
        else
            return Modificar(categoria);
    }

    public bool Eliminar(int categoriaId)
    {
        var eliminado = contexto.Categorias
        .Where(p => p.CategoriaId == categoriaId)
        .SingleOrDefault();

        if(eliminado != null)
        {
            eliminado.EsVisible = false;
            return contexto.SaveChanges() > 0;
        }
        
        return false;
    }

    public Categorias? Buscar(Categorias categoria)
    {
         var valor =  contexto.Categorias.Find(categoria.CategoriaId);

        if(valor != null)
        {
            if(valor.EsVisible == true)
            return contexto.Categorias
            .Where(p => p.CategoriaId == valor.CategoriaId)
            .AsNoTracking()
            .SingleOrDefault();
        else
            return null;
        }
        
        return null;
    }

    public List<Categorias> GetList(Expression<Func<Categorias, bool>> criterio)
    {
        return contexto.Categorias
        .AsNoTracking()
        .Where(criterio)
        .ToList();
    }
}