using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zteam.Migrations
{
    /// <inheritdoc />
    public partial class AddCusIdToCartDtl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CusId",
                table: "CartDtls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CusId",
                table: "CartDtls");
        }
    }
}
