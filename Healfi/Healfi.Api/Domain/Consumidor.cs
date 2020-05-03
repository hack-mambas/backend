using System;
using System.Collections.Generic;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class Consumidor : BaseEntity
    {
        public Guid UsuarioId { get; set; }
        public Guid? CidadePadraoId { get; set; }
        
        public Usuario Usuario { get; set; }
        public Cidade CidadePadrao { get; set; }
        public ICollection<EnderecoConsumidor> Enderecos { get; set; }
    }
}