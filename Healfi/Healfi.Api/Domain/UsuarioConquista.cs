using System;
using System.Text.Json.Serialization;
using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class UsuarioConquista : BaseEntity
    {
        public DateTime DataConquista { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid ConquistaId { get; set; }
        
        
        [JsonIgnore]
        public Usuario Usuario { get; set; }
        public Conquista Conquista { get; set; }
    }
}