using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.Migrations
{
    public partial class AddedSharedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedUser_Notes_NoteId",
                table: "SharedUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedUser",
                table: "SharedUser");

            migrationBuilder.RenameTable(
                name: "SharedUser",
                newName: "SharedUsers");

            migrationBuilder.RenameIndex(
                name: "IX_SharedUser_NoteId",
                table: "SharedUsers",
                newName: "IX_SharedUsers_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedUsers",
                table: "SharedUsers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2e65848-3037-49ea-a7d2-8d8a68194028", "AQAAAAEAACcQAAAAEOxbEY9l4pvAC0Pobh2Vn9RwiH/xzy+79i9xRPo/Cnud/r2eaWRVsNI1hOcDhA9tGA==", "fab5e2ff-cb42-4363-9766-ee67f4ee1676" });

            migrationBuilder.AddForeignKey(
                name: "FK_SharedUsers_Notes_NoteId",
                table: "SharedUsers",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SharedUsers_Notes_NoteId",
                table: "SharedUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedUsers",
                table: "SharedUsers");

            migrationBuilder.RenameTable(
                name: "SharedUsers",
                newName: "SharedUser");

            migrationBuilder.RenameIndex(
                name: "IX_SharedUsers_NoteId",
                table: "SharedUser",
                newName: "IX_SharedUser_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedUser",
                table: "SharedUser",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8aaaa9d5-b24f-4442-82c2-26905f543925", "AQAAAAEAACcQAAAAEDKZZ2GB6XjfiQZRrAzechkXXbMiSR6JC6L4mO2OZPDuJT90myy6B60OQs84fXWcPA==", "7c1da5b4-d946-4859-ae54-baedd75613e9" });

            migrationBuilder.AddForeignKey(
                name: "FK_SharedUser_Notes_NoteId",
                table: "SharedUser",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
