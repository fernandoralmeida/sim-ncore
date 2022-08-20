using Sim.Domain.Cnpj.Entity;
using Sim.Domain.Cnpj.Interfaces;

namespace Sim.Domain.Cnpj.Services
{
    public class ServiceCnpj : ServiceBase<BaseReceitaFederal>, IServiceCnpj
    {
        private readonly IRepositoryCnpj _cnpj;

        public ServiceCnpj(IRepositoryCnpj cnpj):base(cnpj)
        {
            _cnpj = cnpj;   
        }

        public async Task<IEnumerable<BaseReceitaFederal>> DoListBaseRazaoSociosAsync(string param)
        {
            return await _cnpj.DoListBaseRazaoSociosAsync(param);
        }

        public async Task<BaseReceitaFederal> GetCNPJAsync(string cnpj)
        {
            return await _cnpj.GetCNPJAsync(cnpj);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string bairro, string endereco, string cnae, string municipio, string situacaocadastral)
        {
            return await _cnpj.ListAllAsync(bairro, endereco, cnae, municipio, situacaocadastral);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string municipio)
        {
            return await _cnpj.ListAllAsync(municipio);
        }
        public async Task<IEnumerable<BaseReceitaFederal>> ListAllAsync(string municipio, string situacaocadastral)
        {
            return await _cnpj.ListAllAsync(municipio, situacaocadastral);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllMatrizFilialAsync(string cnpjbase)
        {
            return await _cnpj.ListAllMatrizFilialAsync(cnpjbase);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllRazaoSocialAsync(string razaosocial)
        {
            return await _cnpj.ListAllRazaoSocialAsync(razaosocial);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListAllSocioAsync(string nomesocio)
        {
            return await _cnpj.ListAllSocioAsync(nomesocio);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string municipio, string situacaocadastral)
        {
            return await _cnpj.ListOptantesSimplesNacionalAsync(municipio, situacaocadastral);
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ListOptantesSimplesNacionalAsync(string endereco, string cnae, string municipio, string situacaocadastral)
        {
            return await _cnpj.ListOptantesSimplesNacionalAsync(endereco, cnae, municipio, situacaocadastral);
        }

        public async Task<IEnumerable<BICnae>> ToListBICnaeAsync(string municipio)
        {

            var emp = await _cnpj.ToListByCnaeAsync("0000000","9999999", municipio);

            return await Task.Run(() => {

                var l_full_cnae = new List<BICnae>();

                var secao = new KeyValuePair<string, int>();

                var l_secao = new BICnae() { ListaSecao = new() };

                var subclasse = new List<string>();

                foreach (BaseReceitaFederal e in emp.Where(s => s.Estabelecimento.SituacaoCadastral == "Ativa"))
                        subclasse.Add(string.Format("{0} - {1}", e.AtividadePrincipal.Codigo, e.AtividadePrincipal.Descricao));
               

                var s_subclasse = from x in subclasse
                                  group x by x into g
                                  let count = g.Count()
                                  orderby g.Key ascending
                                  select new { Value = g.Key, Count = count };

                var sec_count = 0;
                var sub_sec_count = 0;

                //agro
                var agro = new CnaeSecao() { ListaClasse = new(),
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
                var indetransf = new CnaeSecao() { ListaClasse = new(),
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

        public async Task<IEnumerable<BIEmpresas>> ToListBIEmpresasAsync(string municipio, string situacao, string ano, string mes)
        {
            var t = await _cnpj.ListAllAsync("", "", "", municipio, "");            

            return await Task.Run(() => {
                
                var r_empresas = new List<BIEmpresas>();

                try
                {
                    var _situcao = new List<string>();
                    var _emp = new List<string>();
                    var _atv = new List<string>();
                    var _setores = new List<string>();
                    var _temp = new List<string>();
                    var _porte = new List<string>();
                    var _osn = new List<string>();
                    var _mei = new List<string>();
                    var _var_mes = new List<string>();

                    var _servico = new List<string>();
                    var _comercio = new List<string>();
                    var _industria = new List<string>();
                    var _agro = new List<string>();
                    var _construcao = new List<string>();

                    var _formalizacao = new List<string>();
                    var _baixada = new List<string>();
                    var _mortalidade = new List<string>();

                    var bi_empresas = new BIEmpresas();

                    int cnae = 0;

                    var mortalidade = DateTime.Today.AddDays(-731).ToShortDateString(); //add 2 anos
                    var splitdata = mortalidade.Split(new char[] { '/' });
                    var mortalidade2 = string.Format("{0}{1}{2}", splitdata[2], splitdata[1], splitdata[0]);
                    var datainicial = Convert.ToInt32(mortalidade2);
                    var dataabertura = 0;

                    foreach (BaseReceitaFederal at in t)
                    {

                        if (at.AtividadePrincipal.Codigo != "9492800") //Excluir Servioços políticos
                        {
                            string[] data = at.Estabelecimento.DataInicioAtividade.ToString().Split(new char[] { '-' });

                            dataabertura = Convert.ToInt32(at.Estabelecimento.DataInicioAtividade.Replace("-", ""));

                            if (dataabertura >= datainicial)
                            {
                                if (at.Estabelecimento.SituacaoCadastral == "Baixada")
                                    _mortalidade.Add("B");

                                _mortalidade.Add("F");
                            }

                            if (mes == "99")
                            {

                                if (data[0] == ano)
                                {
                                    if (at.Estabelecimento.SituacaoCadastral == "Baixada")
                                        _baixada.Add("B");
                                    else if (at.Estabelecimento.SituacaoCadastral == "Ativa")
                                        _formalizacao.Add("F");
                                }

                                if (at.Estabelecimento.SituacaoCadastral == situacao)
                                {

                                    _porte.Add(at.Empresa.PorteEmpresa);

                                    if (at.SimplesNacional != null)
                                    {
                                        switch (at.SimplesNacional.OpcaoSimples)
                                        {
                                            case "Sim":
                                                _osn.Add(at.SimplesNacional.OpcaoSimples);
                                                break;
                                        }
                                        switch (at.SimplesNacional.OpcaoMEI)
                                        {
                                            case "Sim":
                                                _mei.Add(at.SimplesNacional.OpcaoMEI);
                                                break;
                                        }
                                    }

                                    _atv.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                    _emp.Add("E");

                                    cnae = Convert.ToInt32(at.AtividadePrincipal.Codigo.Remove(2, 5));

                                    if (cnae >= 1 && cnae <= 3)
                                    {
                                        _setores.Add("Agropecuária");
                                        _agro.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                    }

                                    else if (cnae >= 45 && cnae <= 47)
                                    {
                                        _setores.Add("Comércio");
                                        _comercio.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                    }
                                    else if (cnae >= 05 & cnae <= 09 || cnae >= 10 && cnae <= 33)
                                    {
                                        _setores.Add("Indústria");
                                        _industria.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                    }
                                    else if (cnae >= 41 & cnae <= 43)
                                    {
                                        _setores.Add("Construção");
                                        _construcao.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                    }
                                    else if (cnae == 35 || (cnae >= 36 && cnae <= 39)
                                        || (cnae >= 49 && cnae <= 53)
                                        || (cnae >= 55 && cnae <= 56)
                                        || (cnae >= 58 && cnae <= 63)
                                        || (cnae >= 64 && cnae <= 66)
                                        || (cnae == 68)
                                        || (cnae >= 69 && cnae <= 75)
                                        || (cnae >= 77 && cnae <= 82)
                                        || (cnae == 85)
                                        || (cnae >= 86 && cnae <= 88)
                                        || (cnae >= 86 && cnae <= 88)
                                        || (cnae >= 90 && cnae <= 93)
                                        || (cnae >= 94 && cnae <= 96)
                                        || (cnae == 97)
                                        || (cnae == 99))
                                    {
                                        _setores.Add("Serviços");
                                        _servico.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                    }
                                }

                                _temp.Add("TE");
                                _situcao.Add(at.Estabelecimento.SituacaoCadastral);


                            }

                            else if (mes == "00")
                            {

                                if (data[0] == ano)
                                {
                                    if (at.Estabelecimento.SituacaoCadastral == "Baixada")
                                        _baixada.Add("B");
                                    else if (at.Estabelecimento.SituacaoCadastral == "Ativa")
                                        _formalizacao.Add("F");


                                    if (at.Estabelecimento.SituacaoCadastral == situacao)
                                    {
                                        _porte.Add(at.Empresa.PorteEmpresa);

                                        switch (data[1])
                                        {
                                            case "01":
                                                _var_mes.Add("01Jan");
                                                break;
                                            case "02":
                                                _var_mes.Add("02Fev");
                                                break;
                                            case "03":
                                                _var_mes.Add("03Mar");
                                                break;
                                            case "04":
                                                _var_mes.Add("04Abr");
                                                break;
                                            case "05":
                                                _var_mes.Add("05Mai");
                                                break;
                                            case "06":
                                                _var_mes.Add("06Jun");
                                                break;
                                            case "07":
                                                _var_mes.Add("07Jul");
                                                break;
                                            case "08":
                                                _var_mes.Add("08Ago");
                                                break;
                                            case "09":
                                                _var_mes.Add("09Set");
                                                break;
                                            case "10":
                                                _var_mes.Add("10Out");
                                                break;
                                            case "11":
                                                _var_mes.Add("11Nov");
                                                break;
                                            case "12":
                                                _var_mes.Add("12Dez");
                                                break;
                                        }

                                        if (at.SimplesNacional != null)
                                        {
                                            switch (at.SimplesNacional.OpcaoSimples)
                                            {
                                                case "Sim":
                                                    _osn.Add(at.SimplesNacional.OpcaoSimples);
                                                    break;
                                            }
                                            switch (at.SimplesNacional.OpcaoMEI)
                                            {
                                                case "Sim":
                                                    _mei.Add(at.SimplesNacional.OpcaoMEI);
                                                    break;
                                            }
                                        }

                                        _atv.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        _emp.Add("E");

                                        cnae = Convert.ToInt32(at.AtividadePrincipal.Codigo.Remove(2, 5));

                                        if (cnae >= 1 && cnae <= 3)
                                        {
                                            _setores.Add("Agropecuária");
                                            _agro.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        }

                                        else if (cnae >= 45 && cnae <= 47)
                                        {
                                            _setores.Add("Comércio");
                                            _comercio.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        }
                                        else if (cnae >= 05 & cnae <= 09 || cnae >= 10 && cnae <= 33)
                                        {
                                            _setores.Add("Indústria");
                                            _industria.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        }
                                        else if (cnae >= 41 & cnae <= 43)
                                        {
                                            _setores.Add("Construção");
                                            _construcao.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        }
                                        else if (cnae == 35 || (cnae >= 36 && cnae <= 39)
                                            || (cnae >= 49 && cnae <= 53)
                                            || (cnae >= 55 && cnae <= 56)
                                            || (cnae >= 58 && cnae <= 63)
                                            || (cnae >= 64 && cnae <= 66)
                                            || (cnae == 68)
                                            || (cnae >= 69 && cnae <= 75)
                                            || (cnae >= 77 && cnae <= 82)
                                            || (cnae == 85)
                                            || (cnae >= 86 && cnae <= 88)
                                            || (cnae >= 86 && cnae <= 88)
                                            || (cnae >= 90 && cnae <= 93)
                                            || (cnae >= 94 && cnae <= 96)
                                            || (cnae == 97)
                                            || (cnae == 99))
                                        {
                                            _setores.Add("Serviços");
                                            _servico.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        }
                                    }

                                    _temp.Add("TE");
                                    _situcao.Add(at.Estabelecimento.SituacaoCadastral);
                                }
                            }

                            else
                            {

                                if (data[0] == ano && data[1] == mes)
                                {
                                    if (at.Estabelecimento.SituacaoCadastral == "Baixada")
                                        _baixada.Add("B");
                                    else if (at.Estabelecimento.SituacaoCadastral == "Ativa")
                                        _formalizacao.Add("F");

                                    if (at.Estabelecimento.SituacaoCadastral == situacao)
                                    {
                                        _porte.Add(at.Empresa.PorteEmpresa);

                                        if (at.SimplesNacional != null)
                                        {
                                            switch (at.SimplesNacional.OpcaoSimples)
                                            {
                                                case "Sim":
                                                    _osn.Add(at.SimplesNacional.OpcaoSimples);
                                                    break;
                                            }
                                            switch (at.SimplesNacional.OpcaoMEI)
                                            {
                                                case "Sim":
                                                    _mei.Add(at.SimplesNacional.OpcaoMEI);
                                                    break;
                                            }
                                        }

                                        _atv.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        _emp.Add("E");

                                        cnae = Convert.ToInt32(at.AtividadePrincipal.Codigo.Remove(2, 5));

                                        if (cnae >= 1 && cnae <= 3)
                                        {
                                            _setores.Add("Agropecuária");
                                            _agro.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        }

                                        else if (cnae >= 45 && cnae <= 47)
                                        {
                                            _setores.Add("Comércio");
                                            _comercio.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        }
                                        else if (cnae >= 05 & cnae <= 09 || cnae >= 10 && cnae <= 33)
                                        {
                                            _setores.Add("Indústria");
                                            _industria.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        }
                                        else if (cnae >= 41 & cnae <= 43)
                                        {
                                            _setores.Add("Construção");
                                            _construcao.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        }
                                        else if (cnae == 35 || (cnae >= 36 && cnae <= 39)
                                            || (cnae >= 49 && cnae <= 53)
                                            || (cnae >= 55 && cnae <= 56)
                                            || (cnae >= 58 && cnae <= 63)
                                            || (cnae >= 64 && cnae <= 66)
                                            || (cnae == 68)
                                            || (cnae >= 69 && cnae <= 75)
                                            || (cnae >= 77 && cnae <= 82)
                                            || (cnae == 85)
                                            || (cnae >= 86 && cnae <= 88)
                                            || (cnae >= 86 && cnae <= 88)
                                            || (cnae >= 90 && cnae <= 93)
                                            || (cnae >= 94 && cnae <= 96)
                                            || (cnae == 97)
                                            || (cnae == 99))
                                        {
                                            _setores.Add("Serviços");
                                            _servico.Add(string.Format("{0}", at.AtividadePrincipal.Descricao));
                                        }
                                    }

                                    _temp.Add("TE");
                                    _situcao.Add(at.Estabelecimento.SituacaoCadastral);
                                }
                            }
                        }
                    }

                    bi_empresas.EmpresasAtivas = new KeyValuePair<string, int>("Estatísticas", _emp.Count);
                    bi_empresas.TotalEmpresas = new KeyValuePair<string, int>("Empresas", _temp.Count);
                    bi_empresas.Formalizacoes = new KeyValuePair<string, int>("Formalizadas", _formalizacao.Count);
                    bi_empresas.Baixas = new KeyValuePair<string, int>("Baixadas", _baixada.Count);
                    bi_empresas.SimplesNacional = new KeyValuePair<string, int>("Simples Nacional", _osn.Count);
                    bi_empresas.OptanteMEI = new KeyValuePair<string, int>("Optante MEI", _mei.Count);

                    var c_atv = from x in _atv
                                group x by x into g
                                let count = g.Count()
                                orderby count descending
                                select new { Value = g.Key, Count = count };

                    var n_atv = new List<KeyValuePair<string, int>>();
                    foreach (var x in c_atv)
                    {
                        n_atv.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }
                    bi_empresas.ListaAtividades = n_atv;

                    var c_stc = from x in _situcao
                                group x by x into g
                                let count = g.Count()
                                orderby count descending
                                select new { Value = g.Key, Count = count };

                    var n_stc = new List<KeyValuePair<string, int>>();
                    foreach (var x in c_stc)
                    {
                        n_stc.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }

                    bi_empresas.ListaSituacao = n_stc;

                    var c_srv = from x in _setores
                                group x by x into g
                                let count = g.Count()
                                orderby count descending
                                select new { Value = g.Key, Count = count };

                    var n_srv = new List<KeyValuePair<string, int>>();
                    foreach (var x in c_srv)
                    {
                        n_srv.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }
                    bi_empresas.ListaSetores = n_srv;

                    // Atividades Serviços
                    var c_servico = from x in _servico
                                    group x by x into g
                                    let count = g.Count()
                                    orderby count descending
                                    select new { Value = g.Key, Count = count };

                    var l_servico = new List<KeyValuePair<string, int>>();
                    foreach (var x in c_servico)
                    {
                        l_servico.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }
                    bi_empresas.Servicos = l_servico;

                    // Atividades Comércio
                    var c_comercio = from x in _comercio
                                     group x by x into g
                                     let count = g.Count()
                                     orderby count descending
                                     select new { Value = g.Key, Count = count };

                    var l_comercio = new List<KeyValuePair<string, int>>();
                    foreach (var x in c_comercio)
                    {
                        l_comercio.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }
                    bi_empresas.Comercio = l_comercio;

                    // Atividades Industria
                    var c_industria = from x in _industria
                                      group x by x into g
                                      let count = g.Count()
                                      orderby count descending
                                      select new { Value = g.Key, Count = count };

                    var l_industria = new List<KeyValuePair<string, int>>();
                    foreach (var x in c_industria)
                    {
                        l_industria.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }
                    bi_empresas.Indistria = l_industria;

                    // Atividades Industria
                    var c_agro = from x in _agro
                                 group x by x into g
                                 let count = g.Count()
                                 orderby count descending
                                 select new { Value = g.Key, Count = count };

                    var l_agro = new List<KeyValuePair<string, int>>();
                    foreach (var x in c_agro)
                    {
                        l_agro.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }
                    bi_empresas.Agro = l_agro;

                    // Atividades Construção
                    var c_construcao = from x in _construcao
                                       group x by x into g
                                       let count = g.Count()
                                       orderby count descending
                                       select new { Value = g.Key, Count = count };

                    var l_constucao = new List<KeyValuePair<string, int>>();
                    foreach (var x in c_construcao)
                    {
                        l_constucao.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }
                    bi_empresas.Construcao = l_constucao;

                    // Porte
                    var c_porte = from x in _porte
                                  group x by x into g
                                  let count = g.Count()
                                  orderby count descending
                                  select new { Value = g.Key, Count = count };

                    var l_porte = new List<KeyValuePair<string, int>>();
                    foreach (var x in c_porte)
                    {
                        l_porte.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }
                    bi_empresas.Porte = l_porte;

                    // Varicao Mes a Mes
                    var c_var_mes = from x in _var_mes
                                    group x by x into g
                                    let count = g.Count()
                                    orderby g.Key ascending
                                    select new { Value = g.Key.Remove(0, 2), Count = count };

                    var l_var_mes = new List<KeyValuePair<string, int>>();
                    foreach (var x in c_var_mes)
                    {
                        l_var_mes.Add(new KeyValuePair<string, int>(x.Value, x.Count));
                    }
                    bi_empresas.ListaMensal = l_var_mes;

                    // Taxa de mortalidade empresarial
                    /**/
                    var taxa_mort = from x in _mortalidade
                                    group x by x into g
                                    let count = g.Count()
                                    orderby count descending
                                    select new { Value = g.Key, Count = count };

                    var l_tx_mort = new List<KeyValuePair<string, string>>();
                    float emp_f = 0;
                    float emp_b = 0;
                    foreach (var x in taxa_mort)
                    {
                        emp_f += x.Count;
                        if (x.Value == "B")
                            emp_b = x.Count;
                    }

                    var r_tx_mort = (emp_b * 100) / emp_f;

                    l_tx_mort.Add(new KeyValuePair<string, string>("Mortalidade de empresas com até 2 anos de atividade", string.Format("{0:N2}%", r_tx_mort)));

                    bi_empresas.ListaMortalidadeEmpresas = l_tx_mort;

                    r_empresas.Add(bi_empresas);

                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                
                return r_empresas;
           
            });
        }

        public async Task<IEnumerable<BaseReceitaFederal>> ToListByCnaeAsync(string atividadei, string atividadef, string municipio)
        {
            return await _cnpj.ToListByCnaeAsync(atividadei, atividadef, municipio);
        }

        public async Task<IEnumerable<(string Cnpj, string RazaoSocial, string Tel, string Email, string Cnae)>> ToListCnaeEmpresasJsonAsync(string cnaei, string cnaef, string municipio, string situacao)
        {
            var emp = await ToListByCnaeAsync(cnaei, cnaef, municipio);

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

        public async Task<IEnumerable<Municipio>> ToListMicroRegiaoJahuAsync()
        {
            var list = await ToListMinicipiosAsync();
            return list.Where(s => s.MicroRegiaoJahu(s));
        }

        public async Task<IEnumerable<Municipio>> ToListMinicipiosAsync()
        {
            return await _cnpj.ToListMinicipiosAsync();
        }
    }
}
