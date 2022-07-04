using Sim.Data.Cnpj.Context;
using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Sim.Data.Cnpj.Repository
{
    public class RepositoryCnpj : RepositoryBase<BaseReceitaFederal>, IRepositoryCnpj
    {
        
        public RepositoryCnpj(ApplicationContextCnpj applicationContext)
            :base(applicationContext)
        {

        }

        public async Task<BaseReceitaFederal> GetCNPJAsync(string cnpj)
        {
            return await Task.Run(() =>
            {
                var brf = new BaseReceitaFederal();

                var cnpjbase = cnpj.Remove(8, 6);
                var cnpjordem = cnpj.Remove(0, 8);
                cnpjordem = cnpjordem.Remove(4, 2);
                var cnpjdv = cnpj.Remove(0, 12);

                var qry = (from est in _db.Estabelecimentos
                           from atv in _db.CNAEs
                           .Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                           from msc in _db.MotivoSituacaoCadastral
                           .Where(s => est.MotivoSituacaoCadastral == s.Codigo)
                           from mnp in _db.Municipios
                           .Where(s => est.Municipio == s.Codigo)

                           from emp in _db.Empresas
                           .Where(s => est.CNPJBase == s.CNPJBase)
                           from ntj in _db.NaturezaJuridica
                           .Where(s => emp.NaturezaJuridica == s.Codigo)
                           from qsa in _db.QualificacaoSocios
                           .Where(s => emp.QualificacaoResponsavel == s.Codigo)

                           from snp in _db.Simples
                           .Where(s => est.CNPJBase == s.CNPJBase).DefaultIfEmpty()

                           select new { est, atv, msc, mnp, emp, ntj, qsa, snp })
                          .Where(s => s.est.CNPJBase == cnpjbase && s.est.CNPJOrdem == cnpjordem && s.est.CNPJDV == cnpjdv)
                          .Distinct()
                          .AsNoTracking();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);

                    var qrysco = (from sco in _db.Socios
                                  from qsco in _db.QualificacaoSocios
                                  .Where(s => sco.QualificacaoSocio == s.Codigo)
                                  from qscoresp in _db.QualificacaoSocios
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
                return brf;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string bairro, string endereco, string cnae, string municipio, string situacaocadastral)
        {
            return await Task.Run(() =>
            {
                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from atv in _db.CNAEs.Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                           from sn in _db.Simples.Where(s=>s.CNPJBase == est.CNPJBase).DefaultIfEmpty()
                           select new { est, emp, atv, sn })
                           .Where(s => s.est.Bairro.Contains(bairro))
                           .Where(s => s.est.Logradouro.Contains(endereco))
                           .Where(s => s.est.CnaeFiscalPrincipal.Contains(cnae))
                           .Where(s => s.est.Municipio == municipio)
                           .Where(s => s.est.SituacaoCadastral.Contains(situacaocadastral))
                           .Distinct()
                           .AsNoTracking();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, null, null, null, null));
                }
                return brf;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string municipio)
        {
            return await Task.Run(() =>
            {
                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           select new { est, emp })
                          .Where(s => s.est.Municipio == municipio).Distinct().AsNoTracking();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, null, null, null, null, null, null));
                }
                return brf;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string municipio, string situacaocadastral)
        {
            return await Task.Run(() =>
            {
                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           select new { est, emp })
                          .Where(s => s.est.SituacaoCadastral == situacaocadastral &&
                          s.est.Municipio == municipio)
                          .Distinct().AsNoTracking();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, null, null, null, null, null, null));
                }
                return brf;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllMatrizFilialAsync(string cnpjbase)
        {
            return await Task.Run(() =>
            {
                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           select new { est, emp })
                          .Where(s => s.emp.CNPJBase == cnpjbase).Distinct().AsNoTracking();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, null, null, null, null, null, null));
                }
                return brf;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllRazaoSocialAsync(string razaosocial)
        {
            return await Task.Run(() =>
            {
                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           select new { est, emp })
                          .Where(s => s.emp.RazaoSocial.Contains(razaosocial)).Distinct().AsNoTracking();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, null, null, null, null, null, null));
                }
                return brf;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllSocioAsync(string nomesocio)
        {
            return await Task.Run(() =>
            {
                var brf = new List<BaseReceitaFederal>();

                var qry = (
                           from est in _db.Estabelecimentos
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from sco in _db.Socios.Where(s => s.CNPJBase == est.CNPJBase)
                           select new { est, emp, sco }).Distinct()
                          .Where(s => s.sco.NomeRazaoSocio.Contains(nomesocio) || s.sco.NomeRepresentante.Contains(nomesocio))
                          .Distinct()
                          .AsNoTracking();

                int i = 0;

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);

                    if (!brf.Any(s => s.CNPJ == _cnpj))
                    {
                        i++;
                        brf.Add(new BaseReceitaFederal(
                            i, _cnpj, e.emp, e.est, null, null, null, null, null, null, null, null));
                    }
                }

                return brf;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string municipio, string situacaocadastral)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(situacaocadastral))
                    situacaocadastral = "%";

                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos
                           from atv in _db.CNAEs
                           .Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                           from emp in _db.Empresas
                           .Where(s => est.CNPJBase == s.CNPJBase)
                           from sn in _db.Simples
                           .Where(s => est.CNPJBase == s.CNPJBase)
                           select new { est, emp, atv, sn })
                           .Where(s => s.est.Municipio == municipio)
                           .Where(s => s.est.SituacaoCadastral == situacaocadastral)
                          .Distinct()
                          .AsNoTracking();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, null, null, null, null));

                }

                return brf;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string endereco, string cnae, string municipio, string situacaocadastral)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(situacaocadastral))
                    situacaocadastral = "%";

                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos
                           from atv in _db.CNAEs
                           .Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                           from emp in _db.Empresas
                           .Where(s => est.CNPJBase == s.CNPJBase)
                           from sn in _db.Simples
                           .Where(s => est.CNPJBase == s.CNPJBase)
                           select new { est, emp, atv, sn })
                          .Where(s => s.est.Municipio.Contains(municipio))
                          .Where(s => s.est.Logradouro.Contains(endereco))
                          .Where(s => s.est.CnaeFiscalPrincipal.Contains(cnae))
                          .Where(s => s.est.SituacaoCadastral == situacaocadastral)
                          .AsNoTracking()
                          .Distinct();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, null, null, null, null));

                }
                return brf;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ToListByCnaeAsync(string atividadei, string atividadef, string municipio)
        {
            return await Task.Run(() =>
            {
                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from atv in _db.CNAEs.Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                               //from sn in _db.Simples.Where(s => s.CNPJBase == est.CNPJBase).DefaultIfEmpty()
                           select new { est, emp, atv })
                           .Where(s => s.est.CnaeFiscalPrincipal.CompareTo(atividadei) >= 0 && s.est.CnaeFiscalPrincipal.CompareTo(atividadef) <= 0 && s.est.Municipio.Contains(municipio))
                           .Distinct()
                           .AsNoTracking();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, e.atv, null, null, null, null, null));
                }

                return brf;
            });
        }

        public async Task<IEnumerable<Municipio>> ToListMinicipiosAsync()
        {
           return await _db.Municipios.ToListAsync();
        }
    }
}
