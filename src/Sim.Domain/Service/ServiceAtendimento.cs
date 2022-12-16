using System.Linq.Expressions;
using Sim.Domain.Entity;
using Sim.Domain.Interface.IRepository;
using Sim.Domain.Interface.IService;

namespace Sim.Domain.Service
{

    public class ServiceAtendimento : ServiceBase<EAtendimento>, IServiceAtendimento
    {
        private readonly IRepositoryAtendimento _atendimento;
        public ServiceAtendimento(IRepositoryAtendimento repositoryAtendimento)
            :base(repositoryAtendimento)
        {
            _atendimento = repositoryAtendimento;
        }

        public async Task<EAtendimento> GetAtendimentoAsync(Guid id)
        {
            return await _atendimento.GetAtendimentoAsync(id);
        }

        public async Task<EAtendimento> GetIdAsync(Guid id)
        {
            return await _atendimento.GetIdAsync(id);
        }

        public async Task<IEnumerable<EAtendimento>> ListAllAsync()
        {
            return await _atendimento.ListAllAsync();
        }

        public async Task<IEnumerable<EAtendimento>> ListAtendimentoAtivoAsync(string userid)
        {
            return await _atendimento.ListAtendimentoAtivoAsync(userid);
        }

        public async Task<IEnumerable<EAtendimento>> ListAtendimentosAtivosAsync()
        {
            return await _atendimento.ListAtendimentosAtivosAsync();
        }

        public async Task<IEnumerable<EAtendimento>> ListAtendimentosCanceladosAsync(string userid)
        {
            return await _atendimento.ListAtendimentosCanceladosAsync(userid);
        }

        public async Task<IEnumerable<EAtendimento>> ListCanalAsync(string canal)
        {
            return await _atendimento.ListCanalAsync(canal);
        }

        public async Task<IEnumerable<EAtendimento>> ListDateAsync(DateTime? dateTime)
        {
            return await _atendimento.ListDateAsync(dateTime);
        }

        public async Task<IEnumerable<EAtendimento>> ListEmpresaAsync(string cnpj)
        {
            return await _atendimento.ListEmpresaAsync(cnpj);
        }

        public async Task<IEnumerable<EAtendimento>> ListMeusAtendimentosAsync(string userid, DateTime? date)
        {
            return await _atendimento.ListMeusAtendimentosAsync(userid, date);
        }

        public async Task<IEnumerable<EAtendimento>> ListMeusAtendimentosRaeAsync(string userid)
        {
            return await _atendimento.ListMeusAtendimentosRaeAsync(userid);
        }

        public async Task<IEnumerable<EAtendimento>> ListMonthAsync(DateTime? month)
        {
            return await _atendimento.ListMonthAsync(month);
        }

        public async Task<IEnumerable<EAtendimento>> ListParamAsync(List<object> lparam)
        {
            return await _atendimento.ListParamAsync(lparam);
        }

        public async Task<IEnumerable<EAtendimento>> ListPeriodoAsync(DateTime? dataI, DateTime? dataF)
        {
            return await _atendimento.ListPeriodoAsync(dataI, dataF);
        }

        public async Task<IEnumerable<EAtendimento>> ListPessoaAsync(string cpf)
        {
            return await _atendimento.ListPessoaAsync(cpf);
        }

        public async Task<IEnumerable<EAtendimento>> ListRaeNaoLancadosAsync(IEnumerable<EAtendimento> atendimentos)
        {
            return await Task.Run(() => atendimentos.Where(a => a.RaeNaoLancados(a)).OrderByDescending(o => o.Data));
        }

        public async Task<IEnumerable<EAtendimento>> ListRaeLancadosAsync(IEnumerable<EAtendimento> atendimentos)
        {
            return await Task.Run(() => atendimentos.Where(s => s.RaeLancados(s)));
        }

        public async Task<IEnumerable<EAtendimento>> ListServicosAsync(string servicos)
        {
            return await _atendimento.ListServicosAsync(servicos);
        }

        public async Task<IEnumerable<EAtendimento>> ListSetorAsync(string setor)
        {
            return await _atendimento.ListSetorAsync(setor);
        }

        public async Task<IEnumerable<EAtendimento>> ListUserNameAsync(string username)
        {
            return await _atendimento.ListUserNameAsync(username);
        }

        public async Task<IEnumerable<EAtendimento>> ListUserNamePeriodoAsync(string username, DateTime? date)
        {
            return await _atendimento.ListUserNamePeriodoAsync(username, date);
        }

        /*** BI Area ***/
        private readonly List<string> _meses = new();
        private readonly List<string> _pessoas_mes = new();
        private readonly List<string> _empresas_mes = new();
        private readonly List<string> _mes_servicos = new();
        private readonly List<string> _pessoas_mes_servicos = new();
        private readonly List<string> _empresas_mes_servicos = new();
        private readonly List<string> _servicos = new();

        private void ConstruirMeses(EAtendimento at_param, string[] serv_param)
        {
            _meses.Add(at_param.Data.Value.ToString("MMM"));

            if (at_param.Empresa != null)
                _empresas_mes.Add(at_param.Data.Value.ToString("MMM") + " Empresas");
            else
                _pessoas_mes.Add(at_param.Data.Value.ToString("MMM") + " Pessoas");

            foreach (string sv in serv_param.Where(s => s.Any()))
            {
                _mes_servicos.Add(at_param.Data.Value.ToString("MMM") + " Servicos");

                if (at_param.Empresa != null)
                    _empresas_mes_servicos.Add(at_param.Data.Value.Month + "Servicos");
                else
                    _pessoas_mes_servicos.Add(at_param.Data.Value.Month + "Servicos");
            }
        }

        private void AppUserMonth(EAtendimento at_param, string[] serv_param)
        {
            _meses.Add(at_param.Owner_AppUser_Id);

            if (at_param.Empresa != null)
                _empresas_mes.Add(at_param.Owner_AppUser_Id + " Empresas");
            else
                _pessoas_mes.Add(at_param.Owner_AppUser_Id + " Pessoas");

            foreach (string sv in serv_param.Where(s => s.Any()))
            {
                _mes_servicos.Add(at_param.Owner_AppUser_Id + " Servicos");

                if (at_param.Empresa != null)
                    _empresas_mes_servicos.Add(at_param.Owner_AppUser_Id + "PJ Servicos");
                else
                    _pessoas_mes_servicos.Add(at_param.Owner_AppUser_Id + "PF Servicos");
            }
        }

        public async Task<BIAtendimentos> ToListBIAtendimentos(DateTime periodo)
        {
            var d1 = new DateTime(periodo.Year, 01, 01);
            var d2 = new DateTime(periodo.Year, 12, 31);

            var list = await _atendimento.ListPeriodoAsync(d1, d2);

            var r_all = new BIAtendimentos();

            var t = Task.Run(() => {

                try
                {
                    _meses.Clear();
                    _pessoas_mes.Clear();
                    _empresas_mes.Clear();

                    _mes_servicos.Clear();
                    _pessoas_mes_servicos.Clear();
                    _empresas_mes_servicos.Clear();

                    var _meses_t = new List<(string Nome, string Atendimento, string Servico)>();
                    var _l_servicos = new List<(string Servico, int Quantidade)>();

                    foreach (EAtendimento at in list.Where(s => s.Servicos != null))
                    {
                        string[] servicos = at.Servicos.ToString().Split(new char[] { ';', ',' });

                        foreach (var s in servicos)
                            _servicos.Add(s);

                        for (int i = 1; i < 13; i++)
                            if (at.Data.Value.Month == i)
                                ConstruirMeses(at, servicos);
                    }

                    r_all.Cliente = ("Clientes", _meses.Count, _mes_servicos.Count);
                    r_all.ClientePF = ("Pessoas", _pessoas_mes.Count, _pessoas_mes_servicos.Count);
                    r_all.ClientePJ = ("Empresas", _empresas_mes.Count, _empresas_mes_servicos.Count);

                    var mlist = new List<(string Mes, int EAtendimentos, int Servicos)>();

                    foreach (var x in from a in _meses
                                      group a by a into g
                                      let count = g.Count()
                                      select new { Mes = g.Key, Atend = count })
                    {
                        mlist.Add((x.Mes, x.Atend, _mes_servicos.Where(s => s.Contains(x.Mes)).Count()));
                    }

                    foreach(var x in from a in _servicos
                                     group a by a into g
                                     let count = g.Count()
                                     orderby count descending
                                     select new { Servico = g.Key, Qtde = count })
                    {
                        _l_servicos.Add((x.Servico, x.Qtde));
                    }

                    r_all.ListaServicos = _l_servicos;
                    r_all.ListaMensal = mlist;

                    GC.SuppressFinalize(this);
                }
                catch { }

            });
            await t;

            return r_all;
        }

        public async Task<BIAtendimentos> ToListBIAtendimentosSetor(DateTime periodo, string setor)
        {
            var list = await _atendimento.ListSetorAsync(setor);

            var r_all = new BIAtendimentos();

            var t = Task.Run(() => {

                try
                {
                    _meses.Clear();
                    _pessoas_mes.Clear();
                    _empresas_mes.Clear();

                    _mes_servicos.Clear();
                    _pessoas_mes_servicos.Clear();
                    _empresas_mes_servicos.Clear();

                    var _meses_t = new List<(string Nome, string Atendimento, string Servico)>();

                    foreach (EAtendimento at in list.Where(s => s.Servicos != null && s.Data.Value.Year == periodo.Year))
                    {
                        string[] servicos = at.Servicos.ToString().Split(new char[] { ';', ',' });
                        for (int i = 1; i < 13; i++)
                            if (at.Data.Value.Month == i)
                                ConstruirMeses(at, servicos);
                    }

                    r_all.Cliente = (setor, _meses.Count, _mes_servicos.Count);
                    r_all.ClientePF = ("Pessoas", _pessoas_mes.Count, _pessoas_mes_servicos.Count);
                    r_all.ClientePJ = ("Empresas", _empresas_mes.Count, _empresas_mes_servicos.Count);

                    var mlist = new List<(string Mes, int Atendimentos, int Servicos)>();

                    foreach (var x in from a in _meses
                                      group a by a into g
                                      let count = g.Count()
                                      select new { Mes = g.Key, Atend = count })
                    {
                        mlist.Add((x.Mes, x.Atend, _mes_servicos.Where(s => s.Contains(x.Mes)).Count()));
                    }

                    r_all.ListaMensal = mlist;

                    GC.Collect();

                }
                catch { }

            });
            await t;

            return r_all;
        }

        public async Task<BIAtendimentos> ToListBIAtendimentosAppUser(DateTime periodo)
        {
            var list = await _atendimento.ListPeriodoAsync(new DateTime(periodo.Year, 01, 01), new DateTime(periodo.Year, 12, 31));

            var r_all = new BIAtendimentos();

            var t = Task.Run(() => {

                try
                {
                    _meses.Clear();
                    _pessoas_mes.Clear();
                    _empresas_mes.Clear();

                    _mes_servicos.Clear();
                    _pessoas_mes_servicos.Clear();
                    _empresas_mes_servicos.Clear();

                    var _meses_t = new List<(string Nome, string Atendimento, string Servico)>();

                    foreach (EAtendimento at in list.Where(s => s.Servicos != null && s.Data.Value.Year == periodo.Year))
                    {
                        string[] servicos = at.Servicos.ToString().Split(new char[] { ';', ',' });
                        AppUserMonth(at, servicos);
                    }

                    var mlist = new List<(string Nome, int Atendimentos, int Servicos)>();

                    foreach (var x in from a in _meses
                                      group a by a into g
                                      let count = g.Count()
                                      orderby count descending
                                      select new { AppUser = g.Key, Atend = count })
                    {
                        mlist.Add((x.AppUser, x.Atend, _mes_servicos.Where(s => s.Contains(x.AppUser)).Count()));
                    }

                    r_all.ListaAppUser = mlist;

                    GC.Collect();

                }
                catch { }

            });
            await t;

            return r_all;
        }

        public async Task<IEnumerable<EAtendimento>> DoListAendimentosAsyncBy(string param)
        {
            return await _atendimento.DoListAendimentosAsyncBy(param);
        }

        public async Task<IEnumerable<EAtendimento>> DoListByAnoAsync(int ano)
        {
            return await _atendimento.DoListByAnoAsync(ano);
        }

        public async Task<IEnumerable<EAtendimento>> DoListAsync(Expression<Func<EAtendimento, bool>>? filter = null)
        {
            return await _atendimento.DoListAsync(filter);
        }
    }
}
