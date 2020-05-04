using System;
using Healfi.Api.Domain.Abstract;
using Newtonsoft.Json;

namespace Healfi.Api.Domain
{
    public class FormaEntregaAtendida : BaseEntity
    {
        public Guid ProdutorId { get; set; }
        public Guid FormaEntregaId { get; set; }
        public string Observacoes { get; set; }

        public FormaEntrega FormaEntrega { get; set; }
        [JsonIgnore]
        public Produtor Produtor { get; set; }
    }
}