using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyACTS.Data.Migrations;

public partial class CreateMessagesTable : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "messages",
            columns: table => new {
                Id = table.Column<int>(
                        name: "id_message",
                        type: "int",
                        nullable: false
                    ).Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Subject = table.Column<string>(
                        name: "subject",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: false,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                Body = table.Column<string>(
                        name: "body",
                        type: "text",
                        nullable: false,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                ActionText = table.Column<string>(
                        name: "action_text",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                ActionUrl = table.Column<string>(
                        name: "action_url",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                Source = table.Column<int>(
                        name: "source",
                        type: "int",
                        nullable: false
                    ),
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
                table.PrimaryKey("PK__messages", x => x.Id);
                table.ForeignKey(
                    name: "FK__messages__users__source",
                    column: x => x.Source,
                    principalTable: "users",
                    principalColumn: "id_user");
            })
            .Annotation("MySql:CharSet", "utf8mb4")
            .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

        migrationBuilder.CreateIndex(
            name: "IDX__messages__source",
            table: "messages",
            column: "source");

        migrationBuilder.CreateIndex(
            name: "IDX__messages__target",
            table: "messages",
            column: "target");

        migrationBuilder.CreateIndex(
            name: "IDX__messages__id_target_",
            table: "messages",
            column: "id_target");
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(name: "messages");
    }
}
