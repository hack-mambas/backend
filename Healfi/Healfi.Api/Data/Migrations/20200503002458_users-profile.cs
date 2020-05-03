using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Healfi.Api.Data.Migrations
{
    public partial class usersprofile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cep",
                table: "Produtor");

            migrationBuilder.DropColumn(
                name: "CidadeId",
                table: "Produtor");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Produtor");

            migrationBuilder.DropColumn(
                name: "EnderecoNumero",
                table: "Produtor");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Usuario",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "EnderecoId",
                table: "Produtor",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CategoriaPadrao",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaPadrao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    EstadoSigla = table.Column<string>(nullable: true),
                    EstadoNome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conquista",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Cor = table.Column<string>(nullable: true),
                    CondicaoConquista = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conquista", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Especialidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Cor = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especialidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Cor = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProdutorId = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Ordem = table.Column<int>(nullable: false),
                    CategoriaPadraoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categoria_CategoriaPadrao_CategoriaPadraoId",
                        column: x => x.CategoriaPadraoId,
                        principalTable: "CategoriaPadrao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categoria_Produtor_ProdutorId",
                        column: x => x.ProdutorId,
                        principalTable: "Produtor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CidadeAtendida",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProdutorId = table.Column<Guid>(nullable: false),
                    CidadeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CidadeAtendida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CidadeAtendida_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CidadeAtendida_Produtor_ProdutorId",
                        column: x => x.ProdutorId,
                        principalTable: "Produtor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consumidores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    CidadePadraoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumidores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumidores_Cidade_CidadePadraoId",
                        column: x => x.CidadePadraoId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Consumidores_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Logradouro = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    CidadeId = table.Column<Guid>(nullable: true),
                    Cep = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Cidade_CidadeId",
                        column: x => x.CidadeId,
                        principalTable: "Cidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioConquista",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataConquista = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false),
                    ConquistaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioConquista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioConquista_Conquista_ConquistaId",
                        column: x => x.ConquistaId,
                        principalTable: "Conquista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioConquista_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EspecialidadeAtendida",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProdutorId = table.Column<Guid>(nullable: false),
                    EspecialidadeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EspecialidadeAtendida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EspecialidadeAtendida_Especialidade_EspecialidadeId",
                        column: x => x.EspecialidadeId,
                        principalTable: "Especialidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EspecialidadeAtendida_Produtor_ProdutorId",
                        column: x => x.ProdutorId,
                        principalTable: "Produtor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    LinkFoto = table.Column<string>(nullable: true),
                    DescricaoBreve = table.Column<string>(nullable: true),
                    DescricaoCompleta = table.Column<string>(nullable: true),
                    ValorUnitario = table.Column<decimal>(nullable: false),
                    EmFalta = table.Column<bool>(nullable: false),
                    CategoriaId = table.Column<Guid>(nullable: false),
                    ProdutorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produto_Produtor_ProdutorId",
                        column: x => x.ProdutorId,
                        principalTable: "Produtor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnderecoConsumidor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConsumidorId = table.Column<Guid>(nullable: false),
                    EnderecoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecoConsumidor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnderecoConsumidor_Consumidores_ConsumidorId",
                        column: x => x.ConsumidorId,
                        principalTable: "Consumidores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnderecoConsumidor_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnderecoProdutor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProdutorId = table.Column<Guid>(nullable: false),
                    EnderecoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecoProdutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnderecoProdutor_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnderecoProdutor_Produtor_ProdutorId",
                        column: x => x.ProdutorId,
                        principalTable: "Produtor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtor_EnderecoId",
                table: "Produtor",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_CategoriaPadraoId",
                table: "Categoria",
                column: "CategoriaPadraoId");

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_ProdutorId",
                table: "Categoria",
                column: "ProdutorId");

            migrationBuilder.CreateIndex(
                name: "IX_CidadeAtendida_CidadeId",
                table: "CidadeAtendida",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_CidadeAtendida_ProdutorId",
                table: "CidadeAtendida",
                column: "ProdutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumidores_CidadePadraoId",
                table: "Consumidores",
                column: "CidadePadraoId");

            migrationBuilder.CreateIndex(
                name: "IX_Consumidores_UsuarioId",
                table: "Consumidores",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_CidadeId",
                table: "Endereco",
                column: "CidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoConsumidor_ConsumidorId",
                table: "EnderecoConsumidor",
                column: "ConsumidorId");

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoConsumidor_EnderecoId",
                table: "EnderecoConsumidor",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoProdutor_EnderecoId",
                table: "EnderecoProdutor",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnderecoProdutor_ProdutorId",
                table: "EnderecoProdutor",
                column: "ProdutorId");

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadeAtendida_EspecialidadeId",
                table: "EspecialidadeAtendida",
                column: "EspecialidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_EspecialidadeAtendida_ProdutorId",
                table: "EspecialidadeAtendida",
                column: "ProdutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_ProdutorId",
                table: "Produto",
                column: "ProdutorId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioConquista_ConquistaId",
                table: "UsuarioConquista",
                column: "ConquistaId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioConquista_UsuarioId",
                table: "UsuarioConquista",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtor_EnderecoProdutor_EnderecoId",
                table: "Produtor",
                column: "EnderecoId",
                principalTable: "EnderecoProdutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtor_EnderecoProdutor_EnderecoId",
                table: "Produtor");

            migrationBuilder.DropTable(
                name: "CidadeAtendida");

            migrationBuilder.DropTable(
                name: "EnderecoConsumidor");

            migrationBuilder.DropTable(
                name: "EnderecoProdutor");

            migrationBuilder.DropTable(
                name: "EspecialidadeAtendida");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "UsuarioConquista");

            migrationBuilder.DropTable(
                name: "Consumidores");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "Especialidade");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Conquista");

            migrationBuilder.DropTable(
                name: "Cidade");

            migrationBuilder.DropTable(
                name: "CategoriaPadrao");

            migrationBuilder.DropIndex(
                name: "IX_Produtor_EnderecoId",
                table: "Produtor");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Produtor");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "Usuario",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "Produtor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CidadeId",
                table: "Produtor",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Produtor",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnderecoNumero",
                table: "Produtor",
                type: "text",
                nullable: true);
        }
    }
}
