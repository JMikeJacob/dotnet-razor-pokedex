using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pokemon",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type1 = table.Column<string>(type: "TEXT", nullable: false),
                    Type2 = table.Column<string>(type: "TEXT", nullable: true),
                    Height = table.Column<double>(type: "REAL", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Definition = table.Column<string>(type: "TEXT", nullable: true),
                    PhotoPath = table.Column<string>(type: "TEXT", nullable: true),
                    Genus = table.Column<string>(type: "TEXT", nullable: true),
                    Habitat = table.Column<string>(type: "TEXT", nullable: true),
                    Generation = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemon", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PokemonAbility",
                columns: table => new
                {
                    AbilityId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PokemonId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Effect = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbility", x => x.AbilityId);
                    table.ForeignKey(
                        name: "FK_PokemonAbility_Pokemon_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemon",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAbility_PokemonId",
                table: "PokemonAbility",
                column: "PokemonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokemonAbility");

            migrationBuilder.DropTable(
                name: "Pokemon");
        }
    }
}
