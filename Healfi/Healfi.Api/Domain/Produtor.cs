using System;
using System.Collections.Generic;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class Produtor : BaseEntity
    {
        public string NomeEmpresa { get; set; }
        public string InfosPessoais { get; set; }
        public string LinkGoogleMeuNegocio { get; set; }
        public string LinkFotoCapa { get; set; }
        public bool CadastroCompleto { get; set; }
        public decimal? ValorMinimoCompra { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid? EnderecoId { get; set; }
        
        public Usuario Usuario { get; set; }
        public EnderecoProdutor Endereco { get; set; }
        
        public ICollection<CidadeAtendida> CidadesAtendidas { get; set; } 
        public ICollection<EspecialidadeAtendida> EspecialidadesAtendidas { get; set; }
        public ICollection<Categoria> Categorias { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}