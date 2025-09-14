using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class VirtualNetwork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BridgeName",
                table: "VirtualSwitchEntityModels");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "VirtualSwitchEntityModels");

            migrationBuilder.DropColumn(
                name: "BridgeName",
                table: "InternetEntityModels");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "InternetEntityModels");

            migrationBuilder.AlterColumn<string>(
                name: "Visible",
                table: "VirtualSwitchEntityModels",
                type: "varchar(128)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<int>(
                name: "NetworkId",
                table: "VirtualSwitchEntityModels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VirtualNetworkId",
                table: "InternetEntityModels",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VirtualNetworkModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BridgeName = table.Column<string>(type: "text", nullable: false),
                    Uuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualNetworkModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VirtualSwitchEntityModels_NetworkId",
                table: "VirtualSwitchEntityModels",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_InternetEntityModels_VirtualNetworkId",
                table: "InternetEntityModels",
                column: "VirtualNetworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_InternetEntityModels_VirtualNetworkModel_VirtualNetworkId",
                table: "InternetEntityModels",
                column: "VirtualNetworkId",
                principalTable: "VirtualNetworkModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualSwitchEntityModels_VirtualNetworkModel_NetworkId",
                table: "VirtualSwitchEntityModels",
                column: "NetworkId",
                principalTable: "VirtualNetworkModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternetEntityModels_VirtualNetworkModel_VirtualNetworkId",
                table: "InternetEntityModels");

            migrationBuilder.DropForeignKey(
                name: "FK_VirtualSwitchEntityModels_VirtualNetworkModel_NetworkId",
                table: "VirtualSwitchEntityModels");

            migrationBuilder.DropTable(
                name: "VirtualNetworkModel");

            migrationBuilder.DropIndex(
                name: "IX_VirtualSwitchEntityModels_NetworkId",
                table: "VirtualSwitchEntityModels");

            migrationBuilder.DropIndex(
                name: "IX_InternetEntityModels_VirtualNetworkId",
                table: "InternetEntityModels");

            migrationBuilder.DropColumn(
                name: "NetworkId",
                table: "VirtualSwitchEntityModels");

            migrationBuilder.DropColumn(
                name: "VirtualNetworkId",
                table: "InternetEntityModels");

            migrationBuilder.AlterColumn<bool>(
                name: "Visible",
                table: "VirtualSwitchEntityModels",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(128)");

            migrationBuilder.AddColumn<string>(
                name: "BridgeName",
                table: "VirtualSwitchEntityModels",
                type: "varchar(128)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Uuid",
                table: "VirtualSwitchEntityModels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "BridgeName",
                table: "InternetEntityModels",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Uuid",
                table: "InternetEntityModels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
