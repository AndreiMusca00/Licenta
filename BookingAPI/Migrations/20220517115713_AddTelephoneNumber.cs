using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingAPI.Migrations
{
    public partial class AddTelephoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "User");
        }
    }
}
