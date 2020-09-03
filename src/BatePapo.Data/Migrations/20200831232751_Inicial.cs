using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BatePapo.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CPF_Id = table.Column<Guid>(nullable: true),
                    CPF_Doc = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    SurName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    NickName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    TypeCliente = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Neighborhood = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Number = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    PublicPlace = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    AddressType = table.Column<int>(nullable: false),
                    Principal = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CustomerId",
                table: "Address",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
