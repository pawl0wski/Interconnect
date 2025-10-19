using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class ModificationToVirtualNetwork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BridgeName",
                table: "VirtualNetworkEntityConnectionModels");

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "VirtualNetworkNodeEntityModels",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "VirtualNetworkNodeEntityModels");

            migrationBuilder.AddColumn<string>(
                name: "BridgeName",
                table: "VirtualNetworkEntityConnectionModels",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
