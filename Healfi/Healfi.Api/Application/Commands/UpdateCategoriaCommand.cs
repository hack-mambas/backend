using System;
using System.ComponentModel.DataAnnotations;
using Healfi.Api.Domain.Enumerators;

namespace Healfi.Api.Application.Commands
{
    public class UpdateCategoriaCommand
    {
        public Guid Id { get; internal set; }
        public string Nome { get; set; }
        public int Ordem { get; set; } = 0;
        
        public UpdateCategoriaCommand AtribuirId(Guid id)
        {
            this.Id = id;
            return this;
        }
    }
}