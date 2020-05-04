using System;
using System.ComponentModel.DataAnnotations;
using Healfi.Api.Domain.Enumerators;

namespace Healfi.Api.Application.Commands
{
    public class UpdateProdutoCommand
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string DescricaoBreve { get; set; }
        public string DescricaoCompleta { get; set; }
        public decimal ValorUnitario { get; set; }
        public bool EmFalta { get; set; }
        public string LinkFoto { get; set; }
        
        public UpdateProdutoCommand AtribuirId(Guid id)
        {
            Id = id;
            return this;
        }
    }
}