using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersHub.Data.Migrations
{
    public partial class removeUnusedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlowerColors");

            migrationBuilder.DropTable(
                name: "FlowerFlowerTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlowerColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorTypeKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlowerKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowerFlowerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlowerTypeKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerFlowerTypes", x => x.Id);
                });
        }
    }
}
