using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books.Core.Infra.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Authors");

            migrationBuilder.EnsureSchema(
                name: "Books");

            migrationBuilder.EnsureSchema(
                name: "Categories");

            migrationBuilder.EnsureSchema(
                name: "Publishers");

            migrationBuilder.CreateTable(
                name: "Author",
                schema: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 10, 31, 20, 19, 41, 567, DateTimeKind.Local).AddTicks(675)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    ParentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 10, 31, 20, 19, 41, 566, DateTimeKind.Local).AddTicks(8566)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalSchema: "Categories",
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                schema: "Publishers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 10, 31, 20, 19, 41, 566, DateTimeKind.Local).AddTicks(8197)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    Format = table.Column<int>(type: "int", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Isbn10 = table.Column<string>(type: "varchar(10)", nullable: true),
                    Isbn13 = table.Column<string>(type: "varchar(13)", nullable: true),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PublisherId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 10, 31, 20, 19, 41, 566, DateTimeKind.Local).AddTicks(8920)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "Authors",
                        principalTable: "Author",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Book_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Categories",
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Book_Publisher_PublisherId",
                        column: x => x.PublisherId,
                        principalSchema: "Publishers",
                        principalTable: "Publisher",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Keyword",
                schema: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 10, 31, 20, 19, 41, 566, DateTimeKind.Local).AddTicks(9271)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keyword_Book_BookId",
                        column: x => x.BookId,
                        principalSchema: "Books",
                        principalTable: "Book",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Quote",
                schema: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 10, 31, 20, 19, 41, 566, DateTimeKind.Local).AddTicks(9798)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quote_Book_BookId",
                        column: x => x.BookId,
                        principalSchema: "Books",
                        principalTable: "Book",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                schema: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    AuthorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AuthorNameInfo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReviewType = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 10, 31, 20, 19, 41, 567, DateTimeKind.Local).AddTicks(245)),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Removed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Book_BookId",
                        column: x => x.BookId,
                        principalSchema: "Books",
                        principalTable: "Book",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_AuthorId",
                schema: "Books",
                table: "Book",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryId",
                schema: "Books",
                table: "Book",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_PublisherId",
                schema: "Books",
                table: "Book",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                schema: "Categories",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_BookId",
                schema: "Books",
                table: "Keyword",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Quote_BookId",
                schema: "Books",
                table: "Quote",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_BookId",
                schema: "Books",
                table: "Review",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keyword",
                schema: "Books");

            migrationBuilder.DropTable(
                name: "Quote",
                schema: "Books");

            migrationBuilder.DropTable(
                name: "Review",
                schema: "Books");

            migrationBuilder.DropTable(
                name: "Book",
                schema: "Books");

            migrationBuilder.DropTable(
                name: "Author",
                schema: "Authors");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Categories");

            migrationBuilder.DropTable(
                name: "Publisher",
                schema: "Publishers");
        }
    }
}
