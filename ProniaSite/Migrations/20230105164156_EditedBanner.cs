using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProniaSite.Migrations
{
    /// <inheritdoc />
    public partial class EditedBanner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Banners");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Banners",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
