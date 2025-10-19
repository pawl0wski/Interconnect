using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class RenameSwitchToNode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VirtualSwitchEntityModels");

            migrationBuilder.CreateTable(
                name: "VirtualNetworkNodeEntityModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    Visible = table.Column<string>(type: "varchar(128)", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false),
                    VirtualNetworkId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualNetworkNodeEntityModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VirtualNetworkNodeEntityModels_VirtualNetworkModels_Virtual~",
                        column: x => x.VirtualNetworkId,
                        principalTable: "VirtualNetworkModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VirtualNetworkNodeEntityModels_VirtualNetworkId",
                table: "VirtualNetworkNodeEntityModels",
                column: "VirtualNetworkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VirtualNetworkNodeEntityModels");

            migrationBuilder.CreateTable(
                name: "VirtualSwitchEntityModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VirtualNetworkId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    Visible = table.Column<string>(type: "varchar(128)", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualSwitchEntityModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VirtualSwitchEntityModels_VirtualNetworkModels_VirtualNetwo~",
                        column: x => x.VirtualNetworkId,
                        principalTable: "VirtualNetworkModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VirtualSwitchEntityModels_VirtualNetworkId",
                table: "VirtualSwitchEntityModels",
                column: "VirtualNetworkId");
        }
    }
}
