using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingAPI.Migrations
{
    public partial class PropFaraImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Proprietate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Judet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Oras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Strada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pret = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proprietate", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proprietate");
        }
    }
}
