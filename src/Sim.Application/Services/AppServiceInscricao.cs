using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceInscricao : AppServiceBase<Inscricao>, IAppServiceInscricao
    {
        private readonly IServiceInscricao _inscricao;
        public AppServiceInscricao(IServiceInscricao serviceInscricao)
            :base(serviceInscricao)
        {
            _inscricao = serviceInscricao;
        }

        public async Task<Inscricao> GetIdAsync(Guid id)
        {
            return await _inscricao.GetIdAsync(id);
        }

        public async Task<Inscricao> GetInscritoAsync(Guid id)
        {
            return await _inscricao.GetInscritoAsync(id);
        }

        public bool JaInscrito(string cpf, int evento)
        {
            return _inscricao.JaInscrito(cpf, evento);
        }

        public int LastCodigo()
        {
            return _inscricao.LastCodigo();
        }

        public async Task<IEnumerable<Inscricao>> ListAllAsync()
        {
            return await _inscricao.ListAllAsync();
        }

        public async Task<IEnumerable<Inscricao>> ListEventoAsync(string evento)
        {
            return await _inscricao.ListEventoAsync(evento);
        }

        public async Task<IEnumerable<Inscricao>> ListParticipanteAsync(string nome)
        {
            return await _inscricao.ListParticipanteAsync(nome);
        }

        public async Task<IEnumerable<Inscricao>> ListTipoAsync(string evento)
        {
            return await _inscricao.ListTipoAsync(evento);
        }
    }
}
