using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpringBoard.Data.Migrations
{
    public partial class compteRendu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "compteRendus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "statut",
                table: "compteRendus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "validation",
                table: "compteRendus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Rapport",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    valeur = table.Column<double>(type: "float", nullable: false),
                    CompteRenduid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rapport", x => x.id);
                    table.ForeignKey(
                        name: "FK_Rapport_compteRendus_CompteRenduid",
                        column: x => x.CompteRenduid,
                        principalTable: "compteRendus",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rapport_CompteRenduid",
                table: "Rapport",
                column: "CompteRenduid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rapport");

            migrationBuilder.DropColumn(
                name: "date",
                table: "compteRendus");

            migrationBuilder.DropColumn(
                name: "statut",
                table: "compteRendus");

            migrationBuilder.DropColumn(
                name: "validation",
                table: "compteRendus");
        }
    }
}
