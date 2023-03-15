using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyACTS.Data.Migrations;

public partial class CreatePermissionsTable : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "permissions",
            columns: table => new {
                Id = table.Column<int>(
                        name: "id_permission",
                        type: "int",
                        nullable: false
                    ).Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(
                        name: "name",
                        type: "varchar(255)",
                        nullable: false,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                Description = table.Column<string>(
                        name: "description",
                        type: "text",
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                Action = table.Column<string>(
                        name: "action",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: false,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                Type = table.Column<string>(
                        name: "type",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: false,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                Target = table.Column<string>(
                        name: "target",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: false,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                TargetId = table.Column<int>(
                        name: "id_target",
                        type: "int",
                        nullable: true
                    )
            },
            constraints: table => {
                table.PrimaryKey("PK__permissions", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4")
            .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

        migrationBuilder.CreateIndex(
            name: "IDX__permissions__name",
            table: "permissions",
            column: "name",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(name: "permissions");
    }
}
