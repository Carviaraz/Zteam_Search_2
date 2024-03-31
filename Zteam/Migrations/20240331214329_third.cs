using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zteam.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_salesReportsS",
                table: "salesReportsS");

            migrationBuilder.RenameTable(
                name: "salesReportsS",
                newName: "SalesReports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesReports",
                table: "SalesReports",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesReports",
                table: "SalesReports");

            migrationBuilder.RenameTable(
                name: "SalesReports",
                newName: "salesReportsS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_salesReportsS",
                table: "salesReportsS",
                column: "Id");
        }
    }
}
