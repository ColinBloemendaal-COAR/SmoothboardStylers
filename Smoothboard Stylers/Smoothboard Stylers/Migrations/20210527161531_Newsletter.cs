using Microsoft.EntityFrameworkCore.Migrations;

namespace Smoothboard_Stylers.Migrations
{
    public partial class Newsletter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Newsletters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletters", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Newsletters");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "7859e911-abba-4c75-9a3f-2e2a87741990");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7b2160b-9962-4542-82df-c46290823344", "AQAAAAEAACcQAAAAEHo0S0dN750bDn+VRbERXSxo+WGvWiXVpA576D1egXVMgDfI5XFMeiTIDBLPrHGDfg==", "8a627283-f04a-42d7-8e6b-74c3efd4ca87" });
        }
    }
}
