using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.Migrations
{
    public partial class ChangedTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_Note_NoteId",
                table: "ToDoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItem",
                table: "ToDoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Note",
                table: "Note");

            migrationBuilder.RenameTable(
                name: "ToDoItem",
                newName: "ToDoItems");

            migrationBuilder.RenameTable(
                name: "Note",
                newName: "ToDoNotes");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItem_NoteId",
                table: "ToDoItems",
                newName: "IX_ToDoItems_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoNotes",
                table: "ToDoNotes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoNotes_NoteId",
                table: "ToDoItems",
                column: "NoteId",
                principalTable: "ToDoNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoNotes_NoteId",
                table: "ToDoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoNotes",
                table: "ToDoNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems");

            migrationBuilder.RenameTable(
                name: "ToDoNotes",
                newName: "Note");

            migrationBuilder.RenameTable(
                name: "ToDoItems",
                newName: "ToDoItem");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItems_NoteId",
                table: "ToDoItem",
                newName: "IX_ToDoItem_NoteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Note",
                table: "Note",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItem",
                table: "ToDoItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_Note_NoteId",
                table: "ToDoItem",
                column: "NoteId",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
