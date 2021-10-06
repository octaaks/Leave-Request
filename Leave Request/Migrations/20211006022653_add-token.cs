using Microsoft.EntityFrameworkCore.Migrations;

namespace Leave_Request.Migrations
{
    public partial class addtoken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "tb_m_accounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "tb_m_accounts");
        }
    }
}
