using System;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class Especialidade : BaseEntity
    {
        public string Nome { get; set; }
        public string Cor { get; set; }
    }
}