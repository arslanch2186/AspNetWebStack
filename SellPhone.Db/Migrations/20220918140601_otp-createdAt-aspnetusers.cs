using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellPhone.Db.Migrations
{
    public partial class otpcreatedAtaspnetusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.AddColumn<DateTime>(
                name: "OTPCreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        { 
        }
    }
}
