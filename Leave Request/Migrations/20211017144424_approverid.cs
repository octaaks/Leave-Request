using Microsoft.EntityFrameworkCore.Migrations;

namespace Leave_Request.Migrations
{
    public partial class approverid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_manager_fills_tb_m_employees_EmployeeId",
                table: "tb_m_manager_fills");


            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "tb_m_manager_fills",
                newName: "ApproverId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_manager_fills_EmployeeId",
                table: "tb_m_manager_fills",
                newName: "IX_tb_m_manager_fills_ApproverId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_manager_fills_tb_m_employees_ApproverId",
                table: "tb_m_manager_fills",
                column: "ApproverId",
                principalTable: "tb_m_employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_manager_fills_tb_m_employees_ApproverId",
                table: "tb_m_manager_fills");

            migrationBuilder.RenameColumn(
                name: "ApproverId",
                table: "tb_m_manager_fills",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_m_manager_fills_ApproverId",
                table: "tb_m_manager_fills",
                newName: "IX_tb_m_manager_fills_EmployeeId");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "tb_m_manager_fills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_manager_fills_tb_m_employees_EmployeeId",
                table: "tb_m_manager_fills",
                column: "EmployeeId",
                principalTable: "tb_m_employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
