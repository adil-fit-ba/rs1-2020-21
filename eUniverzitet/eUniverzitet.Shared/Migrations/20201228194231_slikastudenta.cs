using Microsoft.EntityFrameworkCore.Migrations;

namespace eUniverzitet.Shared.Migrations
{
    public partial class slikastudenta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NastavnikID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SlikaStudenta",
                table: "Student",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlikaStudenta",
                table: "Student");

            migrationBuilder.AddColumn<int>(
                name: "NastavnikID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }
    }
}
