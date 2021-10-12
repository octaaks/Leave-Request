using Microsoft.EntityFrameworkCore.Migrations;

namespace Leave_Request.Migrations
{
    public partial class leaveduration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_tr_accounts_roles",
                table: "tb_tr_accounts_roles");

            migrationBuilder.DropIndex(
                name: "IX_tb_tr_accounts_roles_AccountId",
                table: "tb_tr_accounts_roles");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "tb_tr_accounts_roles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "tb_m_leave_types",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_tr_accounts_roles",
                table: "tb_tr_accounts_roles",
                columns: new[] { "AccountId", "RoleId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_tr_accounts_roles",
                table: "tb_tr_accounts_roles");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "tb_m_leave_types");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "tb_tr_accounts_roles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_tr_accounts_roles",
                table: "tb_tr_accounts_roles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_accounts_roles_AccountId",
                table: "tb_tr_accounts_roles",
                column: "AccountId");
        }
    }
}
