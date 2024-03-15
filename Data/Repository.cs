using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UdemyApiDotNet.Models;
using Microsoft.EntityFrameworkCore;
using UdemyApiDotNet.Helpers;

namespace UdemyApiDotNet.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class {
            _context.Update(entity);
        }

        public bool SaveChanges() {
            return(_context.SaveChanges() > 0);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor) {

            //query aluno
            IQueryable<Aluno> query = _context.Alunos;
            
            //join
            if (includeProfessor) {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(d => d.Professor);
            }

            //ordenação
            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();
        }

        public async Task<PageList<Aluno>> GetAllAlunosAsync(
            PageParameters pageParameters,
            bool includeProfessor
            ) {
            
            //query aluno
            IQueryable<Aluno> query = _context.Alunos;
            
            //join
            if (includeProfessor) {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(d => d.Professor);
            }

            //ordenação
            query = query.AsNoTracking().OrderBy(a => a.Id);

            if(!string.IsNullOrEmpty(pageParameters.Nome)) {
                query = query.Where(aluno => aluno.Nome
                            .ToUpper()
                            .Contains(pageParameters.Nome.ToUpper()) ||
                            aluno.Sobrenome
                            .ToUpper()
                            .Contains(pageParameters.Nome.ToUpper())
                );
            }

            if (pageParameters.Matricula > 0) {
                query = query.Where(aluno => aluno.Matricula == pageParameters.Matricula);
            }

            if (pageParameters.Ativo != null) {
                query = query.Where(aluno => aluno.Ativo == (pageParameters.Ativo != 0));
            }
            

            //return await query.ToListAsync();
            return await PageList<Aluno>.CreateAsync(query, pageParameters.PageNumber, pageParameters.PageSize);
        }

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false) {

            //query aluno
            IQueryable<Aluno> query = _context.Alunos;
            
            //join
            if (includeProfessor) {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(d => d.Professor);
            }

            //ordenação e seleção
            query = query.AsNoTracking().OrderBy(a => a.Id)
                        .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));
            
            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool includeProfessor = false) {

            //query aluno
            IQueryable<Aluno> query = _context.Alunos;
            
            //join
            if (includeProfessor) {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(d => d.Professor);
            }

            //ordenação
            query = query.AsNoTracking().OrderBy(a => a.Id)
                        .Where(aluno => aluno.Id == alunoId);
            
            //FirstOrDefault porque estou retornando apenas 1 aluno
            return query.FirstOrDefault();
        }

        public Aluno GetAlunoByName(string nome, string sobrenome, bool includeProfessor = false) {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor) {
                query = query.Include(a => a.AlunosDisciplinas)
                            .ThenInclude(ad => ad.Disciplina)
                            .ThenInclude(d => d.Professor);
            }

            query = query.Where(an => an.Nome == nome)
                        .Where(aso => aso.Sobrenome == sobrenome);
            
            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includeAlunos = false) {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos) {
                query = query.Include(p => p.Disciplina)
                            .ThenInclude(ad => ad.AlunosDisciplinas)
                            .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return query.ToArray();

        }

        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAlunos = false) {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos) {
                query = query.Include(p => p.Disciplina)
                            .ThenInclude(ad => ad.AlunosDisciplinas)
                            .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking().OrderBy(aluno => aluno.Id)
                        .Where(aluno => aluno.Disciplina.Any(d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));

            return query.ToArray();         
        }

        public Professor GetProfessorById(int professorId, bool includeAlunos = false) {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos) {
                query = query.Include(p => p.Disciplina)
                            .ThenInclude(ad => ad.AlunosDisciplinas)
                            .ThenInclude(a => a.Aluno);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id)
                        .Where(professor => professor.Id == professorId);
            
            return query.FirstOrDefault();
        }

        public Professor GetProfessorByName(string nome, string sobrenome, bool includeAlunos = false) {
            IQueryable<Professor> query = _context.Professores;

            if (includeAlunos) {
                query = query.Include(p => p.Disciplina)
                            .ThenInclude(ad => ad.AlunosDisciplinas)
                            .ThenInclude(a => a.Aluno);
            }

            query = query.Where(pn => pn.Nome == nome)
                        .Where(pso => pso.Sobrenome == sobrenome);
            
            return query.FirstOrDefault();
        }
    }
}