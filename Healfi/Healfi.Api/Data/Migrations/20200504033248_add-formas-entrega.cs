using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Healfi.Api.Data.Migrations
{
    public partial class addformasentrega : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormaEntrega",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Cor = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaEntrega", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormaEntregaAtendida",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProdutorId = table.Column<Guid>(nullable: false),
                    FormaEntregaId = table.Column<Guid>(nullable: false),
                    Observacoes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaEntregaAtendida", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormaEntregaAtendida_FormaEntrega_FormaEntregaId",
                        column: x => x.FormaEntregaId,
                        principalTable: "FormaEntrega",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormaEntregaAtendida_Produtor_ProdutorId",
                        column: x => x.ProdutorId,
                        principalTable: "Produtor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormaEntregaAtendida_FormaEntregaId",
                table: "FormaEntregaAtendida",
                column: "FormaEntregaId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaEntregaAtendida_ProdutorId",
                table: "FormaEntregaAtendida",
                column: "ProdutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormaEntregaAtendida");

            migrationBuilder.DropTable(
                name: "FormaEntrega");
        }
    }
}
