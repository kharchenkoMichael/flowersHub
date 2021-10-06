using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersHub.Data.Migrations
{
    public partial class removeprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Flowers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Flowers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
