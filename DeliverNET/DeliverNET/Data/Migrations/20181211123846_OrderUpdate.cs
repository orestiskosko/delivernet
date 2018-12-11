using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliverNET.Migrations
{
    public partial class OrderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Long",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Long",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Lat",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
