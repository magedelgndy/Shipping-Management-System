using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class AddColumn_arabicNameToClaimsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00ae9eda-1a44-4649-976b-44f18d061f81");

            migrationBuilder.AddColumn<string>(
                name: "ArabicName",
                table: "AspNetRoleClaims",
                type: "nvarchar(max)",
                nullable: true);

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
                value: "26/06/2023 05:31:24 م");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[] { "c1af8ae6-77df-414d-b081-3d79a10a490e", null, "26/06/2023 05:31:24 م", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9877dd21-6998-4259-a356-75b7704f0b53", "AQAAAAIAAYagAAAAEMJ+WSti06r2Lq9HXtoSrMyu/Ikh0xvG7YHL6tMM33RZqop2M6/V+3eXq7H7dhTiGA==", "5edb2c85-f9e9-4a26-84b3-d115f5224618" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1af8ae6-77df-414d-b081-3d79a10a490e");

            migrationBuilder.DropColumn(
                name: "ArabicName",
                table: "AspNetRoleClaims");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "26/06/2023 04:46:39 م");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[] { "00ae9eda-1a44-4649-976b-44f18d061f81", null, "26/06/2023 04:46:39 م", "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dea8f72e-55d2-4637-8d95-6df53eb30de4", "AQAAAAIAAYagAAAAEMPD73nzzA0hZmhYJ7/FODSNLkd4S/+tPzXkA1dlBoJK39lwdNTrwNjv6caI2EgH+g==", "720a1bdd-8825-46c8-be68-1648585a0ccc" });
        }
    }
}
