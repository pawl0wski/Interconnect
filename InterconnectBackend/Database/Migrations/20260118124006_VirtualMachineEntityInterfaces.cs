using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class VirtualMachineEntityInterfaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceDefinition",
                table: "VirtualMachineEntityModels");

            migrationBuilder.CreateTable(
                name: "VirtualMachineEntityNetworkInterfaceModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Definition = table.Column<string>(type: "text", nullable: false),
                    VirtualMachineEntityId = table.Column<int>(type: "integer", nullable: false),
                    VirtualNetworkEntityConnectionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualMachineEntityNetworkInterfaceModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VirtualMachineEntityNetworkInterfaceModels_VirtualMachineEn~",
                        column: x => x.VirtualMachineEntityId,
                        principalTable: "VirtualMachineEntityModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VirtualMachineEntityNetworkInterfaceModels_VirtualNetworkEn~",
                        column: x => x.VirtualNetworkEntityConnectionId,
                        principalTable: "VirtualNetworkEntityConnectionModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VirtualMachineEntityNetworkInterfaceModels_VirtualMachineEn~",
                table: "VirtualMachineEntityNetworkInterfaceModels",
                column: "VirtualMachineEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualMachineEntityNetworkInterfaceModels_VirtualNetworkEn~",
                table: "VirtualMachineEntityNetworkInterfaceModels",
                column: "VirtualNetworkEntityConnectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VirtualMachineEntityNetworkInterfaceModels");

            migrationBuilder.AddColumn<string>(
                name: "DeviceDefinition",
                table: "VirtualMachineEntityModels",
                type: "text",
                nullable: true);
        }
    }
}
