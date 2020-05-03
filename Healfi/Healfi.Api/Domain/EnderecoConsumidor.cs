using System;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class EnderecoConsumidor : BaseEntity
    {
        public Guid ConsumidorId { get; set; }
        public Guid EnderecoId { get; set; }

        public Consumidor Consumidor { get; set; }
        public Endereco Endereco { get; set; }
    }
}