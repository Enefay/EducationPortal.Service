using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationPortal.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "Educations");

            migrationBuilder.RenameColumn(
                name: "DurationInDays",
                table: "Educations",
                newName: "EducationStatus");

            migrationBuilder.AddColumn<int>(
                name: "JoinRequestStatus",
                table: "EducationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeaveRequestStatus",
                table: "EducationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Educations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Educations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoinRequestStatus",
                table: "EducationUsers");

            migrationBuilder.DropColumn(
                name: "LeaveRequestStatus",
                table: "EducationUsers");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Educations");

            migrationBuilder.RenameColumn(
                name: "EducationStatus",
                table: "Educations",
                newName: "DurationInDays");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "Educations",
                type: "bit",
                nullable: true);
        }
    }
}
