using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace priority_green_wave_api.Migrations
{
    public partial class CriarBD15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_catadioptrico_localizacao_localizacaoId",
                table: "catadioptrico");

            migrationBuilder.DropForeignKey(
                name: "FK_veiculo_usuario_UsuarioId",
                table: "veiculo");

            migrationBuilder.DropIndex(
                name: "IX_veiculo_UsuarioId",
                table: "veiculo");

            migrationBuilder.DropIndex(
                name: "IX_catadioptrico_localizacaoId",
                table: "catadioptrico");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "veiculo");

            migrationBuilder.DropColumn(
                name: "localizacaoId",
                table: "catadioptrico");

            migrationBuilder.CreateTable(
                name: "veiculoUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdVeiculo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veiculoUsuario", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "veiculoUsuario");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "veiculo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "localizacaoId",
                table: "catadioptrico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_veiculo_UsuarioId",
                table: "veiculo",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_catadioptrico_localizacaoId",
                table: "catadioptrico",
                column: "localizacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_catadioptrico_localizacao_localizacaoId",
                table: "catadioptrico",
                column: "localizacaoId",
                principalTable: "localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_veiculo_usuario_UsuarioId",
                table: "veiculo",
                column: "UsuarioId",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
