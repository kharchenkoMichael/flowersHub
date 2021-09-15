using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowersHub.Data.Migrations
{
    public partial class AddManyToManyTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Flowers_FlowerUrl",
                table: "Colors");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowerTypes_Flowers_FlowerUrl",
                table: "FlowerTypes");

            migrationBuilder.DropIndex(
                name: "IX_FlowerTypes_FlowerUrl",
                table: "FlowerTypes");

            migrationBuilder.DropIndex(
                name: "IX_Colors_FlowerUrl",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "FlowerUrl",
                table: "FlowerTypes");

            migrationBuilder.DropColumn(
                name: "FlowerUrl",
                table: "Colors");

            migrationBuilder.CreateTable(
                name: "ColorTypeFlower",
                columns: table => new
                {
                    ColorsName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FlowersUrl = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorTypeFlower", x => new { x.ColorsName, x.FlowersUrl });
                    table.ForeignKey(
                        name: "FK_ColorTypeFlower_Colors_ColorsName",
                        column: x => x.ColorsName,
                        principalTable: "Colors",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorTypeFlower_Flowers_FlowersUrl",
                        column: x => x.FlowersUrl,
                        principalTable: "Flowers",
                        principalColumn: "Url",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowerColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowerKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorTypeKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FlowerFlowerType",
                columns: table => new
                {
                    FlowerTypesName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FlowersUrl = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowerFlowerType", x => new { x.FlowerTypesName, x.FlowersUrl });
                    table.ForeignKey(
                        name: "FK_FlowerFlowerType_Flowers_FlowersUrl",
                        column: x => x.FlowersUrl,
                        principalTable: "Flowers",
                        principalColumn: "Url",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowerFlowerType_FlowerTypes_FlowerTypesName",
                        column: x => x.FlowerTypesName,
                        principalTable: "FlowerTypes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_ColorTypeFlower_FlowersUrl",
                table: "ColorTypeFlower",
                column: "FlowersUrl");

            migrationBuilder.CreateIndex(
                name: "IX_FlowerFlowerType_FlowersUrl",
                table: "FlowerFlowerType",
                column: "FlowersUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorTypeFlower");

            migrationBuilder.DropTable(
                name: "FlowerColors");

            migrationBuilder.DropTable(
                name: "FlowerFlowerType");

            migrationBuilder.DropTable(
                name: "FlowerFlowerTypes");

            migrationBuilder.AddColumn<string>(
                name: "FlowerUrl",
                table: "FlowerTypes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlowerUrl",
                table: "Colors",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FlowerTypes_FlowerUrl",
                table: "FlowerTypes",
                column: "FlowerUrl");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_FlowerUrl",
                table: "Colors",
                column: "FlowerUrl");

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Flowers_FlowerUrl",
                table: "Colors",
                column: "FlowerUrl",
                principalTable: "Flowers",
                principalColumn: "Url",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowerTypes_Flowers_FlowerUrl",
                table: "FlowerTypes",
                column: "FlowerUrl",
                principalTable: "Flowers",
                principalColumn: "Url",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
