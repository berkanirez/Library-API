using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI2.Migrations
{
    public partial class deneme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.RenameColumn(
                name: "ReceivedDate",
                table: "Borrow",
                newName: "ReceiveDate");

            migrationBuilder.AddColumn<float>(
                name: "PunishmentTaken",
                table: "Borrow",
                type: "real",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PunishmentTaken",
                table: "Borrow");

            migrationBuilder.RenameColumn(
                name: "ReceiveDate",
                table: "Borrow",
                newName: "ReceivedDate");

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CancellationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReservationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                });
        }
    }
}
