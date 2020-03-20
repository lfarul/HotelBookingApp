using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBooking.Migrations
{
    public partial class _10Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountDays",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountDays",
                table: "Reservations");
        }
    }
}
