using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Healfi.Api.Data.Migrations
{
    public partial class usersprofile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnderecoConsumidor_Consumidores_ConsumidorId",
                table: "EnderecoConsumidor");

            migrationBuilder.DropForeignKey(
                name: "FK_EnderecoConsumidor_Endereco_EnderecoId",
                table: "EnderecoConsumidor");

            migrationBuilder.DropForeignKey(
                name: "FK_EnderecoProdutor_Endereco_EnderecoId",
                table: "EnderecoProdutor");

            migrationBuilder.DropForeignKey(
                name: "FK_EnderecoProdutor_Produtor_ProdutorId",
                table: "EnderecoProdutor");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtor_EnderecoProdutor_EnderecoId",
                table: "Produtor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnderecoProdutor",
                table: "EnderecoProdutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnderecoConsumidor",
                table: "EnderecoConsumidor");

            migrationBuilder.RenameTable(
                name: "EnderecoProdutor",
                newName: "EnderecosProdutor");

            migrationBuilder.RenameTable(
                name: "EnderecoConsumidor",
                newName: "EnderecosConsumidor");

            migrationBuilder.RenameIndex(
                name: "IX_EnderecoProdutor_ProdutorId",
                table: "EnderecosProdutor",
                newName: "IX_EnderecosProdutor_ProdutorId");

            migrationBuilder.RenameIndex(
                name: "IX_EnderecoProdutor_EnderecoId",
                table: "EnderecosProdutor",
                newName: "IX_EnderecosProdutor_EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_EnderecoConsumidor_EnderecoId",
                table: "EnderecosConsumidor",
                newName: "IX_EnderecosConsumidor_EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_EnderecoConsumidor_ConsumidorId",
                table: "EnderecosConsumidor",
                newName: "IX_EnderecosConsumidor_ConsumidorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnderecoId",
                table: "Produtor",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnderecosProdutor",
                table: "EnderecosProdutor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnderecosConsumidor",
                table: "EnderecosConsumidor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecosConsumidor_Consumidores_ConsumidorId",
                table: "EnderecosConsumidor",
                column: "ConsumidorId",
                principalTable: "Consumidores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecosConsumidor_Endereco_EnderecoId",
                table: "EnderecosConsumidor",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecosProdutor_Endereco_EnderecoId",
                table: "EnderecosProdutor",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecosProdutor_Produtor_ProdutorId",
                table: "EnderecosProdutor",
                column: "ProdutorId",
                principalTable: "Produtor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtor_EnderecosProdutor_EnderecoId",
                table: "Produtor",
                column: "EnderecoId",
                principalTable: "EnderecosProdutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnderecosConsumidor_Consumidores_ConsumidorId",
                table: "EnderecosConsumidor");

            migrationBuilder.DropForeignKey(
                name: "FK_EnderecosConsumidor_Endereco_EnderecoId",
                table: "EnderecosConsumidor");

            migrationBuilder.DropForeignKey(
                name: "FK_EnderecosProdutor_Endereco_EnderecoId",
                table: "EnderecosProdutor");

            migrationBuilder.DropForeignKey(
                name: "FK_EnderecosProdutor_Produtor_ProdutorId",
                table: "EnderecosProdutor");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtor_EnderecosProdutor_EnderecoId",
                table: "Produtor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnderecosProdutor",
                table: "EnderecosProdutor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnderecosConsumidor",
                table: "EnderecosConsumidor");

            migrationBuilder.RenameTable(
                name: "EnderecosProdutor",
                newName: "EnderecoProdutor");

            migrationBuilder.RenameTable(
                name: "EnderecosConsumidor",
                newName: "EnderecoConsumidor");

            migrationBuilder.RenameIndex(
                name: "IX_EnderecosProdutor_ProdutorId",
                table: "EnderecoProdutor",
                newName: "IX_EnderecoProdutor_ProdutorId");

            migrationBuilder.RenameIndex(
                name: "IX_EnderecosProdutor_EnderecoId",
                table: "EnderecoProdutor",
                newName: "IX_EnderecoProdutor_EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_EnderecosConsumidor_EnderecoId",
                table: "EnderecoConsumidor",
                newName: "IX_EnderecoConsumidor_EnderecoId");

            migrationBuilder.RenameIndex(
                name: "IX_EnderecosConsumidor_ConsumidorId",
                table: "EnderecoConsumidor",
                newName: "IX_EnderecoConsumidor_ConsumidorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnderecoId",
                table: "Produtor",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnderecoProdutor",
                table: "EnderecoProdutor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnderecoConsumidor",
                table: "EnderecoConsumidor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecoConsumidor_Consumidores_ConsumidorId",
                table: "EnderecoConsumidor",
                column: "ConsumidorId",
                principalTable: "Consumidores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecoConsumidor_Endereco_EnderecoId",
                table: "EnderecoConsumidor",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecoProdutor_Endereco_EnderecoId",
                table: "EnderecoProdutor",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnderecoProdutor_Produtor_ProdutorId",
                table: "EnderecoProdutor",
                column: "ProdutorId",
                principalTable: "Produtor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtor_EnderecoProdutor_EnderecoId",
                table: "Produtor",
                column: "EnderecoId",
                principalTable: "EnderecoProdutor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
