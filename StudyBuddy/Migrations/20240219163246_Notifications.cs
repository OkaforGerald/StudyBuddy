using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Migrations
{
    public partial class Notifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileViewerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MatcherId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MatchedId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NotifType = table.Column<int>(type: "int", nullable: false),
                    isRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_MatchedId",
                        column: x => x.MatchedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_MatcherId",
                        column: x => x.MatcherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_ProfileViewerId",
                        column: x => x.ProfileViewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MatchedId",
                table: "Notifications",
                column: "MatchedId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_MatcherId",
                table: "Notifications",
                column: "MatcherId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_OwnerId",
                table: "Notifications",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ProfileViewerId",
                table: "Notifications",
                column: "ProfileViewerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bb823f0-2d92-47e8-be60-b2187df3555d",
                column: "ConcurrencyStamp",
                value: "ade4a649-c3ff-42b6-a0f2-dea6fcbccb70");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1eeb37db-b804-4aad-a378-bcf2ba93e82b",
                column: "ConcurrencyStamp",
                value: "af1ed125-d10d-4e9b-b04b-f5e74d551fb3");
        }
    }
}
