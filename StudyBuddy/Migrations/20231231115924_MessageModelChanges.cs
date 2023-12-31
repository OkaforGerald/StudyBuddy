using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Migrations
{
    public partial class MessageModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipientUsername",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SnederUsername",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bb823f0-2d92-47e8-be60-b2187df3555d",
                column: "ConcurrencyStamp",
                value: "c6e3a92f-f50f-476e-a32a-8df12fdc9d47");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1eeb37db-b804-4aad-a378-bcf2ba93e82b",
                column: "ConcurrencyStamp",
                value: "2165afc8-2843-4cbd-80bc-e864128d0bbb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipientUsername",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SnederUsername",
                table: "Messages");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bb823f0-2d92-47e8-be60-b2187df3555d",
                column: "ConcurrencyStamp",
                value: "a179b840-92a5-4286-9ff2-c7c69659ea79");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1eeb37db-b804-4aad-a378-bcf2ba93e82b",
                column: "ConcurrencyStamp",
                value: "7e51dc77-1279-4c23-80aa-09a709aecf2a");
        }
    }
}
