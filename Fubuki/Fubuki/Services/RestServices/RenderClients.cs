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

    public async Task RestartServiceById(string serviceId)
    {
        var response = await _client.PostAsync($"{EnvStorage.RenderBaseURL}/services/{serviceId}/restart", null);
        if (response.IsSuccessStatusCode is false)
            Console.WriteLine("starting service error: " + response.StatusCode);

        Console.WriteLine("resuming service: " + serviceId);
    }
    public async Task ResumeServiceById(string serviceId)
    {
        var response = await _client.PostAsync($"{EnvStorage.RenderBaseURL}/services/{serviceId}/resume", null);
        if (response.IsSuccessStatusCode is false)
            Console.WriteLine("resuming service error: " + response.StatusCode);

        Console.WriteLine("starting service: " + serviceId);
    }
    public async Task SuspendServiceById(string serviceId)
    {
        var response = await _client.PostAsync($"{EnvStorage.RenderBaseURL}/services/{serviceId}/suspend", null);
        if (response.IsSuccessStatusCode is false)
            Console.WriteLine("suspending service error: " + response.StatusCode);

        Console.WriteLine("suspending service: " + serviceId);
    }

    // paginated client
    public async Task<List<DeployDtos>?> GetDeploysFromCursorId(string serviceId, string cursorId)
    {
        var response = await _client.GetAsync($"{EnvStorage.RenderBaseURL}/services/{serviceId}/deploys?cursor={cursorId}&limit=8");
        if (response.IsSuccessStatusCode is false)
        {
            Console.WriteLine("GetDeploysFromCursorId error: " + response.StatusCode);
            return null!;
        }

        return await response.Content.ReadFromJsonAsync<List<DeployDtos>>();
    }

    public async Task<List<DeployDtos>?> GetAllDeploys(string serviceId)
    {
        var response = await _client.GetAsync($"{EnvStorage.RenderBaseURL}/services/{serviceId}/deploys?limit=8");
        if (response.IsSuccessStatusCode is false)
        {
            Console.WriteLine("GetAllDeploys error: " + response.StatusCode);
            return null!;
        }

        return await response.Content.ReadFromJsonAsync<List<DeployDtos>>();
    }
    public async Task<List<RenderDtos>?> GetAllServices()
    {
        Console.WriteLine("fetching all web services!");
        var response = await _client.GetAsync($"{EnvStorage.RenderBaseURL}/services");
        if (response.IsSuccessStatusCode is false)
        {
            Console.WriteLine("GetAllServices error: " + response.StatusCode);
            return null!;
        }

        return await response.Content.ReadFromJsonAsync<List<RenderDtos>>();
    }
}
