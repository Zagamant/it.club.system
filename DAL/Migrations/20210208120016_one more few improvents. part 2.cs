using Microsoft.EntityFrameworkCore.Migrations;

namespace System.DAL.Migrations
{
    public partial class onemorefewimproventspart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_UserId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Clubs_ClubId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_UserId",
                table: "Payments");

            migrationBuilder.AddColumn<int>(
                name: "ClubId1",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClubId1",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClubId1",
                table: "Costs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClubId1",
                table: "Payments",
                column: "ClubId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_ClubId1",
                table: "Events",
                column: "ClubId1");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_ClubId1",
                table: "Costs",
                column: "ClubId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Clubs_ClubId1",
                table: "Costs",
                column: "ClubId1",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Clubs_ClubId1",
                table: "Events",
                column: "ClubId1",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_ClubId",
                table: "Payments",
                column: "ClubId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Clubs_ClubId1",
                table: "Payments",
                column: "ClubId1",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Clubs_ClubId1",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Clubs_ClubId1",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_AspNetUsers_ClubId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Clubs_ClubId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ClubId1",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Events_ClubId1",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Costs_ClubId1",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "ClubId1",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ClubId1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ClubId1",
                table: "Costs");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                table: "Payments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_AspNetUsers_UserId",
                table: "Payments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Clubs_ClubId",
                table: "Payments",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
