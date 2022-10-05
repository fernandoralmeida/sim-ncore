﻿using Sim.Domain.Evento.Model;
using Sim.Domain.Evento.Interfaces.Service;

namespace Sim.Application.Services
{
    using Interfaces;
    public class AppServiceTipo: AppServiceBase<ETipo>, IAppServiceTipo
    {
        private readonly IServiceTipo _tipo;
        public AppServiceTipo(IServiceTipo tipo)
            :base(tipo)
        {
            _tipo = tipo;
        }

        public async Task<ETipo> GetIdAsync(Guid id)
        {
            return await _tipo.GetIdAsync(id);
        }

        public async Task<IEnumerable<ETipo>> ListAllAsync()
        {
            return await _tipo.ListAllAsync();
        }

        public async Task<IEnumerable<ETipo>> ListTipoOwnerAsync(string owner)
        {
            return await _tipo.ListTipoOwnerAsync(owner);
        }
    }
}
