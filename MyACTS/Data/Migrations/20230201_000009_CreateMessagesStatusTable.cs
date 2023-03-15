using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyACTS.Data.Migrations;

public partial class CreateMessagesStatusTable : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "messages_status",
            columns: table => new {
                MessageId = table.Column<int>(
                        name: "id_message",
                        type: "int",
                        nullable: false
                    ),
                UserId = table.Column<int>(
                        name: "id_user",
                        type: "int",
                        nullable: false
                    ),
                Status = table.Column<string>(
                        name: "status",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                Timestamp = table.Column<DateTime>(
                        name: "timestamp",
                        type: "datetime",
                        nullable: true
                    ),
                CurrentSessionToken = table.Column<string>(
                        name: "current_session_token",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table => {
                table.PrimaryKey("PK__messages_status", x => new { x.MessageId, x.UserId })
                    .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                table.ForeignKey(
                    name: "FK__messages_status__messages__id_user",
                    column: x => x.UserId,
                    principalTable: "users",
                    principalColumn: "id_user",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK__messages_status__messages__id_message",
                    column: x => x.MessageId,
                    principalTable: "messages",
                    principalColumn: "id_message",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4")
            .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

        migrationBuilder.CreateIndex(
            name: "IDX__messages_status__id_message",
            table: "messages_status",
            column: "id_message");

        migrationBuilder.CreateIndex(
            name: "IDX__messages_status__id_user",
            table: "messages_status",
            column: "id_user");
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(name: "messages_status");
    }
}
