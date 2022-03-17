﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseAccessLibrary.Migrations
{
    public partial class init34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bicycles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Registration = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Parkingspot = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bicycles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bicycles");
        }
    }
}
