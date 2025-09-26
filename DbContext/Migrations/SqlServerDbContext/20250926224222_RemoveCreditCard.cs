using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContext.Migrations.SqlServerDbContext
{
    /// <inheritdoc />
    public partial class RemoveCreditCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "Attractions",
                newName: "LocationId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "varchar(200)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Reviews",
                type: "varchar(200)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Locations",
                type: "varchar(200)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Attractions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Attractions",
                type: "varchar(200)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AttractionId",
                table: "Reviews",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attractions_LocationId",
                table: "Attractions",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attractions_Locations_LocationId",
                table: "Attractions",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Attractions_AttractionId",
                table: "Reviews",
                column: "AttractionId",
                principalTable: "Attractions",
                principalColumn: "AttractionsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UsersId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attractions_Locations_LocationId",
                table: "Attractions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Attractions_AttractionId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_AttractionId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Attractions_LocationId",
                table: "Attractions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Attractions");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Attractions",
                newName: "LocationID");

            migrationBuilder.AlterColumn<string>(
                name: "LocationID",
                table: "Attractions",
                type: "varchar(200)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    CreditCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardHolderName = table.Column<string>(type: "varchar(200)", nullable: true),
                    EncryptedToken = table.Column<string>(type: "varchar(200)", nullable: true),
                    ExpirationMonth = table.Column<string>(type: "varchar(200)", nullable: true),
                    ExpirationYear = table.Column<string>(type: "varchar(200)", nullable: true),
                    Issuer = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "varchar(200)", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CreditCardId);
                });
        }
    }
}
