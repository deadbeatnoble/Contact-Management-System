using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_pract.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingacontactsemailtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactsEmails",
                schema: "cms-pract.Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactsEmails_User_UserProfileId",
                        column: x => x.UserProfileId,
                        principalSchema: "cms-pract.Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactsEmails_UserProfileId",
                schema: "cms-pract.Identity",
                table: "ContactsEmails",
                column: "UserProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactsEmails",
                schema: "cms-pract.Identity");
        }
    }
}
