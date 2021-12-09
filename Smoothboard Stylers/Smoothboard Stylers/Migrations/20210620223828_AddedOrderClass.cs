using Microsoft.EntityFrameworkCore.Migrations;

namespace Smoothboard_Stylers.Migrations
{
    public partial class AddedOrderClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleId = table.Column<int>(type: "int", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cellphone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Artikels_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Artikels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "b841e7f0-91b8-4939-8c1c-7e288cb14fd2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58174a03-6a02-45ac-8c33-172cbae397d4", "AQAAAAEAACcQAAAAEJS3oCSMkUoE2rggwnLOjiYAnuGTHSbX+cXgd8Lo3UNv/YriLiBd6lE+oq3yLsnIhQ==", "a8a24933-926b-4b6d-b0c2-0da6e9c053e1" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ArticleId",
                table: "Orders",
                column: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

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
    }
}
