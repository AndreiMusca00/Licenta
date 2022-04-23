using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingAPI.Migrations
{
    public partial class UserProprietateRelatie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Proprietate",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proprietate_UserId",
                table: "Proprietate",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proprietate_User_UserId",
                table: "Proprietate",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proprietate_User_UserId",
                table: "Proprietate");

            migrationBuilder.DropIndex(
                name: "IX_Proprietate_UserId",
                table: "Proprietate");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Proprietate");
        }
    }
}
