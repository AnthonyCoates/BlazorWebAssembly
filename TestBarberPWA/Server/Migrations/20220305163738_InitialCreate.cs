using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestBarberPWA.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    LineOne = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineTwo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentID);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Forename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmployee = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceID);
                });

            migrationBuilder.CreateTable(
                name: "ServicesSold",
                columns: table => new
                {
                    AppointmentID = table.Column<int>(type: "int", nullable: false),
                    ServiceID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesSold", x => new { x.AppointmentID, x.ServiceID });
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressID", "LineOne", "LineTwo", "PersonID", "PostCode", "Town" },
                values: new object[,]
                {
                    { 1, "1 A Street", "Somewhere", 1, "AA11 1AA", "Testville" },
                    { 2, "2 B Street", "Somewhere", 2, "BB22 2BB", "Testville" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentID", "CustomerID", "DateTime", "EmployeeID", "Notes" },
                values: new object[] { 1, 1, new DateTime(2022, 1, 1, 15, 30, 0, 0, DateTimeKind.Unspecified), 2, "Someone having a full shave and hair cut." });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonID", "DateOfBirth", "Email", "Forename", "Gender", "IsEmployee", "PhoneNo", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1991, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a.aaronson@aaronmail.com", "Aaron", 1, false, "01234567890", "Aaronson" },
                    { 2, new DateTime(1992, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "b.bettyson@bettymail.com", "Betty", 0, true, "09876543210", "Bettyson" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceID", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "A standard, dry hair cut.", "Hair Cut", 14.99f },
                    { 2, "A standard, wet shave.", "Shave", 5.99f }
                });

            migrationBuilder.InsertData(
                table: "ServicesSold",
                columns: new[] { "AppointmentID", "ServiceID", "Quantity", "SubTotal" },
                values: new object[,]
                {
                    { 1, 1, 1, 14.99f },
                    { 1, 2, 1, 5.99f }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "ServicesSold");
        }
    }
}
