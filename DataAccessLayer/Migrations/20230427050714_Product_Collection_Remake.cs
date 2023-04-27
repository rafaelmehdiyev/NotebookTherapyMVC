using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class Product_Collection_Remake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionButtonColor",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "CollectionColor",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "CollectionFooterImage",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "CollectionHeaderImage",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "CollectionItemBackgroundImage",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "CollectionItemColor",
                table: "ProductCollections");

            migrationBuilder.AddColumn<string>(
                name: "CollectionIcon",
                table: "ProductCollections",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductCollections",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectionIcon",
                table: "ProductCollections");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductCollections");

            migrationBuilder.AddColumn<string>(
                name: "CollectionButtonColor",
                table: "ProductCollections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CollectionColor",
                table: "ProductCollections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CollectionFooterImage",
                table: "ProductCollections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CollectionHeaderImage",
                table: "ProductCollections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CollectionItemBackgroundImage",
                table: "ProductCollections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CollectionItemColor",
                table: "ProductCollections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
