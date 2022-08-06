using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSA_WebAPI.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "id", "createdAt", "name" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9198), "Team1" },
                    { 2, new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9254), "Team2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "birthDay", "email", "firstName", "lastName", "registeredAt", "teamId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9261), "nikitamikhalchenko@gmail.com", "Nikita", "Mikhalchenko", new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9264), null },
                    { 2, new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9269), "email1@gmail.com", "Name1", "LastName1", new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9272), null }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "id", "authorId", "CreateAt", "Deadline", "description", "Name", "teamId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9292), new DateTime(2022, 8, 11, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9294), "Desc1", "Name1", 1 },
                    { 2, 2, new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9301), new DateTime(2022, 7, 22, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9304), "Desc1", "Name1", 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "birthDay", "email", "firstName", "lastName", "registeredAt", "teamId" },
                values: new object[,]
                {
                    { 3, new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9276), "email2@gmail.com", "Name2", "LastName2", new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9279), 1 },
                    { 4, new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9282), "email3@gmail.com", "Name3", "LastName3", new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9285), 2 }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "id", "createdAt", "description", "finishedAt", "name", "performerId", "projectId", "state" },
                values: new object[] { 1, new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9310), "Desc1", null, "Name1", 3, 1, 0 });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "id", "createdAt", "description", "finishedAt", "name", "performerId", "projectId", "state" },
                values: new object[] { 2, new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9316), "Desc2", null, "Name2", 4, 2, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
