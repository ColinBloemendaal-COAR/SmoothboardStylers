using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Smoothboard_Stylers.Migrations
{
    public partial class AddDefaultAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01d6daf3-49cf-4613-9a69-0c56f84bdcd5", "0400bbb1-02d2-415a-9c1b-76eabde9fa5d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "Firstname", "LockoutEnabled", "LockoutEnd", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SurName", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a044b51d-7b26-4267-be4a-dcb5141be0b2", 0, "adc0a09a-fa50-477c-984e-2323429481e3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@smoothboardstyler.nl", false, "Admin", false, null, "SmoothBoard", "ADMIN@SMOOTHBOARDSTYLER.nl", "ADMIN@SMOOTHBOARDSTYLER.nl", "AQAAAAEAACcQAAAAENShFNdC3Ngwe/SA+2RKeqrp5cBxcgkfTM0SC2uuCEPJ4bbHdmihjqFQG7Az7FmL3Q==", null, false, "9d679b4d-e41d-4097-9053-cc46b1b97b99", "Styler", false, "admin@smoothboardstyler.nl" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "01d6daf3-49cf-4613-9a69-0c56f84bdcd5", "a044b51d-7b26-4267-be4a-dcb5141be0b2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "01d6daf3-49cf-4613-9a69-0c56f84bdcd5", "a044b51d-7b26-4267-be4a-dcb5141be0b2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2");
        }
    }
}
