using System;
using System.Collections.Generic;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class Categoria : BaseEntity
    {
        public Guid ProdutorId { get; set; }
        public string Nome { get; set; }
        public int Ordem { get; set; }
        public Guid? CategoriaPadraoId { get; set; }
        
        // EF 
        public Produtor Produtor { get; set; }
        public CategoriaPadrao CategoriaPadrao { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}