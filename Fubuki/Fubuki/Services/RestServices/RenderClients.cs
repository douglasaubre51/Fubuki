using Fubuki.Dtos;
using Fubuki.Storage;
using System.Net.Http.Json;

namespace Fubuki.Services.RestServices;

public class RenderClients
{
    HttpClient _client;

    public RenderClients()
    {
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Add("authorization", "Bearer " + RenderKeyStore.ApiKey);
    }

    public async Task<List<RenderDtos>?> GetAllServices()
    {
        Console.WriteLine("fetching all web services!");
        var response = await _client.GetAsync($"{EnvStorage.RenderBaseURL}/services");
        if (response.IsSuccessStatusCode is false)
        {
            Console.WriteLine("GetAllServices error: "+response.StatusCode);
            return null!;
        }

        return await response.Content.ReadFromJsonAsync<List<RenderDtos>>();
    }
}
