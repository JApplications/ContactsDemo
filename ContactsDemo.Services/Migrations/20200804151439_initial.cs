using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsDemo.Services.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactNumbers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    ContactId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactNumbers_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactNumbers_ContactId",
                table: "ContactNumbers",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Firstname_Lastname_Address",
                table: "Contacts",
                columns: new[] { "Firstname", "Lastname", "Address" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactNumbers");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
