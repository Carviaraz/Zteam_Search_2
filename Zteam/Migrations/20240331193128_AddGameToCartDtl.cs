using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zteam.Migrations
{
    /// <inheritdoc />
    public partial class AddGameToCartDtl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CartDtls_GameId",
                table: "CartDtls",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDtls_Game_GameId",
                table: "CartDtls",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDtls_Game_GameId",
                table: "CartDtls");

            migrationBuilder.DropIndex(
                name: "IX_CartDtls_GameId",
                table: "CartDtls");
        }
    }
}
