using System;

namespace Healfi.Api.Application.Common
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string LinkFoto { get; set; }
        public string DescricaoBreve { get; set; }
        public string DescricaoCompleta { get; set; }
        public decimal ValorUnitario { get; set; }
        public bool EmFalta { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid ProdutorId { get; set; }
    }
}