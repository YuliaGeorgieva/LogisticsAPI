using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogisticsApi.Migrations
{
    public partial class updatecommonfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Shipments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Shipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "ShipmentDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "ShipmentDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ShipmentDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ShipmentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ShipmentDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "CompanyBranches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "CompanyBranches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CompanyBranches",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "CompanyBranches",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "CompanyBranches",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddedBy",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Companies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Companies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Companies",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "ShipmentDetails");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "ShipmentDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ShipmentDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ShipmentDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ShipmentDetails");

            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "CompanyBranches");

            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Companies");
        }
    }
}
