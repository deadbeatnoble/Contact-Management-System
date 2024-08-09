using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_pract.Data.Migrations
{
    /// <inheritdoc />
    public partial class forgottoaddtheemailcolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "cms-pract.Identity",
                table: "ContactsEmails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "cms-pract.Identity",
                table: "ContactsEmails");
        }
    }
}
