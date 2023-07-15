using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRoleEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44c0c619-af54-4a83-b99b-2dfe92223d32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98c1be01-39b5-4b5c-ab9c-adc5494ebb2c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "08/07/2023 06:42:11 م");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13551064-d2ea-40ce-aad3-247a8622e38a", null, "08/07/2023 06:42:11 م", "التجار", "التجار" },
                    { "16da2458-b241-456b-924b-e971e04407cb", null, "08/07/2023 06:42:11 م", "المناديب", "المناديب" },
                    { "177b6a7c-4de3-4254-96fd-dab1dbc4f868", null, "08/07/2023 06:42:11 م", "الموظفين", "الموظفين" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b3be9dbf-3dae-4e06-a12d-b0b9096752cf", "AQAAAAIAAYagAAAAENhoxyCsRNE/GCFMf0iLd6pXejkogtZchq3Y7f/QVthw0e2FAOGsWYq5jf8typHgqA==", "f2693b15-699a-40f9-95e6-4012d550c52e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13551064-d2ea-40ce-aad3-247a8622e38a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16da2458-b241-456b-924b-e971e04407cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "177b6a7c-4de3-4254-96fd-dab1dbc4f868");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "7/5/2023 9:20:23 PM");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44c0c619-af54-4a83-b99b-2dfe92223d32", null, "7/5/2023 9:20:23 PM", "المناديب", "المناديب" },
                    { "98c1be01-39b5-4b5c-ab9c-adc5494ebb2c", null, "7/5/2023 9:20:23 PM", "التجار", "التجار" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "633806e6-cac5-4203-90e5-e5edc991c21d", "AQAAAAIAAYagAAAAED21VEVSDPxTXUvx5qzJBgIcFz3FlsXN+zYwFlcDhzQEdRQGfWTNQsQMi9bUYRDHNg==", "6bb61d7d-5439-4931-8874-0536ba2f1b0e" });
        }
    }
}
