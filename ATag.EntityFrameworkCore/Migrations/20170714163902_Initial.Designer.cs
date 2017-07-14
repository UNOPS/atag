using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ATag.EntityFrameworkCore.DataAccess;

namespace ATag.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(TagsDbContext))]
    [Migration("20170714163902_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ATag.Core.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedByUserId")
                        .HasColumnName("CreatedByUserId");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("CreatedOn");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("IsDeleted");

                    b.Property<int?>("ModifiedByUserId")
                        .HasColumnName("ModifiedByUserId");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("ModifiedOn");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasMaxLength(30);

                    b.Property<string>("OwnerId")
                        .HasColumnName("OwnerId")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int>("OwnerType")
                        .HasColumnName("OwnerType");

                    b.HasKey("Id");

                    b.HasIndex("OwnerType", "OwnerId")
                        .HasName("IX_Tag_OwnerType_OwnerId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("ATag.Core.TaggedEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedByUserId")
                        .HasColumnName("CreatedByUserId");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("EntityKey")
                        .HasColumnName("EntityKey")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<string>("EntityType")
                        .HasColumnName("EntityType")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<int>("TagId")
                        .HasColumnName("TagId");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("EntityType", "EntityKey", "TagId")
                        .HasName("IX_TaggedEntity_EntityType_EntityKey_TagId");

                    b.ToTable("TaggedEntity");
                });

            modelBuilder.Entity("ATag.Core.TagNote", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("Id");

                    b.Property<int>("CreatedByUserId")
                        .HasColumnName("CreatedByUserId");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("CreatedOn");

                    b.Property<int?>("ModifiedByUserId")
                        .HasColumnName("ModifiedByUserId");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("ModifiedOn");

                    b.Property<string>("Note")
                        .HasColumnName("Note")
                        .HasMaxLength(1000);

                    b.HasKey("Id");

                    b.ToTable("TagNote");
                });

            modelBuilder.Entity("ATag.Core.TaggedEntity", b =>
                {
                    b.HasOne("ATag.Core.Tag", "Tag")
                        .WithMany("TaggedEntities")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ATag.Core.TagNote", b =>
                {
                    b.HasOne("ATag.Core.TaggedEntity", "TaggedEntity")
                        .WithOne("TagNote")
                        .HasForeignKey("ATag.Core.TagNote", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
