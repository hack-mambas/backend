using System;
using System.Collections.Generic;
using Healfi.Api.Domain;

namespace Healfi.Api.Application.Common
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }
        public Guid ProdutorId { get; set; }
        public string Nome { get; set; }
        public int Ordem { get; set; }
        public Guid? CategoriaPadraoId { get; set; }
        
        public IEnumerable<ProdutoViewModel> Produtos { get; set; }
    }
}