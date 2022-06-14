using Newtonsoft.Json;

namespace Sim.Domain.WebService.RWS.Entity
{
    public class Atividade
    {

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }
}
