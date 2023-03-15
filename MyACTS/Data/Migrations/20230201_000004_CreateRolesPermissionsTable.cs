using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyACTS.Data.Migrations;

public partial class CreateRolesPermissionsTable : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "roles_permissions",
            columns: table => new {
                RoleId = table.Column<int>(
                        name: "id_role",
                        type: "int",
                        nullable: false
                    ),
                PermissionId = table.Column<int>(
                        name: "id_permission",
                        type: "int",
                        nullable: false
                    )
            },
            constraints: table => {
                table.PrimaryKey("PK__role_permissions", x => new { x.RoleId, x.PermissionId })
                    .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                table.ForeignKey(
                    name: "FK__role_permissions__permissions__id_permission",
                    column: x => x.PermissionId,
                    principalTable: "permissions",
                    principalColumn: "id_permission",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK__role_permissions__roles__id_role",
                    column: x => x.RoleId,
                    principalTable: "roles",
                    principalColumn: "id_role",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4")
            .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

        migrationBuilder.CreateIndex(
            name: "IDX__roles_permissions__id_role",
            table: "roles_permissions",
            column: "id_role");

        migrationBuilder.CreateIndex(
            name: "IDX__roles_permissions__id_permission",
            table: "roles_permissions",
            column: "id_permission");
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(name: "roles_permissions");
    }
}
