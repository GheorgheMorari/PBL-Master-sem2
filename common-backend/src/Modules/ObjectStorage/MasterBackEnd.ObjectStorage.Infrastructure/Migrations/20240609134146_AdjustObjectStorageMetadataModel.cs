using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterBackEnd.ObjectStorage.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustObjectStorageMetadataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileNamme",
                schema: "ObjectStorage",
                table: "FileMetadata");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "ObjectStorage",
                table: "FileMetadata",
                newName: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                schema: "ObjectStorage",
                table: "FileMetadata",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "FileNamme",
                schema: "ObjectStorage",
                table: "FileMetadata",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
