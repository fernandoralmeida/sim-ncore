﻿using Newtonsoft.Json;
using Sim.Domain.WebService.RWS.Entity;
using Sim.Domain.WebService.RWS.Functions;

namespace Sim.Domain.WebService.RWS.Services
{
    public class ReceitaWS : IReceitaWS
    {
        private static readonly string ReceitaWSApi = "https://www.receitaws.com.br/v1/cnpj/{0}";
        private readonly HttpClient _httpClient;

        public ReceitaWS()
        {
            _httpClient = new HttpClient();
        }

        public CNPJ ConsultarCPNJ(string cnpj)
        {
            return ConsultarCPNJAsync(cnpj).Result;
        }

        public async Task<CNPJ> ConsultarCPNJAsync(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj) || !Validate.CNPJ(cnpj))
                throw new ArgumentException("CNPJ Invalido.");

            try
            {
                using (HttpResponseMessage response = await _httpClient.GetAsync(string.Format(ReceitaWSApi, cnpj)).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<CNPJ>(result);
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
            }

            return null;
        }
    }
}
