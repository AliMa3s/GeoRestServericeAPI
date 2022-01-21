using Microsoft.EntityFrameworkCore.Migrations;

namespace GeoService.DataEF.Migrations
{
    public partial class initDBCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Continent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Population = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "River",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_River", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Population = table.Column<int>(type: "int", nullable: false),
                    Surface = table.Column<double>(type: "float", nullable: false),
                    ContinentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Population = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Country_River",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country_Id = table.Column<int>(type: "int", nullable: false),
                    River_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country_River", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_River_Country",
                        column: x => x.Country_Id,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Country_River_River",
                        column: x => x.River_Id,
                        principalTable: "River",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Continent",
                columns: new[] { "Id", "Name", "Population" },
                values: new object[,]
                {
                    { 1, "Europe", 8000 },
                    { 2, "Asia", 8000 },
                    { 3, "Afrika", 12000 },
                    { 4, "North-America", 12000 },
                    { 5, "South-America", 15000 }
                });

            migrationBuilder.InsertData(
                table: "River",
                columns: new[] { "Id", "Length", "Name" },
                values: new object[,]
                {
                    { 1, 5464.0, "Berlins Wall River" },
                    { 2, 2348.0, "Salween River" },
                    { 3, 6300.0, "Gent Kanal" },
                    { 4, 1012.0, "japanees blosom river" },
                    { 5, 4200.0, "Istanbul River" },
                    { 6, 3625.0, "London bridge river" },
                    { 7, 3142.0, "Moscow kanal" },
                    { 8, 890.0, "New York River" },
                    { 9, 4200.0, "Tehran River" },
                    { 10, 3000.0, "Abotalal River" }
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "ContinentId", "Name", "Population", "Surface" },
                values: new object[,]
                {
                    { 1, 1, "Belgium", 8000, 0.0 },
                    { 2, 1, "Germany", 8000, 0.0 },
                    { 10, 1, "England", 20000, 0.0 },
                    { 3, 2, "China", 12000, 0.0 },
                    { 4, 2, "Japan", 12000, 0.0 },
                    { 5, 2, "Turkey", 15000, 0.0 },
                    { 6, 2, "Iran", 15000, 0.0 },
                    { 9, 2, "Russia", 20000, 0.0 },
                    { 7, 3, "Nigeria", 15000, 0.0 },
                    { 8, 4, "Usa", 15000, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "City",
                columns: new[] { "Id", "CountryId", "Name", "Population" },
                values: new object[,]
                {
                    { 1, 1, "Brussel", 8000 },
                    { 2, 2, "Berlin", 8000 },
                    { 7, 7, "Nairobi", 15000 },
                    { 10, 10, "London", 20000 },
                    { 3, 3, "Peking", 12000 },
                    { 9, 9, "Moscow", 20000 },
                    { 4, 4, "Tokyo", 12000 },
                    { 8, 8, "Newyork", 15000 },
                    { 5, 5, "Istanbul", 15000 },
                    { 6, 6, "Tehran", 15000 }
                });

            migrationBuilder.InsertData(
                table: "Country_River",
                columns: new[] { "Id", "Country_Id", "River_Id" },
                values: new object[,]
                {
                    { 8, 7, 10 },
                    { 7, 9, 7 },
                    { 9, 6, 9 },
                    { 3, 4, 4 },
                    { 2, 3, 2 },
                    { 6, 10, 6 },
                    { 1, 2, 1 },
                    { 4, 1, 3 },
                    { 5, 5, 5 },
                    { 10, 8, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryId",
                table: "City",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_ContinentId",
                table: "Country",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_Country_River_Country_Id",
                table: "Country_River",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Country_River_River_Id",
                table: "Country_River",
                column: "River_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country_River");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "River");

            migrationBuilder.DropTable(
                name: "Continent");
        }
    }
}
