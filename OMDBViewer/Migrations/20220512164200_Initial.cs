// <copyright file="20220512164200_Initial.cs" company="Andrejs Macko">
// Copyright (c) Andrejs Macko. All rights reserved. Unauthorized copying of this file, via any medium is strictly prohibited. Proprietary and confidential.
// </copyright>

using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace OMDBViewer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "SearchStatistic",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultsCount = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchStatistic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShowStatistic",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowStatistic", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchStatistic",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ShowStatistic",
                schema: "dbo");
        }
    }
}
