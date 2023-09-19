using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interfaces;
using Sim.Domain.Cnpj.Extensions;
using System.Linq.Expressions;
using System.Globalization;
using System.Linq;

namespace Sim.Domain.Cnpj.Services
{
    public class ServiceCnpj : ServiceBase<BaseReceitaFederal>, IServiceCnpj
    {
        private readonly IRepositoryCnpj _cnpj;

        public ServiceCnpj(IRepositoryCnpj cnpj) : base(cnpj)
        {
            _cnpj = cnpj;
        }

        public async Task<IEnumerable<BaseReceitaFederal>> DoListBaseRazaoSociosAsync(string param)
        {
            return await _cnpj.DoListBaseRazaoSociosAsync(param);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> DoListEmpresasAsync(string municipio) =>
            await _cnpj.DoListEmpresasAsync(municipio);

        public async Task<IEnumerable<KeyValuePair<string, int>>> DoListMappingLogradourosAsync(IEnumerable<BaseReceitaFederal> obj) =>
            await Task.Run(() =>
            {

                var _list = new List<KeyValuePair<string, int>>();

                foreach (var nome in obj
                                    .OrderBy(o => o.Estabelecimento.Logradouro)
                                    .GroupBy(g => g.Estabelecimento.Logradouro))
                {
                    _list.Add(new KeyValuePair<string, int>(key: nome.Key, value: nome.Count()));
                }

                return _list;
            });

        public async Task<IEnumerable<string>> DoListMappingZonasAsync(IEnumerable<BaseReceitaFederal> obj) =>
            await Task.Run(() =>
            {

                var _list = new List<string>();

                foreach (var nome in obj
                                    .OrderBy(o => o.Estabelecimento.Bairro)
                                    .GroupBy(g => g.Estabelecimento.Bairro))
                {
                    _list.Add(nome.Key);
                }

                return _list;
            });

        public async Task<BaseReceitaFederal> GetCNPJAsync(string cnpj)
        {
            return await _cnpj.GetCNPJAsync(cnpj);
        }

        public async Task<IEnumerable<BICnae>> DoListBICnaeAsync(IEnumerable<BaseReceitaFederal> param)
        {
            return await Task.Run(() =>
            {

                var l_full_cnae = new List<BICnae>();

                var secao = new KeyValuePair<string, int>();

                var l_secao = new BICnae() { ListaSecao = new() };

                var subclasse = new List<string>();

                foreach (BaseReceitaFederal e in param)
                    subclasse.Add(string.Format("{0} - {1}", e.AtividadePrincipal.Codigo, e.AtividadePrincipal.Descricao));


                var s_subclasse = from x in subclasse
                                  group x by x into g
                                  let count = g.Count()
                                  orderby g.Key ascending
                                  select new { Value = g.Key, Count = count };

                var sec_count = 0;
                var sub_sec_count = 0;

                //agro
                var agro = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "0100000",
                    FaixaFinal = "0399999"
                };

                for (int i = 1; i <= 3; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 01 && sec <= 03)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("AGRICULTURA, PECUÁRIA, PRODUÇÃO FLORESTAL, PESCA E AQUICULTURA", sec_count);

                            if (i == 01 && sec == 01)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("01 AGRICULTURA, PECUÁRIA E SERVIÇOS RELACIONADOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            if (i == 02 && sec == 02)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("02 PRODUÇÃO FLORESTAL", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            if (i == 03 && sec == 03)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("03 PESCA E AQUICULTURA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        agro.ListaClasse.Add(n_cnae);
                        agro.Secao = secao;
                    }
                }
                if (agro.ListaClasse.Any())
                    l_secao.ListaSecao.Add(agro);

                //Ind Ext
                var indextrativa = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "0500000",
                    FaixaFinal = "0999999"
                };

                for (int i = 5; i <= 9; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 05 && sec <= 09)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("INDÚSTRIAS EXTRATIVAS", sec_count);


                            if (i == 05 && sec == 05)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("05 EXTRAÇÃO DE CARVÃO MINERAL", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 06 && sec == 06)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("06 EXTRAÇÃO DE PETRÓLEO E GÁS NATURAL", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 07 && sec == 07)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("07 EXTRAÇÃO DE MINERAIS METÁLICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 08 && sec == 08)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("08 EXTRAÇÃO DE MINERAIS NÃO-METÁLICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 09 && sec == 09)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("09 ATIVIDADES DE APOIO À EXTRAÇÃO DE MINERAIS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        indextrativa.ListaClasse.Add(n_cnae);
                        indextrativa.Secao = secao;
                    }
                }
                if (indextrativa.ListaClasse.Any())
                    l_secao.ListaSecao.Add(indextrativa);

                //Ind Transf
                var indetransf = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "1000000",
                    FaixaFinal = "3399999"
                };
                for (int i = 10; i <= 33; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 10 && sec <= 33)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("INDÚSTRIAS DE TRANSFORMAÇÃO", sec_count);

                            if (i == 10 && sec == 10)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("10 FABRICAÇÃO DE PRODUTOS ALIMENTÍCIOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 11 && sec == 11)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("11 FABRICAÇÃO DE BEBIDAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 12 && sec == 12)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("12 FABRICAÇÃO DE PRODUTOS DO FUMO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 13 && sec == 13)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("13 FABRICAÇÃO DE PRODUTOS TÊXTEIS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 14 && sec == 14)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("14 CONFECÇÃO DE ARTIGOS DO VESTUÁRIO E ACESSÓRIOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 15 && sec == 15)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("15 PREPARAÇÃO DE COUROS E FABRICAÇÃO DE ARTEFATOS DE COURO, ARTIGOS PARA VIAGEM E CALÇADOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 16 && sec == 16)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("16 FABRICAÇÃO DE PRODUTOS DE MADEIRA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 17 && sec == 17)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("17 FABRICAÇÃO DE CELULOSE, PAPEL E PRODUTOS DE PAPEL", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 18 && sec == 18)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("18 IMPRESSÃO E REPRODUÇÃO DE GRAVAÇÕES", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 19 && sec == 19)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("19 FABRICAÇÃO DE COQUE, DE PRODUTOS DERIVADOS DO PETRÓLEO E DE BIOCOMBUSTÍVEIS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 20 && sec == 20)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("20 FABRICAÇÃO DE PRODUTOS QUÍMICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 21 && sec == 21)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("21 FABRICAÇÃO DE PRODUTOS FARMOQUÍMICOS E FARMACÊUTICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 22 && sec == 22)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("22 FABRICAÇÃO DE PRODUTOS DE BORRACHA E DE MATERIAL PLÁSTICO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 23 && sec == 23)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("23 FABRICAÇÃO DE PRODUTOS DE MINERAIS NÃO-METÁLICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 24 && sec == 24)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("24 METALURGIA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 25 && sec == 25)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("25 FABRICAÇÃO DE PRODUTOS DE METAL, EXCETO MÁQUINAS E EQUIPAMENTOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 26 && sec == 26)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("26 FABRICAÇÃO DE EQUIPAMENTOS DE INFORMÁTICA, PRODUTOS ELETRÔNICOS E ÓPTICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 27 && sec == 27)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("27 FABRICAÇÃO DE MÁQUINAS, APARELHOS E MATERIAIS ELÉTRICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 28 && sec == 28)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("28 FABRICAÇÃO DE MÁQUINAS E EQUIPAMENTOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 29 && sec == 29)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("29 FABRICAÇÃO DE VEÍCULOS AUTOMOTORES, REBOQUES E CARROCERIAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 30 && sec == 30)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("30 FABRICAÇÃO DE OUTROS EQUIPAMENTOS DE TRANSPORTE, EXCETO VEÍCULOS AUTOMOTORES", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 31 && sec == 31)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("31 FABRICAÇÃO DE MÓVEIS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 32 && sec == 32)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("32 FABRICAÇÃO DE PRODUTOS DIVERSOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 33 && sec == 33)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("33 MANUTENÇÃO, REPARAÇÃO E INSTALAÇÃO DE MÁQUINAS E EQUIPAMENTOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        indetransf.ListaClasse.Add(n_cnae);
                        indetransf.Secao = secao;
                    }
                }
                if (indetransf.ListaClasse.Any())
                    l_secao.ListaSecao.Add(indetransf);


                //Eletricidade e Gás
                var eletrigas = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "3500000",
                    FaixaFinal = "3599999"
                };
                for (int i = 35; i <= 35; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 35 && sec <= 35)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("ELETRICIDADE E GÁS", sec_count);

                            if (i == 35 && sec == 35)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("35 ELETRICIDADE, GÁS E OUTRAS UTILIDADES", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        eletrigas.ListaClasse.Add(n_cnae);
                        eletrigas.Secao = secao;
                    }
                }
                if (eletrigas.ListaClasse.Any())
                    l_secao.ListaSecao.Add(eletrigas);

                //Agua e Esgoto etc...
                var aguaesgot = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "3600000",
                    FaixaFinal = "3999999"
                };
                for (int i = 36; i <= 39; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 36 && sec <= 39)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("ÁGUA, ESGOTO, ATIVIDADES DE GESTÃO DE RESÍDUOS E DESCONTAMINAÇÃO", sec_count);

                            if (i == 36 && sec == 36)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("36 CAPTAÇÃO, TRATAMENTO E DISTRIBUIÇÃO DE ÁGUA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 37 && sec == 37)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("37 ESGOTO E ATIVIDADES RELACIONADAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 38 && sec == 38)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("38 COLETA, TRATAMENTO E DISPOSIÇÃO DE RESÍDUOS; RECUPERAÇÃO DE MATERIAIS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 39 && sec == 39)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("39 DESCONTAMINAÇÃO E OUTROS SERVIÇOS DE GESTÃO DE RESÍDUOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        aguaesgot.ListaClasse.Add(n_cnae);
                        aguaesgot.Secao = secao;
                    }
                }
                if (aguaesgot.ListaClasse.Any())
                    l_secao.ListaSecao.Add(aguaesgot);

                //Construcao...
                var construcao = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "4100000",
                    FaixaFinal = "4399999"
                };
                for (int i = 41; i <= 43; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 41 && sec <= 43)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("CONSTRUÇÃO", sec_count);

                            if (i == 41 && sec == 41)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("41 CONSTRUÇÃO DE EDIFÍCIOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 42 && sec == 42)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("42 OBRAS DE INFRA-ESTRUTURA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 43 && sec == 43)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("43 SERVIÇOS ESPECIALIZADOS PARA CONSTRUÇÃO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        construcao.ListaClasse.Add(n_cnae);
                        construcao.Secao = secao;
                    }
                }
                if (construcao.ListaClasse.Any())
                    l_secao.ListaSecao.Add(construcao);

                //Comércio...
                var comercio = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "4500000",
                    FaixaFinal = "4799999"
                };
                for (int i = 45; i <= 47; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 45 && sec <= 47)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("COMÉRCIO; REPARAÇÃO DE VEÍCULOS AUTOMOTORES E MOTOCICLETAS", sec_count);

                            if (i == 45 && sec == 45)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("45 COMÉRCIO E REPARAÇÃO DE VEÍCULOS AUTOMOTORES E MOTOCICLETAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 46 && sec == 46)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("46 COMÉRCIO POR ATACADO, EXCETO VEÍCULOS AUTOMOTORES E MOTOCICLETAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 47 && sec == 47)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("47 COMÉRCIO VAREJISTA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        comercio.ListaClasse.Add(n_cnae);
                        comercio.Secao = secao;
                    }
                }
                if (comercio.ListaClasse.Any())
                    l_secao.ListaSecao.Add(comercio);

                //Transporte...
                var transporte = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "4900000",
                    FaixaFinal = "5399999"
                };
                for (int i = 49; i <= 53; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 49 && sec <= 53)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("TRANSPORTE, ARMAZENAGEM E CORREIO", sec_count);

                            if (i == 49 && sec == 49)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("49 TRANSPORTE TERRESTRE", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 50 && sec == 50)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("50 TRANSPORTE AQUAVIÁRIO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 51 && sec == 51)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("51 TRANSPORTE AÉREO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 52 && sec == 52)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("52 ARMAZENAMENTO E ATIVIDADES AUXILIARES DOS TRANSPORTES", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 53 && sec == 53)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("53 CORREIO E OUTRAS ATIVIDADES DE ENTREGA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        transporte.ListaClasse.Add(n_cnae);
                        transporte.Secao = secao;
                    }
                }
                if (transporte.ListaClasse.Any())
                    l_secao.ListaSecao.Add(transporte);

                //Alojamento e Alimentacao...
                var alojamento = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "5500000",
                    FaixaFinal = "5699999"
                };
                for (int i = 55; i <= 56; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 55 && sec <= 56)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("ALOJAMENTO E ALIMENTAÇÃO", sec_count);

                            if (i == 55 && sec == 55)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("55 ALOJAMENTO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 56 && sec == 56)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("56 ALIMENTAÇÃO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        alojamento.ListaClasse.Add(n_cnae);
                        alojamento.Secao = secao;
                    }
                }
                if (alojamento.ListaClasse.Any())
                    l_secao.ListaSecao.Add(alojamento);

                //INFORMAÇÃO E COMUNICAÇÃO...
                var inforcom = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "5800000",
                    FaixaFinal = "6399999"
                };
                for (int i = 58; i <= 63; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 58 && sec <= 63)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("INFORMAÇÃO E COMUNICAÇÃO", sec_count);

                            if (i == 58 && sec == 58)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("58 EDIÇÃO E EDIÇÃO INTEGRADA À IMPRESSÃO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 59 && sec == 59)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("59 ATIVIDADES CINEMATOGRÁFICAS, PRODUÇÃO DE VÍDEOS E DE PROGRAMAS DE TELEVISÃO; GRAVAÇÃO DE SOM E EDIÇÃO DE MÚSICA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 60 && sec == 60)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("60 ATIVIDADES DE RÁDIO E DE TELEVISÃO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 61 && sec == 61)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("61 TELECOMUNICAÇÕES", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 62 && sec == 62)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("62 ATIVIDADES DOS SERVIÇOS DE TECNOLOGIA DA INFORMAÇÃO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 63 && sec == 63)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("63 ATIVIDADES DE PRESTAÇÃO DE SERVIÇOS DE INFORMAÇÃO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        inforcom.ListaClasse.Add(n_cnae);
                        inforcom.Secao = secao;
                    }
                }
                if (inforcom.ListaClasse.Any())
                    l_secao.ListaSecao.Add(inforcom);

                //Financeiras etc...
                var finance = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "6400000",
                    FaixaFinal = "6699999"
                };
                for (int i = 64; i <= 66; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 64 && sec <= 66)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("ATIVIDADES FINANCEIRAS, DE SEGUROS E SERVIÇOS RELACIONADOS", sec_count);

                            if (i == 64 && sec == 64)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("64 ATIVIDADES DE SERVIÇOS FINANCEIROS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 65 && sec == 65)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("65 SEGUROS, RESSEGUROS, PREVIDÊNCIA COMPLEMENTAR E PLANOS DE SAÚDE", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 66 && sec == 66)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("66 ATIVIDADES AUXILIARES DOS SERVIÇOS FINANCEIROS, SEGUROS, PREVIDÊNCIA COMPLEMENTAR E PLANOS DE SAÚDE", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        finance.ListaClasse.Add(n_cnae);
                        finance.Secao = secao;
                    }
                }
                if (finance.ListaClasse.Any())
                    l_secao.ListaSecao.Add(finance);

                //Imobiliárias etc...
                var imobili = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "6800000",
                    FaixaFinal = "6899999"
                };
                for (int i = 68; i <= 68; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 64 && sec <= 66)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("ATIVIDADES IMOBILIÁRIAS", sec_count);

                            if (i == 68 && sec == 68)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("68 ATIVIDADES IMOBILIÁRIAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        imobili.ListaClasse.Add(n_cnae);
                        imobili.Secao = secao;
                    }
                }
                if (imobili.ListaClasse.Any())
                    l_secao.ListaSecao.Add(imobili);

                //Cientificas Tecnicas etc...
                var cientifica = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "6900000",
                    FaixaFinal = "7599999"
                };
                for (int i = 69; i <= 75; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 69 && sec <= 75)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS", sec_count);

                            if (i == 69 && sec == 69)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("69 ATIVIDADES JURÍDICAS, DE CONTABILIDADE E DE AUDITORIA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 70 && sec == 70)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("70 ATIVIDADES DE SEDES DE EMPRESAS E DE CONSULTORIA EM GESTÃO EMPRESARIAL", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 71 && sec == 71)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("71 SERVIÇOS DE ARQUITETURA E ENGENHARIA; TESTES E ANÁLISES TÉCNICAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 72 && sec == 72)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("72 PESQUISA E DESENVOLVIMENTO CIENTÍFICO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 73 && sec == 73)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("73 PUBLICIDADE E PESQUISA DE MERCADO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 74 && sec == 74)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("74 OUTRAS ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 75 && sec == 75)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("75 ATIVIDADES VETERINÁRIAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        cientifica.ListaClasse.Add(n_cnae);
                        cientifica.Secao = secao;
                    }
                }
                if (cientifica.ListaClasse.Any())
                    l_secao.ListaSecao.Add(cientifica);

                //Administrativo etc...
                var administrativo = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "7700000",
                    FaixaFinal = "8299999"
                };
                for (int i = 77; i <= 82; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 77 && sec <= 82)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("ATIVIDADES ADMINISTRATIVAS E SERVIÇOS COMPLEMENTARES", sec_count);

                            if (i == 77 && sec == 77)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("77 ALUGUÉIS NÃO-IMOBILIÁRIOS E GESTÃO DE ATIVOS INTANGÍVEIS NÃO-FINANCEIROS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 78 && sec == 78)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("78 SELEÇÃO, AGENCIAMENTO E LOCAÇÃO DE MÃO-DE-OBRA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 79 && sec == 79)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("79 AGÊNCIAS DE VIAGENS, OPERADORES TURÍSTICOS E SERVIÇOS DE RESERVAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 80 && sec == 80)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("80 ATIVIDADES DE VIGILÂNCIA, SEGURANÇA E INVESTIGAÇÃO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 81 && sec == 81)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("81 SERVIÇOS PARA EDIFÍCIOS E ATIVIDADES PAISAGÍSTICAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 82 && sec == 82)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("82 SERVIÇOS DE ESCRITÓRIO, DE APOIO ADMINISTRATIVO E OUTROS SERVIÇOS PRESTADOS PRINCIPALMENTE ÀS EMPRESAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        administrativo.ListaClasse.Add(n_cnae);
                        administrativo.Secao = secao;
                    }
                }
                if (administrativo.ListaClasse.Any())
                    l_secao.ListaSecao.Add(administrativo);

                //Adm Publico etc...
                var admpublic = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "8400000",
                    FaixaFinal = "8499999"
                };
                for (int i = 84; i <= 84; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 84 && sec <= 84)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("ADMINISTRAÇÃO PÚBLICA, DEFESA E SEGURIDADE SOCIAL", sec_count);

                            if (i == 84 && sec == 84)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("84 ADMINISTRAÇÃO PÚBLICA, DEFESA E SEGURIDADE SOCIAL", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }

                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        admpublic.ListaClasse.Add(n_cnae);
                        admpublic.Secao = secao;
                    }
                }
                if (admpublic.ListaClasse.Any())
                    l_secao.ListaSecao.Add(admpublic);

                //Educação etc...
                var educa = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "8500000",
                    FaixaFinal = "8599999"
                };
                for (int i = 85; i <= 85; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 85 && sec <= 85)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("EDUCAÇÃO", sec_count);

                            if (i == 85 && sec == 85)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("85 EDUCAÇÃO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }

                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        educa.ListaClasse.Add(n_cnae);
                        educa.Secao = secao;
                    }
                }
                if (educa.ListaClasse.Any())
                    l_secao.ListaSecao.Add(educa);

                //Saude humana etc...
                var saude = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "8600000",
                    FaixaFinal = "8899999"
                };
                for (int i = 86; i <= 88; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 86 && sec <= 88)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("SAÚDE HUMANA E SERVIÇOS SOCIAIS", sec_count);

                            if (i == 86 && sec == 86)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("86 ATIVIDADES DE ATENÇÃO À SAÚDE HUMANA", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 87 && sec == 87)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("87 ATIVIDADES DE ATENÇÃO À SAÚDE HUMANA INTEGRADAS COM ASSISTÊNCIA SOCIAL, PRESTADAS EM RESIDÊNCIAS COLETIVAS E PARTICULARES", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 88 && sec == 88)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("88 SERVIÇOS DE ASSISTÊNCIA SOCIAL SEM ALOJAMENTO", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        saude.ListaClasse.Add(n_cnae);
                        saude.Secao = secao;
                    }
                }
                if (saude.ListaClasse.Any())
                    l_secao.ListaSecao.Add(saude);

                //Artes etc...
                var artes = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "9000000",
                    FaixaFinal = "9399999"
                };
                for (int i = 90; i <= 93; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 90 && sec <= 93)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("ARTES, CULTURA, ESPORTE E RECREAÇÃO", sec_count);

                            if (i == 90 && sec == 90)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("90 ATIVIDADES ARTÍSTICAS, CRIATIVAS E DE ESPETÁCULOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 91 && sec == 91)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("91 ATIVIDADES LIGADAS AO PATRIMÔNIO CULTURAL E AMBIENTAL", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 92 && sec == 92)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("92 ATIVIDADES DE EXPLORAÇÃO DE JOGOS DE AZAR E APOSTAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 93 && sec == 93)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("93 ATIVIDADES ESPORTIVAS E DE RECREAÇÃO E LAZER", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        artes.ListaClasse.Add(n_cnae);
                        artes.Secao = secao;
                    }
                }
                if (artes.ListaClasse.Any())
                    l_secao.ListaSecao.Add(artes);

                //Outras atv, servicos etc...
                var outrosservi = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "9400000",
                    FaixaFinal = "9699999"
                };
                for (int i = 94; i <= 96; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 94 && sec <= 96)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("OUTRAS ATIVIDADES DE SERVIÇOS", sec_count);

                            if (i == 94 && sec == 94)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("94 ATIVIDADES DE ORGANIZAÇÕES ASSOCIATIVAS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 95 && sec == 95)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("95 REPARAÇÃO E MANUTENÇÃO DE EQUIPAMENTOS DE INFORMÁTICA E COMUNICAÇÃO E DE OBJETOS PESSOAIS E DOMÉSTICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                            else if (i == 96 && sec == 96)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("96 OUTRAS ATIVIDADES DE SERVIÇOS PESSOAIS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        outrosservi.ListaClasse.Add(n_cnae);
                        outrosservi.Secao = secao;
                    }
                }
                if (outrosservi.ListaClasse.Any())
                    l_secao.ListaSecao.Add(outrosservi);

                //Domestico etc...
                var domestic = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "9700000",
                    FaixaFinal = "9799999"
                };
                for (int i = 97; i <= 97; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 97 && sec <= 97)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("SERVIÇOS DOMÉSTICOS", sec_count);

                            if (i == 97 && sec == 97)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("97 SERVIÇOS DOMÉSTICOS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        domestic.ListaClasse.Add(n_cnae);
                        domestic.Secao = secao;
                    }
                }
                if (domestic.ListaClasse.Any())
                    l_secao.ListaSecao.Add(domestic);

                //Internacionais etc...
                var internac = new CnaeSecao()
                {
                    ListaClasse = new(),
                    Secao = new(),
                    FaixaInicial = "9900000",
                    FaixaFinal = "9999999"
                };
                for (int i = 99; i <= 99; i++)
                {
                    var n_cnae = new CnaeClasse() { ListaSubClasse = new(), Classe = new() };
                    sec_count = 0;
                    sub_sec_count = 0;
                    foreach (var s in s_subclasse)
                    {
                        string[] cd = s.Value.Split(" - ");

                        int sec = Convert.ToInt32(cd[0].Remove(2, 5));

                        if (sec >= 99 && sec <= 99)
                        {
                            sec_count += s.Count;
                            secao = new KeyValuePair<string, int>("ORGANISMOS INTERNACIONAIS E OUTRAS INSTITUIÇÕES EXTRATERRITORIAIS", sec_count);

                            if (i == 99 && sec == 99)
                            {
                                sub_sec_count += s.Count;
                                n_cnae.Classe = new KeyValuePair<string, int>("99 ORGANISMOS INTERNACIONAIS E OUTRAS INSTITUIÇÕES EXTRATERRITORIAIS", sub_sec_count);
                                n_cnae.ListaSubClasse.Add(new KeyValuePair<string, int>(s.Value, s.Count));
                            }
                        }
                    }
                    if (n_cnae.ListaSubClasse.Any())
                    {
                        internac.ListaClasse.Add(n_cnae);
                        internac.Secao = secao;
                    }
                }
                if (internac.ListaClasse.Any())
                    l_secao.ListaSecao.Add(internac);

                l_full_cnae.Add(l_secao);

                return l_full_cnae;
            });
        }

        public async Task<IEnumerable<BIEmpresas>> DoListBIEmpresasAsync(string municipio, int ano)
        {
            var t = await _cnpj.DoListEmpresasAsync(municipio);
            var _emp = t.Where(s => !s.Estabelecimento.CnaeFiscalPrincipal.StartsWith("84")
                                && !s.Estabelecimento.CnaeFiscalPrincipal.StartsWith("94")
                                && DateTime.ParseExact(
                                    s.Estabelecimento.DataInicioAtividade,
                                    "yyyy-MM-dd",
                                    CultureInfo.InvariantCulture)
                                    .Date.Year <= ano);

            return await Task.Run(() =>
            {


                var r_empresas = new List<BIEmpresas>();

                //try
                //{
                var _bi_empresas = new BIEmpresas();

                var _emp_novas = _emp.Where(s => s.Estabelecimento.DataInicioAtividade.StartsWith(ano.ToString()));

                var _emp_ativas = _emp.Where(s => s.Estabelecimento.SituacaoCadastral == "Ativa");
                var _emp_ativas_ano = _emp_ativas.Where(s => s.Estabelecimento.DataInicioAtividade.StartsWith(ano.ToString()));

                var _emp_baixadas = _emp.Where(s => s.Estabelecimento.SituacaoCadastral == "Baixada");
                var _emp_baixadas_ano = _emp_baixadas.Where(s => s.Estabelecimento.DataSituacaoCadastral.StartsWith(ano.ToString()));
                var _emp_baixadas_novas = _emp_novas.Where(s => s.Estabelecimento.SituacaoCadastral == "Baixada");

                var _emp_nulas = _emp.Where(s => s.Estabelecimento.SituacaoCadastral == "Nula");
                var _emp_nulas_novas = _emp_novas.Where(s => s.Estabelecimento.SituacaoCadastral == "Nula");
                var _emp_nulas_ano = _emp_nulas.Where(s => s.Estabelecimento.DataSituacaoCadastral.StartsWith(ano.ToString()));

                var _emp_suspensas = _emp.Where(s => s.Estabelecimento.SituacaoCadastral == "Suspensa");
                var _emp_suspensas_novas = _emp_novas.Where(s => s.Estabelecimento.SituacaoCadastral == "Suspensa");
                var _emp_suspensas_ano = _emp_suspensas.Where(s => s.Estabelecimento.DataSituacaoCadastral.StartsWith(ano.ToString()));

                var _emp_inapta = _emp.Where(s => s.Estabelecimento.SituacaoCadastral == "Inapta");
                var _emp_inapta_novas = _emp_novas.Where(s => s.Estabelecimento.SituacaoCadastral == "Suspensa");
                var _emp_inapta_ano = _emp_inapta.Where(s => s.Estabelecimento.DataSituacaoCadastral.StartsWith(ano.ToString()));

                float _t_emp = _emp.Count();
                float _t_emp_novas = _emp_novas.Count();
                float _t_emp_ativas = _emp_ativas.Count();
                float _t_emp_ativas_ano = _emp_ativas.Where(s => s.Estabelecimento.DataInicioAtividade.StartsWith(ano.ToString())).Count();

                var _emp_porte_me = _emp_ativas.Where(s => s.Empresa.PorteEmpresa == "ME");
                var _emp_porte_me_ano = _emp_ativas_ano.Where(s => s.Empresa.PorteEmpresa == "ME");

                var _emp_porte_epp = _emp_ativas.Where(s => s.Empresa.PorteEmpresa == "EPP");
                var _emp_porte_epp_ano = _emp_ativas_ano.Where(s => s.Empresa.PorteEmpresa == "EPP");

                var _emp_porte_demais = _emp_ativas.Where(s => s.Empresa.PorteEmpresa.ToLower() == "demais");
                var _emp_porte_demais_ano = _emp_ativas_ano.Where(s => s.Empresa.PorteEmpresa.ToLower() == "demais");

                var _emp_opt_mei = _emp_ativas.Where(s => s.SimplesNacional != null && s.SimplesNacional.OpcaoMEI == "Sim");
                var _emp_opt_mei_ano = _emp_ativas_ano.Where(s => s.SimplesNacional != null && s.SimplesNacional.OpcaoMEI == "Sim");

                var _emp_opt_sn = _emp_ativas.Where(s => s.SimplesNacional != null && s.SimplesNacional.OpcaoSimples == "Sim" && s.SimplesNacional.OpcaoMEI == "Não");
                var _emp_opt_sn_ano = _emp_ativas_ano.Where(s => s.SimplesNacional != null && s.SimplesNacional.OpcaoSimples == "Sim" && s.SimplesNacional.OpcaoMEI == "Não");

                var _emp_opt_lrp = _emp_ativas.Where(s => s.SimplesNacional == null);
                var _emp_opt_lrp_ano = _emp_ativas_ano.Where(s => s.SimplesNacional == null);

                var _empresas = new List<(string item, int value, float percent)>
                {
                    ("Ativas", _emp_ativas.Count(), _emp_ativas.Count() / _t_emp * 100F),
                    ("Baixadas", _emp_baixadas.Count(), _emp_baixadas.Count() / _t_emp * 100F),
                    ("Suspensas", _emp_suspensas.Count(), _emp_suspensas.Count() / _t_emp * 100F),
                    ("Nulas", _emp_nulas.Count(), _emp_nulas.Count() / _t_emp * 100F),
                    ("Inaptas", _emp_inapta.Count(), _emp_inapta.Count() / _t_emp * 100F)
                };
                _bi_empresas.Empresas = _empresas.OrderByDescending(s => s.value);

                var _empresas_novas = new List<(string item, int value, float percent)>
                {
                    ("Ativas", _emp_ativas_ano.Count(), _emp_ativas_ano.Count() / _t_emp_novas * 100F),
                    ("Baixadas", _emp_baixadas_novas.Count(), _emp_baixadas_novas.Count() / _t_emp_novas * 100F),
                    ("Suspensas", _emp_suspensas_novas.Count(), _emp_suspensas_novas.Count() / _t_emp_novas * 100F),
                    ("Nulas", _emp_nulas_novas.Count(), _emp_nulas_novas.Count() / _t_emp_novas * 100F),
                    ("Inaptas", _emp_inapta_novas.Count(), _emp_inapta_novas.Count() / _t_emp_novas * 100F)
                };
                _bi_empresas.EmpresasNovas = _empresas_novas.OrderByDescending(s => s.value);

                var _porte_list = new List<(string item, int value, float percent)>
                {
                    ("ME", _emp_porte_me.Count(), _emp_porte_me.Count() / _t_emp_ativas * 100F),
                    ("EPP", _emp_porte_epp.Count(), _emp_porte_epp.Count() / _t_emp_ativas * 100F),
                    ("Demais", _emp_porte_demais.Count(), _emp_porte_demais.Count() / _t_emp_ativas * 100F)
                };
                _bi_empresas.Porte = _porte_list.OrderByDescending(s => s.value);

                var _porte_list_ano = new List<(string item, int value, float percent)>
                {
                    ("ME", _emp_porte_me_ano.Count(), _emp_porte_me_ano.Count() / _t_emp_ativas_ano * 100F),
                    ("EPP", _emp_porte_epp_ano.Count(), _emp_porte_epp_ano.Count() / _t_emp_ativas_ano * 100F),
                    ("Demais", _emp_porte_demais_ano.Count(), _emp_porte_demais_ano.Count() / _t_emp_ativas_ano * 100F)
                };
                _bi_empresas.PorteAnual = _porte_list_ano.OrderByDescending(s => s.value);

                var _rfiscal_list = new List<(string item, int value, float percent)>
                {
                    ("MEI", _emp_opt_mei.Count(), _emp_opt_mei.Count() / _t_emp_ativas * 100F),
                    ("Simples", _emp_opt_sn.Count(), _emp_opt_sn.Count() / _t_emp_ativas * 100F),
                    ("Lucro RP", _emp_opt_lrp.Count(), _emp_opt_lrp.Count() / _t_emp_ativas * 100F)
                };
                _bi_empresas.RFiscal = _rfiscal_list.OrderByDescending(s => s.value);

                var _rfiscal_list_ano = new List<(string item, int value, float percent)>
                {
                    ("MEI", _emp_opt_mei_ano.Count(), _emp_opt_mei_ano.Count() / _t_emp_ativas * 100F),
                    ("Simples", _emp_opt_sn_ano.Count(), _emp_opt_sn_ano.Count() / _t_emp_ativas * 100F),
                    ("Lucro RP", _emp_opt_lrp_ano.Count(), _emp_opt_lrp_ano.Count() / _t_emp_ativas * 100F)
                };
                _bi_empresas.RFiscalAnual = _rfiscal_list_ano.OrderByDescending(s => s.value);

                //CNAE Serviços
                var _n35a39 = Enumerable.Range(35, 5).Select(s => s.ToString("D2"));
                var _n49a53 = Enumerable.Range(49, 5).Select(s => s.ToString("D2"));
                var _n55e56 = Enumerable.Range(55, 2).Select(s => s.ToString("D2"));
                var _n58a66 = Enumerable.Range(58, 9).Select(s => s.ToString("D2"));
                var _n68a75 = Enumerable.Range(68, 8).Select(s => s.ToString("D2"));
                var _n77a82 = Enumerable.Range(77, 6).Select(s => s.ToString("D2"));
                var _n85a88 = Enumerable.Range(85, 4).Select(s => s.ToString("D2"));
                var _n90a93 = Enumerable.Range(90, 4).Select(s => s.ToString("D2"));
                var _n95a97 = Enumerable.Range(95, 3).Select(s => s.ToString("D2"));
                var _n99 = Enumerable.Range(99, 1).Select(s => s.ToString("D2"));
                var _serv_list = _emp_ativas.Where(s =>
                                            _n35a39.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)) ||
                                            _n49a53.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)) ||
                                            _n55e56.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)) ||
                                            _n58a66.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)) ||
                                            _n68a75.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)) ||
                                            _n77a82.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)) ||
                                            _n85a88.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)) ||
                                            _n90a93.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)) ||
                                            _n95a97.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)) ||
                                            _n99.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)));

                var _serv_list_ano = _serv_list.Where(s => s.Estabelecimento.DataInicioAtividade.StartsWith(ano.ToString()));
                float _t_serv_ano = _serv_list_ano.Count();
                float _t_serv = _serv_list.Count();

                //CNAE Agro
                var _n1a3 = Enumerable.Range(1, 3).Select(s => s.ToString("D2"));
                var _agro_list = _emp_ativas.Where(s => _n1a3.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)));
                var _agro_list_ano = _agro_list.Where(s => s.Estabelecimento.DataInicioAtividade.StartsWith(ano.ToString()));
                float _t_agro_ano = _agro_list_ano.Count();
                float _t_agro = _agro_list.Count();

                // CNAE Industria
                var _n5a9 = Enumerable.Range(5, 5).Select(n => n.ToString("D2")).ToList();
                var _n10a33 = Enumerable.Range(10, 24).Select(n => n.ToString("D2")).ToList();
                var _ind_list = _emp_ativas.Where(s =>
                    _n5a9.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)) ||
                    _n10a33.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)));
                var _ind_list_ano = _ind_list.Where(s => s.Estabelecimento.DataInicioAtividade.StartsWith(ano.ToString()));
                float _t_ind_ano = _ind_list_ano.Count();
                float _t_ind = _ind_list.Count();

                //CNAE Comércio
                var _n45a47 = Enumerable.Range(45, 3).Select(s => s.ToString("D2")).ToList();
                var _com_list = _emp_ativas.Where(s => _n45a47.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)));
                var _com_list_ano = _com_list.Where(s => s.Estabelecimento.DataInicioAtividade.StartsWith(ano.ToString()));
                float _t_com_ano = _com_list_ano.Count();
                float _t_com = _com_list.Count();

                //CNAE Construção
                var _n41a43 = Enumerable.Range(41, 3).Select(s => s.ToString("D2")).ToList();
                var _const_list = _emp_ativas.Where(s => _n41a43.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)));
                var _const_list_ano = _const_list.Where(s => s.Estabelecimento.DataInicioAtividade.StartsWith(ano.ToString()));
                float _t_const_ano = _const_list_ano.Count();
                float _t_const = _agro_list.Count();

                var _emp_formalizadas_group_mes = _emp_ativas_ano
                                                    .OrderBy(s => DateTime.ParseExact(s.Estabelecimento.DataInicioAtividade, "yyyy-MM-dd", CultureInfo.InvariantCulture).Date)
                                                    .GroupBy(s => DateTime.ParseExact(s.Estabelecimento.DataInicioAtividade, "yyyy-MM-dd", CultureInfo.InvariantCulture).ToString("MMM"));

                var _emp_formalizadas_mes_mes = new List<(string item, int value, float percent)>();
                foreach (var g in _emp_formalizadas_group_mes)
                    _emp_formalizadas_mes_mes.Add((g.Key, g.Count(), g.Count() / _t_emp_ativas_ano * 100F));

                _bi_empresas.FormalizadasMensal = _emp_formalizadas_mes_mes;

                _bi_empresas.Servicos = ("Serviços", _serv_list.Count(), _serv_list.Count() / _t_emp_ativas * 100F);
                _bi_empresas.ServicosAnual = ("Serviços", _serv_list_ano.Count(), _serv_list_ano.Count() / _t_emp_ativas_ano * 100F);

                _bi_empresas.Industria = ("Indústria", _ind_list.Count(), _ind_list.Count() / _t_emp_ativas * 100F);
                _bi_empresas.IndustriaAnual = ("Indústria", _ind_list_ano.Count(), _ind_list_ano.Count() / _t_emp_ativas_ano * 100F);

                _bi_empresas.Comercio = ("Comércio", _com_list.Count(), _com_list.Count() / _t_emp_ativas * 100F);
                _bi_empresas.ComercioAnual = ("Comércio", _com_list_ano.Count(), _com_list_ano.Count() / _t_emp_ativas_ano * 100F);

                _bi_empresas.Agro = ("Agro", _agro_list.Count(), _agro_list.Count() / _t_emp_ativas * 100F);
                _bi_empresas.AgroAnual = ("Agro", _agro_list_ano.Count(), _agro_list_ano.Count() / _t_emp_ativas_ano * 100F);

                _bi_empresas.Construcao = ("Construção", _const_list.Count(), _const_list.Count() / _t_emp_ativas * 100F);
                _bi_empresas.ConstrucaoAnual = ("Construção", _const_list_ano.Count(), _const_list_ano.Count() / _t_emp_ativas_ano * 100F);

                var _maturidade_list = new List<(string item, int value, float percent)>();

                var _emp_ativas_top_10_cnae = (from g in _emp_ativas
                                    .GroupBy(s => s.AtividadePrincipal.Descricao)
                                    .OrderByDescending(o => o.Count())
                                    .Take(10)
                                               select (g.Key, g.Count())).ToList();

                _bi_empresas.CnaesTop10 = _emp_ativas_top_10_cnae;

                var _servicos_subclasses_ano = new List<(string item, int value, float percent)>();
                _servicos_subclasses_ano.AddRange(from sb in _serv_list_ano
                                        .GroupBy(g => g.Estabelecimento.CnaeFiscalPrincipal[..2])
                                        .Where(s =>
                                        _n35a39.Any(n => s.Key.StartsWith(n)) ||
                                        _n49a53.Any(n => s.Key.StartsWith(n)) ||
                                        _n55e56.Any(n => s.Key.StartsWith(n)) ||
                                        _n58a66.Any(n => s.Key.StartsWith(n)) ||
                                        _n68a75.Any(n => s.Key.StartsWith(n)) ||
                                        _n77a82.Any(n => s.Key.StartsWith(n)) ||
                                        _n85a88.Any(n => s.Key.StartsWith(n)) ||
                                        _n90a93.Any(n => s.Key.StartsWith(n)) ||
                                        _n95a97.Any(n => s.Key.StartsWith(n)) ||
                                        _n99.Any(n => s.Key.StartsWith(n)))
                                        .OrderByDescending(o => o.Count())
                                        .Take(3)
                                                  select (sb.Key.SubClasses(), sb.Count(), sb.Count() / _t_serv_ano * 100F));

                _bi_empresas.SubServicosAnual = _servicos_subclasses_ano;
                _bi_empresas.SubServicos = from sb in _serv_list
                                        .GroupBy(g => g.Estabelecimento.CnaeFiscalPrincipal[..2])
                                        .Where(s =>
                                        _n35a39.Any(n => s.Key.StartsWith(n)) ||
                                        _n49a53.Any(n => s.Key.StartsWith(n)) ||
                                        _n55e56.Any(n => s.Key.StartsWith(n)) ||
                                        _n58a66.Any(n => s.Key.StartsWith(n)) ||
                                        _n68a75.Any(n => s.Key.StartsWith(n)) ||
                                        _n77a82.Any(n => s.Key.StartsWith(n)) ||
                                        _n85a88.Any(n => s.Key.StartsWith(n)) ||
                                        _n90a93.Any(n => s.Key.StartsWith(n)) ||
                                        _n95a97.Any(n => s.Key.StartsWith(n)) ||
                                        _n99.Any(n => s.Key.StartsWith(n)))
                                        .OrderByDescending(o => o.Count())
                                        .Take(3)
                                           select (sb.Key.SubClasses(), sb.Count(), sb.Count() / _t_serv * 100F);

                var _comercio_sub_cnaes_ano = new List<(string item, int value, float percent)>();
                _comercio_sub_cnaes_ano.AddRange(from sb in _com_list_ano
                                        .GroupBy(g => g.Estabelecimento.CnaeFiscalPrincipal[..2])
                                        .Where(s => _n45a47.Any(n => s.Key.StartsWith(n)))
                                        .OrderByDescending(o => o.Count())
                                        .Take(3)
                                                 select (sb.Key.SubClasses(), sb.Count(), sb.Count() / _t_com_ano * 100F));
                _bi_empresas.SubComercioAnual = _comercio_sub_cnaes_ano;

                _bi_empresas.SubComercio = from sc in _com_list
                                            .GroupBy(g => g.Estabelecimento.CnaeFiscalPrincipal[..2])
                                            .Where(s => _n45a47.Any(n => s.Key.StartsWith(n)))
                                            .OrderByDescending(o => o.Count())
                                            .Take(3)
                                           select (sc.Key.SubClasses(), sc.Count(), sc.Count() / _t_com * 100F);

                var _ind_sub_cnaes_ano = new List<(string item, int value, float percent)>();
                _ind_sub_cnaes_ano.AddRange(from sb in _ind_list_ano
                                    .GroupBy(g => g.Estabelecimento.CnaeFiscalPrincipal[..2])
                                    .Where(s =>
                                    _n5a9.Any(n => s.Key.StartsWith(n)) ||
                                    _n10a33.Any(n => s.Key.StartsWith(n)))
                                    .OrderByDescending(o => o.Count())
                                    .Take(3)
                                            select (sb.Key.SubClasses(), sb.Count(), sb.Count() / _t_ind_ano * 100F));
                _bi_empresas.SubIndustriaAnual = _ind_sub_cnaes_ano;

                _bi_empresas.SubIndustria = from si in _ind_list
                                    .GroupBy(g => g.Estabelecimento.CnaeFiscalPrincipal[..2])
                                    .Where(s =>
                                    _n5a9.Any(n => s.Key.StartsWith(n)) ||
                                    _n10a33.Any(n => s.Key.StartsWith(n)))
                                    .OrderByDescending(o => o.Count())
                                    .Take(3)
                                            select (si.Key.SubClasses(), si.Count(), si.Count() / _t_ind * 100F);

                var _agro_sub_cnaes_ano = new List<(string item, int value, float percent)>();
                _agro_sub_cnaes_ano.AddRange(from sb in _agro_list_ano
                                    .GroupBy(g => g.Estabelecimento.CnaeFiscalPrincipal[..2])
                                    .Where(s => _n1a3.Any(n => s.Key.StartsWith(n)))
                                    .OrderByDescending(o => o.Count())
                                    .Take(3)
                                             select (sb.Key.SubClasses(), sb.Count(), sb.Count() / _t_agro_ano * 100F));
                _bi_empresas.SubAgroAnual = _agro_sub_cnaes_ano;

                _bi_empresas.SubAgro = from sb in _agro_list
                                    .GroupBy(g => g.Estabelecimento.CnaeFiscalPrincipal[..2])
                                    .Where(s => _n1a3.Any(n => s.Key.StartsWith(n)))
                                    .OrderByDescending(o => o.Count())
                                    .Take(3)
                                       select (sb.Key.SubClasses(), sb.Count(), sb.Count() / _t_agro * 100F);

                var _const_sub_cnaes_ano = new List<(string item, int value, float percent)>();
                _const_sub_cnaes_ano.AddRange(from sb in _const_list_ano
                                    .Where(s => _n41a43.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)))
                                    .GroupBy(g => g.AtividadePrincipal.Codigo[..2])
                                    .OrderByDescending(o => o.Count())
                                    .Take(3)
                                              select (sb.Key.SubClasses(), sb.Count(), sb.Count() / _t_const_ano * 100F));
                _bi_empresas.SubConstrucaoAnual = _const_sub_cnaes_ano;

                _bi_empresas.SubConstrucao = from sb in _const_list
                                    .Where(s => _n41a43.Any(n => s.Estabelecimento.CnaeFiscalPrincipal.StartsWith(n)))
                                    .GroupBy(g => g.AtividadePrincipal.Codigo[..2])
                                    .OrderByDescending(o => o.Count())
                                    .Take(3)
                                             select (sb.Key.SubClasses(), sb.Count(), sb.Count() / _t_const * 100F);

                var _lifetime = new List<(string item, int value, float percent)>();
                _lifetime.AddRange(from dt in _emp_ativas
                                                .GroupBy(d => DateTime.ParseExact(d.Estabelecimento.DataInicioAtividade, "yyyy-MM-dd", CultureInfo.InvariantCulture).DateDiference())
                                                .OrderByDescending(o => o.Count())
                                   select (dt.Key, dt.Count(), dt.Count() / _t_emp_ativas * 100F));
                _bi_empresas.Maturidade = _lifetime;


                r_empresas.Add(_bi_empresas);

                //}
                //catch (Exception ex) { throw new Exception(ex.Message); }

                return r_empresas;
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> DoListByCnaeAsync(string atividadei, string atividadef, string municipio)
        {
            return await _cnpj.DoListByCnaeAsync(atividadei, atividadef, municipio);
        }

        public async Task<IEnumerable<(string Cnpj, string RazaoSocial, string Tel, string Email, string Cnae)>> DoListCnaeEmpresasJsonAsync(string cnaei, string cnaef, string municipio, string situacao)
        {
            var emp = await DoListByCnaeAsync(cnaei, cnaef, municipio);

            return await Task.Run(() =>
            {
                var lista = new List<(string Cnpj, string RazaoSocial, string Tel, string Email, string Cnae)>();

                foreach (var s in emp.Where(s => s.Estabelecimento.SituacaoCadastral == situacao).OrderBy(s => s.AtividadePrincipal.Codigo))
                {
                    lista.Add(new()
                    {
                        Cnpj = s.CNPJ,
                        RazaoSocial = s.Empresa.RazaoSocial,
                        Tel = String.Format("{0} {1}", s.Estabelecimento.DDD1, s.Estabelecimento.Telefone1),
                        Email = s.Estabelecimento.CorreioEletronico,
                        Cnae = s.AtividadePrincipal.Codigo + " - " + s.AtividadePrincipal.Descricao
                    });
                }

                return lista;
            });
        }

        public async Task<IEnumerable<Municipio>> DoListMicroRegiaoJahuAsync()
        {
            var list = await DoListMinicipiosAsync();
            return list.Where(s => s.MicroRegiaoJahu(s));
        }

        public async Task<IEnumerable<Municipio>> DoListMinicipiosAsync()
        {
            return await _cnpj.DoListMinicipiosAsync();
        }

        public async Task<IEnumerable<ELocalizacao>> DoListZonaJsonAsync(string zona, string municipio, string situacao) =>
            await Task.Run(async () =>
            {
                var emp = await DoListByZonaAsync(zona, municipio);

                var lista = new List<ELocalizacao>();

                foreach (var s in emp.Where(s => s.Estabelecimento.SituacaoCadastral == situacao).OrderBy(s => s.AtividadePrincipal.Codigo))
                {
                    lista.Add(new ELocalizacao(
                        zona: s.Estabelecimento.Bairro,
                        rua: String.Format("{0} {1}", s.Estabelecimento.TipoLogradouro, s.Estabelecimento.Logradouro),
                        numero: s.Estabelecimento.Numero
                    ));
                }
                return lista;
            });

        public async Task<IEnumerable<ELocalizacao>> DoListLogradouroJsonAsync(string logradouro, string municipio, string situacao) =>
            await Task.Run(async () =>
            {
                var emp = await DoListByLogradouroAsync(logradouro, municipio);

                var lista = new List<ELocalizacao>();

                foreach (var s in emp.Where(s => s.Estabelecimento.SituacaoCadastral == situacao).OrderBy(s => s.AtividadePrincipal.Codigo))
                {
                    lista.Add(new ELocalizacao(
                        zona: s.Estabelecimento.Bairro,
                        rua: String.Format("{0} {1}", s.Estabelecimento.TipoLogradouro, s.Estabelecimento.Logradouro),
                        numero: s.Estabelecimento.Numero
                    ));
                }
                return lista;
            });

        public async Task<IEnumerable<BaseReceitaFederal>> DoListByZonaAsync(string zona, string municipio) =>
            await _cnpj.DoListByZonaAsync(zona, municipio);
        public async Task<IEnumerable<BaseReceitaFederal>> DoListByLogradouroAsync(string logradouro, string municipio) =>
            await _cnpj.DoListByLogradouroAsync(logradouro, municipio);

        public async Task<IEnumerable<EExport>> DoListExport(IEnumerable<BaseReceitaFederal> obj) =>
            await Task.Run(() =>
            {
                var _list = new List<EExport>();
                var cont = 0;
                foreach (var item in obj)
                {
                    _list.Add(new EExport(
                        contador: cont++,
                        cnpj: item.CNPJ.Mask("##.###.###/####-##"),
                        razaosocial: item.Empresa.RazaoSocial,
                        matriz: item.Estabelecimento.IdentificadorMatrizFilial,
                        abertura: item.Estabelecimento.DataInicioAtividade,
                        situacao: item.Estabelecimento.SituacaoCadastral,
                        zona: item.Estabelecimento.Bairro,
                        logradouro: string.Format("{0} {1}", item.Estabelecimento.TipoLogradouro, item.Estabelecimento.Logradouro),
                        numero: item.Estabelecimento.Numero,
                        localizacao: string.Format("{0} {1}, {2}, {3}-{4}", item.Estabelecimento.TipoLogradouro, item.Estabelecimento.Logradouro, item.Estabelecimento.Numero, item.Cidade.Descricao, item.Estabelecimento.UF),
                        municipio: item.Cidade.Descricao,
                        cnae: item.AtividadePrincipal.Codigo.Mask("##.##-#/##"),
                        atividade: item.AtividadePrincipal.Descricao,
                        porte: item.Empresa.PorteEmpresa,
                        regimefiscal: item.SimplesNacional == null ? item.Empresa.RegimeFiscal("", "") : item.Empresa.RegimeFiscal(item.SimplesNacional.OpcaoSimples, item.SimplesNacional.OpcaoMEI),
                        setor: item.Estabelecimento.SetorProdutivo(item.AtividadePrincipal.Codigo)
                    ));
                }

                return _list;
            });

        public async Task<IEnumerable<BaseReceitaFederal>> DoListAsync(Expression<Func<Estabelecimento, bool>> filter = null) =>
            await _cnpj.DoListAsync(filter);

        public async Task<IEnumerable<(int Value, string Key, string Code)>> DoListCnaesAsync(Expression<Func<Estabelecimento, bool>> filter = null) =>
            await Task.Run(async () =>
            {

                var _return = new List<(int Value, string Key, string Code)>();
                var _list = new List<string>();

                foreach (var _e in await _cnpj.DoListAsync(filter))
                {
                    _list.Add($"{_e.AtividadePrincipal.Codigo} - {_e.AtividadePrincipal.Descricao}");
                }

                foreach (var _v in _list.GroupBy(s => s))
                {
                    string[] cnae = _v.Key.Split(" - ");
                    _return.Add((_v.Count(), _v.Key, cnae[0]));
                }

                return _return.OrderByDescending(s => s.Value).ToList();
            });

        public async Task<IEnumerable<BaseReceitaFederal>> DoListCNAEAsync(string municipio, Expression<Func<CNAE, bool>> param = null)
            => await _cnpj.DoListCNAEAsync(municipio, param);

        public async Task<IEnumerable<string>> DoListMappingLogradourosIIAsync(IEnumerable<BaseReceitaFederal> obj)
            => await Task.Run(() =>
            {

                var _list = new List<string>();

                foreach (var nome in obj
                                    .OrderBy(o => o.Estabelecimento.TipoLogradouro + " " + o.Estabelecimento.Logradouro)
                                    .GroupBy(g => g.Estabelecimento.TipoLogradouro + " " + g.Estabelecimento.Logradouro))
                {
                    _list.Add(nome.Key);
                }

                return _list;
            });
    }
}
