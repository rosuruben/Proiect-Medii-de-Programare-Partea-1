using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaVeterinaraP1.Migrations
{
    /// <inheritdoc />
    public partial class recenzie1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recenzie_ProgramareId",
                table: "Recenzie");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramareId",
                table: "Recenzie",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Recenzie_ProgramareId",
                table: "Recenzie",
                column: "ProgramareId",
                unique: true,
                filter: "[ProgramareId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Recenzie_ProgramareId",
                table: "Recenzie");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramareId",
                table: "Recenzie",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recenzie_ProgramareId",
                table: "Recenzie",
                column: "ProgramareId",
                unique: true);
        }
    }
}
