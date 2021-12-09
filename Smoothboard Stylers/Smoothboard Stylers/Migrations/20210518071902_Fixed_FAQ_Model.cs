using Microsoft.EntityFrameworkCore.Migrations;

namespace Smoothboard_Stylers.Migrations
{
    public partial class Fixed_FAQ_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FAQs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQs", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FAQs");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "657d0056-fcc5-49b4-9470-5545150221e4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb675dbf-7b82-4b68-acd3-a6007abda408", "AQAAAAEAACcQAAAAEBgaZxHhfGgrK+grCWDSC8peBITKMrnFgFvCZq31ELO2wk0M/Vk12PUFcJQcx3Xm6g==", "7b43386e-7bdc-47e5-8858-86e9cb0a03de" });
        }
    }
}
