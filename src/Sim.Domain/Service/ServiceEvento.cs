using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{

    public class ServiceEvento : ServiceBase<Evento>, IServiceEvento
    {
        private readonly IRepositoryEvento _evento;
        public ServiceEvento(IRepositoryEvento repositoryEvento)
            :base(repositoryEvento)
        {
            _evento = repositoryEvento;
        }

        public async Task<IEnumerable<Evento>> ListEventosAtivosAsync(IEnumerable<Evento> eventos)
        {
            var t = Task.Run(() => eventos.Where(s => s.EventosAtivos(s)).OrderBy(o => o.Data));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<Evento>> ListEventosCanceladosAsync(IEnumerable<Evento> eventos)
        {
            var t = Task.Run(() => eventos.Where(s => s.EventosCancelados(s)).OrderByDescending(o => o.Data));

            await t;

            return t.Result;
        }
        public async Task<IEnumerable<Evento>> ListEventosFinalizadosAsync(IEnumerable<Evento> eventos)
        {
            var t = Task.Run(() => eventos.Where(s => s.EventosFinalizados(s)).OrderByDescending(o => o.Data));

            await t;

            return t.Result;
        }
        public async Task<Evento> GetCodigoAsync(int codigo)
        {
            return await _evento.GetCodigoAsync(codigo);
        }

        public async Task<Evento> GetCodigoParticipanteAsync(int codigo)
        {
            return await _evento.GetCodigoParticipanteAsync(codigo);
        }

        public async Task<IEnumerable<Evento>> ListNomeAsync(string nome)
        {
            return await _evento.ListNomeAsync(nome);
        }

        public async Task<IEnumerable<Evento>> ListOwnerAsync(string setor)
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
        public async Task<IEnumerable<(string Mes, int Qtde, IEnumerable<Evento>)>> ListEventosPorMesAsync(IEnumerable<Evento> eventos)
        {
            try
            {
                var t = Task.Run(() =>
                {
                    var _lista_meses = new List<(string Mes, int Qtde, IEnumerable<Evento>)>();
                    var _eventos = new List<Evento>();
                    var _meses = new List<(string Mes, int Qtde)>();
                    var _mes = new List<string>();

                    for (int i = 1; i < 13; i++)
                    {
                        _eventos = eventos.Where(s => s.Data.Value.Month == i).ToList();
                        if(_eventos.Any())
                            _lista_meses.Add((_eventos.FirstOrDefault().Data.Value.ToString("MMM"), _eventos.Count, _eventos));
                    }

                    return _lista_meses;
                });

                await t;

                return t.Result;

            }
            catch
            {
                return null;
            }
        }

        public async Task<Evento> GetIdAsync(Guid id)
        {
            return await _evento.GetIdAsync(id);
        }

        public async Task<IEnumerable<Evento>> ListAllAsync()
        {
            return await _evento.ListAllAsync();
        }
    }
}
