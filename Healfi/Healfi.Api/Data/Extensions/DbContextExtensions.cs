using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Healfi.Api.Domain;
using Healfi.Api.Domain.Enumerators;
using Microsoft.EntityFrameworkCore;

namespace Healfi.Api.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static TEntity Atualizar<TEntity>(this DbSet<TEntity> dbSet, TEntity obj)
            where TEntity : class
        {
            dbSet.Update(obj).State = EntityState.Modified;

            return obj;
        }
        public static TEntity Adicionar<TEntity>(this DbSet<TEntity> dbSet, TEntity obj)
            where TEntity : class
        {
            dbSet.Add(obj);

            return obj;
        }
        public static void Remover<TEntity>(this DbSet<TEntity> dbSet, Guid id)
            where TEntity : class
        {
            var obj = dbSet.FindById(id);

            dbSet.Remove(obj);
        }

        public static void Remover<TEntity>(this DbSet<TEntity> dbSet, TEntity entity)
            where TEntity : class
        {
            dbSet.Remove(entity);
        }

        public static TEntity FindById<TEntity>(this DbSet<TEntity> dbSet, Guid id)
            where TEntity : class
        {
            return dbSet.Find(id);
        }

        public static IQueryable<TEntity> Query<TEntity>(this DbSet<TEntity> dbSet)
            where TEntity : class
        {
            return dbSet.AsNoTracking();
        }
        
        public static async Task SeedDefaultData(this HealfiContext context, CancellationToken token)
        {
            context.Cidades.Adicionar(new Cidade()
            {
                Id = Guid.NewGuid(),
                Nome = "Pato Branco",
                EstadoNome = "Paraná",
                EstadoSigla = "PR"
            });

            context.Conquistas.Adicionar(new Conquista()
            {
                Id = Guid.NewGuid(),
                Nome = "Conquista inicial",
                Cor = "#000",
                Tipo = TipoVinculoEnum.Ambos,
                CondicaoConquista = "Efetuar Cadastro"
            });
            
            context.Especialidades.Adicionar(new Especialidade()
            {
                Id = Guid.NewGuid(),
                Nome = "Orgânicos",
                Cor = "#15b37e"
            });
            
            context.Especialidades.Adicionar(new Especialidade()
            {
                Id = Guid.NewGuid(),
                Nome = "Laticínios",
                Cor = "#15b37e"
            });
            
            context.CategoriasPadrao.Adicionar(new CategoriaPadrao()
            {
                Id = Guid.NewGuid(),
                Nome = "Cestas"
            });
            
            context.FormasEntrega.AddRange(new List<FormaEntrega>()
            {
                new FormaEntrega()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Entrega expressa",
                    Cor = "#000"
                },
                new FormaEntrega()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Entrega normal",
                    Cor = "#000"
                },
                new FormaEntrega()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Entrega Presencial",
                    Cor = "#000"
                }
            });
            

            await context.SaveChangesAsync(token);
        }
    }
}