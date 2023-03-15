using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyACTS.Data.Migrations;

public partial class CreateUsersTable : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.CreateTable(
            name: "users",
            columns: table => new {
                Id = table.Column<int>(
                        name: "id_user",
                        type: "int",
                        nullable: false
                    ).Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                UserName = table.Column<string>(
                        name: "user_name",
                        type: "varchar(255)",
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                FullName = table.Column<string>(
                        name: "full_name",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                Email = table.Column<string>(
                        name: "email",
                        type: "varchar(255)",
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                PhoneNumber = table.Column<string>(
                        name: "phone_number",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                DateOfBirth = table.Column<DateOnly>(
                        name: "date_of_birth",
                        type: "date",
                        nullable: true
                    ),
                PasswordHash = table.Column<string>(
                        name: "password_hash",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                /*
                NormalizedUserName = table.Column<string>(
                        name: "normalized_user_name",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                NormalizedEmail = table.Column<string>(
                        name: "normalized_email",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                EmailConfirmed = table.Column<bool>(
                        name: "email_confirmed",
                        type: "bool"
                    ),
                PhoneNumberConfirmed = table.Column<bool>(
                        name: "phone_number_confirmed",
                        type: "bool"
                    ),
                TwoFactorEnabled = table.Column<bool>(
                        name: "two_factor_enabled",
                        type: "bool"
                    ),
                LockoutEnabled = table.Column<bool>(
                        name: "lockout_enabled",
                        type: "bool"
                    ),
                LockoutEnd = table.Column<DateTimeOffset>(
                        name: "lockout_end",
                        type: "timestamp",
                        nullable: true
                    ),
                SecurityStamp = table.Column<string>(
                        name: "security_stamp",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                ConcurrencyStamp = table.Column<string>(
                        name: "concurrency_stamp",
                        type: "varchar(255)",
                        maxLength: 255,
                        nullable: true,
                        collation: "utf8mb4_0900_ai_ci"
                    ).Annotation("MySql:CharSet", "utf8mb4"),
                AccessFailedCount = table.Column<int>(
                        name: "access_failed_count",
                        type: "int",
                        nullable: false
                    )
                */
            },
            constraints: table => {
                table.PrimaryKey("PK__users", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4")
            .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

        migrationBuilder.CreateIndex(
            name: "IDX__users__user_name",
            table: "users",
            column: "user_name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IDX__users__email",
            table: "users",
            column: "email",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DropTable(name: "users");
    }
}
