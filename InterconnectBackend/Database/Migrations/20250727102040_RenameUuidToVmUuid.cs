using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class RenameUuidToVmUuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "VirtualMachineEntityModels");

            migrationBuilder.AddColumn<string>(
                name: "VmUuid",
                table: "VirtualMachineEntityModels",
                type: "varchar(255)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VmUuid",
                table: "VirtualMachineEntityModels");

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "VirtualMachineEntityModels",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }
    }
}
