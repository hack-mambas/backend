using System;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class FormaEntrega : BaseEntity
    {
        public string Nome { get; set; }
        public string Cor { get; set; }
    }
}