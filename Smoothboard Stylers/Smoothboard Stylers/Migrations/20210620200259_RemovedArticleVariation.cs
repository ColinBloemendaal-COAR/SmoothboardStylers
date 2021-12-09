using Microsoft.EntityFrameworkCore.Migrations;

namespace Smoothboard_Stylers.Migrations
{
    public partial class RemovedArticleVariation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Variantions");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Artikels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InSale",
                table: "Artikels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "Artikels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Artikels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SalePrice",
                table: "Artikels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Artikels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalInStock",
                table: "Artikels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "23df2209-936b-4be7-91c0-b399f9d6dc28");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b63ea53-7d11-4645-8952-9658f1141ed8", "AQAAAAEAACcQAAAAEIswxyxrtiuLfcYaO6jydXkIvZDXCbN88qCpAuu6Z4rj3qvdC9Hu6xTvLsADIeCR1A==", "5789cecb-ed24-4aa3-b3e4-f1f5809d45a9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Artikels");

            migrationBuilder.DropColumn(
                name: "InSale",
                table: "Artikels");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "Artikels");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Artikels");

            migrationBuilder.DropColumn(
                name: "SalePrice",
                table: "Artikels");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Artikels");

            migrationBuilder.DropColumn(
                name: "TotalInStock",
                table: "Artikels");

            migrationBuilder.CreateTable(
                name: "Variantions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InSale = table.Column<bool>(type: "bit", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentArticleId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
    }
}
