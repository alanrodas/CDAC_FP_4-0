using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyACTS.Data.Migrations;

public partial class CreateEventsStatusTable : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "events_status",
            columns: table => new {
                EventId = table.Column<int>(
                        name: "id_event",
                        type: "int",
                        nullable: false
                    ),
                UserId = table.Column<int>(
                        name: "id_user",
                        type: "int",
                        nullable: false
                    ),
                Status = table.Column<string>(
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                Timestamp = table.Column<DateTime>(
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
                table.PrimaryKey("PK__events_status", x => new { x.EventId, x.UserId })
                    .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
            })
            .Annotation("MySql:CharSet", "utf8mb4")
            .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

        migrationBuilder.CreateIndex(
            name: "IDX__events_status__id_event",
            table: "events_status",
            column: "id_event");

        migrationBuilder.CreateIndex(
            name: "IDX__events_status__id_user",
            table: "events_status",
            column: "id_user");
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(name: "events");
    }
}
