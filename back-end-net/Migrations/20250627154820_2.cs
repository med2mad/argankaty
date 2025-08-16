using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace back_end_net.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_product_orders_Orderid",
                table: "order_product");

            migrationBuilder.AlterColumn<string>(
                name: "coupon_code",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_orders_Orderid",
                table: "order_product",
                column: "Orderid",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_product_orders_Orderid",
                table: "order_product");

            migrationBuilder.AlterColumn<string>(
                name: "coupon_code",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_order_product_orders_Orderid",
                table: "order_product",
                column: "Orderid",
                principalTable: "orders",
                principalColumn: "id");
        }
    }
}
