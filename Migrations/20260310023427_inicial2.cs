using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hamlet_GarciaAP1_P2.Migrations
{
    /// <inheritdoc />
    public partial class inicial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Estudiantes",
                keyColumn: "EstudianteId",
                keyValue: 1,
                columns: new[] { "Edad", "Email", "Nombres" },
                values: new object[] { 21, "Jose_garcia30@ucne.edu.do", "Jose Hamlet Garcia" });

            migrationBuilder.UpdateData(
                table: "TiposPuntos",
                keyColumn: "TipoId",
                keyValue: 1,
                column: "Nombre",
                value: "Participacion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Estudiantes",
                keyColumn: "EstudianteId",
                keyValue: 1,
                columns: new[] { "Edad", "Email", "Nombres" },
                values: new object[] { 20, "ana@universidad.edu", "Ana Martínez" });

            migrationBuilder.UpdateData(
                table: "TiposPuntos",
                keyColumn: "TipoId",
                keyValue: 1,
                column: "Nombre",
                value: "Participación");
        }
    }
}
