using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellPhone.Db.Migrations
{
    public partial class Models_lookups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "LU_Cities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LU_Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LU_Models", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LU_Models_LU_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "LU_Brands",
                        principalColumn: "Id");
                });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
