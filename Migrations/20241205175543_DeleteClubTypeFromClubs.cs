using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChessStatistics.Migrations
{
    /// <inheritdoc />
    public partial class DeleteClubTypeFromClubs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Clubs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Clubs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
