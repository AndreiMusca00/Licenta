using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingAPI.Migrations
{
    public partial class AddImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Imagine",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idProprietate = table.Column<int>(type: "int", nullable: false),
                    proprietateId = table.Column<int>(type: "int", nullable: true),
                    imagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagine", x => x.id);
                    table.ForeignKey(
                        name: "FK_Imagine_Proprietate_proprietateId",
                        column: x => x.proprietateId,
                        principalTable: "Proprietate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Imagine_proprietateId",
                table: "Imagine",
                column: "proprietateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imagine");
        }
    }
}
