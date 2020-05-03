using System;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class Cidade : BaseEntity
    {
        public string Nome { get; set; }
        public string EstadoSigla { get; set; }
        public string EstadoNome { get; set; }
    }
}