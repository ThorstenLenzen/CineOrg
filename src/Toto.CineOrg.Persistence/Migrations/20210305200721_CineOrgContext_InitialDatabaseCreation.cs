using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Toto.CineOrg.Persistence.Migrations
{
    public partial class CineOrgContext_InitialDatabaseCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    YearReleased = table.Column<int>(type: "int", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Theatres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theatres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    RowLetter = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    TheatreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Theatres_TheatreId",
                        column: x => x.TheatreId,
                        principalTable: "Theatres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CreatedAt", "Description", "Genre", "Title", "YearReleased" },
                values: new object[] { new Guid("20db69d0-7760-4c3f-a484-032423e61018"), new DateTime(2020, 4, 2, 22, 0, 0, 0, DateTimeKind.Utc), "An ugly duckling having undergone a remarkable change, still harbors feelings for her crush: a carefree playboy, but not before his business-focused brother has something to say about it.", "romance", "Sabrina", 1995 });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CreatedAt", "Description", "Genre", "Title", "YearReleased" },
                values: new object[] { new Guid("428429c0-9108-401c-b571-a09dc156ae50"), new DateTime(2019, 4, 2, 22, 0, 0, 0, DateTimeKind.Utc), "When CIA Analyst Jack Ryan interferes with an IRA assassination, a renegade faction targets him and his family for revenge.", "thriller", "Patriot Games", 1992 });

            migrationBuilder.InsertData(
                table: "Theatres",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("7973dadd-c7f8-42e5-83b9-729a8ff7c614"), "Test Theatre - One" });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "Category", "RowLetter", "SeatNumber", "TheatreId" },
                values: new object[,]
                {
                    { new Guid("b409157a-5d8c-4545-aad5-7d0f02b5e515"), "stalls", "A", 1, new Guid("7973dadd-c7f8-42e5-83b9-729a8ff7c614") },
                    { new Guid("b5111d43-7b41-45e3-a3b8-8f041275829b"), "stalls", "A", 2, new Guid("7973dadd-c7f8-42e5-83b9-729a8ff7c614") },
                    { new Guid("0306c201-81d1-441d-bd5b-54dbd35fa3ee"), "stalls", "A", 3, new Guid("7973dadd-c7f8-42e5-83b9-729a8ff7c614") },
                    { new Guid("4ff65e8f-c8a8-44af-b7b3-c9b17852205d"), "loge", "B", 1, new Guid("7973dadd-c7f8-42e5-83b9-729a8ff7c614") },
                    { new Guid("24f905ea-7e41-4e3c-b2c6-83c08b9ae3a2"), "loge", "B", 2, new Guid("7973dadd-c7f8-42e5-83b9-729a8ff7c614") },
                    { new Guid("b160874d-5e67-410b-a6b6-14525c10467a"), "loge", "B", 3, new Guid("7973dadd-c7f8-42e5-83b9-729a8ff7c614") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_Title",
                table: "Movies",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_TheatreId",
                table: "Seats",
                column: "TheatreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Theatres");
        }
    }
}
