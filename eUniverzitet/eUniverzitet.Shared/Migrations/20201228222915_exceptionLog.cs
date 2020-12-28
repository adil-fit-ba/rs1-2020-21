using Microsoft.EntityFrameworkCore.Migrations;

namespace eUniverzitet.Shared.Migrations
{
    public partial class exceptionLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "exceptionMessage",
                table: "LogKretanjePoSistemu",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isException",
                table: "LogKretanjePoSistemu",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "exceptionMessage",
                table: "LogKretanjePoSistemu");

            migrationBuilder.DropColumn(
                name: "isException",
                table: "LogKretanjePoSistemu");
        }
    }
}
