using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServicePessoa : AppServiceBase<Pessoa>, IAppServicePessoa
    {
        private readonly IServicePessoa _pessoa;

        public AppServicePessoa(IServicePessoa pessoa):base(pessoa)
        {
            _pessoa = pessoa;
        }

        public async Task<IEnumerable<Pessoa>> ConsultaCPFAsync(string cpf)
        {
            return await _pessoa.ConsultaCPFAsync(cpf);
        }

        public async Task<IEnumerable<Pessoa>> ConsultaNomeAsync(string nome)
        {
            return await _pessoa.ConsultaNomeAsync(nome);
        }

        public async Task<IEnumerable<Pessoa>> DoListAsyncBy(string param)
        {
            return await _pessoa.DoListAsyncBy(param);
        }

        public async Task<Pessoa> GetIdAsync(Guid id)
        {
            return await _pessoa.GetIdAsync(id);
        }

        public async Task<IEnumerable<Pessoa>> ListAllAsync()
        {
            return await _pessoa.ListAllAsync();
        }

        public async Task<IEnumerable<Pessoa>> ListTop10Async()
        {
            return await _pessoa.ListTop10Async();
        }
    }
}
