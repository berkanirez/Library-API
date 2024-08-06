using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI2.Migrations
{
    public partial class deneme2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBorrowed",
                table: "Borrow",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHarmed",
                table: "Borrow",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBorrowed",
                table: "Borrow");

            migrationBuilder.DropColumn(
                name: "IsHarmed",
                table: "Borrow");
        }
    }
}
