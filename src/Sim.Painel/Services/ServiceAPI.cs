using Sim.Painel.Dto;

namespace Sim.Painel.Services;
public class ServiceAPI {
    private readonly HttpClient _httpClient;

    public ServiceAPI(HttpClient httpClient) {
        _httpClient = httpClient;        
    }

    public async Task<IEnumerable<DtoSetores>> DoListUsers() {
        var _resp = _httpClient.GetAsync("https://localhost:7116/v1/bi-users_status");
        _resp.EnsureSuccessStatusCode();
        var _json = await _resp.Content.ReadAsStringAsync();
        
        return null;
    }
    public Task<IEnumerable<DtoChartDual>> DoListMonths() {
        return null;
    }

}   