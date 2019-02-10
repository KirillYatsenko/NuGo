using Microsoft.EntityFrameworkCore.Migrations;

namespace EventWebScrapper.Migrations
{
    public partial class eventratingfieldadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Deleted",
                table: "Events",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Events",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<short>(
                name: "Deleted",
                table: "EventDates",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Events");

            migrationBuilder.AlterColumn<short>(
                name: "Deleted",
                table: "Events",
                nullable: false,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<short>(
                name: "Deleted",
                table: "EventDates",
                nullable: false,
                oldClrType: typeof(short));
        }
    }
}
