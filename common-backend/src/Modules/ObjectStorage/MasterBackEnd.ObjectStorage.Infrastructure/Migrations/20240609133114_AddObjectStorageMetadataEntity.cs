using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterBackEnd.ObjectStorage.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddObjectStorageMetadataEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ObjectStorage");

            migrationBuilder.CreateTable(
                name: "FileMetadata",
                schema: "ObjectStorage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileNamme = table.Column<string>(type: "text", nullable: false),
                    FileType = table.Column<string>(type: "text", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileMetadata", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileMetadata",
                schema: "ObjectStorage");
        }
    }
}
