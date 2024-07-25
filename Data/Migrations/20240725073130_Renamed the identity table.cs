using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_pract.Data.Migrations
{
    /// <inheritdoc />
    public partial class Renamedtheidentitytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.EnsureSchema(
                name: "cms-pract.Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "UserTokens",
                newSchema: "cms-pract.Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "User",
                newSchema: "cms-pract.Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "UserRoles",
                newSchema: "cms-pract.Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "UserLogins",
                newSchema: "cms-pract.Identity");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "UserClaims",
                newSchema: "cms-pract.Identity");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "Role",
                newSchema: "cms-pract.Identity");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "RoleClaims",
                newSchema: "cms-pract.Identity");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "cms-pract.Identity",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "cms-pract.Identity",
                table: "UserLogins",
                newName: "IX_UserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "cms-pract.Identity",
                table: "UserClaims",
                newName: "IX_UserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "cms-pract.Identity",
                table: "RoleClaims",
                newName: "IX_RoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                schema: "cms-pract.Identity",
                table: "UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "cms-pract.Identity",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                schema: "cms-pract.Identity",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogins",
                schema: "cms-pract.Identity",
                table: "UserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserClaims",
                schema: "cms-pract.Identity",
                table: "UserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                schema: "cms-pract.Identity",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaims",
                schema: "cms-pract.Identity",
                table: "RoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaims_Role_RoleId",
                schema: "cms-pract.Identity",
                table: "RoleClaims",
                column: "RoleId",
                principalSchema: "cms-pract.Identity",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaims_User_UserId",
                schema: "cms-pract.Identity",
                table: "UserClaims",
                column: "UserId",
                principalSchema: "cms-pract.Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogins_User_UserId",
                schema: "cms-pract.Identity",
                table: "UserLogins",
                column: "UserId",
                principalSchema: "cms-pract.Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Role_RoleId",
                schema: "cms-pract.Identity",
                table: "UserRoles",
                column: "RoleId",
                principalSchema: "cms-pract.Identity",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_User_UserId",
                schema: "cms-pract.Identity",
                table: "UserRoles",
                column: "UserId",
                principalSchema: "cms-pract.Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_User_UserId",
                schema: "cms-pract.Identity",
                table: "UserTokens",
                column: "UserId",
                principalSchema: "cms-pract.Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaims_Role_RoleId",
                schema: "cms-pract.Identity",
                table: "RoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaims_User_UserId",
                schema: "cms-pract.Identity",
                table: "UserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogins_User_UserId",
                schema: "cms-pract.Identity",
                table: "UserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Role_RoleId",
                schema: "cms-pract.Identity",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_User_UserId",
                schema: "cms-pract.Identity",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_User_UserId",
                schema: "cms-pract.Identity",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                schema: "cms-pract.Identity",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                schema: "cms-pract.Identity",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogins",
                schema: "cms-pract.Identity",
                table: "UserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserClaims",
                schema: "cms-pract.Identity",
                table: "UserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "cms-pract.Identity",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaims",
                schema: "cms-pract.Identity",
                table: "RoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                schema: "cms-pract.Identity",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "UserTokens",
                schema: "cms-pract.Identity",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                schema: "cms-pract.Identity",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "UserLogins",
                schema: "cms-pract.Identity",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "UserClaims",
                schema: "cms-pract.Identity",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "cms-pract.Identity",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "RoleClaims",
                schema: "cms-pract.Identity",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "cms-pract.Identity",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLogins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserClaims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RoleClaims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
