using Microsoft.EntityFrameworkCore.Migrations;

namespace Smoothboard_Stylers.Migrations
{
    public partial class DefaultAdminUserEmailConfirmed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "51223904-aa7c-4704-b719-2b1560573c56");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1251db02-6821-419f-b206-78cb894ab859", true, "AQAAAAEAACcQAAAAECHfoKeTpqzRZ85ZIqnKpr7UIiIYixt1j4uG+yeSZ6yPKuisWTPaQ6TIohFIWRntLQ==", "25cd932d-cbad-4071-b324-c078cac97252" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "0400bbb1-02d2-415a-9c1b-76eabde9fa5d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "PasswordHash", "SecurityStamp" },
                values: new object[] { "adc0a09a-fa50-477c-984e-2323429481e3", false, "AQAAAAEAACcQAAAAENShFNdC3Ngwe/SA+2RKeqrp5cBxcgkfTM0SC2uuCEPJ4bbHdmihjqFQG7Az7FmL3Q==", "9d679b4d-e41d-4097-9053-cc46b1b97b99" });
        }
    }
}
