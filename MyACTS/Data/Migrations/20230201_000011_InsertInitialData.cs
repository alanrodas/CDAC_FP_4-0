using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyACTS.Data.Migrations;

public partial class InsertInitialData : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
        migrationBuilder.InsertData(table: "roles",
            columns: new string[] { "id_role", "name", "description" },
            values: new object[] { 1, "admin", "Administrator Role" }
        );
        migrationBuilder.InsertData(table: "roles",
            columns: new string[] { "id_role", "name", "description" },
            values: new object[] { 2, "user", "Regular User Role" }
        );

        migrationBuilder.InsertData(table: "users",
            columns: new string[] { "id_user", "user_name", "full_name", "email", "phone_number", "date_of_birth", "password_hash" },
            values: new object[] { 1, "admin", "System Admin", "admin@system.com", "123456789", new DateOnly(), CryptoHelper.Crypto.HashPassword("123456789") }
        );
        migrationBuilder.InsertData(table: "users",
            columns: new string[] { "id_user", "user_name", "full_name", "email", "phone_number", "date_of_birth", "password_hash" },
            values: new object[] { 2, "user", "Regular User", "user@system.com", "123456789", new DateOnly(), CryptoHelper.Crypto.HashPassword("123456789") }
        );

        migrationBuilder.InsertData(table: "user_roles",
            columns: new string[] { "id_user", "id_role"},
            values: new object[] { 1, 1 }
        );

        migrationBuilder.InsertData(table: "user_roles",
            columns: new string[] { "id_user", "id_role" },
            values: new object[] { 2, 2 }
        );


    }

    protected override void Down(MigrationBuilder migrationBuilder) {
        migrationBuilder.DeleteData(table: "user_roles",
            keyColumns: new string[] { "id_user", "id_role" }, keyValues: new object[] { 1, 1 }
        );

        migrationBuilder.DeleteData(table: "user_roles",
            keyColumns: new string[] { "id_user", "id_role" }, keyValues: new object[] { 2, 2 }
        );

        migrationBuilder.DeleteData(table: "users",
            keyColumns: new string[] { "id_user" }, keyValues: new object[] { 1 }
        );

        migrationBuilder.DeleteData(table: "users",
            keyColumns: new string[] { "id_user" }, keyValues: new object[] { 2 }
        );

        migrationBuilder.DeleteData(table: "roles",
            keyColumns: new string[] { "id_role" }, keyValues: new object[] { 1 }
        );

        migrationBuilder.DeleteData(table: "roles",
            keyColumns: new string[] { "id_role" }, keyValues: new object[] { 2 }
        );
    }
}
