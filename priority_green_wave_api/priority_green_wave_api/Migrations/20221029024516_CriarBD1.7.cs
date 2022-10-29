using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace priority_green_wave_api.Migrations
{
    public partial class CriarBD17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_semaforo_localizacao_localizacaoId",
                table: "semaforo");

            migrationBuilder.DropIndex(
                name: "IX_semaforo_localizacaoId",
                table: "semaforo");

            migrationBuilder.DropColumn(
                name: "localizacaoId",
                table: "semaforo");

            migrationBuilder.RenameColumn(
                name: "AreaID",
                table: "localizacao",
                newName: "Area");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "semaforo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "catadioptrico",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "localizacaoCatadioptrico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLocalizacao = table.Column<int>(type: "int", nullable: false),
                    IdCatadioptrico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localizacaoCatadioptrico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "localizacaoSemaforo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLocalizacao = table.Column<int>(type: "int", nullable: false),
                    IdSemaforo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localizacaoSemaforo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "localizacaoCatadioptrico");

            migrationBuilder.DropTable(
                name: "localizacaoSemaforo");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "semaforo");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "catadioptrico");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "localizacao",
                newName: "AreaID");

            migrationBuilder.AddColumn<int>(
                name: "localizacaoId",
                table: "semaforo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_semaforo_localizacaoId",
                table: "semaforo",
                column: "localizacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_semaforo_localizacao_localizacaoId",
                table: "semaforo",
                column: "localizacaoId",
                principalTable: "localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
