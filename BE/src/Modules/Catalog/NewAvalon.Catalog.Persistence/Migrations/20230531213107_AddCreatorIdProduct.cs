﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewAvalon.Catalog.Persistence.Migrations
{
    public partial class AddCreatorIdProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Products");
        }
    }
}
