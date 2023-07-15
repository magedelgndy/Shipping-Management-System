using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class ConvertColumnOrderStatusToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "104a5399-cc55-41ea-9c74-e96b7a80f02e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d23efc4b-ea7c-493c-a6cf-2db83c7faf38");

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "01/07/2023 10:57:36 م");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2fdf1f8a-ae66-4fff-bdf2-4ee2bb901bae", null, "01/07/2023 10:57:36 م", "المناديب", "المناديب" },
                    { "3970a52b-0086-4c5e-983b-c065d910a1da", null, "01/07/2023 10:57:36 م", "التجار", "التجار" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17e6ea86-2057-4ecb-903d-6900e74bdd5f", "AQAAAAIAAYagAAAAEChasOoyWytk3x8AsDTfHW65fi7zc3vurBN763GmZ8zYWKfKWcEEG5TqQQGO0b94AQ==", "cc2d71d8-9de8-4289-9c50-ead4d5884fc6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fdf1f8a-ae66-4fff-bdf2-4ee2bb901bae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3970a52b-0086-4c5e-983b-c065d910a1da");

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "01/07/2023 06:01:58 م");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "104a5399-cc55-41ea-9c74-e96b7a80f02e", null, "01/07/2023 06:01:58 م", "التجار", "التجار" },
                    { "d23efc4b-ea7c-493c-a6cf-2db83c7faf38", null, "01/07/2023 06:01:58 م", "المناديب", "المناديب" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c47f2c32-ed06-4c17-b9d6-37bbeca4d671", "AQAAAAIAAYagAAAAEG4opaeVyk/RKIBv8vVIAJQ7aYgGUzMM5mn3Eqh8VsH1ERD2hywd15CIfnAtbDTQLQ==", "afb4ed3d-5ef9-4926-aeca-5b5182132efa" });
        }
    }
}
