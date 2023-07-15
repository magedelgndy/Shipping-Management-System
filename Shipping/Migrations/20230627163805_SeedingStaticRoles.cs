using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class SeedingStaticRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "27/06/2023 07:38:05 م");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "79dc327d-2708-4496-ab20-3fef9cd29947", null, "27/06/2023 07:38:05 م", "التجار", "التجار" },
                    { "d646a6ef-743d-41b7-bf3f-6e117678eaba", null, "27/06/2023 07:38:05 م", "المناديب", "المناديب" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41af7030-2a15-4853-bc2b-3356010e3f08", "AQAAAAIAAYagAAAAEBblRFkhvjVchGpnzUTAkNfyvJfnx5fQ4AQVRMJB5HJRFXuts996PN0ddlFrVoY1Cg==", "258a6d73-973a-4644-993c-85718584aa9b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79dc327d-2708-4496-ab20-3fef9cd29947");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d646a6ef-743d-41b7-bf3f-6e117678eaba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "27/06/2023 05:50:13 م");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbe3e7e9-2465-463a-a5f0-6fd7d09978d8", "AQAAAAIAAYagAAAAECaPSq7PzeCl/tSznIMSP+cNlHulwuHS2AmGWOjYtQZ0livE/9oPjaXFyay0KDtHtg==", "4c1ed4e4-96ac-4b9e-aa2d-9b47e39a5d03" });
        }
    }
}
