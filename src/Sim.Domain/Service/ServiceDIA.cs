﻿using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{
    public class ServiceDIA : ServiceBase<DIA>, IServiceDIA
    {
        private readonly IRepositoryDIA _dia;
        public ServiceDIA(IRepositoryDIA repositoryDIA):base(repositoryDIA)
        {
            _dia = repositoryDIA;
        }

        public async Task<DIA> GetIdAsync(Guid id)
        {
            return await _dia.GetIdAsync(id);
        }

        public async Task<IEnumerable<DIA>> ListAllAsync()
        {
            return await _dia.ListAllAsync();
        }

        public async Task<IEnumerable<DIA>> ListAtividadeAsync(string atividade)
        {
            return await _dia.ListAtividadeAsync(atividade);
        }

        public async Task<IEnumerable<DIA>> ListAuxiliarAsync(string nome)
        {
            return await _dia.ListAuxiliarAsync(nome);
        }

        public async Task<IEnumerable<DIA>> ListTitularAsync(string nome)
        {
            return await _dia.ListTitularAsync(nome);
        }

    }
}
