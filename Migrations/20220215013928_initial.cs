using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dacon_exam.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "INFO",
                schema: "dbo",
                columns: table => new
                {
                    Info_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    line_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    from = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    to = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    drawing_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    service = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    inservice_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pipe_size = table.Column<double>(type: "float", nullable: false),
                    original_thickness = table.Column<double>(type: "float", nullable: false),
                    stress = table.Column<int>(type: "int", nullable: false),
                    joint_efficiency = table.Column<int>(type: "int", nullable: false),
                    ca = table.Column<int>(type: "int", nullable: false),
                    design_life = table.Column<int>(type: "int", nullable: false),
                    design_pressure = table.Column<int>(type: "int", nullable: false),
                    design_temperature = table.Column<int>(type: "int", nullable: false),
                    operating_pressure = table.Column<int>(type: "int", nullable: false),
                    operating_temperature = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INFO", x => x.Info_id);
                });

            migrationBuilder.CreateTable(
                name: "CML",
                schema: "dbo",
                columns: table => new
                {
                    cml_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cml_number = table.Column<int>(type: "int", nullable: false),
                    info_id = table.Column<int>(type: "int", nullable: false),
                    cml_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    actual_outside_diameter = table.Column<double>(type: "float", nullable: false),
                    design_thickness = table.Column<double>(type: "float", nullable: false),
                    structural_thickness = table.Column<double>(type: "float", nullable: false),
                    required_thickness = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CML", x => x.cml_id);
                    table.ForeignKey(
                        name: "FK_CML_INFO_info_id",
                        column: x => x.info_id,
                        principalSchema: "dbo",
                        principalTable: "INFO",
                        principalColumn: "Info_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TEST_POINT",
                schema: "dbo",
                columns: table => new
                {
                    tp_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tp_number = table.Column<int>(type: "int", nullable: false),
                    cml_id = table.Column<int>(type: "int", nullable: false),
                    tp_description = table.Column<int>(type: "int", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    line_number = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEST_POINT", x => x.tp_id);
                    table.ForeignKey(
                        name: "FK_TEST_POINT_CML_cml_id",
                        column: x => x.cml_id,
                        principalSchema: "dbo",
                        principalTable: "CML",
                        principalColumn: "cml_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "THICKNESS",
                schema: "dbo",
                columns: table => new
                {
                    thickness_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tp_number = table.Column<int>(type: "int", nullable: false),
                    inspection_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    actual_thickness = table.Column<double>(type: "float", nullable: false),
                    cml_id = table.Column<int>(type: "int", nullable: false),
                    line_number = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THICKNESS", x => x.thickness_id);
                    table.ForeignKey(
                        name: "FK_THICKNESS_TEST_POINT_tp_number",
                        column: x => x.tp_number,
                        principalSchema: "dbo",
                        principalTable: "TEST_POINT",
                        principalColumn: "tp_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CML_info_id",
                schema: "dbo",
                table: "CML",
                column: "info_id");

            migrationBuilder.CreateIndex(
                name: "IX_TEST_POINT_cml_id",
                schema: "dbo",
                table: "TEST_POINT",
                column: "cml_id");

            migrationBuilder.CreateIndex(
                name: "IX_THICKNESS_tp_number",
                schema: "dbo",
                table: "THICKNESS",
                column: "tp_number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "THICKNESS",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TEST_POINT",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CML",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "INFO",
                schema: "dbo");
        }
    }
}
