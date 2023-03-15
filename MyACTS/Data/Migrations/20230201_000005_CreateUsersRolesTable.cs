using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyACTS.Data.Migrations;

public partial class CreateUserRolesTable : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "user_roles",
            columns: table => new {
                UserId = table.Column<int>(
                        name: "id_user",
                        type: "int",
                        nullable: false
                    ),
                RoleId = table.Column<int>(
                        name: "id_role",
                        type: "int",
                        nullable: false
                    )
            },
            constraints: table => {
                table.PrimaryKey("PK__user_roles", x => new { x.UserId, x.RoleId })
                    .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                table.ForeignKey(
                    name: "FK__user_roles__roles__id_role",
                    column: x => x.RoleId,
                    principalTable: "roles",
                    principalColumn: "id_role",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK__user_roles__users__id_user",
                    column: x => x.UserId,
                    principalTable: "users",
                    principalColumn: "id_user",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4")
            .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

        migrationBuilder.CreateIndex(
            name: "IDX__user_roles__id_user",
            table: "user_roles",
            column: "id_user");

        migrationBuilder.CreateIndex(
            name: "IDX__user_roles__id_role",
            table: "user_roles",
            column: "id_role");
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(name: "user_roles");
    }
}
