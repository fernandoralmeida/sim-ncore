﻿using Sim.Domain.Entity;
using Sim.Domain.Interface.IService;
using Sim.Application.Interfaces;

namespace Sim.Application.Services
{
    public class AppServiceEvento : AppServiceBase<Evento>, IAppServiceEvento
    {
        private readonly IServiceEvento _evento;
        public AppServiceEvento(IServiceEvento evento)
            :base(evento)
        {
            _evento = evento;
        }

        public async Task<IEnumerable<Evento>> DoListEventByParam(string nome, string tipo, string setor, int ano)
        {
            return await _evento.DoListEventByParam(nome, tipo, setor, ano);
        }

        public async Task<Evento> GetCodigoAsync(int codigo)
        {
            return await _evento.GetCodigoAsync(codigo);
        }

        public async Task<Evento> GetCodigoParticipanteAsync(int codigo)
        {
            return await _evento.GetCodigoParticipanteAsync(codigo);
        }

        public async Task<Evento> GetEventoToListParticipantes(int codigo)
        {
            return await _evento.GetEventoToListParticipantes(codigo);
        }

        public async Task<Evento> GetIdAsync(Guid id)
        {
            return await _evento.GetIdAsync(id);
        }

        public  int LastCodigo()
        {
            return _evento.LastCodigo();
        }

        public async Task<IEnumerable<Evento>> ListAllAsync()
        {
            return await _evento.ListAllAsync();
        }

        public async Task<IEnumerable<Evento>> ListEventosAtivosAsync(int ano)
        {
            return await _evento.ListEventosAtivosAsync(ListAllAsync().Result.Where(s => s.Data.Value.Year == ano));
        }

        public async Task<IEnumerable<Evento>> ListEventosCanceladosAsync(int ano)
        {
            return await _evento.ListEventosCanceladosAsync(ListAllAsync().Result.Where(s => s.Data.Value.Year == ano));
        }

        public async Task<IEnumerable<Evento>> ListEventosFinalizadosAsync(int ano)
        {
            return await _evento.ListEventosFinalizadosAsync(ListAllAsync().Result.Where(s => s.Data.Value.Year == ano));
        }

        public async Task<IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)>> ListEventosPorMesAsync(IEnumerable<Evento> eventos)
        {
            return await _evento.ListEventosPorMesAsync(eventos);
        }

        public async Task<IEnumerable<Evento>> ListNomeAsync(string nome)
        {
            return await _evento.ListNomeAsync(nome);
        }

        public async Task<IEnumerable<Evento>> ListOwnerAsync(string setor)
        {
            return await _evento.ListOwnerAsync(setor);
        }
    }
}
