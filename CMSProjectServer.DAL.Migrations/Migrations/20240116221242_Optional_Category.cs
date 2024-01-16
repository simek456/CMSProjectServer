using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProjectServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Optional_Category : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Articles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "374e159e-f851-43b8-ad36-f01174e4ccc0", "AQAAAAEAACcQAAAAEEzrXUnuNz5H4B+JXEZvm5OF9tsiV0f7PrqVHj7OWyoX8eu0FPILM7jRbjfOcFy4Ww==", "273f1699-6a40-465a-abef-f7c24f44525c" });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Articles",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d1333a31-c147-47b4-a7c7-87ababa01fca", "AQAAAAEAACcQAAAAENxz7GbexjsQigoLWtYr5x0L8+8lxFaA/7B9dYZ4QJ1ogR9OfN+8US/lwDnp9rPGQQ==", "0d3f2cd7-fc8f-4ce5-bf00-292c7d1bb874" });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Categories_CategoryId",
                table: "Articles",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
