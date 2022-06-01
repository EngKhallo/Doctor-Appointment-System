using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctor_Appointment_System.Migrations
{
    public partial class DbVersion_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Starttime",
                table: "Timeslots",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "paymentMethod",
                table: "Bookings",
                newName: "PaymentMethod");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "StartTime",
                table: "Timeslots",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndTime",
                table: "Timeslots",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Timeslots");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Timeslots",
                newName: "Starttime");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Bookings",
                newName: "paymentMethod");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Starttime",
                table: "Timeslots",
                type: "interval",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");
        }
    }
}
