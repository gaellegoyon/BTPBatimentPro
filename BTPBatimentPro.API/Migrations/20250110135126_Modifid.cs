using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTPBatimentPro.API.Migrations
{
    /// <inheritdoc />
    public partial class Modifid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Projects_ProjectId1",
                table: "Assignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_ProjectId1",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Assignments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_EmployeeId",
                table: "Assignments",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_EmployeeId",
                table: "Assignments");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Assignments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId1",
                table: "Assignments",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assignments",
                table: "Assignments",
                columns: new[] { "EmployeeId", "ProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ProjectId1",
                table: "Assignments",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Projects_ProjectId1",
                table: "Assignments",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
