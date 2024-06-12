using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postcards.gRPC.Migrations
{
    /// <inheritdoc />
    public partial class changeLocationIdToBaseImgName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Postcards");

            migrationBuilder.AddColumn<string>(
                name: "baseImgName",
                table: "Postcards",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "baseImgName",
                table: "Postcards");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Postcards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
