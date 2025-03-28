﻿using System;
using System.Collections.Generic;
using Healfi.Api.Domain.Enumerators;
using Microsoft.AspNetCore.Identity;

namespace Healfi.Api.Domain
{
    public class Usuario : IdentityUser<Guid>
    {
        public string Nome { get; set; }
        public string LinkFotoPerfil { get; set; }
        public DateTime? DataNascimento { get; set; }
        public GeneroEnum? Genero { get; set; }
        public string GoogleId { get; set; }
        public bool TutorialRalizado { get; set; }

        public TipoVinculoEnum Tipo { get; set; }
        public Produtor Produtor { get; set; }
        public Consumidor Consumidor { get; set; }
        
        public ICollection<UsuarioConquista> Conquistas { get; set; }
    }
}