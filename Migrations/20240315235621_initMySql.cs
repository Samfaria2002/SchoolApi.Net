using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UdemyApiDotNet.Migrations
{
    public partial class initMySql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Matricula = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Telefone = table.Column<int>(nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Registro = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Telefone = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunosCursos",
                columns: table => new
                {
                    AlunoId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Nota = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosCursos", x => new { x.AlunoId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_AlunosCursos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunosCursos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    CargaHoraria = table.Column<int>(nullable: false),
                    PrerequisitoId = table.Column<int>(nullable: true),
                    ProfessorId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Disciplinas_PrerequisitoId",
                        column: x => x.PrerequisitoId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunosDisciplinas",
                columns: table => new
                {
                    AlunoId = table.Column<int>(nullable: false),
                    DisciplinaId = table.Column<int>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Nota = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunosDisciplinas", x => new { x.AlunoId, x.DisciplinaId });
                    table.ForeignKey(
                        name: "FK_AlunosDisciplinas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunosDisciplinas_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFim", "DataInicio", "DataNascimento", "Matricula", "Nome", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(434), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Marta", "Kent", 33225555 },
                    { 2, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(2670), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Paula", "Isabela", 3354288 },
                    { 3, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(2751), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Laura", "Antonia", 55668899 },
                    { 4, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(2754), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Luiza", "Maria", 6565659 },
                    { 5, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(2757), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Lucas", "Machado", 565685415 },
                    { 6, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(2763), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Pedro", "Alvares", 456454545 },
                    { 7, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(2765), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Paulo", "José", 9874512 }
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Análise e Desenvolvimento de Sistemas" },
                    { 2, "Sistemas de Informação" },
                    { 3, "Ciências da Computação" }
                });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Ativo", "DataFim", "DataInicio", "Nome", "Registro", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 218, DateTimeKind.Local).AddTicks(882), "Lauro", 423, "Abreu", 312321 },
                    { 2, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 219, DateTimeKind.Local).AddTicks(3488), "Roberto", 1289, "Fagundes", 12312312 },
                    { 3, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 219, DateTimeKind.Local).AddTicks(3564), "Ronaldo", 61231, "Aldo", 555432 },
                    { 4, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 219, DateTimeKind.Local).AddTicks(3567), "Rodrigo", 32, "Milho", 983921 },
                    { 5, true, null, new DateTime(2024, 3, 15, 20, 56, 21, 219, DateTimeKind.Local).AddTicks(3569), "Takamasa", 22, "Nomuro", 67678961 }
                });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PrerequisitoId", "ProfessorId" },
                values: new object[,]
                {
                    { 1, 0, 1, "Matemática", null, 1 },
                    { 8, 0, 2, "Biologia", null, 1 },
                    { 2, 0, 3, "Física", null, 2 },
                    { 6, 0, 2, "Português", null, 2 },
                    { 3, 0, 1, "Português", null, 3 },
                    { 4, 0, 3, "Inglês", null, 4 },
                    { 7, 0, 2, "Biologia", null, 4 },
                    { 5, 0, 1, "Programação", null, 5 },
                    { 9, 0, 1, "Matemática", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "AlunosDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataInicio", "Nota" },
                values: new object[,]
                {
                    { 2, 1, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4551), null },
                    { 4, 5, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4569), null },
                    { 2, 5, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4557), null },
                    { 1, 5, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4549), null },
                    { 7, 4, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4585), null },
                    { 6, 4, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4579), null },
                    { 5, 4, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4570), null },
                    { 4, 4, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4567), null },
                    { 1, 4, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4527), null },
                    { 7, 3, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4584), null },
                    { 5, 5, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4572), null },
                    { 6, 3, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4576), null },
                    { 7, 2, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4582), null },
                    { 6, 2, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4575), null },
                    { 3, 2, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4560), null },
                    { 2, 2, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4553), null },
                    { 1, 2, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(3918), null },
                    { 7, 1, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4581), null },
                    { 6, 1, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4573), null },
                    { 4, 1, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4566), null },
                    { 3, 1, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4559), null },
                    { 3, 3, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4562), null },
                    { 7, 5, null, new DateTime(2024, 3, 15, 20, 56, 21, 221, DateTimeKind.Local).AddTicks(4587), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunosCursos_CursoId",
                table: "AlunosCursos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunosDisciplinas_DisciplinaId",
                table: "AlunosDisciplinas",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_CursoId",
                table: "Disciplinas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_PrerequisitoId",
                table: "Disciplinas",
                column: "PrerequisitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_ProfessorId",
                table: "Disciplinas",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunosCursos");

            migrationBuilder.DropTable(
                name: "AlunosDisciplinas");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
