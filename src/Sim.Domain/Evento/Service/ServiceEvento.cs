﻿
namespace Sim.Domain.Evento.Service
{
    using Model;
    using Interfaces.Repository;
    using Interfaces.Service;
    public class ServiceEvento : ServiceBase<EEvento>, IServiceEvento
    {
        private readonly IRepositoryEvento _evento;
        public ServiceEvento(IRepositoryEvento repositoryEvento)
            :base(repositoryEvento)
        {
            _evento = repositoryEvento;
        }
        
        public async Task<EEvento> GetCodigoAsync(int codigo)
        {
            return await _evento.GetCodigoAsync(codigo);
        }

        public async Task<EEvento> GetCodigoParticipanteAsync(int codigo)
        {
            return await _evento.GetCodigoParticipanteAsync(codigo);
        }

        public async Task<IEnumerable<EEvento>> ListNomeAsync(string nome)
        {
            return await _evento.ListNomeAsync(nome);
        }

        public async Task<IEnumerable<EEvento>> ListOwnerAsync(string setor)
        {
            return await _evento.ListOwnerAsync(setor);
        }

        public int LastCodigo()
        {
            return _evento.LastCodigo();
        }

        /// <summary>
        /// Lista eventos classificados por mês
        /// </summary>
        /// <param name="eventos">Lista de eventos</param>
        /// <returns>Lista de eventos por mês</returns>
        public async Task<IEnumerable<(string Mes, int Qtde, IEnumerable<EEvento>)>> ListEventosPorMesAsync(IEnumerable<EEvento> eventos)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var _lista_meses = new List<(string Mes, int Qtde, IEnumerable<EEvento>)>();
                    var _eventos = new List<EEvento>();                    
                
                    for(int a = 2020; a <= DateTime.Now.Year; a++ )
                    {
                        for (int i = 1; i < 13; i++)
                        {
                            _eventos = eventos.Where(s => s.Data.Value.Month == i && s.Data.Value.Year == a).OrderBy(o => o.Data).ToList();
                            if(_eventos.Any())
                                _lista_meses.Add((string.Format("{0} {1}", a, _eventos.FirstOrDefault().Data.Value.ToString("MMM")), _eventos.Count, _eventos));
                        }

                    }
                    return _lista_meses;
                });
            }
            catch
            {
                return null;
            }
        }

        public async Task<EEvento> GetIdAsync(Guid id)
        {
            return await _evento.GetIdAsync(id);
        }

        public async Task<IEnumerable<EEvento>> ListAllAsync()
        {
            return await _evento.ListAllAsync();
        }

        public async Task<EEvento> GetEventoToListParticipantes(int codigo)
        {
            return await _evento.GetEventoToListParticipantes(codigo);
        }

        public async Task<IEnumerable<EEvento>> DoListEventByParam(string nome, string tipo, string setor, int ano)
        {
            return await _evento.DoListEventByParam(nome, tipo, setor, ano);
        }

        public async Task<IEnumerable<EEvento>> DoListAsyncBy(string param)
        {
            return await _evento.DoListAsyncBy(param);
        }

        public async Task<IEnumerable<EEvento>> DoListSituacaoAsyncBy(IEnumerable<EEvento> eventos, EEvento.ESituacao situacao)
        {
            return await Task.Run(() => eventos.Where(s => s.EventoBySituacao(s, situacao)).OrderByDescending(o => o.Data));
        }
    }
}