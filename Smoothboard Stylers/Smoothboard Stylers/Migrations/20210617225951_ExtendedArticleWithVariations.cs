using Microsoft.EntityFrameworkCore.Migrations;

namespace Smoothboard_Stylers.Migrations
{
    public partial class ExtendedArticleWithVariations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Artikels");

            migrationBuilder.AlterColumn<int>(
                name: "Model",
                table: "Artikels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Variantions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentArticleId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    InSale = table.Column<bool>(type: "bit", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    TotalInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variantions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variantions_Artikels_ParentArticleId",
                        column: x => x.ParentArticleId,
                        principalTable: "Artikels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "654c2adf-bea6-4724-a724-1c145040f12e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9040485f-4167-46ae-8ccd-6ec5dd47e380", "AQAAAAEAACcQAAAAEE8V4qVy7QwKfHeAv3laWCY9qdFziIDDLBEFDfib1ctXYJBuWMa0N8/Wm32WRwOw4g==", "8ae2d296-55a1-42cb-a794-cecf837600c3" });

            migrationBuilder.CreateIndex(
                name: "IX_Variantions_ParentArticleId",
                table: "Variantions",
                column: "ParentArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variantions");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Artikels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Artikels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "fc146919-9a79-4c0f-9b44-ce6c3ca8b436");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "79377033-a070-4a82-943a-fb50013b1b9e", "AQAAAAEAACcQAAAAEMHJM3tegifkPRQRW5yvfFQeQ6sb54aRsf/RAvn/7yb49qA4fFndGfsWt5vFGYYj2A==", "f4174f00-0c16-4a18-a3bb-3a3b39200e85" });
        }
    }
}
