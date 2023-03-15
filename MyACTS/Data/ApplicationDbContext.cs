using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyACTS.Models.Entities;

namespace MyACTS.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventStatus> EventsStatuses { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageStatus> MessagesStatuses { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        this.DefineModelPermission(modelBuilder);
        this.DefineModelRole(modelBuilder);
        this.DefineModelUser(modelBuilder);
        this.DefineModelService(modelBuilder);
        this.DefineModelMessage(modelBuilder);
        this.DefineModelEvent(modelBuilder);
        this.DefineModelMessageStatus(modelBuilder);
        this.DefineModelEventStatus(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    public void DefineModelPermission(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Permission>(entity => {

            entity.ToTable("permissions");

            entity.Property(e => e.Id)
                .HasColumnName("id_permission");

            entity.Property(e => e.Name)
                .HasColumnName("name");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.Action)
                .HasMaxLength(255)
                .HasColumnName("action")
                .HasConversion(new EnumToStringConverter<EAction>()); ;

            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type")
                .HasConversion(new EnumToStringConverter<EType>());

            entity.Property(e => e.Target)
                .HasMaxLength(255)
                .HasColumnName("target")
                .HasConversion(new EnumToStringConverter<ETarget>());

            entity.Property(e => e.IdTarget)
                .HasColumnName("id_target");

            entity.HasKey(e => e.Id)
                .HasName("PK__permissions");

            entity.HasIndex(e => e.Name, "IDX__permissions__name")
                .IsUnique();
        });
    }

    public void DefineModelRole(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Role>(entity => {

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .HasColumnName("id_role");

            entity.Property(e => e.Name)
                .HasColumnName("name");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolesPermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("IdRole")
                        .HasConstraintName("FK__role_permissions__permissions__id_permission"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("IdPermission")
                        .HasConstraintName("FK__role_permissions__roles__id_role"),
                    j => {
                        j.Property<int>("IdRole")
                            .HasColumnName("id_role")
                            .HasColumnType("int");

                        j.Property<int>("IdPermission")
                            .HasColumnName("id_permission")
                            .HasColumnType("int");

                        j.HasKey("IdRole", "IdPermission")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("roles_permissions");
                        j.HasIndex(new[] { "IdPermission" }, "permissionId_idx");
                    });

            entity.HasKey(e => e.Id)
                .HasName("PK__roles");

            entity.HasIndex(e => e.Name, "IDX__roles__name")
                .IsUnique();
        });
    }

    public void DefineModelUser(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>(entity => {

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .HasColumnName("id_user");

            entity.Property(e => e.UserName)
                .HasColumnName("user_name");

            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");

            entity.Property(e => e.Email)
                .HasColumnName("email");

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .HasColumnName("phone_number");

            entity.Property(e => e.DateOfBirth)
                .HasColumnName("date_of_birth");

            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("IdRole")
                        .HasConstraintName("FK__user_roles__roles__id_role"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK__user_roles__users__id_user"),
                    j => {
                        j.Property<int>("IdUser")
                            .HasColumnName("id_user")
                            .HasColumnType("int");

                        j.Property<int>("IdRole")
                            .HasColumnName("id_role")
                            .HasColumnType("int");

                        j.HasKey("IdUser", "IdRole")
                            .HasName("PK__user_roles")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("user_roles");
                        j.HasIndex(new[] { "IdRole" }, "FK_user_roles__roles__roleId_idx");
                    });

            entity.HasKey(e => e.Id)
                .HasName("PK__users");

            entity.HasIndex(e => e.UserName, "IDX__users__user_name")
                .IsUnique();

            entity.HasIndex(e => e.Email, "IDX__users__email")
                .IsUnique();
        });
    }

    public void DefineModelService(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Service>(entity => {

            entity.ToTable("services");

            entity.Property(e => e.Id)
                .HasColumnName("id_service");

            entity.Property(e => e.Name)
                .HasColumnName("name");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");

            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");

            entity.Property(e => e.Icon)
                .HasColumnType("blob")
                .HasColumnName("icon");

            entity.HasKey(e => e.Id)
                .HasName("PK__services");

            entity.HasIndex(e => e.Name, "IDX__services__name")
                .IsUnique();
        });
    }

    public void DefineModelMessage(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Message>(entity => {
            
            entity.ToTable("messages");

            entity.Property(e => e.Id)
                .HasColumnName("id_message");

            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .HasColumnName("subject");

            entity.Property(e => e.Body)
             .HasColumnType("text")
             .HasColumnName("body");

            entity.Property(e => e.ActionText)
                .HasMaxLength(255)
                .HasColumnName("action_text");

            entity.Property(e => e.ActionUrl)
                .HasMaxLength(255)
                .HasColumnName("action_url");

            entity.Property(e => e.Source)
                .HasColumnName("source");

            entity.Property(e => e.Target)
                .HasMaxLength(255)
                .HasColumnName("target")
                .HasConversion(new EnumToStringConverter<ETarget>());

            entity.Property(e => e.TargetId)
                .HasColumnName("id_target");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.Source)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__users__source");

            entity.HasKey(e => e.Id)
                .HasName("PK__messages");

            entity.HasIndex(e => e.Source, "IDX__messages__source");
            entity.HasIndex(e => e.Target, "IDX__messages__target");
            entity.HasIndex(e => e.TargetId, "IDX__messages__id_target");
        });
    }

    public void DefineModelEvent(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Event>(entity => {

            entity.ToTable("events");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id_event");

            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .HasColumnName("subject");

            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");

            entity.Property(e => e.Start)
                .HasColumnType("datetime")
                .HasColumnName("start");

            entity.Property(e => e.End)
                .HasColumnType("datetime")
                .HasColumnName("end");

            entity.Property(e => e.ActionText)
                .HasMaxLength(255)
                .HasColumnName("action_text");

            entity.Property(e => e.ActionUrl)
                .HasMaxLength(255)
                .HasColumnName("action_url");

            entity.Property(e => e.Source)
                .HasColumnName("source");

            entity.Property(e => e.Target)
                .HasMaxLength(255)
                .HasColumnName("target")
                .HasConversion(new EnumToStringConverter<ETarget>());

            entity.Property(e => e.TargetId)
                .HasColumnName("id_target");

            entity.HasOne(d => d.User).WithMany(p => p.Events)
                .HasForeignKey(d => d.Source)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__events__users__source");

            entity.HasKey(e => e.Id)
                .HasName("PK__events");

            entity.HasIndex(e => e.Source, "IDX__events__source");
            entity.HasIndex(e => e.Target, "IDX__events__target");
            entity.HasIndex(e => e.TargetId, "IDX__events__id_target");
        });
    }

    public void DefineModelMessageStatus(ModelBuilder modelBuilder) {
        modelBuilder.Entity<MessageStatus>(entity => {

            entity.ToTable("messages_status");

            entity.Property(e => e.MessageId)
                .HasColumnName("id_message");

            entity.Property(e => e.UserId)
                .HasColumnName("id_user");

            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status")
                .HasConversion(new EnumToStringConverter<EStatus>()); ;

            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.Property(e => e.CurrentSessionToken)
                .HasMaxLength(255)
                .HasColumnName("current_session_token");

            entity.HasOne(d => d.Message).WithMany(p => p.MessageStatuses)
                .HasForeignKey(d => d.MessageId)
                .HasConstraintName("FK__messages_status__messages__id_message");

            entity.HasKey(e => new { e.MessageId, e.UserId })
                .HasName("PK__messages_status")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            //entity.HasIndex(e => e.MessageId, "IDX__messages_status__id_message");
            //entity.HasIndex(e => e.UserId, "IDX__messages_status__id_user");
        });
    }

    public void DefineModelEventStatus(ModelBuilder modelBuilder) {
        modelBuilder.Entity<EventStatus>(entity => {

            entity.ToTable("events_status");

            entity.Property(e => e.EventId)
                .HasColumnName("id_event");

            entity.Property(e => e.UserId)
                .HasColumnName("id_user");

            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status")
                .HasConversion(new EnumToStringConverter<EStatus>()); ;

            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.Property(e => e.CurrentSessionToken)
                .HasMaxLength(255)
                .HasColumnName("current_session_token");

            entity.HasOne(d => d.Event).WithMany(p => p.EventStatuses)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__messages_status__messages__id_message");

            entity.HasKey(e => new { e.EventId, e.UserId })
                .HasName("PK__events_Status")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.HasIndex(e => e.EventId, "IDX__events_status__id_event");
            entity.HasIndex(e => e.UserId, "IDX__events_status__id_user");
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

