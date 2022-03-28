using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.API.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Perfil = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "todo",
                columns: table => new
                {
                    TodoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateInsertion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateConclusion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_todo", x => x.TodoId);
                    table.ForeignKey(
                        name: "FK_todo_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "UserId", "Email", "FullName", "Password", "Perfil" },
                values: new object[] { 1, "admin@admin.com", "Walterli Valadares Junior", "8D-96-9E-EF-6E-CA-D3-C2-9A-3A-62-92-80-E6-86-CF-0C-3F-5D-5A-86-AF-F3-CA-12-02-0C-92-3A-DC-6C-92", 1 });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "UserId", "Email", "FullName", "Password", "Perfil" },
                values: new object[] { 2, "teste_um@teste_um.com", "Walterli Valadares Junior", "8D-96-9E-EF-6E-CA-D3-C2-9A-3A-62-92-80-E6-86-CF-0C-3F-5D-5A-86-AF-F3-CA-12-02-0C-92-3A-DC-6C-92", 0 });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "UserId", "Email", "FullName", "Password", "Perfil" },
                values: new object[] { 3, "teste_dois@teste_dois.com", "Walterli Valadares Junior", "8D-96-9E-EF-6E-CA-D3-C2-9A-3A-62-92-80-E6-86-CF-0C-3F-5D-5A-86-AF-F3-CA-12-02-0C-92-3A-DC-6C-92", 0 });

            migrationBuilder.InsertData(
                table: "todo",
                columns: new[] { "TodoId", "DateConclusion", "DateInsertion", "DateLastUpdate", "Deadline", "Description", "Status", "UserId" },
                values: new object[,]
                {
                    { 9, null, new DateTime(2022, 3, 28, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(422), null, new DateTime(2022, 4, 11, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(423), "Gerenciar a equipe", 1, 1 },
                    { 1, null, new DateTime(2022, 3, 24, 14, 38, 20, 95, DateTimeKind.Local).AddTicks(1490), null, new DateTime(2022, 4, 2, 14, 38, 20, 95, DateTimeKind.Local).AddTicks(9088), "Primeira tarefa", 0, 2 },
                    { 2, null, new DateTime(2022, 3, 26, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(359), null, new DateTime(2022, 3, 31, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(373), "Segunda tarefa", 1, 2 },
                    { 3, null, new DateTime(2022, 3, 27, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(400), null, new DateTime(2022, 3, 29, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(402), "Terceira tarefa", 2, 2 },
                    { 4, null, new DateTime(2022, 3, 18, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(404), null, new DateTime(2022, 3, 27, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(406), "Quarta tarefa", 2, 2 },
                    { 5, null, new DateTime(2022, 3, 19, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(408), null, new DateTime(2022, 3, 26, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(409), "Quinta tarefa", 1, 2 },
                    { 6, null, new DateTime(2022, 3, 18, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(411), null, new DateTime(2022, 3, 27, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(413), "Primeira tarefa", 2, 3 },
                    { 7, null, new DateTime(2022, 3, 19, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(415), null, new DateTime(2022, 3, 26, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(416), "Segunda tarefa", 1, 3 },
                    { 8, null, new DateTime(2022, 3, 24, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(418), null, new DateTime(2022, 4, 2, 14, 38, 20, 96, DateTimeKind.Local).AddTicks(419), "Terceira tarefa", 0, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_todo_UserId",
                table: "todo",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "todo");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
