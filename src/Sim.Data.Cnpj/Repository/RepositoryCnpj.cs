﻿using Sim.Data.Cnpj.Context;
using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Sim.Data.Cnpj.Repository
{
    public class RepositoryCnpj : RepositoryBase<BaseReceitaFederal>, IRepositoryCnpj
    {

        public RepositoryCnpj(ApplicationContextCnpj applicationContext)
            : base(applicationContext)
        {

        }

        public async Task<IEnumerable<BaseReceitaFederal>> DoListBaseRazaoSociosAsync(string param)
        {
            return await Task.Run(() =>
            {
                var brf = new List<BaseReceitaFederal>();

                var qrysco = (from sco in _db.Socios.Where(s => s.NomeRazaoSocio.Contains(param))
                              from emp in _db.Empresas.Where(s => s.CNPJBase == sco.CNPJBase)
                              from est in _db.Estabelecimentos.Where(s => s.CNPJBase == sco.CNPJBase)
                              from atv in _db.Cnaes.Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                              from mnp in _db.Municipios.Where(s => s.Codigo == est.Municipio)
                              from sn in _db.Simples.Where(s => s.CNPJBase == emp.CNPJBase).DefaultIfEmpty()
                              select new { emp, est, atv, mnp, sn })
                            .Distinct()
                            .AsNoTrackingWithIdentityResolution();

                foreach (var e in qrysco)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);
                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, null, null, e.mnp, null));
                }

                var qryemp = (from emp in _db.Empresas.Where(s => s.RazaoSocial.Contains(param))
                              from est in _db.Estabelecimentos.Where(s => s.CNPJBase == emp.CNPJBase)
                              from atv in _db.Cnaes.Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                              from mnp in _db.Municipios.Where(s => s.Codigo == est.Municipio)
                              from sn in _db.Simples.Where(s => s.CNPJBase == emp.CNPJBase).DefaultIfEmpty()
                              select new { emp, est, atv, mnp, sn })
                            .Distinct()
                            .AsNoTrackingWithIdentityResolution();

                foreach (var e in qryemp)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);
                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, null, null, e.mnp, null));
                }
                return brf;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> DoListEmpresasAsync(string municipio) =>
            await Task.Run(() =>
            {
                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos.Where(s => s.Municipio == municipio)
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from atv in _db.Cnaes.Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                           from mpo in _db.Municipios.Where(s => s.Codigo == est.Municipio)
                           from sn in _db.Simples.Where(s => s.CNPJBase == est.CNPJBase).DefaultIfEmpty()
                           select new { est, emp, atv, sn, mpo })
                           .Distinct()
                           .AsNoTrackingWithIdentityResolution();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);

                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, null, null, e.mpo, null));
                }
                return brf;
            });

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
                           from atv in _db.Cnaes
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
                        .AsNoTrackingWithIdentityResolution();

                var qrysco = (from sco in _db.Socios
                              from qsco in _db.QualificacaoSocios
                              .Where(s => sco.QualificacaoSocio == s.Codigo)
                              from qscoresp in _db.QualificacaoSocios
                              .Where(s => sco.QualificacaoRepresentanteLegal == s.Codigo)
                              select new { sco, qsco })
                        .Where(s => s.sco.CNPJBase == cnpjbase).Distinct();

                var socios = new List<Socio>();

                foreach (var q in qrysco)
                {
                    socios.Add(new Socio(q.sco.CNPJBase, q.sco.IdentificadorSocio, q.sco.NomeRazaoSocio, q.sco.CnpjCpfSocio
                        , q.qsco.Descricao, q.sco.DataEntradaSociedade, q.sco.Pais, q.sco.RepresentanteLegal, q.sco.NomeRepresentante
                        , q.qsco.Descricao, q.sco.FaixaEtaria));
                }

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);
                    brf = new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, socios, e.snp, e.atv, null, e.ntj, e.msc, e.mnp, e.qsa);

                }
                return brf;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> DoListByCnaeAsync(string atividadei, string atividadef, string municipio)
        {
            return await Task.Run(() =>
            {
                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos.Where(s => s.CnaeFiscalPrincipal.CompareTo(atividadei) >= 0 &&
                                        s.CnaeFiscalPrincipal.CompareTo(atividadef) <= 0 &&
                                        s.Municipio.Contains(municipio))
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from atv in _db.Cnaes.Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                               //from sn in _db.Simples.Where(s => s.CNPJBase == est.CNPJBase).DefaultIfEmpty()
                           select new { est, emp, atv })
                           .Distinct()
                           .AsNoTrackingWithIdentityResolution();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, e.atv, null, null, null, null, null));
                }

                return brf;
            });
        }

        public async Task<IEnumerable<Municipio>> DoListMinicipiosAsync()
        {
            return await _db.Municipios.ToListAsync();
        }

        public async Task<IEnumerable<BaseReceitaFederal>> DoListByZonaAsync(string bairro, string municipio) =>
            await Task.Run(() =>
            {

                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos.Where(s => s.Bairro == bairro &&
                                        s.Municipio.Contains(municipio))
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from atv in _db.Cnaes.Where(s => est.CnaeFiscalPrincipal == s.Codigo)

                           select new { est, emp, atv })
                           .Distinct()
                           .AsNoTrackingWithIdentityResolution();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, e.atv, null, null, null, null, null));
                }

                return brf;
            });

        public async Task<IEnumerable<BaseReceitaFederal>> DoListByLogradouroAsync(string logradouro, string municipio) =>
            await Task.Run(() =>
            {

                var brf = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos.Where(s => s.Logradouro == logradouro &&
                                        s.Municipio.Contains(municipio))
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from atv in _db.Cnaes.Where(s => est.CnaeFiscalPrincipal == s.Codigo)

                           select new { est, emp, atv })
                           .Distinct()
                           .AsNoTrackingWithIdentityResolution();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);


                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, e.atv, null, null, null, null, null));
                }

                return brf;
            });

        public async Task<IEnumerable<BaseReceitaFederal>> DoListAsync(Expression<Func<Estabelecimento, bool>> filter = null) =>
            await Task.Run(() =>
                {
                    var brf = new List<BaseReceitaFederal>();

                    var qry = (from est in _db.Estabelecimentos.Where(filter)
                               from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                               from atv in _db.Cnaes.Where(s => est.CnaeFiscalPrincipal == s.Codigo)
                               from mpo in _db.Municipios.Where(s => s.Codigo == est.Municipio)
                               from nj in _db.NaturezaJuridica.Where(s => s.Codigo == emp.NaturezaJuridica)
                               from sn in _db.Simples.Where(s => s.CNPJBase == est.CNPJBase).DefaultIfEmpty()
                               select new { est, emp, atv, sn, mpo, nj })
                            .Distinct()
                            .AsNoTrackingWithIdentityResolution();

                    foreach (var e in qry)
                    {
                        var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);

                        brf.Add(new BaseReceitaFederal(
                            0, _cnpj, e.emp, e.est, null, e.sn, e.atv, null, e.nj, null, e.mpo, null));
                    }
                    return brf;
                });

        public async Task<IEnumerable<BaseReceitaFederal>> DoListCNAEAsync(string municipio, Expression<Func<CNAE, bool>> param = null) =>
            await Task.Run(() =>
            {
                var brf = new List<BaseReceitaFederal>();

                var qry = (from atv in _db.Cnaes.Where(param)
                           from est in _db.Estabelecimentos.Where(s => s.CnaeFiscalPrincipal == atv.Codigo && s.Municipio == municipio && s.SituacaoCadastral == "02")
                           from emp in _db.Empresas.Where(s => s.CNPJBase == est.CNPJBase)
                           from mpo in _db.Municipios.Where(s => s.Codigo == est.Municipio)
                           select new { est, emp, atv, mpo })
                        .Distinct()
                        .AsNoTrackingWithIdentityResolution();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);

                    brf.Add(new BaseReceitaFederal(
                        0, _cnpj, e.emp, e.est, null, null, e.atv, null, null, null, e.mpo, null));
                }
                return brf;
            });

        public async Task<IEnumerable<BaseReceitaFederal>> RepositoryEstabelecimento(Expression<Func<Estabelecimento, bool>> param = null) =>
            await Task.Run(() =>
            {
                var _empresas = new List<BaseReceitaFederal>();

                var qry = (from est in _db.Estabelecimentos.Where(param)
                           from atv in _db.Cnaes.Where(s => s.Codigo == est.CnaeFiscalPrincipal)
                            select new { est, atv })
                            .AsNoTrackingWithIdentityResolution();

                foreach (var e in qry)
                {
                    var _cnpj = string.Format("{0}{1}{2}", e.est.CNPJBase, e.est.CNPJOrdem, e.est.CNPJDV);

                    _empresas.Add(new BaseReceitaFederal(
                        0, _cnpj, null, e.est, null, null, e.atv, null, null, null, null, null));
                }
                return _empresas;
            });
    }
}
