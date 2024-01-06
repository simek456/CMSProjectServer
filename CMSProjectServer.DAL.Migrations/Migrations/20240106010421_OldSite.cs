using System;
using CMSProjectServer.Domain.Entities.SiteContents;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CMSProjectServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class OldSite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_AspNetUsers_ChangeAuthorId",
                table: "Site");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Site",
                table: "Site");

            migrationBuilder.RenameTable(
                name: "Site",
                newName: "CurrentSites");

            migrationBuilder.RenameIndex(
                name: "IX_Site_Name",
                table: "CurrentSites",
                newName: "IX_CurrentSites_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Site_ChangeAuthorId",
                table: "CurrentSites",
                newName: "IX_CurrentSites_ChangeAuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentSites",
                table: "CurrentSites",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HistoricSites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    SiteContent = table.Column<Contents>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ChangeAuthorId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricSites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricSites_AspNetUsers_ChangeAuthorId",
                        column: x => x.ChangeAuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e849f86e-2ee0-4685-b8cf-21d5d458e053", "AQAAAAEAACcQAAAAEDzz/dV3l0Vko5YkskDg6v+H00GeGWy9AC3uZ8RE9JweGj3qT6chA+cMVV7WrZTHqA==", "21e13cf9-56d3-4e36-99af-17bafe0ef3b9" });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricSites_ChangeAuthorId",
                table: "HistoricSites",
                column: "ChangeAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricSites_Name",
                table: "HistoricSites",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentSites_AspNetUsers_ChangeAuthorId",
                table: "CurrentSites",
                column: "ChangeAuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentSites_AspNetUsers_ChangeAuthorId",
                table: "CurrentSites");

            migrationBuilder.DropTable(
                name: "HistoricSites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentSites",
                table: "CurrentSites");

            migrationBuilder.RenameTable(
                name: "CurrentSites",
                newName: "Site");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentSites_Name",
                table: "Site",
                newName: "IX_Site_Name");

            migrationBuilder.RenameIndex(
                name: "IX_CurrentSites_ChangeAuthorId",
                table: "Site",
                newName: "IX_Site_ChangeAuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Site",
                table: "Site",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65623ab1-2c54-42f7-87eb-63f10b5388dc", "AQAAAAEAACcQAAAAECy72LmErlwb1py1SQurZE7pmaKAeg23QYbIZYtmAygtKz/NhhxkZQUAQD1k1FVICA==", "f0798db1-4a9f-4e1f-88b7-0cb5ac23abd3" });

            migrationBuilder.AddForeignKey(
                name: "FK_Site_AspNetUsers_ChangeAuthorId",
                table: "Site",
                column: "ChangeAuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
