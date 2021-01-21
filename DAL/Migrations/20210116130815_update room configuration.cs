using Microsoft.EntityFrameworkCore.Migrations;

namespace System.DAL.Migrations
{
    public partial class updateroomconfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Clubs_ClubId",
                table: "Rooms");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Clubs_ClubId",
                table: "Rooms",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Clubs_ClubId",
                table: "Rooms");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Clubs_ClubId",
                table: "Rooms",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
