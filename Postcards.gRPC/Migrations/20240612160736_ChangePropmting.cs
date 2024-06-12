using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Postcards.gRPC.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropmting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prompt",
                table: "Postcards",
                newName: "Text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FulfillmentDate",
                table: "Postcards",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Postcards",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Postcards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Postcards");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Postcards",
                newName: "Prompt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FulfillmentDate",
                table: "Postcards",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Postcards",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
