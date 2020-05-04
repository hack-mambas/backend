using System;
using System.ComponentModel.DataAnnotations;
using Healfi.Api.Domain.Enumerators;

namespace Healfi.Api.Application.Commands
{
    public class CreateCategoriaCommand
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Ordem { get; set; } = 0;
        public Guid ProdutorId { get; set; }
        public Guid? CategoriaPadraoId { get; set; }
        
        public CreateCategoriaCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}