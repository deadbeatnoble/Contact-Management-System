using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_pract.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingisselectedasacolumntocontactemailtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                schema: "cms-pract.Identity",
                table: "ContactsEmails",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                schema: "cms-pract.Identity",
                table: "ContactsEmails");
        }
    }
}
