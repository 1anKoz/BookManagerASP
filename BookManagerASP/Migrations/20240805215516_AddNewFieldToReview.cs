using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagerASP.Migrations
{
    public partial class AddNewFieldToReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Review");
        }
    }
}
