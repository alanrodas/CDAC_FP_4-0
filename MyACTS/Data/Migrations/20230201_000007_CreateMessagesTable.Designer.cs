﻿// <auto-generated />
using System;
using MyACTS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MyACTS.Data.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20230201_000007_CreateMessagesTable")]
partial class CreateMessagesTable
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder) {
        modelBuilder.Entity("MyACTS.Models.Entities.Message", entity => {
            entity.ToTable("messages");

            entity.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasColumnName("id_message");

            entity.Property<string>("Subject")
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)")
                .HasColumnName("subject");

            entity.Property<string>("Body")
                .IsRequired()
                .HasColumnType("text")
                .HasColumnName("body");

            entity.Property<string>("ActionText")
                .HasMaxLength(255)
                .HasColumnType("varchar(255)")
                .HasColumnName("action_text");

            entity.Property<string>("ActionUrl")
                .HasMaxLength(255)
                .HasColumnType("varchar(255)")
                .HasColumnName("action_url");

            entity.Property<int>("Source")
                .HasColumnType("int")
                .HasColumnName("source");

            entity.Property<string>("Target")
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnType("varchar(255)")
                .HasColumnName("target");

            entity.Property<int?>("TargetId")
                .HasColumnType("int")
                .HasColumnName("id_target");

            entity.HasKey("Id")
                .HasName("PK__messages");

            entity.HasOne("MyACTS.Models.Entities.User", "User")
                .WithMany("Messages")
                .HasForeignKey("Source")
                .IsRequired()
                .HasConstraintName("FK__messages__users__source");

            entity.HasIndex(new[] { "Source" }, "IDX__messages__source");
            entity.HasIndex(new[] { "Target" }, "IDX__messages__target");
            entity.HasIndex(new[] { "TargetId" }, "IDX__messages__id_target");

            entity.Navigation("User");
        });
    }
}

