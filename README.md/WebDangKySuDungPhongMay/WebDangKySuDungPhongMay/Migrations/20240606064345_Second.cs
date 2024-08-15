using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebDangKySuDungPhongMay.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaGiangVien",
                table: "LopHocPhans",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "LopHocPhans",
                keyColumn: "MaLopHocPhan",
                keyValue: "LHP001",
                column: "MaGiangVien",
                value: "GV001");

            migrationBuilder.UpdateData(
                table: "LopHocPhans",
                keyColumn: "MaLopHocPhan",
                keyValue: "LHP002",
                column: "MaGiangVien",
                value: "GV002");

            migrationBuilder.UpdateData(
                table: "LopHocPhans",
                keyColumn: "MaLopHocPhan",
                keyValue: "LHP003",
                column: "MaGiangVien",
                value: "GV003");

            migrationBuilder.UpdateData(
                table: "LopHocPhans",
                keyColumn: "MaLopHocPhan",
                keyValue: "LHP004",
                column: "MaGiangVien",
                value: "GV004");

            migrationBuilder.CreateIndex(
                name: "IX_LopHocPhans_MaGiangVien",
                table: "LopHocPhans",
                column: "MaGiangVien");

            migrationBuilder.AddForeignKey(
                name: "FK_LopHocPhans_GiangViens_MaGiangVien",
                table: "LopHocPhans",
                column: "MaGiangVien",
                principalTable: "GiangViens",
                principalColumn: "MaGiangVien",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LopHocPhans_GiangViens_MaGiangVien",
                table: "LopHocPhans");

            migrationBuilder.DropIndex(
                name: "IX_LopHocPhans_MaGiangVien",
                table: "LopHocPhans");

            migrationBuilder.DropColumn(
                name: "MaGiangVien",
                table: "LopHocPhans");
        }
    }
}
