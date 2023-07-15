using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class AddWeightSettingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "517ef5a6-14f0-43a5-8879-27c6f7281a38");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e0477da-b37a-43c5-ad52-f93d8dcbf291");

            migrationBuilder.CreateTable(
                name: "weightSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Addition_Cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weightSettings", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "weightSettings",
                columns: new[] { "Id", "Addition_Cost", "Cost" },
                values: new object[] { 1, 100, 10 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "weightSettings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a6ce09a-7811-4081-bae4-ddbfc971aa35");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bea046d-b91b-46bf-9f43-7d6f1e6c3e3b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf",
                column: "Date",
                value: "02/07/2023 06:46:29 م");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "517ef5a6-14f0-43a5-8879-27c6f7281a38", null, "02/07/2023 06:46:29 م", "التجار", "التجار" },
                    { "9e0477da-b37a-43c5-ad52-f93d8dcbf291", null, "02/07/2023 06:46:29 م", "المناديب", "المناديب" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7674137-128f-4d0d-972f-b298bcef3441", "AQAAAAIAAYagAAAAEMnjHZVnN5Ora1dvT2CYygPr0+Nz8Z5Cs1yrVquU39oGXI8Tb41VxIg2k0y02qBE2A==", "877fe84f-c964-41dd-bdca-9d5a2ac5bd70" });
        }
    }
}
