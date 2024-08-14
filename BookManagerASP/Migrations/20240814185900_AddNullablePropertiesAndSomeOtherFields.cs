using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagerASP.Migrations
{
    public partial class AddNullablePropertiesAndSomeOtherFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_BooksPrivates_BookPrivateId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_AspNetUsers_UserEntityId",
                table: "Shelves");

            migrationBuilder.AlterColumn<string>(
                name: "UserEntityId",
                table: "Shelves",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Shelves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Shelves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "BookPrivateId",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "BooksPrivates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Isbn",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_BooksPrivates_BookPrivateId",
                table: "Quotes",
                column: "BookPrivateId",
                principalTable: "BooksPrivates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_AspNetUsers_UserEntityId",
                table: "Shelves",
                column: "UserEntityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_BooksPrivates_BookPrivateId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_AspNetUsers_UserEntityId",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "BooksPrivates");

            migrationBuilder.DropColumn(
                name: "CoverUrl",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Isbn",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserEntityId",
                table: "Shelves",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookPrivateId",
                table: "Quotes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_BooksPrivates_BookPrivateId",
                table: "Quotes",
                column: "BookPrivateId",
                principalTable: "BooksPrivates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_AspNetUsers_UserEntityId",
                table: "Shelves",
                column: "UserEntityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
