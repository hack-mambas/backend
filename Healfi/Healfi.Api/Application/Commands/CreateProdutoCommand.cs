using System;
using System.ComponentModel.DataAnnotations;
using Healfi.Api.Domain.Enumerators;

namespace Healfi.Api.Application.Commands
{
    public class CreateProdutoCommand
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string LinkFoto { get; set; }
        public string DescricaoBreve { get; set; }
        public string DescricaoCompleta { get; set; }
        public decimal ValorUnitario { get; set; }
        public bool EmFalta { get; set; }    
        public Guid CategoriaId { get; set; }
        
        public CreateProdutoCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}