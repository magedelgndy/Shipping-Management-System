using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class AddingshippingCostForOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "970dcdbc-c88b-410a-b50b-2bc9d3367818");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fc7ac835-145d-40a7-80be-b39b4d8add6e");

            migrationBuilder.AddColumn<int>(
                name: "ShippingCost",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44c0c619-af54-4a83-b99b-2dfe92223d32");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98c1be01-39b5-4b5c-ab9c-adc5494ebb2c");

            migrationBuilder.DropColumn(
                name: "ShippingCost",
                table: "Orders");

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
    }
}
