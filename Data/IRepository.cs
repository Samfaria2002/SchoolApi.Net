using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyApiDotNet.Models;
using UdemyApiDotNet.Helpers;

namespace UdemyApiDotNet.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        Task<PageList<Aluno>> GetAllAlunosAsync(PageParameters pageParameters, bool includeProfessor);
        Aluno[] GetAllAlunos(bool includeProfessor);
        Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false);
        Aluno GetAlunoById(int alunoId, bool includeProfessor = false);
        Aluno GetAlunoByName(string nome, string sobrenome, bool includeProfessor = false);
        Professor[] GetAllProfessores(bool includeAlunos = false);
        Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false);
        Professor GetProfessorById(int professorId, bool includeAlunos = false);
        Professor GetProfessorByName(string nome, string sobrenome, bool includeAlunos = false);

    }
}