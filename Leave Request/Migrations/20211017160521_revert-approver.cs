using Microsoft.EntityFrameworkCore.Migrations;

namespace Leave_Request.Migrations
{
    public partial class revertapprover : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_manager_fills_tb_m_employees_ApproverId",
                table: "tb_m_manager_fills");

            migrationBuilder.DropIndex(
                name: "IX_tb_m_manager_fills_ApproverId",
                table: "tb_m_manager_fills");

            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "tb_m_manager_fills");

            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "tb_m_leave_requests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "tb_m_leave_requests");

            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "tb_m_manager_fills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_manager_fills_ApproverId",
                table: "tb_m_manager_fills",
                column: "ApproverId");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_manager_fills_tb_m_employees_ApproverId",
                table: "tb_m_manager_fills",
                column: "ApproverId",
                principalTable: "tb_m_employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
