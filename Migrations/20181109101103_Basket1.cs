using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class Basket1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "BasketItems",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "BasketItems",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "BasketItems");
        }
    }
}
