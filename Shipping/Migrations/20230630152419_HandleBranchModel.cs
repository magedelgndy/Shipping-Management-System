using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class HandleBranchModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "396e3f3f-6874-4228-ad7d-3bd9c8564580");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89cea0ae-00b5-4bd1-92a9-9185a07bbeff");

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "ArabicName",
                value: "صلاحيات المجموعات");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "ArabicName",
                value: "صلاحيات المجموعات");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "ArabicName",
                value: "صلاحيات المجموعات");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "ArabicName",
                value: "صلاحيات المجموعات");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "30/06/2023 06:24:18 م");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6f7f5712-8875-4d91-8c0d-673b2eb759bd", null, "30/06/2023 06:24:18 م", "المناديب", "المناديب" },
                    { "bd2e8491-5539-4fd9-b12b-779fb537fdfb", null, "30/06/2023 06:24:18 م", "التجار", "التجار" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b1d003f2-ac34-466c-b8b2-2d07c2527141", "AQAAAAIAAYagAAAAENc52v/o0AYhzgsYUKb8LWAXOHz2nZGGOtemuPhZxH269WHdMfrvrlhw75fmkY2zdg==", "2db41352-c2cd-4c61-a936-f621742b3c61" });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_StateId",
                table: "Branches",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_States_StateId",
                table: "Branches",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_States_StateId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_StateId",
                table: "Branches");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f7f5712-8875-4d91-8c0d-673b2eb759bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd2e8491-5539-4fd9-b12b-779fb537fdfb");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Branches");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1,
                column: "ArabicName",
                value: "الصلاحيات");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2,
                column: "ArabicName",
                value: "الصلاحيات");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3,
                column: "ArabicName",
                value: "الصلاحيات");

            migrationBuilder.UpdateData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4,
                column: "ArabicName",
                value: "الصلاحيات");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "6/29/2023 9:43:16 AM");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "396e3f3f-6874-4228-ad7d-3bd9c8564580", null, "6/29/2023 9:43:16 AM", "التجار", "التجار" },
                    { "89cea0ae-00b5-4bd1-92a9-9185a07bbeff", null, "6/29/2023 9:43:16 AM", "المناديب", "المناديب" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a657f5f-ece0-4b7a-ab24-9fcf53de803e", "AQAAAAIAAYagAAAAEArtwmaO7lAtPUsmQGjO9CR2CYs0SLqhQD94MoBPjlqTsFBgmtyTzDgMjDO1IyJLUA==", "a1ba6320-5a0c-4803-afd5-742a496b7f07" });
        }
    }
}
