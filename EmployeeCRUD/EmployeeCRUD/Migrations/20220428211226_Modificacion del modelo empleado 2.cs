using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeCRUD.Migrations
{
    public partial class Modificaciondelmodeloempleado2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecordUptadeOn",
                table: "Employees",
                newName: "RecordUpdateOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecordUpdateOn",
                table: "Employees",
                newName: "RecordUptadeOn");
        }
    }
}
