using Microsoft.EntityFrameworkCore.Migrations;

namespace System.DAL.Migrations
{
    public partial class addclubsincostsandevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Costs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Events_ClubId",
                table: "Events",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_ClubId",
                table: "Costs",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Costs_Clubs_ClubId",
                table: "Costs",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Clubs_ClubId",
                table: "Events",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Costs_Clubs_ClubId",
                table: "Costs");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Clubs_ClubId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ClubId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Costs_ClubId",
                table: "Costs");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Costs");
        }
    }
}
