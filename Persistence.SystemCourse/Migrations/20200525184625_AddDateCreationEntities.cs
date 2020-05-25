using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.SystemCourse.Migrations
{
    public partial class AddDateCreationEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreation",
                table: "Instructor",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreation",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCration",
                table: "Comment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreation",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "DateCreation",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "DateCration",
                table: "Comment");
        }
    }
}
