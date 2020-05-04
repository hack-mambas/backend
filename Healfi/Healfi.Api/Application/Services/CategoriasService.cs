using Healfi.Api.Application.Commands;
using Healfi.Api.Application.Common;
using Healfi.Api.Application.Services.Abstracts;
using Healfi.Api.Data;
using Healfi.Api.Data.Extensions;
using Healfi.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace Healfi.Api.Application.Services
{
    public class CategoriasService : BaseCrudService<Categoria>
    {
        public CategoriasService(HealfiContext context) : base(context)
        {
        }

        public Task<List<CategoriaViewModel>> ObterTodas(string search, Guid? produtorId)
        {
            var query = _context.Categorias.AsNoTracking()
                .Include(c => c.CategoriaPadrao)
                .Include(c => c.Produtos)
                .OrderBy(c => c.Ordem)
                .ThenBy(c => c.Nome)
                .Select(c => new CategoriaViewModel()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Ordem = c.Ordem,
                    ProdutorId = c.ProdutorId,
                    CategoriaPadraoId = c.CategoriaPadraoId,
                    Produtos = c.Produtos.Select(p => new ProdutoViewModel()
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        CategoriaId = p.CategoriaId,
                        DescricaoBreve = p.DescricaoBreve,
                        DescricaoCompleta = p.DescricaoCompleta,
                        EmFalta = p.EmFalta,
                        LinkFoto = p.LinkFoto,
                        ProdutorId = p.ProdutorId,
                        ValorUnitario = p.ValorUnitario
                    })
                })
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Nome.ToUpper().Contains(search.ToUpper().Trim()));
            }

            if (produtorId.HasValue)
            {
                query = query.Where(c => c.ProdutorId == produtorId);
            }

            return query.ToListAsync();
        }

        public Task<Categoria> ObterPorId(Guid id)
        {
            return _context.Categorias
                .Include(c => c.CategoriaPadrao)
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        
        public Task<CategoriaViewModel> ObterPorIdView(Guid id)
        {
            return _context.Categorias
                .Include(c => c.CategoriaPadrao)
                .Include(c => c.Produtos)
                .Where(c => c.Id == id)
                .Select(c => new CategoriaViewModel()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Ordem = c.Ordem,
                    ProdutorId = c.ProdutorId,
                    CategoriaPadraoId = c.CategoriaPadraoId,
                    Produtos = c.Produtos.Select(p => new ProdutoViewModel()
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        CategoriaId = p.CategoriaId,
                        DescricaoBreve = p.DescricaoBreve,
                        DescricaoCompleta = p.DescricaoCompleta,
                        EmFalta = p.EmFalta,
                        LinkFoto = p.LinkFoto,
                        ProdutorId = p.ProdutorId,
                        ValorUnitario = p.ValorUnitario
                    })
                })
                .FirstOrDefaultAsync();
        }

        public async Task<(bool result, object res)> Create(CreateCategoriaCommand command)
        {
            if (!await _context.Produtores.AnyAsync(c => c.Id == command.ProdutorId))
            {
                return (false, "Produtor não existe");
            }
            
            var categoria = new Categoria()
            {
                Id = command.Id,
                Nome = command.Nome,
                Ordem = command.Ordem,
                ProdutorId = command.ProdutorId,
                CategoriaPadraoId = command.CategoriaPadraoId
            };

            _context.Categorias.Adicionar(categoria);
            
            return (true, new CategoriaViewModel
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
                Ordem = categoria.Ordem,
                ProdutorId = categoria.ProdutorId,
                CategoriaPadraoId = categoria.CategoriaPadraoId
            });
        }

        public async Task<(bool result, object res)> Update(UpdateCategoriaCommand command)
        {
            var categoria = await ObterPorId(command.Id);
            
            if (categoria == null)
            {
                return (false, "Categoria não existe");
            }

            categoria.Nome = command.Nome;
            categoria.Ordem = command.Ordem;
            
            _context.Categorias.Atualizar(categoria);

            return (true, await ObterPorIdView(command.Id));
        }
        
        public async Task<(bool result, object res)> Delete(Guid id)
        {
            var categoria = await ObterPorId(id);

            if (categoria == null)
            {
                return (false, "Categoria não existe");
            }
            
            _context.Categorias.Remover(categoria);

            return (true, null);
        }
    }
}
