using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyACTS.Data.Migrations;

public partial class CreateServicesTable : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "services",
            columns: table => new {
                Id = table.Column<int>(
                        name: "id_service",
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
                Url = table.Column<string>(
                        name: "url",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: false,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                ImageUrl = table.Column<string>(
                        name: "image_url",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: false,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                Icon = table.Column<byte[]>(
                        name: "icon",
                        type: "blob",
                        nullable: true
                    )
            },
            constraints: table => {
                table.PrimaryKey("PK__services", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4")
            .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

        migrationBuilder.CreateIndex(
            name: "IDX__services__name",
            table: "services",
            column: "name",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(name: "services");
    }
}
