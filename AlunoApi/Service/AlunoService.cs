using AlunoApi.Context;
using AlunoApi.Model;
using AlunoApi.Service;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Service
{
    public class AlunosService : IAlunoService
    {
        private readonly AppDbContext _context;

        public AlunosService(AppDbContext context)
        {
            _context = context;
        }

        //Se não for utilizar a listagem com intuito de realizar atualizações nos objetos
        //utilizar o AsNoTracking, pois você não irá rastrear as entidades
        //ficaria:  return await _context.Alunos.AsNoTracking().ToListAsync();
        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _context.Aluno.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            IEnumerable<Aluno> alunos;
            if (!string.IsNullOrWhiteSpace(nome))
            {
                alunos = await _context.Aluno.Where(n => n.Nome.Contains(nome)).ToListAsync();
            }
            else
            {
                alunos = await GetAlunos();
            }
            return alunos;
        }

        public async Task<Aluno> GetAluno(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            return aluno;
        }

        public async Task CreateAluno(Aluno aluno)
        {
            _context.Aluno.Add(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
