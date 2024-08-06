using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI2.Migrations
{
    public partial class deneme43 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MemberId",
                table: "Votes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MemberId",
                table: "Votes",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Members_MemberId",
                table: "Votes",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Members_MemberId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_MemberId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Votes");
        }
    }
}
