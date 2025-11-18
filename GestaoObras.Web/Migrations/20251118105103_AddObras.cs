using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoObras.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddObras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RegistosMaoObra_ObraId",
                table: "RegistosMaoObra",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_ObraId",
                table: "Pagamentos",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_Obras_ClienteId",
                table: "Obras",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosStock_MaterialId",
                table: "MovimentosStock",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentosStock_ObraId",
                table: "MovimentosStock",
                column: "ObraId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentosStock_Materiais_MaterialId",
                table: "MovimentosStock",
                column: "MaterialId",
                principalTable: "Materiais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentosStock_Obras_ObraId",
                table: "MovimentosStock",
                column: "ObraId",
                principalTable: "Obras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Obras_Clientes_ClienteId",
                table: "Obras",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamentos_Obras_ObraId",
                table: "Pagamentos",
                column: "ObraId",
                principalTable: "Obras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistosMaoObra_Obras_ObraId",
                table: "RegistosMaoObra",
                column: "ObraId",
                principalTable: "Obras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentosStock_Materiais_MaterialId",
                table: "MovimentosStock");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentosStock_Obras_ObraId",
                table: "MovimentosStock");

            migrationBuilder.DropForeignKey(
                name: "FK_Obras_Clientes_ClienteId",
                table: "Obras");

            migrationBuilder.DropForeignKey(
                name: "FK_Pagamentos_Obras_ObraId",
                table: "Pagamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistosMaoObra_Obras_ObraId",
                table: "RegistosMaoObra");

            migrationBuilder.DropIndex(
                name: "IX_RegistosMaoObra_ObraId",
                table: "RegistosMaoObra");

            migrationBuilder.DropIndex(
                name: "IX_Pagamentos_ObraId",
                table: "Pagamentos");

            migrationBuilder.DropIndex(
                name: "IX_Obras_ClienteId",
                table: "Obras");

            migrationBuilder.DropIndex(
                name: "IX_MovimentosStock_MaterialId",
                table: "MovimentosStock");

            migrationBuilder.DropIndex(
                name: "IX_MovimentosStock_ObraId",
                table: "MovimentosStock");
        }
    }
}
