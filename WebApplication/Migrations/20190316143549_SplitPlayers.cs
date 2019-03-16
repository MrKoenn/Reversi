using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class SplitPlayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Games_GameId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_GameId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "Player1Id",
                table: "Games",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Player2Id",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_Player1Id",
                table: "Games",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Player2Id",
                table: "Games",
                column: "Player2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_Player1Id",
                table: "Games",
                column: "Player1Id",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_Player2Id",
                table: "Games",
                column: "Player2Id",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_Player1Id",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_Player2Id",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_Player1Id",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_Player2Id",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player1Id",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Player2Id",
                table: "Games");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Players",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId",
                table: "Players",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Games_GameId",
                table: "Players",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
