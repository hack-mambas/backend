using System;
using System.Collections.Generic;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class Produto : BaseEntity
    {
        public string Nome { get; set; }
        public string LinkFoto { get; set; }
        public string DescricaoBreve { get; set; }
        public string DescricaoCompleta { get; set; }
        public decimal ValorUnitario { get; set; }
        public bool EmFalta { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid ProdutorId { get; set; }

        public Categoria Categoria { get; set; }
        public Produtor Produtor { get; set; }
    }
}