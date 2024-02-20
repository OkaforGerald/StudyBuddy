using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Migrations
{
    public partial class Matching : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Matches",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatcherId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MatchedId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_AspNetUsers_MatchedId",
                        column: x => x.MatchedId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_AspNetUsers_MatcherId",
                        column: x => x.MatcherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchedId",
                table: "Matches",
                column: "MatchedId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatcherId",
                table: "Matches",
                column: "MatcherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropColumn(
                name: "Matches",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bb823f0-2d92-47e8-be60-b2187df3555d",
                column: "ConcurrencyStamp",
                value: "b6c2451a-a4c7-4615-9996-6ac47ee8f9eb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1eeb37db-b804-4aad-a378-bcf2ba93e82b",
                column: "ConcurrencyStamp",
                value: "7a260801-2177-450c-851e-bbea6d18a874");
        }
    }
}
