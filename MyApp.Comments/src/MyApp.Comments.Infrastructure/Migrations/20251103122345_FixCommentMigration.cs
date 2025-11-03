using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Comments.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCommentMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_CreatedAtUtc",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Comments",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<byte>(
                name: "Depth",
                table: "Comments",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<Guid>(
                name: "RootId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TargetId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "TargetType",
                table: "Comments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Comments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentId",
                table: "Comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RootId",
                table: "Comments",
                column: "RootId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TargetType_TargetId_CreatedAt",
                table: "Comments",
                columns: new[] { "TargetType", "TargetId", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Comments_ParentId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_RootId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_TargetType_TargetId_CreatedAt",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Depth",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "RootId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TargetId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TargetType",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Comments",
                newName: "CreatedAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedAtUtc",
                table: "Comments",
                column: "CreatedAtUtc");
        }
    }
}
