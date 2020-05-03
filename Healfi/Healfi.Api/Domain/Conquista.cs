using Healfi.Api.Domain.Abstract;
using Healfi.Api.Domain.Enumerators;

namespace Healfi.Api.Domain
{
    public class Conquista : BaseEntity
    {
        public string Nome { get; set; }
        public string Cor { get; set; }
        public string CondicaoConquista { get; set; }
        public TipoVinculoEnum Tipo { get; set; }
    }
}