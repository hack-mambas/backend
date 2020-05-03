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
    public class ProdutoresService : BaseCrudService<Produtor>
    {
        public ProdutoresService(HealfiContext context) : base(context)
        {
        }

        public Task<List<Produtor>> ObterTodas(Guid? cidadeId, Guid? categoriaId, string search)
        {
            var query = _context.Produtores.Query()
                .Include(c => c.Endereco)
                .ThenInclude(c => c.Endereco)
                .ThenInclude(c => c.Cidade)
                .Include(c => c.Usuario)
                .ThenInclude(c => c.Conquistas)
                .ThenInclude(c => c.Conquista)
                .Include(c => c.CidadesAtendidas)
                .ThenInclude(c => c.Cidade)
                .Include(c => c.EspecialidadesAtendidas)
                .ThenInclude(c => c.Especialidade)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Usuario.Nome.ToUpper().Contains(search.ToUpper().Trim()) || c.NomeEmpresa.ToUpper().Contains(search.ToUpper().Trim()));
            }

            if (cidadeId.HasValue)
            {
                query = query.Where(c => c.CidadesAtendidas.Any(cid => cid.Id == cidadeId));
            }

            if (categoriaId.HasValue)
            {
                query = query.Where(c => c.Categorias.Any(cid => cid.Id == categoriaId));
            }

            return query.ToListAsync();
        }


        public async Task<ProdutorViewModel> ObterPorId(Guid id)
        {
            var produtor = await _context.Produtores
                .Include(c => c.Endereco)
                .ThenInclude(c => c.Endereco)
                .Include(c => c.Usuario)
                .ThenInclude(c => c.Conquistas)
                .ThenInclude(c => c.Conquista)
                .Include(c => c.CidadesAtendidas)
                .ThenInclude(c => c.Cidade)
                .Include(c => c.EspecialidadesAtendidas)
                .ThenInclude(c => c.Especialidade)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (produtor == null)
            {
                return null;
            }

            return new ProdutorViewModel()
            {
                Id = produtor.Id,
                Conquistas = produtor.Usuario.Conquistas?.Select(c => c.Conquista).ToList(),
                Email = produtor.Usuario.Email,
                Endereco = produtor.Endereco.Endereco,
                Genero = produtor.Usuario.Genero,
                Nome = produtor.Usuario.Nome,
                Telefone = produtor.Usuario.PhoneNumber,
                CadastroCompleto = produtor.CadastroCompleto,
                CidadesAtendidas = produtor.CidadesAtendidas?.Select(c => c.Cidade).ToList(),
                EspecialidadesAtendidas = produtor.EspecialidadesAtendidas?.Select(c => c.Especialidade).ToList(),
                DataNascimento = produtor.Usuario.DataNascimento,
                GoogleId = produtor.Usuario.GoogleId,
                InfosPessoais = produtor.InfosPessoais,
                LinkFotoPerfil = produtor.Usuario.LinkFotoPerfil,
                NomeEmpresa = produtor.NomeEmpresa,
                TutorialRalizado = produtor.Usuario.TutorialRalizado,
                LinkFotoCapa = produtor.LinkFotoCapa,
                ValorMinimoCompra = produtor.ValorMinimoCompra,
                LinkGoogleMeuNegocio = produtor.LinkGoogleMeuNegocio
            };
        }

        public async Task<ProdutorViewModel> AtualizarProdutor(AtualizarProdutorCommand command)
        {
            var produtor = await _context.Produtores.FindAsync(command.Id);
            if (produtor == null)
            {
                return null;
            }
            var usuario = await _context.Users.FindAsync(produtor.UsuarioId);

            produtor.NomeEmpresa = command.NomeEmpresa;
            produtor.InfosPessoais = produtor.InfosPessoais;
            produtor.LinkGoogleMeuNegocio = produtor.LinkGoogleMeuNegocio;
            produtor.LinkFotoCapa = produtor.LinkFotoCapa;
            produtor.CadastroCompleto = produtor.CadastroCompleto;
            produtor.ValorMinimoCompra = produtor.ValorMinimoCompra;
            produtor.Endereco ??= new EnderecoProdutor()
            {
                Id = Guid.NewGuid(),
                ProdutorId = produtor.Id,
                Endereco = new Endereco()
                {
                    Id = new Guid()
                }
            };
            produtor.Endereco.Endereco.Logradouro = command.Logradouro;
            produtor.Endereco.Endereco.Numero = command.Numero;
            produtor.Endereco.Endereco.CidadeId = command.CidadeId;

            produtor.Endereco.Endereco.Cep = command.Cep;

            usuario.Nome = command.Nome;
            usuario.LinkFotoPerfil = command.LinkFotoPerfil;
            usuario.DataNascimento = command.DataNascimento;
            usuario.Genero = command.Genero;
            usuario.GoogleId = command.GoogleId;
            usuario.TutorialRalizado = command.TutorialRalizado;

            _context.Produtores.Update(produtor);
            _context.Users.Update(usuario);

            return await ObterPorId(command.Id);
        }
    }
}
