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

namespace Healfi.Api.Application.Services
{
    public class ProdutosService : BaseCrudService<Produto>
    {
        public ProdutosService(HealfiContext context) : base(context)
        {
        }

        public Task<List<ProdutoViewModel>> ObterTodas(string search, Guid? categoriaId, Guid? produtorId)
        {
            var query = _context.Produtos.AsNoTracking()
                .OrderBy(c => c.Nome)
                .Select(c => new ProdutoViewModel()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    ProdutorId = c.ProdutorId,
                    CategoriaId = c.CategoriaId,
                    DescricaoBreve = c.DescricaoBreve,
                    DescricaoCompleta = c.DescricaoCompleta,
                    EmFalta = c.EmFalta,
                    LinkFoto = c.LinkFoto,
                    ValorUnitario = c.ValorUnitario
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
            
            if (categoriaId.HasValue)
            {
                query = query.Where(c => c.CategoriaId == categoriaId);
            }

            return query.ToListAsync();
        }

        public Task<Produto> ObterPorId(Guid id)
        {
            return _context.Produtos
                .Include(c => c.Categoria)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        
        public Task<ProdutoViewModel> ObterPorIdView(Guid id)
        {
            return  _context.Produtos
                .Select(c => new ProdutoViewModel()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    ProdutorId = c.ProdutorId,
                    CategoriaId = c.CategoriaId,
                    DescricaoBreve = c.DescricaoBreve,
                    DescricaoCompleta = c.DescricaoCompleta,
                    EmFalta = c.EmFalta,
                    LinkFoto = c.LinkFoto,
                    ValorUnitario = c.ValorUnitario
                })
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<(bool result, object res)> Create(CreateProdutoCommand command)
        {
            if (!await _context.Categorias.AnyAsync(c => c.Id == command.CategoriaId))
            {
                return (false, "Categoria não existe");
            }

            var categoria = await _context.Categorias.FindAsync(command.CategoriaId).AsTask();
            
            var produto = new Produto()
            {
                Id = command.Id,
                Nome = command.Nome,
                CategoriaId = command.CategoriaId,
                ProdutorId = categoria.ProdutorId,
                LinkFoto = command.LinkFoto,
                ValorUnitario = command.ValorUnitario,
                EmFalta = command.EmFalta,
                DescricaoBreve = command.DescricaoBreve,
                DescricaoCompleta = command.DescricaoCompleta
            };

            _context.Produtos.Adicionar(produto);

            return (true, new ProdutoViewModel()
            {
                Id = produto.Id,
                Nome = produto.Nome,
                ProdutorId = produto.ProdutorId,
                CategoriaId = produto.CategoriaId,
                DescricaoBreve = produto.DescricaoBreve,
                DescricaoCompleta = produto.DescricaoCompleta,
                EmFalta = produto.EmFalta,
                LinkFoto = produto.LinkFoto,
                ValorUnitario = produto.ValorUnitario
            });
        }

        public async Task<(bool result, object res)> Update(UpdateProdutoCommand command)
        {
            var produto = await ObterPorId(command.Id);
            
            if (produto == null)
            {
                return (false, "Produto não existe");
            }

            produto.Nome = command.Nome;
            produto.LinkFoto = command.LinkFoto;
            produto.ValorUnitario = command.ValorUnitario;
            produto.EmFalta = command.EmFalta;
            produto.DescricaoBreve = command.DescricaoBreve;
            produto.DescricaoCompleta = command.DescricaoCompleta;
            
            _context.Produtos.Atualizar(produto);

            return (true, await ObterPorIdView(command.Id));
        }
        
        public async Task<(bool result, object res)> Delete(Guid id)
        {
            var produto = await ObterPorId(id);

            if (produto == null)
            {
                return (false, "Produto não existe");
            }
            
            _context.Produtos.Remover(produto);

            return (true, null);
        }
    }
}
