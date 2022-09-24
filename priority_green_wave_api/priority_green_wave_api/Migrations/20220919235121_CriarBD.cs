using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace priority_green_wave_api.Migrations
{
    public partial class CriarBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "catadioptricoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_catadioptricoModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "grupoSemaforoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupoSemaforoModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuarioModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotoristaEmergencia = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarioModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "semaforoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orientacao = table.Column<int>(type: "int", nullable: false),
                    Sentido = table.Column<int>(type: "int", nullable: false),
                    GrupoSemaforoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_semaforoModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_semaforoModel_grupoSemaforoModel_GrupoSemaforoId",
                        column: x => x.GrupoSemaforoId,
                        principalTable: "grupoSemaforoModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "veiculoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fabricante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    TipoVeiculo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VeiculoEmergencia = table.Column<bool>(type: "bit", nullable: false),
                    EstadoEmergencia = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veiculoModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_veiculoModel_usuarioModel_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarioModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "localizacaoModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logradouro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Regional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistanciaSemaforo = table.Column<double>(type: "float", nullable: false),
                    semaforoId = table.Column<int>(type: "int", nullable: false),
                    catadioptricoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localizacaoModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_localizacaoModel_catadioptricoModel_catadioptricoId",
                        column: x => x.catadioptricoId,
                        principalTable: "catadioptricoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_localizacaoModel_semaforoModel_semaforoId",
                        column: x => x.semaforoId,
                        principalTable: "semaforoModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_localizacaoModel_catadioptricoId",
                table: "localizacaoModel",
                column: "catadioptricoId");

            migrationBuilder.CreateIndex(
                name: "IX_localizacaoModel_semaforoId",
                table: "localizacaoModel",
                column: "semaforoId");

            migrationBuilder.CreateIndex(
                name: "IX_semaforoModel_GrupoSemaforoId",
                table: "semaforoModel",
                column: "GrupoSemaforoId");

            migrationBuilder.CreateIndex(
                name: "IX_veiculoModel_UsuarioId",
                table: "veiculoModel",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "localizacaoModel");

            migrationBuilder.DropTable(
                name: "veiculoModel");

            migrationBuilder.DropTable(
                name: "catadioptricoModel");

            migrationBuilder.DropTable(
                name: "semaforoModel");

            migrationBuilder.DropTable(
                name: "usuarioModel");

            migrationBuilder.DropTable(
                name: "grupoSemaforoModel");
        }
    }
}
