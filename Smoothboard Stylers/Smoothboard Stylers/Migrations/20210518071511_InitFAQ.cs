using Microsoft.EntityFrameworkCore.Migrations;

namespace Smoothboard_Stylers.Migrations
{
    public partial class InitFAQ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "2a9f5658-1368-4abd-9594-9078ba944401");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "739f061a-fd99-4ff2-875b-7585d97b4a5d", "AQAAAAEAACcQAAAAEM5TMW2Q1HtbGpnOwpTW6jwl0jnlKMefD8eUcNzCOOV7t4saR8maPbVWNBxNhDs1tg==", "a5c2c2f9-419f-4c59-9a2d-e35805053c57" });
        }
    }
}
