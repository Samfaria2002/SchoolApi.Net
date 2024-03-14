using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UdemyApiDotNet.Models;

namespace UdemyApiDotNet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Professor> Professores { get; set; }

        public DbSet<Disciplina> Disciplinas { get; set; }

        public DbSet<AlunoDisciplina> AlunosDisciplinas { get; set; }

        public DbSet<AlunoCurso> AlunosCursos { get; set; }

        public DbSet<Curso> Cursos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<AlunoDisciplina>()
                .HasKey(AD => new {AD.AlunoId, AD.DisciplinaId});

            builder.Entity<AlunoCurso>()
                .HasKey(AD => new {AD.AlunoId, AD.CursoId});

            builder.Entity<Professor>()
                .HasData(new List<Professor>(){
                    new Professor(1, 423, "Lauro", "Abreu", 312321),
                    new Professor(2, 1289, "Roberto", "Fagundes", 12312312),
                    new Professor(3, 61231, "Ronaldo", "Aldo", 555432),
                    new Professor(4, 32, "Rodrigo", "Milho", 983921),
                    new Professor(5, 22, "Takamasa", "Nomuro", 67678961),
                });

            builder.Entity<Curso>()
                .HasData(new List<Curso>{
                    new Curso(1, "Análise e Desenvolvimento de Sistemas"),
                    new Curso(2, "Sistemas de Informação"),
                    new Curso(3, "Ciências da Computação")
                });
            
            builder.Entity<Disciplina>()
                .HasData(new List<Disciplina>{
                    new Disciplina(1, "Matemática", 1, 1),
                    new Disciplina(2, "Física", 2, 3),
                    new Disciplina(3, "Português", 3, 1),
                    new Disciplina(4, "Inglês", 4, 3),
                    new Disciplina(5, "Programação", 5, 1),
                    new Disciplina(6, "Português", 2, 2),
                    new Disciplina(7, "Biologia", 4, 2),
                    new Disciplina(8, "Biologia", 1, 2),
                    new Disciplina(9, "Matemática", 5, 1)
                });
            
            builder.Entity<Aluno>()
                .HasData(new List<Aluno>(){
                    new Aluno(1, 1, "Marta", "Kent", 33225555, new DateTime(2005, 5, 28)),
                    new Aluno(2, 2, "Paula", "Isabela", 3354288, new DateTime(2005, 5, 28)),
                    new Aluno(3, 3, "Laura", "Antonia", 55668899, new DateTime(2005, 5, 28)),
                    new Aluno(4, 4, "Luiza", "Maria", 6565659, new DateTime(2005, 5, 28)),
                    new Aluno(5, 5, "Lucas", "Machado", 565685415, new DateTime(2005, 5, 28)),
                    new Aluno(6, 6, "Pedro", "Alvares", 456454545, new DateTime(2005, 5, 28)),
                    new Aluno(7, 7, "Paulo", "José", 9874512, new DateTime(2005, 5, 28))
                });

            builder.Entity<AlunoDisciplina>()
                .HasData(new List<AlunoDisciplina>() {
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 5 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 1 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 2 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 3 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 4 },
                    new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 5 }
                });
        }

    }
}