using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMSProjectServer.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Unify_Name_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleArticleCategory_Categories_TagsId",
                table: "ArticleArticleCategory");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "ArticleArticleCategory",
                newName: "CategoriesId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleArticleCategory_TagsId",
                table: "ArticleArticleCategory",
                newName: "IX_ArticleArticleCategory_CategoriesId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d51e6aa3-be26-4eaf-87b9-d036fc9ef5db", "AQAAAAEAACcQAAAAEHT7g2nfmVzsxqoHQ1HbQyVn1Oa7fD5Mp6F8SWWluUK4yWRKDHf+Df/l1tZjx7hF3A==", "b47277f6-654f-4ae1-acdf-f09d2699b35f" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleArticleCategory_Categories_CategoriesId",
                table: "ArticleArticleCategory",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleArticleCategory_Categories_CategoriesId",
                table: "ArticleArticleCategory");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "ArticleArticleCategory",
                newName: "TagsId");

            migrationBuilder.RenameIndex(
                name: "IX_ArticleArticleCategory_CategoriesId",
                table: "ArticleArticleCategory",
                newName: "IX_ArticleArticleCategory_TagsId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ad606a-2f3d-4ff5-89f4-278100d10b85",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7ac455fc-a0cb-405b-9420-18a455678806", "AQAAAAEAACcQAAAAEK8PtVox52DdyMnpTsnt/z1RMY2k2aa5qBFzuDRDLNXMUJUtar9B3+MgwTQvptiaFw==", "1b3bf205-89e4-429c-99d3-7f38d028aa43" });

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleArticleCategory_Categories_TagsId",
                table: "ArticleArticleCategory",
                column: "TagsId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
