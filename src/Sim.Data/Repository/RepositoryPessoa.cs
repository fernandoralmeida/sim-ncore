using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim.Cross.Data.Repository.SDE
{
    using Sim.Domain.SDE.Entity;
    using Sim.Domain.SDE.Interface;
    using Context;
    public class RepositoryPessoa : RepositoryBase<Pessoa>, IRepositoryPessoa
    {
        public RepositoryPessoa(ApplicationContext dbContext)
            :base(dbContext)
        {

        }

        public IEnumerable<Pessoa> ConsultaByCPF(string cpf)
        {
            return _db.Pessoa.Where(c => c.CPF == cpf).OrderBy(c => c.Nome);
        }

        public IEnumerable<Pessoa> ConsultaByNome(string nome)
        {
            return _db.Pessoa.Where(c => c.Nome.Contains(nome) || c.Nome_Social.Contains(nome)).OrderBy(c => c.Nome);
        }

        public IEnumerable<Pessoa> Top10()
        {
            var top = (from q in _db.Pessoa
                       orderby q.Ultima_Alteracao descending
                       select q).Take(10);

            return top;
        }
    }
}
