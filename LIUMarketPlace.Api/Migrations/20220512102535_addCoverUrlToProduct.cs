using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LIUMarketPlace.Api.Migrations
{
    public partial class addCoverUrlToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageCoverUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageCoverUrl",
                table: "Products");
        }
    }
}
