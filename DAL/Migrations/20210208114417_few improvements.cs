using Microsoft.EntityFrameworkCore.Migrations;

namespace System.DAL.Migrations
{
    public partial class fewimprovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClubId",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClubId",
                table: "Payments",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Clubs_ClubId",
                table: "Payments",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Clubs_ClubId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ClubId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Payments");
        }
    }
}
