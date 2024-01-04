using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProjectServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Site_Content : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b8604e4c-6895-410a-970c-9d13208da754", "AQAAAAEAACcQAAAAEOQy6f8I9QMFCNq3Y/lIDkaPr6ccfB6PO0edAREtFZWoHIV0b/UCQqGmdH+ngSyAMg==", "fc0f0bea-42ed-4945-a660-bf4e3ad9e372" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d51e6aa3-be26-4eaf-87b9-d036fc9ef5db", "AQAAAAEAACcQAAAAEHT7g2nfmVzsxqoHQ1HbQyVn1Oa7fD5Mp6F8SWWluUK4yWRKDHf+Df/l1tZjx7hF3A==", "b47277f6-654f-4ae1-acdf-f09d2699b35f" });
        }
    }
}
