using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstEntityUuid",
                table: "VirtualNetworkEntityConnectionModels");

            migrationBuilder.DropColumn(
                name: "SecondEntityUuid",
                table: "VirtualNetworkEntityConnectionModels");

            migrationBuilder.AddColumn<int>(
                name: "DestinationEntityId",
                table: "VirtualNetworkEntityConnectionModels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DestinationEntityType",
                table: "VirtualNetworkEntityConnectionModels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceEntityId",
                table: "VirtualNetworkEntityConnectionModels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceEntityType",
                table: "VirtualNetworkEntityConnectionModels",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationEntityId",
                table: "VirtualNetworkEntityConnectionModels");

            migrationBuilder.DropColumn(
                name: "DestinationEntityType",
                table: "VirtualNetworkEntityConnectionModels");

            migrationBuilder.DropColumn(
                name: "SourceEntityId",
                table: "VirtualNetworkEntityConnectionModels");

            migrationBuilder.DropColumn(
                name: "SourceEntityType",
                table: "VirtualNetworkEntityConnectionModels");

            migrationBuilder.AddColumn<Guid>(
                name: "FirstEntityUuid",
                table: "VirtualNetworkEntityConnectionModels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SecondEntityUuid",
                table: "VirtualNetworkEntityConnectionModels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
