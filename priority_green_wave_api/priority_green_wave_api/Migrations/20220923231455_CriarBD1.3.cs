using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace priority_green_wave_api.Migrations
{
    public partial class CriarBD13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_localizacao_catadioptrico_catadioptricoId",
                table: "localizacao");

            migrationBuilder.DropForeignKey(
                name: "FK_localizacao_semaforo_semaforoId",
                table: "localizacao");

            migrationBuilder.DropForeignKey(
                name: "FK_semaforo_grupoSemaforo_GrupoSemaforoId",
                table: "semaforo");

            migrationBuilder.DropTable(
                name: "grupoSemaforo");

            migrationBuilder.DropIndex(
                name: "IX_semaforo_GrupoSemaforoId",
                table: "semaforo");

            migrationBuilder.DropIndex(
                name: "IX_localizacao_catadioptricoId",
                table: "localizacao");

            migrationBuilder.DropIndex(
                name: "IX_localizacao_semaforoId",
                table: "localizacao");

            migrationBuilder.DropColumn(
                name: "GrupoSemaforoId",
                table: "semaforo");

            migrationBuilder.DropColumn(
                name: "Orientacao",
                table: "semaforo");

            migrationBuilder.DropColumn(
                name: "catadioptricoId",
                table: "localizacao");

            migrationBuilder.RenameColumn(
                name: "Sentido",
                table: "semaforo",
                newName: "localizacaoId");

            migrationBuilder.RenameColumn(
                name: "semaforoId",
                table: "localizacao",
                newName: "AreaID");

            migrationBuilder.AddColumn<int>(
                name: "localizacaoId",
                table: "catadioptrico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_area", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_semaforo_localizacaoId",
                table: "semaforo",
                column: "localizacaoId");

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
                name: "FK_semaforo_localizacao_localizacaoId",
                table: "semaforo",
                column: "localizacaoId",
                principalTable: "localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_catadioptrico_localizacao_localizacaoId",
                table: "catadioptrico");

            migrationBuilder.DropForeignKey(
                name: "FK_semaforo_localizacao_localizacaoId",
                table: "semaforo");

            migrationBuilder.DropTable(
                name: "area");

            migrationBuilder.DropIndex(
                name: "IX_semaforo_localizacaoId",
                table: "semaforo");

            migrationBuilder.DropIndex(
                name: "IX_catadioptrico_localizacaoId",
                table: "catadioptrico");

            migrationBuilder.DropColumn(
                name: "localizacaoId",
                table: "catadioptrico");

            migrationBuilder.RenameColumn(
                name: "localizacaoId",
                table: "semaforo",
                newName: "Sentido");

            migrationBuilder.RenameColumn(
                name: "AreaID",
                table: "localizacao",
                newName: "semaforoId");

            migrationBuilder.AddColumn<int>(
                name: "GrupoSemaforoId",
                table: "semaforo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Orientacao",
                table: "semaforo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "catadioptricoId",
                table: "localizacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "grupoSemaforo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupoSemaforo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_semaforo_GrupoSemaforoId",
                table: "semaforo",
                column: "GrupoSemaforoId");

            migrationBuilder.CreateIndex(
                name: "IX_localizacao_catadioptricoId",
                table: "localizacao",
                column: "catadioptricoId");

            migrationBuilder.CreateIndex(
                name: "IX_localizacao_semaforoId",
                table: "localizacao",
                column: "semaforoId");

            migrationBuilder.AddForeignKey(
                name: "FK_localizacao_catadioptrico_catadioptricoId",
                table: "localizacao",
                column: "catadioptricoId",
                principalTable: "catadioptrico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_localizacao_semaforo_semaforoId",
                table: "localizacao",
                column: "semaforoId",
                principalTable: "semaforo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_semaforo_grupoSemaforo_GrupoSemaforoId",
                table: "semaforo",
                column: "GrupoSemaforoId",
                principalTable: "grupoSemaforo",
                principalColumn: "Id");
        }
    }
}
