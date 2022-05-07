using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityLayer.Migrations
{
    public partial class PermissionFilter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRolesAsString",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "RoleGroups",
                columns: table => new
                {
                    RoleGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroups", x => x.RoleGroupID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleIDForBitwise = table.Column<long>(type: "bigint", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleGroupID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                    table.ForeignKey(
                        name: "FK_Roles_RoleGroups_RoleGroupID",
                        column: x => x.RoleGroupID,
                        principalTable: "RoleGroups",
                        principalColumn: "RoleGroupID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserRoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Roles = table.Column<long>(type: "bigint", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    RoleGroupID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleID);
                    table.ForeignKey(
                        name: "FK_UserRole_RoleGroups_RoleGroupID",
                        column: x => x.RoleGroupID,
                        principalTable: "RoleGroups",
                        principalColumn: "RoleGroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "RoleGroups",
                columns: new[] { "RoleGroupID", "GroupName" },
                values: new object[] { 1, "User" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleID", "RoleGroupID", "Roles", "UserID" },
                values: new object[] { 2, 2, 15L, 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "UserGuidID",
                value: new Guid("ac4d7700-a339-4b56-b1cf-d38aa6ec9e6c"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "UserGuidID",
                value: new Guid("7828c2e8-69a6-4578-93dd-7575d63740df"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "RoleGroupID", "RoleIDForBitwise", "RoleName" },
                values: new object[,]
                {
                    { 1, 1, 1L, "GetUser" },
                    { 2, 1, 2L, "Get" },
                    { 3, 1, 4L, "Post" },
                    { 4, 1, 8L, "Update" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleID", "RoleGroupID", "Roles", "UserID" },
                values: new object[] { 1, 1, 7L, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleGroupID",
                table: "Roles",
                column: "RoleGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleGroupID",
                table: "UserRole",
                column: "RoleGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserID",
                table: "UserRole",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "RoleGroups");

            migrationBuilder.AddColumn<string>(
                name: "UserRolesAsString",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "UserGuidID", "UserRolesAsString" },
                values: new object[] { new Guid("e67402a3-d6ed-443c-b81e-7dd2ba09052a"), "[\"Get\"]" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "UserGuidID", "UserRolesAsString" },
                values: new object[] { new Guid("c5165054-d2bd-41a1-91c2-30657e712a56"), "[\"Get\"]" });
        }
    }
}
