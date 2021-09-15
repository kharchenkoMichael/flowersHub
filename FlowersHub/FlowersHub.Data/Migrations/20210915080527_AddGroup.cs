using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersHub.Data.Migrations
{
    public partial class AddGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Flowers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Updater",
                table: "Flowers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "Flowers");

            migrationBuilder.DropColumn(
                name: "Updater",
                table: "Flowers");
        }
    }
}
