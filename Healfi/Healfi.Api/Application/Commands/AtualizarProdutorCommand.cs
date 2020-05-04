using Healfi.Api.Domain.Enumerators;
using System;

namespace Healfi.Api.Application.Commands
{
    public class AtualizarProdutorCommand
    {
        public Guid Id { get; internal set; }
        public string Nome { get; set; }
        public string LinkFotoPerfil { get; set; }
        public DateTime? DataNascimento { get; set; }
        public GeneroEnum? Genero { get; set; }
        public string GoogleId { get; set; }
        public bool TutorialRalizado { get; set; }

        public string NomeEmpresa { get; set; }
        public string InfosPessoais { get; set; }
        public string LinkGoogleMeuNegocio { get; set; }
        public string LinkFotoCapa { get; set; }
        public bool CadastroCompleto { get; set; }
        public decimal? ValorMinimoCompra { get; set; }

        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public Guid? CidadeId { get; set; }
        public string Cep { get; set; }

        public AtualizarProdutorCommand AtribuirId(Guid id)
        {
            this.Id = id;
            return this;
        }
    }
}
