using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProjectServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SiteName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Site",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65623ab1-2c54-42f7-87eb-63f10b5388dc", "AQAAAAEAACcQAAAAECy72LmErlwb1py1SQurZE7pmaKAeg23QYbIZYtmAygtKz/NhhxkZQUAQD1k1FVICA==", "f0798db1-4a9f-4e1f-88b7-0cb5ac23abd3" });

            migrationBuilder.CreateIndex(
                name: "IX_Site_Name",
                table: "Site",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Site_Name",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Site");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8604e4c-6895-410a-970c-9d13208da754", "AQAAAAEAACcQAAAAEOQy6f8I9QMFCNq3Y/lIDkaPr6ccfB6PO0edAREtFZWoHIV0b/UCQqGmdH+ngSyAMg==", "fc0f0bea-42ed-4945-a660-bf4e3ad9e372" });
        }
    }
}
