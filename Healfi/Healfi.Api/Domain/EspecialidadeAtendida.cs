using System;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class EspecialidadeAtendida : BaseEntity
    {
        public Guid ProdutorId { get; set; }
        public Guid EspecialidadeId { get; set; }

        public Especialidade Especialidade { get; set; }
        public Produtor Produtor { get; set; }
    }
}