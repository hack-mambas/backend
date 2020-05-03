using System;
using System.Collections.Generic;
using Healfi.Api.Domain;
using Healfi.Api.Domain.Enumerators;

namespace Healfi.Api.Application.Common
{
    public class ProdutorViewModel
    {
        public Guid Id { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string LinkFotoPerfil { get; set; }
        public DateTime? DataNascimento { get; set; }
        public GeneroEnum? Genero { get; set; }
        public string GoogleId { get; set; }
        public bool TutorialRalizado { get; set; }
        
        public string NomeEmpresa { get; set; }
        public string InfosPessoais { get; set; }
        public string LinkGoogleMeuNegocio { get; set; }
        public string LinkFotoCapa { get; set; }
        public bool CadastroCompleto { get; set; }
        public decimal? ValorMinimoCompra { get; set; }
        
        public Endereco Endereco { get; set; }
        
        public ICollection<Cidade> CidadesAtendidas { get; set; } 
        public ICollection<Especialidade> EspecialidadesAtendidas { get; set; }
        public ICollection<Conquista> Conquistas { get; set; }
    }
}