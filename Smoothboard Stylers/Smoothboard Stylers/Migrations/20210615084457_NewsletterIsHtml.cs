using Microsoft.EntityFrameworkCore.Migrations;

namespace Smoothboard_Stylers.Migrations
{
    public partial class NewsletterIsHtml : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHtml",
                table: "Newsletters",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHtml",
                table: "Newsletters");

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
    }
}
