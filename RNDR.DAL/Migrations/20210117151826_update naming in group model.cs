using Microsoft.EntityFrameworkCore.Migrations;

namespace System.DAL.Migrations
{
    public partial class updatenamingingroupmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SkypeConversation",
                table: "Groups",
                newName: "OnlineConversationLink");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OnlineConversationLink",
                table: "Groups",
                newName: "SkypeConversation");
        }
    }
}
