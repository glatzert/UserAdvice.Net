using Microsoft.EntityFrameworkCore.Migrations;

namespace UserAdvice.Data.Migrations
{
    public partial class AddPostCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostCount",
                table: "Categories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostCount",
                table: "Categories");
        }
    }
}
