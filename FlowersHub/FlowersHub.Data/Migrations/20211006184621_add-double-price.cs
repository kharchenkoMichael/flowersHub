using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersHub.Data.Migrations
{
    public partial class adddoubleprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PriceDouble",
                table: "Flowers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceDouble",
                table: "Flowers");
        }
    }
}
