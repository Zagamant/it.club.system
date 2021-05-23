using Microsoft.EntityFrameworkCore.Migrations;

namespace System.DAL.Migrations
{
    public partial class changephotosnametoimagesinusersentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "18ae5216-8147-419e-ab56-89d028be2771");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 100,
                column: "ConcurrencyStamp",
                value: "0865631e-a7c5-4eb9-95b8-2992ded20bde");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 101,
                column: "ConcurrencyStamp",
                value: "f8f2a21d-ac38-4163-8e54-f1338d0d365d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 777,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0614f5c5-41e4-4f97-8940-7d06b34b575e", "AQAAAAEAACcQAAAAEKK5plKMDKZeNcTNWZgSNzB7AL5ZkSIdG7lZPeQ9Bn49fAQsikoOl/vy9Nnq4hjFHQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2ffba966-dc83-4183-9270-33d1b1f0c46a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 100,
                column: "ConcurrencyStamp",
                value: "37521123-907a-4b3f-82ae-1448426b0431");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 101,
                column: "ConcurrencyStamp",
                value: "28472ccc-5a12-41e7-ab04-b87c00dc1646");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 777,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0721020e-5f7b-4e62-9d45-30bff71fb85b", "AQAAAAEAACcQAAAAEDOnwPX5BnDHUGPnH+xvgugcnx9QyQ6fQxVypy9lWERbJtLlbbJU6nppxvFceim6Sg==" });
        }
    }
}
