using System;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class EnderecoProdutor : BaseEntity
    {
        public Guid ProdutorId { get; set; }
        public Guid EnderecoId { get; set; }

        public Produtor Produtor { get; set; }
        public Endereco Endereco { get; set; }
    }
}