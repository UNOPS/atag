﻿namespace ATag.EntityFrameworkCore.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Initial : Migration
    {
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagComment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TaggedEntity",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tag",
                schema: "dbo");
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    OwnerId = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    OwnerType = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Tag", x => x.Id); });

            migrationBuilder.CreateTable(
                name: "TaggedEntity",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    EntityKey = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    EntityType = table.Column<string>(unicode: false, maxLength: 30, nullable: true),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaggedEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaggedEntity_Tag_TagId",
                        column: x => x.TagId,
                        principalSchema: "dbo",
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagComment",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comment = table.Column<string>(maxLength: 1000, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ModifiedByUserId = table.Column<int>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true),
                    TaggedEntityDataId = table.Column<int>(nullable: false),
                    TaggedEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagComment_TaggedEntity_TaggedEntityId",
                        column: x => x.TaggedEntityId,
                        principalSchema: "dbo",
                        principalTable: "TaggedEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_OwnerType_OwnerId",
                schema: "dbo",
                table: "Tag",
                columns: new[] { "OwnerType", "OwnerId" });

            migrationBuilder.CreateIndex(
                name: "IX_TagComment_TaggedEntityId",
                schema: "dbo",
                table: "TagComment",
                column: "TaggedEntityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaggedEntity_TagId",
                schema: "dbo",
                table: "TaggedEntity",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TaggedEntity_EntityType_EntityKey_TagId",
                schema: "dbo",
                table: "TaggedEntity",
                columns: new[] { "EntityType", "EntityKey", "TagId" });
        }
    }
}