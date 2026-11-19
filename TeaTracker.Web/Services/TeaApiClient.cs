using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TeaTracker.Web.Models;

namespace TeaTracker.Web.Services;

public class TeaApiClient(HttpClient http)
{

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public async Task<List<TeaResponse>> GetTeasAsync(TeaType? type = null, string? search = null)
    {
        List<string> query = [];

        if (type is not null)
        {
            query.Add($"type={type}");
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            query.Add($"search={Uri.EscapeDataString(search)}");
        }

        string url = "api/teas" + (query.Count > 0 ? "?" + string.Join("&", query) : "");

        return await http.GetFromJsonAsync<List<TeaResponse>>(url, JsonOptions) ?? [];
    }

    public async Task<TeaResponse?> GetTeaAsync(int id)
    {
        try
        {
            return await http.GetFromJsonAsync<TeaResponse>($"api/teas/{id}", JsonOptions);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<TeaResponse?> CreateTeaAsync(CreateTeaRequest request)
    {
        HttpResponseMessage response = await http.PostAsJsonAsync("api/teas", request, JsonOptions);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TeaResponse>(JsonOptions);
    }

    public async Task UpdateTeaAsync(int id, UpdateTeaRequest request)
    {
        HttpResponseMessage response = await http.PatchAsJsonAsync($"api/teas/{id}", request, JsonOptions);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTeaAsync(int id)
    {
        HttpResponseMessage response = await http.DeleteAsync($"api/teas/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<List<TastingResponse>> GetTastingsAsync(int teaId)
    {
        return await http.GetFromJsonAsync<List<TastingResponse>>($"api/teas/{teaId}/tastings", JsonOptions) ?? [];
    }

    public async Task<TastingResponse?> CreateTastingAsync(int teaId, CreateTastingRequest request)
    {
        HttpResponseMessage response = await http.PostAsJsonAsync($"api/teas/{teaId}/tastings", request, JsonOptions);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TastingResponse>(JsonOptions);
    }

    public async Task DeleteTastingAsync(int teaId, int tastingId)
    {
        HttpResponseMessage response = await http.DeleteAsync($"api/teas/{teaId}/tastings/{tastingId}");
        response.EnsureSuccessStatusCode();
    }
}
