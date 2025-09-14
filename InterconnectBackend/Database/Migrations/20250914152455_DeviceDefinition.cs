using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class DeviceDefinition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternetEntityModels_VirtualNetworkModel_VirtualNetworkId",
                table: "InternetEntityModels");

            migrationBuilder.DropForeignKey(
                name: "FK_VirtualSwitchEntityModels_VirtualNetworkModel_NetworkId",
                table: "VirtualSwitchEntityModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VirtualNetworkModel",
                table: "VirtualNetworkModel");

            migrationBuilder.RenameTable(
                name: "VirtualNetworkModel",
                newName: "VirtualNetworkModels");

            migrationBuilder.RenameColumn(
                name: "NetworkId",
                table: "VirtualSwitchEntityModels",
                newName: "VirtualNetworkId");

            migrationBuilder.RenameIndex(
                name: "IX_VirtualSwitchEntityModels_NetworkId",
                table: "VirtualSwitchEntityModels",
                newName: "IX_VirtualSwitchEntityModels_VirtualNetworkId");

            migrationBuilder.AddColumn<string>(
                name: "DeviceDefinition",
                table: "VirtualMachineEntityModels",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VirtualNetworkModels",
                table: "VirtualNetworkModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InternetEntityModels_VirtualNetworkModels_VirtualNetworkId",
                table: "InternetEntityModels",
                column: "VirtualNetworkId",
                principalTable: "VirtualNetworkModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualSwitchEntityModels_VirtualNetworkModels_VirtualNetwo~",
                table: "VirtualSwitchEntityModels",
                column: "VirtualNetworkId",
                principalTable: "VirtualNetworkModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InternetEntityModels_VirtualNetworkModels_VirtualNetworkId",
                table: "InternetEntityModels");

            migrationBuilder.DropForeignKey(
                name: "FK_VirtualSwitchEntityModels_VirtualNetworkModels_VirtualNetwo~",
                table: "VirtualSwitchEntityModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VirtualNetworkModels",
                table: "VirtualNetworkModels");

            migrationBuilder.DropColumn(
                name: "DeviceDefinition",
                table: "VirtualMachineEntityModels");

            migrationBuilder.RenameTable(
                name: "VirtualNetworkModels",
                newName: "VirtualNetworkModel");

            migrationBuilder.RenameColumn(
                name: "VirtualNetworkId",
                table: "VirtualSwitchEntityModels",
                newName: "NetworkId");

            migrationBuilder.RenameIndex(
                name: "IX_VirtualSwitchEntityModels_VirtualNetworkId",
                table: "VirtualSwitchEntityModels",
                newName: "IX_VirtualSwitchEntityModels_NetworkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VirtualNetworkModel",
                table: "VirtualNetworkModel",
                column: "Id");

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
    }
}
