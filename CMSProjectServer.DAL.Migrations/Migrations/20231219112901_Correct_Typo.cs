using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CMSProjectServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Correct_Typo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleArticleTag_Tags_TagsArticleId",
                table: "ArticleArticleTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleArticleTag",
                table: "ArticleArticleTag");

            migrationBuilder.DropIndex(
                name: "IX_ArticleArticleTag_TagsArticleId",
                table: "ArticleArticleTag");

            migrationBuilder.DropColumn(
                name: "TagsArticleId",
                table: "ArticleArticleTag");

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Tags",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "ArticleId",
                table: "Tags",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "TagsArticleId",
                table: "ArticleArticleTag",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleArticleTag",
                table: "ArticleArticleTag",
                columns: new[] { "ArticleId", "TagsArticleId" });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleArticleTag_TagsArticleId",
                table: "ArticleArticleTag",
                column: "TagsArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleArticleTag_Tags_TagsArticleId",
                table: "ArticleArticleTag",
                column: "TagsArticleId",
                principalTable: "Tags",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
