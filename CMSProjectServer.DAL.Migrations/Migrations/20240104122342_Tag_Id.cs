using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CMSProjectServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Tag_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleArticleTag_Tags_TagsTag",
                table: "ArticleArticleTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleArticleTag",
                table: "ArticleArticleTag");

            migrationBuilder.DropIndex(
                name: "IX_ArticleArticleTag_TagsTag",
                table: "ArticleArticleTag");

            migrationBuilder.DropColumn(
                name: "TagsTag",
                table: "ArticleArticleTag");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Tags",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "TagsId",
                table: "ArticleArticleTag",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleArticleTag",
                table: "ArticleArticleTag",
                columns: new[] { "ArticleId", "TagsId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "614fae53-cce3-401e-b805-78a197c116db", "AQAAAAEAACcQAAAAELMA+Gttx2MlbX1wLOCgOL9sSjBxVrgdI/QtMhsGugoRzq/WpT+dJ0z9ahDXFdGsow==", "b2a95b64-5b4d-4e57-8136-2f8f8e56e8b6" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleArticleTag_TagsId",
                table: "ArticleArticleTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleArticleTag_Tags_TagsId",
                table: "ArticleArticleTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleArticleTag_Tags_TagsId",
                table: "ArticleArticleTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleArticleTag",
                table: "ArticleArticleTag");

            migrationBuilder.DropIndex(
                name: "IX_ArticleArticleTag_TagsId",
                table: "ArticleArticleTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TagsId",
                table: "ArticleArticleTag");

            migrationBuilder.AddColumn<string>(
                name: "TagsTag",
                table: "ArticleArticleTag",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Tag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleArticleTag",
                table: "ArticleArticleTag",
                columns: new[] { "ArticleId", "TagsTag" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82a2a086-8c05-4b01-8b52-2a1436890adf", "AQAAAAEAACcQAAAAEAbDsflR9bQSQPFdle3GKxP9FF6imhMTnqLc+Mzxrtijh1m/LPx+Hb0Nbf2FEXi05A==", "9c6d2f9c-6902-4baa-853e-59abf3d6dd3d" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleArticleTag_TagsTag",
                table: "ArticleArticleTag",
                column: "TagsTag");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleArticleTag_Tags_TagsTag",
                table: "ArticleArticleTag",
                column: "TagsTag",
                principalTable: "Tags",
                principalColumn: "Tag",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
