using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Migrations
{
    public partial class NotificationCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isRead",
                table: "Notifications");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bb823f0-2d92-47e8-be60-b2187df3555d",
                column: "ConcurrencyStamp",
                value: "7be5b4db-db20-42de-9158-732737511724");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1eeb37db-b804-4aad-a378-bcf2ba93e82b",
                column: "ConcurrencyStamp",
                value: "d4f40e31-f6e0-4434-971e-f2a25cc30f5e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notifications");

            migrationBuilder.AddColumn<bool>(
                name: "isRead",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bb823f0-2d92-47e8-be60-b2187df3555d",
                column: "ConcurrencyStamp",
                value: "11ffecde-16bb-4a5e-8442-6cfa56330f48");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1eeb37db-b804-4aad-a378-bcf2ba93e82b",
                column: "ConcurrencyStamp",
                value: "7f77370d-5e42-4a14-b039-af3f0cab12a5");
        }
    }
}
