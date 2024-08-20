using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagerASP.Migrations
{
    public partial class ChangeMostlyShelfAndBokPrivateRelationshipsWithUserAndAddSomeNewPropertiesAndConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksPrivates_Shelves_ShelfId",
                table: "BooksPrivates");

            migrationBuilder.DropIndex(
                name: "IX_BooksPrivates_ShelfId",
                table: "BooksPrivates");

            migrationBuilder.DropColumn(
                name: "ShelfId",
                table: "BooksPrivates");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "BookPrivateId",
                table: "Shelves",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Rating",
                table: "Reviews",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte>(
                name: "Rating",
                table: "BooksPrivates",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEntityId",
                table: "BooksPrivates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Genre",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_BookPrivateId",
                table: "Shelves",
                column: "BookPrivateId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksPrivates_UserEntityId",
                table: "BooksPrivates",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksPrivates_AspNetUsers_UserEntityId",
                table: "BooksPrivates",
                column: "UserEntityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_BooksPrivates_BookPrivateId",
                table: "Shelves",
                column: "BookPrivateId",
                principalTable: "BooksPrivates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksPrivates_AspNetUsers_UserEntityId",
                table: "BooksPrivates");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_BooksPrivates_BookPrivateId",
                table: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_Shelves_BookPrivateId",
                table: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_BooksPrivates_UserEntityId",
                table: "BooksPrivates");

            migrationBuilder.DropColumn(
                name: "BookPrivateId",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "BooksPrivates");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "BooksPrivates",
                type: "int",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShelfId",
                table: "BooksPrivates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_BooksPrivates_ShelfId",
                table: "BooksPrivates",
                column: "ShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksPrivates_Shelves_ShelfId",
                table: "BooksPrivates",
                column: "ShelfId",
                principalTable: "Shelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
