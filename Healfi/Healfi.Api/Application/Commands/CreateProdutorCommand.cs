using System;
using System.ComponentModel.DataAnnotations;
using Healfi.Api.Domain.Enumerators;

namespace Healfi.Api.Application.Commands
{
    public class CreateProdutorCommand
    {
        [Required]
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        public string NomeEmpresa { get; set; }
        public Guid? CidadeId { get; set; }

        public TipoVinculoEnum TipoVinculo => TipoVinculoEnum.Produtor;
    }
}