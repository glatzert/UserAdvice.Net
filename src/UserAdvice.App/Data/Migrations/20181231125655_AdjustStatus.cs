using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAdvice.Data.Migrations
{
    public partial class AdjustStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsClosed",
                table: "Status",
                newName: "IsPublic");

            migrationBuilder.AddColumn<bool>(
                name: "IsClosing",
                table: "Status",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosing",
                table: "Status");

            migrationBuilder.RenameColumn(
                name: "IsPublic",
                table: "Status",
                newName: "IsClosed");
        }
    }
}
