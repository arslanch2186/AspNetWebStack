using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellPhone.Db.Migrations
{
    public partial class otpcountryCodeaspnetusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
        }
    }
}
