using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI2.Migrations
{
    public partial class deneme3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBorrowed",
                table: "Borrow");

            migrationBuilder.AddColumn<bool>(
                name: "IsBorrowed",
                table: "BookCopy",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHarmed",
                table: "BookCopy",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBorrowed",
                table: "BookCopy");

            migrationBuilder.DropColumn(
                name: "IsHarmed",
                table: "BookCopy");

            migrationBuilder.AddColumn<bool>(
                name: "IsBorrowed",
                table: "Borrow",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
