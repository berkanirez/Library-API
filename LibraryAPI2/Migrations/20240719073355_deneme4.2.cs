using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI2.Migrations
{
    public partial class deneme42 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalVotes",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVotes",
                table: "Books");
        }
    }
}
