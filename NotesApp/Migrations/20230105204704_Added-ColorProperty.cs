using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.Migrations
{
    public partial class AddedColorProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ColorClass",
                table: "Notes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da7fb0e6-2265-4d5d-be7e-08a4cdefedda", "AQAAAAEAACcQAAAAEIp2PnJHNKrM6M6FRFc5JBNsSCHTdYgLSyHi35IxMe8UPXImXNCRk11wAWse5A/+IQ==", "ead7204f-ce23-4264-b18d-624e1726ebeb" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ColorClass",
                table: "Notes");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ff2359b-cfee-4d4c-aae4-8e198f46d1c9", "AQAAAAEAACcQAAAAECYheIhoyp6E1Qq/M8brEaF/5K9Nosy51tuJmHNEpTpDgJ6fiwO+tPxC6Q+NBaHh2g==", "1edd5a28-07bc-4243-a38a-2931891ae030" });
        }
    }
}
