using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ATag.EntityFrameworkCore.DataAccess;

namespace ATag.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(TagsDbContext))]
    partial class TagsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnName("DateCreated");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("IsDeleted");

                    b.Property<int?>("ModifiedByUserId")
                        .HasColumnName("ModifiedByUserId");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("DateModified");

                    b.Property<string>("Name")
                        .HasColumnName("Name")
                        .HasMaxLength(30)
                        .IsUnicode(false);

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

            modelBuilder.Entity("ATag.Core.TagComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnName("Comment")
                        .HasMaxLength(1000);

                    b.Property<int>("CreatedByUserId")
                        .HasColumnName("CreatedByUserId");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnName("DateCreated");

                    b.Property<int?>("ModifiedByUserId")
                        .HasColumnName("ModifiedByUserId");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnName("DateModified");

                    b.Property<int>("TaggedEntityDataId");

                    b.Property<int?>("TaggedEntityId");

                    b.HasKey("Id");

                    b.HasIndex("TaggedEntityId")
                        .IsUnique();

                    b.ToTable("TagComment");
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
                        .HasColumnName("DateCreated");

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

            modelBuilder.Entity("ATag.Core.TagComment", b =>
                {
                    b.HasOne("ATag.Core.TaggedEntity", "TaggedEntity")
                        .WithOne("TagComment")
                        .HasForeignKey("ATag.Core.TagComment", "TaggedEntityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ATag.Core.TaggedEntity", b =>
                {
                    b.HasOne("ATag.Core.Tag", "Tag")
                        .WithMany("TaggedEntities")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
