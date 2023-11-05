using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigHub.Data.Migrations
{
    public partial class AddForgeinkeyPropertiesToGig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gigs_AspNetUsers_ArtistId",
                table: "Gigs");
            /*
                    migrationBuilder.DropColumn(
                        name: "Artist_Id",
                        table: "Gigs");

                    migrationBuilder.DropColumn(
                        name: "Genre_Id",
                        table: "Gigs");
            */

            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Gigs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gigs_AspNetUsers_ArtistId",
                table: "Gigs",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gigs_AspNetUsers_ArtistId",
                table: "Gigs");

            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Gigs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
            /*
            migrationBuilder.AddColumn<string>(
                name: "Artist_Id",
                table: "Gigs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "Genre_Id",
                table: "Gigs",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
            */
            migrationBuilder.AddForeignKey(
                name: "FK_Gigs_AspNetUsers_ArtistId",
                table: "Gigs",
                column: "ArtistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
