using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagerASP.Migrations
{
    public partial class ChangeReviewsTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookUserReviews_Review_ReviewId",
                table: "BookUserReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookUserReviews_Reviews_ReviewId",
                table: "BookUserReviews",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookUserReviews_Reviews_ReviewId",
                table: "BookUserReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookUserReviews_Review_ReviewId",
                table: "BookUserReviews",
                column: "ReviewId",
                principalTable: "Review",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
