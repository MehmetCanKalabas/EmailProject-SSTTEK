using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Email.Model.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Port",
                table: "EmailInformations");

            migrationBuilder.DropColumn(
                name: "SSL",
                table: "EmailInformations");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "EmailInformations",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "EmailInformations",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "HostName",
                table: "EmailInformations",
                newName: "Email");

            migrationBuilder.CreateTable(
                name: "SMTPSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HostName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Port = table.Column<int>(type: "int", nullable: false),
                    SSL = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMTPSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SMTPSettings");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "EmailInformations",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EmailInformations",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "EmailInformations",
                newName: "HostName");

            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "EmailInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SSL",
                table: "EmailInformations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
