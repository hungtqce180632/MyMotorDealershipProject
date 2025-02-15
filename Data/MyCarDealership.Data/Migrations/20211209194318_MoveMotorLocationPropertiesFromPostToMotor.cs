namespace MyMotorDealershipProject.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MoveMotorLocationPropertiesFromPostToMotor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotorLocationCity",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "MotorLocationCountry",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "LocationCity",
                table: "Motors",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationCountry",
                table: "Motors",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationCity",
                table: "Motors");

            migrationBuilder.DropColumn(
                name: "LocationCountry",
                table: "Motors");

            migrationBuilder.AddColumn<string>(
                name: "MotorLocationCity",
                table: "Posts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotorLocationCountry",
                table: "Posts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
