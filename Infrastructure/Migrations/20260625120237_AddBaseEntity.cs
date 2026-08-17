using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActice",
                table : "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "CraetedAt",
                table: "Services",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Appointments",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Specialties",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Specialties",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Prescriptions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Medicines",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Medicines",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Invoices",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "IsActice");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Services",
                newName: "CraetedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Appointments",
                newName: "UpdateAt");
        }
    }
}
