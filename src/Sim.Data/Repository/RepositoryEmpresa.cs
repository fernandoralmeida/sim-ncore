using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sim.Cross.Data.Repository.SDE
{
    using Sim.Domain.SDE.Entity;
    using Sim.Domain.SDE.Interface;
    using Context;
    using Sim.Domain.Cnpj.Entity;

    public class RepositoryEmpresa : RepositoryBase<Empresas>, IRepositoryEmpresa
    {
        protected readonly RFBContext db;
        public RepositoryEmpresa(ApplicationContext dbContext, RFBContext rfbcontext)
            :base(dbContext)
        {
            db = rfbcontext;
        }

        public IEnumerable<Empresas> ConsultaByCNPJ(string cnpj)
        {
            return _db.Empresa.Where(p => p.CNPJ == cnpj);
        }

        public IEnumerable<Empresas> ConsultaByRazaoSocial(string name)
        {
            return _db.Empresa.Where(p => p.Nome_Empresarial.Contains(name) || p.Nome_Fantasia.Contains(name));
        }

        /** Consulta RFB **/
        public async Task<BaseReceitaFederal> GetCnpjAsync(string cnpj)
        {
            var brf = new BaseReceitaFederal();

            var t = Task.Run(() =>
            {
                var cnpjbase = cnpj.Remove(8, 6);
                var cnpjordem = cnpj.Remove(0, 8);
                cnpjordem = cnpjordem.Remove(4, 2);
                var cnpjdv = cnpj.Remove(0, 12);

                var qry = (from est in db.Estabelecimentos
                           from atv in db.CNAEs
                           .Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                           from msc in db.MotivoSituacaoCadastral
                           .Where(s => est.MotivoSituacaoCadastral == s.Codigo)
                           from mnp in db.Municipios
                           .Where(s => est.Municipio == s.Codigo)

                           from emp in db.Empresas
                           .Where(s => est.CNPJBase == s.CNPJBase)
                           from ntj in db.NaturezaJuridica
                           .Where(s => emp.NaturezaJuridica == s.Codigo)
                           from qsa in db.QualificacaoSocios
                           .Where(s => emp.QualificacaoResponsavel == s.Codigo)

                           from snp in db.Simples
                           .Where(s => est.CNPJBase == s.CNPJBase).DefaultIfEmpty()

                           select new { est, atv, msc, mnp, emp, ntj, qsa, snp })
                          .Where(s => s.est.CNPJBase == cnpjbase && s.est.CNPJOrdem == cnpjordem && s.est.CNPJDV == cnpjdv).Distinct();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);

                    var qrysco = (from sco in db.Socios
                                  from qsco in db.QualificacaoSocios
                                  .Where(s => sco.QualificacaoSocio == s.Codigo)
                                  from qscoresp in db.QualificacaoSocios
                                  .Where(s => sco.QualificacaoRepresentanteLegal == s.Codigo)
                                  select new { sco, qsco })
                                 .Where(s => s.sco.CNPJBase == e.est.CNPJBase).Distinct();

                    var socios = new List<Socio>();

                    foreach (var q in qrysco)
                    {
                        socios.Add(new Socio(q.sco.CNPJBase, q.sco.IdentificadorSocio, q.sco.NomeRazaoSocio, q.sco.CnpjCpfSocio
                            , q.qsco.Descricao, q.sco.DataEntradaSociedade, q.sco.Pais, q.sco.RepresentanteLegal, q.sco.NomeRepresentante
                            , q.qsco.Descricao, q.sco.FaixaEtaria));
                    }

                    brf = new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, socios, e.snp, e.atv, null, e.ntj, e.msc, e.mnp, e.qsa);

                }

            });

            await t;

            return brf;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAsync()
        {
            var brf = new List<BaseReceitaFederal>();

            var t = Task.Run(() =>
            {

                var qry = from est in db.Estabelecimentos
                          from atv in db.CNAEs
                          .Where(s => est.CnaeFiscalPrincipal == s.Codigo).Distinct()
                          from emp in db.Empresas
                          .Where(s => est.CNPJBase == s.CNPJBase).Distinct()
                          select new { est, emp, atv };

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, e.atv, null, null, null, null, null));

                }

            });

            await t;


            return brf;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByBairroAsync(string bairro, string atividade, string municipio, string situacao)
        {
            var brf = new List<BaseReceitaFederal>();

            var t = Task.Run(() =>
            {

                var qry = (from est in db.Estabelecimentos
                           from emp in db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from sn in db.Simples.Where(s => s.CNPJBase == est.CNPJBase).DefaultIfEmpty()
                           select new { est, emp, sn })
                          .Where(s => s.est.Bairro.Contains(bairro) && s.est.Municipio.Contains(municipio)).Distinct();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, null, null, null, null, null, null));
                }

            });
            await t;

            return brf;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByCNAEAsync(string atividade, string municipio)
        {
            var brf = new List<BaseReceitaFederal>();

            var t = Task.Run(() =>
            {

                var qry = (from est in db.Estabelecimentos
                           from emp in db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from sn in db.Simples.Where(s => s.CNPJBase == est.CNPJBase).DefaultIfEmpty()
                           select new { est, emp, sn })
                          .Where(s => s.est.CnaeFiscalPrincipal.Contains(atividade) && s.est.Municipio.Contains(municipio)).Distinct();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, null, null, null, null, null, null));
                }

            });
            await t;

            return brf;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByCNPJBaseAsync(string cnpj)
        {
            var brf = new List<BaseReceitaFederal>();

            var t = Task.Run(() =>
            {
                var cnpjbase = cnpj.Split("/");

                var qry = (from est in db.Estabelecimentos
                           from emp in db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           select new { est, emp })
                          .Where(s => s.est.CNPJBase.Contains(cnpjbase[0].Replace(".", ""))).Distinct();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, null, null, null, null, null, null));
                }

            });
            await t;

            return brf;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByLogradouroAsync(string logradouro, string atividade, string municipio, string situacao)
        {
            var brf = new List<BaseReceitaFederal>();

            var t = Task.Run(() =>
            {

                var qry = (from est in db.Estabelecimentos
                           from emp in db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from sn in db.Simples.Where(s => s.CNPJBase == est.CNPJBase).DefaultIfEmpty()
                           select new { est, emp, sn })
                          .Where(s => s.est.Logradouro.Contains(logradouro) && s.est.Municipio.Contains(municipio)).Distinct();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, null, null, null, null, null, null));
                }

            });
            await t;

            return brf;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByMunicipioAsync(string municipio)
        {
            var brf = new List<BaseReceitaFederal>();

            var t = Task.Run(() =>
            {

                var qry = (from est in db.Estabelecimentos
                           from emp in db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from atv in db.CNAEs.Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                           from sn in db.Simples.Where(s => s.CNPJBase == est.CNPJBase).DefaultIfEmpty()
                           select new { est, emp, sn, atv })
                          .Where(s => s.est.Municipio.Contains(municipio)).Distinct();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, null, null, null, null));
                }

            });
            await t;

            return brf;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListByOptanteSimplesAsync(string municipio)
        {
            var brf = new List<BaseReceitaFederal>();

            var t = Task.Run(() =>
            {

                var qry = (from est in db.Estabelecimentos
                           from atv in db.CNAEs
                           .Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                           from emp in db.Empresas
                           .Where(s => est.CNPJBase == s.CNPJBase)
                           from sn in db.Simples
                           .Where(s => est.CNPJBase == s.CNPJBase)
                           select new { est, emp, atv, sn })
                          .Where(s => s.est.Municipio.Contains(municipio)).Distinct();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, null, null, null, null));

                }

            });

            await t;

            return brf;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListBySociosAsync(string nomesocio)
        {
            var brf = new List<BaseReceitaFederal>();

            var t = Task.Run(() =>
            {

                var qry = (
                           from est in db.Estabelecimentos.Distinct()
                           from emp in db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from sco in db.Socios.Where(s => s.CNPJBase == est.CNPJBase)
                           select new { est, emp, sco })
                          .Where(s => s.sco.NomeRazaoSocio.Contains(nomesocio));

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, null, null, null, null, null, null));
                }

            });

            await t;

            return brf;
        }

        public async Task<IEnumerable<Municipio>> ListMinicipios()
        {
            return await db.Municipios.OrderBy(s => s.Descricao).ToListAsync();
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListTop10()
        {
            var brf = new List<BaseReceitaFederal>();

            var t = Task.Run(() =>
            {

                var qry = (from est in db.Estabelecimentos
                           from atv in db.CNAEs
                           .Where(s => est.CnaeFiscalPrincipal == s.Codigo).Distinct()
                           from emp in db.Empresas
                          .Where(s => est.CNPJBase == s.CNPJBase).Distinct()
                           from sn in db.Simples
                          .Where(s => est.CNPJBase == s.CNPJBase).Distinct()
                           orderby est.DataInicioAtividade descending
                           select new { est, emp, sn, atv }).Take(10);

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);

                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, null, null, null, null));

                }

            });
            await t;

            return brf;
        }

        public async Task<IEnumerable<Empresas>> ListTop20()
        {
            var t = Task.Run(() => _db.Empresa.Take(20));

            await t;

            return t.Result;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListForBIAsync(string municipio, string situacao, string ano, string mes) 
        {
            var brf = new List<BaseReceitaFederal>();

            var stc = situacao.Split(",");
            var listasituacao = new List<string>();

            foreach(string l in stc)
            {
                listasituacao.Add(l);
            }

            var t = Task.Run(() =>
            {

                var qry = (from est in db.Estabelecimentos
                           from emp in db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from atv in db.CNAEs.Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                           from sn in db.Simples.Where(s => s.CNPJBase == est.CNPJBase).DefaultIfEmpty()
                           select new { est, emp, sn, atv })
                          .Where(s => s.est.Municipio.Contains(municipio) && s.est.SituacaoCadastral.CompareTo(listasituacao.Where(x => x.Any())) > 0).Distinct();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, null, null, null, null));
                }

            });
            await t;

            return brf;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListForBICnaeAsync(string municipio)
        {
            var brf = new List<BaseReceitaFederal>();

            var t = Task.Run(() =>
            {

                var qry = (from est in db.Estabelecimentos
                           from emp in db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from atv in db.CNAEs.Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                           from sn in db.Simples.Where(s => s.CNPJBase == est.CNPJBase).DefaultIfEmpty()
                           select new { est, emp, sn, atv })
                          .Where(s => s.est.Municipio.Contains(municipio)).OrderBy(o => o.atv.Codigo).Distinct();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);

                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, null, null, null, null));
                }

            });
            await t;

            return brf;
        }

        public async Task<IEnumerable<Empresas>> ListByParam(List<object> lparam)
        {
            var brf = new List<Empresas>();

            var cnpj = lparam[0] != null ? (string)lparam[0] : "#";
            var razaosocial = lparam[1] != null ? (string)lparam[1] : "#";
            var cnae = lparam[2] != null ? (string)lparam[2] : "#";
            var logradouro = lparam[3] != null ? (string)lparam[3] : "#";
            var bairro = lparam[4] != null ? (string)lparam[4] : "#";

            var t = Task.Run(() => _db.Empresa.Where(s => s.CNPJ == cnpj
            || s.Nome_Empresarial.Contains(razaosocial)
            || s.CNAE_Principal.Contains(cnae)
            || s.Logradouro.Contains(logradouro)
            || s.Bairro.Contains(bairro)).OrderByDescending(o => o.Data_Abertura));

            await t;

            return t.Result;
        }
    }
}
