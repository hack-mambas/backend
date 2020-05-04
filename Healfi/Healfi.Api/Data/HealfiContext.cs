using System;
using Healfi.Api.Domain;
using Healfi.Api.Domain.Enumerators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Healfi.Api.Data
{
    public class HealfiContext : IdentityDbContext<Usuario, IdentityRole<Guid>, Guid>
    {
        public virtual DbSet<Produtor> Produtores { get; set;}
        public virtual DbSet<Consumidor> Consumidores { get; set;}
        public virtual DbSet<Endereco> Enderecos { get; set;}
        public virtual DbSet<Cidade> Cidades { get; set;}
        public virtual DbSet<Especialidade> Especialidades { get; set;}
        public virtual DbSet<FormaEntrega> FormasEntrega { get; set;}
        public virtual DbSet<FormaEntregaAtendida> FormasEntregaAtendidas { get; set;}
        public virtual DbSet<EspecialidadeAtendida> EspecialidadesAtendidas { get; set;}
        public virtual DbSet<Tag> Tags { get; set;}
        public virtual DbSet<CidadeAtendida> CidadeAtendidas { get; set;}
        public virtual DbSet<CategoriaPadrao> CategoriasPadrao { get; set;}
        public virtual DbSet<Categoria> Categorias { get; set;}
        public virtual DbSet<Produto> Produtos { get; set;}
        public virtual DbSet<Conquista> Conquistas { get; set;}
        public virtual DbSet<UsuarioConquista> UsuarioConquistas { get; set;}
        public virtual DbSet<EnderecoConsumidor> EnderecosConsumidor { get; set;}
        public virtual DbSet<EnderecoProdutor> EnderecosProdutor { get; set;}
        
        public HealfiContext(DbContextOptions<HealfiContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Usuario>().ToTable(nameof(Usuario));

            builder.Entity<Usuario>()
                .Property(t => t.Tipo)
                .HasConversion(new EnumToNumberConverter<TipoVinculoEnum, int>())
                .IsRequired();

            builder.Entity<Usuario>()
                .Property(c => c.DataNascimento);

            builder.Entity<Usuario>()
                .Property(c => c.Nome);

            builder.Entity<Usuario>()
                .Property(t => t.Genero)
                .HasConversion(new EnumToNumberConverter<GeneroEnum, int>());

            builder.Entity<Usuario>()
                .Property(t => t.LinkFotoPerfil)
                .HasColumnType("text");

            builder.Entity<Usuario>()
                .Property(t => t.GoogleId);

            builder.Entity<Usuario>()
                .Property(t => t.TutorialRalizado)
                .HasDefaultValue(false);

            builder.Entity<Usuario>()
                .HasOne(c => c.Produtor)
                .WithOne(c => c.Usuario)
                .HasForeignKey<Produtor>(c => c.UsuarioId);
            
            builder.Entity<Usuario>()
                .HasOne(c => c.Consumidor)
                .WithOne(c => c.Usuario)
                .HasForeignKey<Consumidor>(c => c.UsuarioId);

            builder.Entity<EnderecoConsumidor>()
                .HasKey(c => c.Id);

            builder.Entity<EnderecoConsumidor>()
                .HasOne(c => c.Endereco)
                .WithOne()
                .HasForeignKey<EnderecoConsumidor>(c => c.EnderecoId);
            
            builder.Entity<EnderecoConsumidor>()
                .HasOne(c => c.Consumidor)
                .WithMany(c => c.Enderecos)
                .HasForeignKey(c => c.ConsumidorId);

            builder.Entity<EnderecoProdutor>()
                .HasKey(c => c.Id);

            builder.Entity<EnderecoProdutor>()
                .HasOne(c => c.Endereco)
                .WithOne()
                .HasForeignKey<EnderecoProdutor>(c => c.EnderecoId);
            
            builder.Entity<EnderecoProdutor>()
                .HasOne(c => c.Produtor)
                .WithOne(c => c.Endereco)
                .HasForeignKey<EnderecoProdutor>(c => c.ProdutorId);
            
            builder.Entity<Especialidade>().ToTable(nameof(Especialidade));

            builder.Entity<Especialidade>()
                .HasKey(c => c.Id);

            builder.Entity<Especialidade>()
                .Property(c => c.Nome)
                .IsRequired();

            builder.Entity<Especialidade>()
                .Property(c => c.Cor)
                .IsRequired();
            
            builder.Entity<FormaEntrega>().ToTable(nameof(FormaEntrega));

            builder.Entity<FormaEntrega>()
                .HasKey(c => c.Id);

            builder.Entity<FormaEntrega>()
                .Property(c => c.Nome)
                .IsRequired();

            builder.Entity<FormaEntrega>()
                .Property(c => c.Cor)
                .IsRequired();

            builder.Entity<Tag>().ToTable(nameof(Tag));

            builder.Entity<Tag>()
                .HasKey(c => c.Id);

            builder.Entity<Tag>()
                .Property(c => c.Nome)
                .IsRequired();

            builder.Entity<Tag>()
                .Property(c => c.Cor)
                .IsRequired();

            builder.Entity<Cidade>().ToTable(nameof(Cidade));

            builder.Entity<Cidade>()
                .HasKey(c => c.Id);

            builder.Entity<Cidade>()
                .Property(c => c.Nome)
                .IsRequired();

            builder.Entity<Cidade>()
                .Property(c => c.EstadoSigla);

            builder.Entity<Cidade>()
                .Property(c => c.EstadoNome);

            builder.Entity<Endereco>().ToTable(nameof(Endereco));
            
            builder.Entity<Endereco>()
                .HasKey(c => c.Id);

            builder.Entity<Endereco>()
                .Property(c => c.Logradouro);

            builder.Entity<Endereco>()
                .Property(c => c.Numero);

            builder.Entity<Endereco>()
                .Property(c => c.Cep);

            builder.Entity<Endereco>()
                .HasOne(c => c.Cidade)
                .WithMany()
                .HasForeignKey(c => c.CidadeId);

            builder.Entity<Produtor>().ToTable(nameof(Produtor));
            
            builder.Entity<Produtor>()
                .HasKey(c => c.Id);

            builder.Entity<Produtor>()
                .Property(c => c.CadastroCompleto);

            builder.Entity<Produtor>()
                .Property(c => c.NomeEmpresa);

            builder.Entity<Produtor>()
                .Property(c => c.InfosPessoais);

            builder.Entity<Produtor>()
                .Property(c => c.LinkGoogleMeuNegocio);

            builder.Entity<Produtor>()
                .Property(c => c.LinkFotoCapa)
                .HasColumnType("text");

            builder.Entity<Produtor>()
                .Property(c => c.CadastroCompleto)
                .HasDefaultValue(false);

            builder.Entity<Produtor>()
                .Property(c => c.ValorMinimoCompra);

            builder.Entity<Produtor>()
                .HasOne(c => c.Endereco)
                .WithMany()
                .HasForeignKey(c => c.EnderecoId);

            builder.Entity<CidadeAtendida>().ToTable(nameof(CidadeAtendida));
            
            builder.Entity<CidadeAtendida>().HasKey(c => c.Id);

            builder.Entity<CidadeAtendida>()
                .HasOne(c => c.Cidade)
                .WithMany()
                .HasForeignKey(c => c.CidadeId);

            builder.Entity<CidadeAtendida>()
                .HasOne(c => c.Produtor)
                .WithMany(c => c.CidadesAtendidas)
                .HasForeignKey(c => c.ProdutorId);

            builder.Entity<EspecialidadeAtendida>().ToTable(nameof(EspecialidadeAtendida));
            
            builder.Entity<EspecialidadeAtendida>().HasKey(c => c.Id);

            builder.Entity<EspecialidadeAtendida>()
                .HasOne(c => c.Especialidade)
                .WithMany()
                .HasForeignKey(c => c.EspecialidadeId);

            builder.Entity<EspecialidadeAtendida>()
                .HasOne(c => c.Produtor)
                .WithMany(c => c.EspecialidadesAtendidas)
                .HasForeignKey(c => c.ProdutorId);
            
            builder.Entity<FormaEntregaAtendida>().ToTable(nameof(FormaEntregaAtendida));
            
            builder.Entity<FormaEntregaAtendida>().HasKey(c => c.Id);

            builder.Entity<FormaEntregaAtendida>().Property(c => c.Observacoes);

            builder.Entity<FormaEntregaAtendida>()
                .HasOne(c => c.FormaEntrega)
                .WithMany()
                .HasForeignKey(c => c.FormaEntregaId);

            builder.Entity<FormaEntregaAtendida>()
                .HasOne(c => c.Produtor)
                .WithMany(c => c.FormasEntrega)
                .HasForeignKey(c => c.ProdutorId);

            builder.Entity<CategoriaPadrao>().ToTable(nameof(CategoriaPadrao));

            builder.Entity<CategoriaPadrao>()
                .HasKey(c => c.Id);

            builder.Entity<CategoriaPadrao>()
                .Property(c => c.Nome)
                .IsRequired();
            
            builder.Entity<Categoria>().ToTable(nameof(Categoria));

            builder.Entity<Categoria>()
                .HasKey(c => c.Id);

            builder.Entity<Categoria>()
                .Property(c => c.Nome)
                .IsRequired();

            builder.Entity<Categoria>()
                .Property(c => c.Ordem);

            builder.Entity<Categoria>()
                .HasOne(c => c.Produtor)
                .WithMany(c => c.Categorias)
                .HasForeignKey(c => c.ProdutorId);

            builder.Entity<Categoria>()
                .HasOne(c => c.CategoriaPadrao)
                .WithMany()
                .HasForeignKey(c => c.CategoriaPadraoId);

            builder.Entity<Produto>().ToTable(nameof(Produto));
            
            builder.Entity<Produto>()
                .HasKey(c => c.Id);
            
            builder.Entity<Produto>()
                .Property(c => c.Nome);
            
            builder.Entity<Produto>()
                .Property(c => c.LinkFoto)
                .HasColumnType("text");
            
            builder.Entity<Produto>()
                .Property(c => c.DescricaoBreve);
            
            builder.Entity<Produto>()
                .Property(c => c.DescricaoCompleta);
            
            builder.Entity<Produto>()
                .Property(c => c.ValorUnitario);
            
            builder.Entity<Produto>()
                .Property(c => c.EmFalta);

            builder.Entity<Produto>()
                .HasOne(c => c.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(c => c.CategoriaId);

            builder.Entity<Produto>()
                .HasOne(c => c.Produtor)
                .WithMany(c => c.Produtos)
                .HasForeignKey(c => c.ProdutorId);

            builder.Entity<Conquista>().ToTable(nameof(Conquista));
            
            builder.Entity<Conquista>()
                .HasKey(c => c.Id);

            builder.Entity<Conquista>()
                .Property(c => c.Nome);
            
            builder.Entity<Conquista>()
                .Property(c => c.Cor);
            
            builder.Entity<Conquista>()
                .Property(c => c.CondicaoConquista);
            
            builder.Entity<UsuarioConquista>().ToTable(nameof(UsuarioConquista));
            
            builder.Entity<UsuarioConquista>()
                .HasKey(c => c.Id);

            builder.Entity<UsuarioConquista>()
                .HasOne(c => c.Usuario)
                .WithMany(c => c.Conquistas)
                .HasForeignKey(c => c.UsuarioId);

            builder.Entity<UsuarioConquista>()
                .HasOne(c => c.Conquista)
                .WithMany()
                .HasForeignKey(c => c.ConquistaId);

            builder.Entity<UsuarioConquista>()
                .Property(c => c.DataConquista);
        }
    }
}