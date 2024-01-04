using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProjectServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cf86c6c1-6dc9-4247-bbf4-4bf0dc31a345", "cf86c6c1-6dc9-4247-bbf4-4bf0dc31a345", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "About", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b7ad606a-2f3d-4ff5-89f4-278100d10b85", null, 0, "e4832353-aecf-40c3-bd13-5e47bc5bbf26", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", false, false, null, "FirstAdmin", null, "ADMIN", "AQAAAAEAACcQAAAAEJYlGfZNQANDeW6YCj7uryW+4LGV5xZMOixQCWqTqLWvs2Iy4SEtoG5LFj9bdYpSUA==", null, false, "e1cf55f9-2fda-48ad-b0c0-c4e211f65d50", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cf86c6c1-6dc9-4247-bbf4-4bf0dc31a345", "b7ad606a-2f3d-4ff5-89f4-278100d10b85" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cf86c6c1-6dc9-4247-bbf4-4bf0dc31a345", "b7ad606a-2f3d-4ff5-89f4-278100d10b85" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf86c6c1-6dc9-4247-bbf4-4bf0dc31a345");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85");
        }
    }
}