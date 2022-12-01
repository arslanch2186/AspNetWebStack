using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellPhone.Db.Migrations
{
    public partial class otpaspnetusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OTP",
                table: "AspNetUsers",
                type: "int",
                nullable: true); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        { 
        }
    }
}
