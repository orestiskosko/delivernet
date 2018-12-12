using Microsoft.EntityFrameworkCore.Migrations;

namespace DeliverNET.Migrations
{
    public partial class ChangeInOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRejected",
                table: "Orders",
                newName: "IsTimedOut");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTimedOut",
                table: "Orders",
                newName: "IsRejected");
        }
    }
}
