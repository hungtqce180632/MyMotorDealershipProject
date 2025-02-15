namespace MyMotorDealershipProject.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    
    public partial class PostEntityExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "SellerName",
                table: "Posts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SellerPhoneNumber",
                table: "Posts",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotorLocationCity",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "MotorLocationCountry",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SellerName",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SellerPhoneNumber",
                table: "Posts");
        }
    }
}
