using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureValue.Persistence.EfImplementation.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspUser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectionForm",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresetValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LowerBoundInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpperBoundInterest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IncrementalRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaturityYears = table.Column<int>(type: "int", nullable: false),
                    AspUserId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectionForm", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspUser");

            migrationBuilder.DropTable(
                name: "ProjectionForm");
        }

    }
}
