using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSA_WebAPI.Migrations
{
    public partial class Renaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Projects",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Deadline",
                table: "Projects",
                newName: "deadline");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Projects",
                newName: "createdAt");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "createdAt", "deadline" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4410), new DateTime(2022, 8, 11, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4412) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "createdAt", "deadline" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4492), new DateTime(2022, 7, 22, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4495) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "id",
                keyValue: 1,
                column: "createdAt",
                value: new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4501));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "id",
                keyValue: 2,
                column: "createdAt",
                value: new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4507));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "id",
                keyValue: 1,
                column: "createdAt",
                value: new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4322));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "id",
                keyValue: 2,
                column: "createdAt",
                value: new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4373));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "birthDay", "registeredAt" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4380), new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4383) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "birthDay", "registeredAt" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4388), new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4391) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "birthDay", "registeredAt" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4395), new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4398) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "birthDay", "registeredAt" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4401), new DateTime(2022, 7, 12, 16, 10, 50, 98, DateTimeKind.Local).AddTicks(4404) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Projects",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "deadline",
                table: "Projects",
                newName: "Deadline");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Projects",
                newName: "CreateAt");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "CreateAt", "Deadline" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9292), new DateTime(2022, 8, 11, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9294) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "CreateAt", "Deadline" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9301), new DateTime(2022, 7, 22, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9304) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "id",
                keyValue: 1,
                column: "createdAt",
                value: new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9310));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "id",
                keyValue: 2,
                column: "createdAt",
                value: new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9316));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "id",
                keyValue: 1,
                column: "createdAt",
                value: new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9198));

            migrationBuilder.UpdateData(
                table: "Teams",
                keyColumn: "id",
                keyValue: 2,
                column: "createdAt",
                value: new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9254));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "birthDay", "registeredAt" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9261), new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9264) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "birthDay", "registeredAt" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9269), new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9272) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "birthDay", "registeredAt" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9276), new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9279) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "id",
                keyValue: 4,
                columns: new[] { "birthDay", "registeredAt" },
                values: new object[] { new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9282), new DateTime(2022, 7, 12, 16, 7, 41, 758, DateTimeKind.Local).AddTicks(9285) });
        }
    }
}
