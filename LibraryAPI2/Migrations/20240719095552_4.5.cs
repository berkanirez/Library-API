using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI2.Migrations
{
    public partial class _45 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVoted",
                table: "Members");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVoted",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
