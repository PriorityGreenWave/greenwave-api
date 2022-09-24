using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace priority_green_wave_api.Migrations
{
    public partial class CriarBD11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_localizacaoModel_catadioptricoModel_catadioptricoId",
                table: "localizacaoModel");

            migrationBuilder.DropForeignKey(
                name: "FK_localizacaoModel_semaforoModel_semaforoId",
                table: "localizacaoModel");

            migrationBuilder.DropForeignKey(
                name: "FK_semaforoModel_grupoSemaforoModel_GrupoSemaforoId",
                table: "semaforoModel");

            migrationBuilder.DropForeignKey(
                name: "FK_veiculoModel_usuarioModel_UsuarioId",
                table: "veiculoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_veiculoModel",
                table: "veiculoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuarioModel",
                table: "usuarioModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_semaforoModel",
                table: "semaforoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_localizacaoModel",
                table: "localizacaoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_grupoSemaforoModel",
                table: "grupoSemaforoModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_catadioptricoModel",
                table: "catadioptricoModel");

            migrationBuilder.RenameTable(
                name: "veiculoModel",
                newName: "veiculo");

            migrationBuilder.RenameTable(
                name: "usuarioModel",
                newName: "usuario");

            migrationBuilder.RenameTable(
                name: "semaforoModel",
                newName: "semaforo");

            migrationBuilder.RenameTable(
                name: "localizacaoModel",
                newName: "localizacao");

            migrationBuilder.RenameTable(
                name: "grupoSemaforoModel",
                newName: "grupoSemaforo");

            migrationBuilder.RenameTable(
                name: "catadioptricoModel",
                newName: "catadioptrico");

            migrationBuilder.RenameIndex(
                name: "IX_veiculoModel_UsuarioId",
                table: "veiculo",
                newName: "IX_veiculo_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_semaforoModel_GrupoSemaforoId",
                table: "semaforo",
                newName: "IX_semaforo_GrupoSemaforoId");

            migrationBuilder.RenameIndex(
                name: "IX_localizacaoModel_semaforoId",
                table: "localizacao",
                newName: "IX_localizacao_semaforoId");

            migrationBuilder.RenameIndex(
                name: "IX_localizacaoModel_catadioptricoId",
                table: "localizacao",
                newName: "IX_localizacao_catadioptricoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_veiculo",
                table: "veiculo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario",
                table: "usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_semaforo",
                table: "semaforo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_localizacao",
                table: "localizacao",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_grupoSemaforo",
                table: "grupoSemaforo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_catadioptrico",
                table: "catadioptrico",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_veiculo_usuario_UsuarioId",
                table: "veiculo",
                column: "UsuarioId",
                principalTable: "usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_veiculo_usuario_UsuarioId",
                table: "veiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_veiculo",
                table: "veiculo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario",
                table: "usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_semaforo",
                table: "semaforo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_localizacao",
                table: "localizacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_grupoSemaforo",
                table: "grupoSemaforo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_catadioptrico",
                table: "catadioptrico");

            migrationBuilder.RenameTable(
                name: "veiculo",
                newName: "veiculoModel");

            migrationBuilder.RenameTable(
                name: "usuario",
                newName: "usuarioModel");

            migrationBuilder.RenameTable(
                name: "semaforo",
                newName: "semaforoModel");

            migrationBuilder.RenameTable(
                name: "localizacao",
                newName: "localizacaoModel");

            migrationBuilder.RenameTable(
                name: "grupoSemaforo",
                newName: "grupoSemaforoModel");

            migrationBuilder.RenameTable(
                name: "catadioptrico",
                newName: "catadioptricoModel");

            migrationBuilder.RenameIndex(
                name: "IX_veiculo_UsuarioId",
                table: "veiculoModel",
                newName: "IX_veiculoModel_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_semaforo_GrupoSemaforoId",
                table: "semaforoModel",
                newName: "IX_semaforoModel_GrupoSemaforoId");

            migrationBuilder.RenameIndex(
                name: "IX_localizacao_semaforoId",
                table: "localizacaoModel",
                newName: "IX_localizacaoModel_semaforoId");

            migrationBuilder.RenameIndex(
                name: "IX_localizacao_catadioptricoId",
                table: "localizacaoModel",
                newName: "IX_localizacaoModel_catadioptricoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_veiculoModel",
                table: "veiculoModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuarioModel",
                table: "usuarioModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_semaforoModel",
                table: "semaforoModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_localizacaoModel",
                table: "localizacaoModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_grupoSemaforoModel",
                table: "grupoSemaforoModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_catadioptricoModel",
                table: "catadioptricoModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_localizacaoModel_catadioptricoModel_catadioptricoId",
                table: "localizacaoModel",
                column: "catadioptricoId",
                principalTable: "catadioptricoModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_localizacaoModel_semaforoModel_semaforoId",
                table: "localizacaoModel",
                column: "semaforoId",
                principalTable: "semaforoModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_semaforoModel_grupoSemaforoModel_GrupoSemaforoId",
                table: "semaforoModel",
                column: "GrupoSemaforoId",
                principalTable: "grupoSemaforoModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_veiculoModel_usuarioModel_UsuarioId",
                table: "veiculoModel",
                column: "UsuarioId",
                principalTable: "usuarioModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
