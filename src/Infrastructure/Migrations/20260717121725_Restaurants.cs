using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Restaurants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menus_restaurant_restaurant_id",
                table: "menus");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_restaurant_restaurant_id",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "pk_restaurant",
                table: "restaurant");

            migrationBuilder.RenameTable(
                name: "restaurant",
                newName: "restaurants");

            migrationBuilder.AddPrimaryKey(
                name: "pk_restaurants",
                table: "restaurants",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_menus_restaurants_restaurant_id",
                table: "menus",
                column: "restaurant_id",
                principalTable: "restaurants",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_restaurants_restaurant_id",
                table: "orders",
                column: "restaurant_id",
                principalTable: "restaurants",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_menus_restaurants_restaurant_id",
                table: "menus");

            migrationBuilder.DropForeignKey(
                name: "fk_orders_restaurants_restaurant_id",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "pk_restaurants",
                table: "restaurants");

            migrationBuilder.RenameTable(
                name: "restaurants",
                newName: "restaurant");

            migrationBuilder.AddPrimaryKey(
                name: "pk_restaurant",
                table: "restaurant",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_menus_restaurant_restaurant_id",
                table: "menus",
                column: "restaurant_id",
                principalTable: "restaurant",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_orders_restaurant_restaurant_id",
                table: "orders",
                column: "restaurant_id",
                principalTable: "restaurant",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
