using Microsoft.EntityFrameworkCore.Migrations;

namespace Smoothboard_Stylers.Migrations
{
    public partial class AddedMessageToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d6daf3-49cf-4613-9a69-0c56f84bdcd5",
                column: "ConcurrencyStamp",
                value: "11ea2426-b0c3-4fdc-974d-ffd309d1cdb9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a044b51d-7b26-4267-be4a-dcb5141be0b2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb3fad39-4738-4981-899b-6adf8c7213c6", "AQAAAAEAACcQAAAAEJLS9XnrNEEM0pdn6vIWXEuBkFVapSPjuKnqpg1V8t8+VuY7XPLJw30g9QC23wqpGA==", "3f508c28-2c6f-476b-8fe2-1e406b985822" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Orders");

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
        }
    }
}
