using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace System.DAL.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ClubId", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 100, null, "dc224cb6-522d-4471-aa45-21fcc1d6ac4f", "main_admin", "MAIN_ADMIN" },
                    { 101, null, "53d4bf38-4500-4332-9fb5-690b4c91bfa4", "admin", "ADMIN" },
                    { 2, null, "cc59afb3-afab-4262-baae-1402bb47c608", "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AdditionalInfo", "AddressId", "BirthDay", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "MiddleName", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TwoFactorEnabled", "UserName" },
                values: new object[] { 777, 0, null, null, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "eb86c8d9-84de-4296-9576-2c7a87258220", "danik5311@gmail.com", true, false, null, null, "Daniel", "DANIK5311@GMAIL.COM", "ZAGAMANT", "AQAAAAEAACcQAAAAEO14mrwubTdMmSL8K2eO/w9MZ8G+ywd4H3WiDFRHMV00xdU1PCLPccOo5sdO4ZMbpA==", "375291376955", true, "00000000-0000-0000-0000-000000000000", "Istomin", false, "zagamant" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 101, 777 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 100, 777 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 100, 777 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 101, 777 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 777);
        }
    }
}
