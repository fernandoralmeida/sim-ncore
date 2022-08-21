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
                return await Task.Run(() =>
                {
                    var _lista_meses = new List<(string Mes, int Qtde, IEnumerable<Evento>)>();
                    var _eventos = new List<Evento>();                    
                
                    for(int a = 2020; a <= DateTime.Now.Year; a++ )
                    {
                        for (int i = 1; i < 13; i++)
                        {
                            _eventos = eventos.Where(s => s.Data.Value.Month == i && s.Data.Value.Year == a).ToList();
                            if(_eventos.Any())
                                _lista_meses.Add((string.Format("{0}/{1}", _eventos.FirstOrDefault().Data.Value.ToString("MMM"), a), _eventos.Count, _eventos));
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

        public async Task<Evento> GetIdAsync(Guid id)
        {
            return await _evento.GetIdAsync(id);
        }

        public async Task<IEnumerable<Evento>> ListAllAsync()
        {
            return await _evento.ListAllAsync();
        }

        public async Task<Evento> GetEventoToListParticipantes(int codigo)
        {
            return await _evento.GetEventoToListParticipantes(codigo);
        }

        public async Task<IEnumerable<Evento>> DoListEventByParam(string nome, string tipo, string setor, int ano)
        {
            return await _evento.DoListEventByParam(nome, tipo, setor, ano);
        }

        public async Task<IEnumerable<Evento>> DoListAsyncBy(string param)
        {
            return await _evento.DoListAsyncBy(param);
        }

        public async Task<IEnumerable<Evento>> DoListSituacaoAsyncBy(IEnumerable<Evento> eventos, Evento.ESituacao situacao)
        {
            return await Task.Run(() => eventos.Where(s => s.EventoBySituacao(s, situacao)).OrderByDescending(o => o.Data));
        }
    }
}
