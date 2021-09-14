using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersHub.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flowers",
                columns: table => new
                {
                    Url = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flowers", x => x.Url);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Variations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlowerUrl = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Colors_Flowers_FlowerUrl",
                        column: x => x.FlowerUrl,
                        principalTable: "Flowers",
                        principalColumn: "Url",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlowerTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Variations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlowerUrl = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerTypes", x => x.Name);
                    table.ForeignKey(
                        name: "FK_FlowerTypes_Flowers_FlowerUrl",
                        column: x => x.FlowerUrl,
                        principalTable: "Flowers",
                        principalColumn: "Url",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colors_FlowerUrl",
                table: "Colors",
                column: "FlowerUrl");

            migrationBuilder.CreateIndex(
                name: "IX_FlowerTypes_FlowerUrl",
                table: "FlowerTypes",
                column: "FlowerUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "FlowerTypes");

            migrationBuilder.DropTable(
                name: "Flowers");
        }
    }
}
