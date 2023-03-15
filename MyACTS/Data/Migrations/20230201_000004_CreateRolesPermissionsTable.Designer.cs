﻿// <auto-generated />
using System;
using MyACTS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MyACTS.Data.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20230201_000004_CreateRolesPermissionsTable")]
partial class CreateRolesPermissionsTable
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity("RolesPermission", entity =>
        {
            entity.ToTable("roles_permissions", (string) null);

            entity.Property<int>("RoleId")
                .HasColumnName("id_role")
                .HasColumnType("int");

            entity.Property<int>("PermissionId")
                .HasColumnName("id_permission")
                .HasColumnType("int");

            entity.HasKey("RoleId", "PermissionId")
                .HasName("PK__roles_permissions")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasIndex(new[] { "PermissionId" }, "IDX__roles_permissions__id_role");

            entity.HasIndex(new[] { "PermissionId" }, "IDX__roles_permissions__id_permission");
        });
    }
}

