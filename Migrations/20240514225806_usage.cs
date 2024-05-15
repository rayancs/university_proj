using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace university_proj.Migrations
{
    /// <inheritdoc />
    public partial class usage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "usage",
                table: "Shoe",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "usage",
                table: "Shoe");
        }
    }
}
