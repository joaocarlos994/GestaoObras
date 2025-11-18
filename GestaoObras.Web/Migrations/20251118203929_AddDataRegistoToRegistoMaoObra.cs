using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoObras.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddDataRegistoToRegistoMaoObra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataHora",
                table: "RegistosMaoObra",
                newName: "DataRegisto");

            migrationBuilder.RenameColumn(
                name: "DataHora",
                table: "Pagamentos",
                newName: "DataRegisto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataRegisto",
                table: "RegistosMaoObra",
                newName: "DataHora");

            migrationBuilder.RenameColumn(
                name: "DataRegisto",
                table: "Pagamentos",
                newName: "DataHora");
        }
    }
}
