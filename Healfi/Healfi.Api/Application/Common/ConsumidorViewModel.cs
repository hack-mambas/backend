using System;
using System.Collections.Generic;
using Healfi.Api.Domain;
using Healfi.Api.Domain.Enumerators;

namespace Healfi.Api.Application.Common
{
    public class ConsumidorViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string LinkFotoPerfil { get; set; }
        public DateTime? DataNascimento { get; set; }
        public GeneroEnum? Genero { get; set; }
        public string GoogleId { get; set; }
        public bool TutorialRalizado { get; set; }

        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Conquista> Conquistas { get; set; }
    }
}