using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProjectServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Liking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67e34425-5c07-4ce9-a443-066ce5a12749", "AQAAAAEAACcQAAAAEIJcDTFUnpml0rBmySHh92E9EYwwPuqc8OwjW6sK3L/ZBlb1sJcZuRaqB+26x1klVg==", "fe5f8227-13f7-42ac-a711-f836adb8aef9" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e849f86e-2ee0-4685-b8cf-21d5d458e053", "AQAAAAEAACcQAAAAEDzz/dV3l0Vko5YkskDg6v+H00GeGWy9AC3uZ8RE9JweGj3qT6chA+cMVV7WrZTHqA==", "21e13cf9-56d3-4e36-99af-17bafe0ef3b9" });
        }
    }
}
