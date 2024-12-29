using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MinhasFinancas.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartaoCredito",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Ativa = table.Column<bool>(type: "INTEGER", nullable: false),
                    Paga = table.Column<bool>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    Banco = table.Column<string>(type: "TEXT", nullable: false),
                    Documento = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartaoCredito", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Ativa = table.Column<bool>(type: "INTEGER", nullable: false),
                    TipoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tipo = table.Column<int>(type: "INTEGER", nullable: false),
                    Ativa = table.Column<bool>(type: "INTEGER", nullable: false),
                    Paga = table.Column<bool>(type: "INTEGER", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    Banco = table.Column<string>(type: "TEXT", nullable: false),
                    Documento = table.Column<string>(type: "TEXT", nullable: false),
                    Saldo = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Saldo",
                columns: table => new
                {
                    CartaoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saldo", x => new { x.CartaoId, x.Data });
                    table.ForeignKey(
                        name: "FK_Saldo_CartaoCredito_CartaoId",
                        column: x => x.CartaoId,
                        principalTable: "CartaoCredito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Financas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContaId = table.Column<Guid>(type: "TEXT", nullable: true),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Paga = table.Column<bool>(type: "INTEGER", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", nullable: false),
                    TipoId = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoriaId = table.Column<string>(type: "TEXT", nullable: false),
                    CartaoCreditoId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financas_CartaoCredito_CartaoCreditoId",
                        column: x => x.CartaoCreditoId,
                        principalTable: "CartaoCredito",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Financas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Financas_Contas_ContaId",
                        column: x => x.ContaId,
                        principalTable: "Contas",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Ativa", "Nome", "TipoId" },
                values: new object[,]
                {
                    { "compras", false, "Compras", 1 },
                    { "lanches", false, "Lanches", 1 },
                    { "mercado", false, "Mercado", 1 },
                    { "salario", false, "Salário", 0 }
                });

            migrationBuilder.InsertData(
                table: "Financas",
                columns: new[] { "Id", "CartaoCreditoId", "CategoriaId", "ContaId", "Data", "Descricao", "Paga", "TipoId", "Valor" },
                values: new object[,]
                {
                    { new Guid("6cd6a787-cc87-4c17-8c75-b18ebc4bf901"), null, "salario", null, new DateTime(2024, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salário", false, 0, 3000.00m },
                    { new Guid("6cd6a787-cc87-4c17-8c75-b18ebc4bf902"), null, "mercado", null, new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Atacadão", false, 1, 30.00m },
                    { new Guid("6cd6a787-cc87-4c17-8c75-b18ebc4bf903"), null, "lanches", null, new DateTime(2024, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mc Donalds", false, 1, 45.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Financas_CartaoCreditoId",
                table: "Financas",
                column: "CartaoCreditoId");

            migrationBuilder.CreateIndex(
                name: "IX_Financas_CategoriaId",
                table: "Financas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Financas_ContaId",
                table: "Financas",
                column: "ContaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Financas");

            migrationBuilder.DropTable(
                name: "Saldo");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Contas");

            migrationBuilder.DropTable(
                name: "CartaoCredito");
        }
    }
}
