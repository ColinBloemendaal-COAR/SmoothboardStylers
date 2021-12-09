using Microsoft.EntityFrameworkCore.Migrations;

namespace Smoothboard_Stylers.Migrations
{
    public partial class NewsletterSubscriber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsletterSubscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsletterSubscribers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "ae785c9c-4f4a-446e-85d7-7d87dd37ba67");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83aa9d99-b683-4b5b-b424-26ec09b1eb66", "AQAAAAEAACcQAAAAEPMb99wUdLsavPrVuwU/v6cOXF/NB/hqbV9bWyoI4eusuVt3jeox+VVOTOUJFx56+g==", "9605805d-6ce3-42e2-b500-b6554e274563" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsletterSubscribers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "e414578f-0d44-43b7-9f3e-23152f9d7298");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c43cf17e-68f7-4bb7-a979-babc7e122d50", "AQAAAAEAACcQAAAAEPmV7rq7DWHgFKYatNrrEJVn9/PvSPNG5G9/q9FWh+12x5+lHhtXdoDNKS7kpzOzPw==", "9947c90d-bff0-4602-b3ef-bf42ab40adb0" });
        }
    }
}
