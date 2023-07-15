using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class makeorderproductsNamestring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a6ce09a-7811-4081-bae4-ddbfc971aa35");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bea046d-b91b-46bf-9f43-7d6f1e6c3e3b");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "OrderProducts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "7/3/2023 5:03:23 AM");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "970dcdbc-c88b-410a-b50b-2bc9d3367818", null, "7/3/2023 5:03:23 AM", "التجار", "التجار" },
                    { "fc7ac835-145d-40a7-80be-b39b4d8add6e", null, "7/3/2023 5:03:23 AM", "المناديب", "المناديب" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c79889b4-bbb9-4674-a904-15a496764b57", "AQAAAAIAAYagAAAAEGuLBzeUAk39lRjQQdSP2rtxhg8G2tdKUkbu4H5lxNJCIKf2DUUGgodeiIH6Rh4t4g==", "aa5686cb-6352-4170-b2e0-b9a34073cf42" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "970dcdbc-c88b-410a-b50b-2bc9d3367818");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc7ac835-145d-40a7-80be-b39b4d8add6e");

            migrationBuilder.AlterColumn<int>(
                name: "ProductName",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "03/07/2023 03:21:33 ص");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a6ce09a-7811-4081-bae4-ddbfc971aa35", null, "03/07/2023 03:21:33 ص", "التجار", "التجار" },
                    { "7bea046d-b91b-46bf-9f43-7d6f1e6c3e3b", null, "03/07/2023 03:21:33 ص", "المناديب", "المناديب" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4d3cf17-5a08-4142-8e3c-4a8826f0f0dd", "AQAAAAIAAYagAAAAEKSxXHdKXucVj6xVtOj73YO1yIjfs9hALpcTNOt/HiXFhg+GFfuSVax2Vhwrq1qrVw==", "99d5a0d4-24d0-43a1-a407-e597d0914dae" });
        }
    }
}
