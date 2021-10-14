using Microsoft.EntityFrameworkCore.Migrations;

namespace Leave_Request.Migrations
{
    public partial class adddurationonlt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "tb_m_leave_types",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "tb_m_leave_types");
        }
    }
}
