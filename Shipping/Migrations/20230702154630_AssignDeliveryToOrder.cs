using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shipping.Migrations
{
    /// <inheritdoc />
    public partial class AssignDeliveryToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2fdf1f8a-ae66-4fff-bdf2-4ee2bb901bae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3970a52b-0086-4c5e-983b-c065d910a1da");

            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryId",
                table: "Orders",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BranchId",
                table: "Orders",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryId",
                table: "Orders",
                column: "DeliveryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Branches_BranchId",
                table: "Orders",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Deliveries_DeliveryId",
                table: "Orders",
                column: "DeliveryId",
                principalTable: "Deliveries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Branches_BranchId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Deliveries_DeliveryId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BranchId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryId",
                table: "Orders");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "517ef5a6-14f0-43a5-8879-27c6f7281a38");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e0477da-b37a-43c5-ad52-f93d8dcbf291");

            migrationBuilder.DropColumn(
                name: "BranchId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "Orders");

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
    }
}
