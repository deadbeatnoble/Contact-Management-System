using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_pract.Data.Migrations
{
    /// <inheritdoc />
    public partial class removedtokenfromtheinvitationtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                schema: "cms-pract.Identity",
                table: "Invitations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                schema: "cms-pract.Identity",
                table: "Invitations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
