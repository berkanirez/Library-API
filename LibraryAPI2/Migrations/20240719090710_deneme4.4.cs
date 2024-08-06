using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI2.Migrations
{
    public partial class deneme44 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVoted",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVoted",
                table: "Members");
        }
    }
}
