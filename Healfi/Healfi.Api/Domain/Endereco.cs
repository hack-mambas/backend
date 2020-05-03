using System;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class Endereco : BaseEntity
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public Guid? CidadeId { get; set; }
        public string Cep { get; set; }

        public Cidade Cidade { get; set; }
    }
}