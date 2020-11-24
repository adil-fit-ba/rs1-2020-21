using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Podaci.Migrations
{
    public partial class inicijalnamigracija : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Opstina",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opstina", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Predmet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojIndeksa = table.Column<string>(nullable: true),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    OpstinaRodjenjaID = table.Column<int>(nullable: false),
                    OpstinaPrebivalistaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Student_Opstina_OpstinaPrebivalistaID",
                        column: x => x.OpstinaPrebivalistaID,
                        principalTable: "Opstina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Student_Opstina_OpstinaRodjenjaID",
                        column: x => x.OpstinaRodjenjaID,
                        principalTable: "Opstina",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Ocjene",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(nullable: false),
                    PredmetID = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    OcjenaBrojacna = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocjene", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ocjene_Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "Predmet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Ocjene_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PrisustvoNaNastavi",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(nullable: false),
                    PredmetID = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    IsPrisutan = table.Column<bool>(nullable: false),
                    Komentar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrisustvoNaNastavi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PrisustvoNaNastavi_Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "Predmet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PrisustvoNaNastavi_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ocjene_PredmetID",
                table: "Ocjene",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjene_StudentID",
                table: "Ocjene",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_PrisustvoNaNastavi_PredmetID",
                table: "PrisustvoNaNastavi",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_PrisustvoNaNastavi_StudentID",
                table: "PrisustvoNaNastavi",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_OpstinaPrebivalistaID",
                table: "Student",
                column: "OpstinaPrebivalistaID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_OpstinaRodjenjaID",
                table: "Student",
                column: "OpstinaRodjenjaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ocjene");

            migrationBuilder.DropTable(
                name: "PrisustvoNaNastavi");

            migrationBuilder.DropTable(
                name: "Predmet");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Opstina");
        }
    }
}
