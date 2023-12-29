using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProjectServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Article_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Articles",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82a2a086-8c05-4b01-8b52-2a1436890adf", "AQAAAAEAACcQAAAAEAbDsflR9bQSQPFdle3GKxP9FF6imhMTnqLc+Mzxrtijh1m/LPx+Hb0Nbf2FEXi05A==", "9c6d2f9c-6902-4baa-853e-59abf3d6dd3d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Articles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4832353-aecf-40c3-bd13-5e47bc5bbf26", "AQAAAAEAACcQAAAAEJYlGfZNQANDeW6YCj7uryW+4LGV5xZMOixQCWqTqLWvs2Iy4SEtoG5LFj9bdYpSUA==", "e1cf55f9-2fda-48ad-b0c0-c4e211f65d50" });
        }
    }
}
