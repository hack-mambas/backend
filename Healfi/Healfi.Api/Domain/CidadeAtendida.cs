using System;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class CidadeAtendida : BaseEntity
    {
        public Guid ProdutorId { get; set; }
        public Guid CidadeId { get; set; }

        public Cidade Cidade { get; set; }
        public Produtor Produtor { get; set; }
    }
}