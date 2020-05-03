using Healfi.Api.Domain.Abstract;

namespace Healfi.Api.Domain
{
    public class Tag : BaseEntity
    {
        public string Nome { get; set; }
        public string Cor { get; set; }
    }
}