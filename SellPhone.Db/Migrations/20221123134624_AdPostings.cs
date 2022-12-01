using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SellPhone.Db.Migrations
{
    public partial class AdPostings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 
            migrationBuilder.CreateTable(
                name: "AdPostings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AspNetUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    BrandId = table.Column<int>(type: "int", nullable: true),
                    ModelId = table.Column<int>(type: "int", nullable: true),
                    Storage = table.Column<int>(type: "int", nullable: true),
                    BatteryHealth = table.Column<int>(type: "int", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    IsApprovedByAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsRejectedByAdmin = table.Column<bool>(type: "bit", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddStatus = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_AdPostings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdPostings_AspNetUsers_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdPostings_LU_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "LU_Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdPostings_LU_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "LU_Cities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdPostings_LU_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "LU_Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdPostings_LU_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "LU_Models",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdPostings_AspNetUserId",
                table: "AdPostings",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdPostings_BrandId",
                table: "AdPostings",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_AdPostings_CityId",
                table: "AdPostings",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_AdPostings_CountryId",
                table: "AdPostings",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdPostings_ModelId",
                table: "AdPostings",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
