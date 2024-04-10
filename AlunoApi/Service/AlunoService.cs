using AlunoApi.Context;
using AlunoApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlunoApi.Service
{
    public class AlunoService : IAlunoService
    {

        private readonly AppDbContext _context;

        public AlunoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            //Se não for utilizar a listagem com intuito de realizar atualizações nos objetos
            //utilizar o AsNoTracking, pois você não irá rastrear as entidades
            //ficaria:  return await _context.Alunos.AsNoTracking().ToListAsync();

            try
            {

                return await _context.Aluno.ToListAsync();

            }catch
            {

                throw;

            }
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByName(string nome)
        {
            IEnumerable<Aluno> aluno;
            if (!string.IsNullOrWhiteSpace(nome))
            {
                var alunos = await _context.Aluno.Where(n => n.Nome.Contains(nome)).ToListAsync();
                return alunos;
            }
            else
            {
                aluno = await GetAlunos();
                return aluno;
            }   
        }

        public async Task<Aluno> GetAluno(int id)
        {
            //Nesse caso. Sem estar utilizando um trycath, ou uma validação com ifElse.
            //Necessitariamos de uma implementação de um Middleware de Erros global.

            Aluno aluno = await _context.Aluno.FindAsync(id);
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
