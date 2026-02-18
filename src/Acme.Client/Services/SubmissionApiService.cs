using Acme.Shared.DTOs;
using Acme.Web.Interfaces;

namespace Acme.Web.Services;

public class SubmissionApiService : ISubmissionApiService
{
    private readonly string _apiUrl = "api/submissions";
    private HttpClient _client;
    
    public SubmissionApiService(HttpClient client)
    {
        _client = client;
    }
    public async Task SubmitAsync(SubmissionDto dto)
    {
        var response = await _client.PostAsJsonAsync(_apiUrl, dto);
        
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Error: {response.StatusCode}");
            Console.WriteLine($"Error Details: {errorContent}");
            throw new HttpRequestException($"API returned {response.StatusCode}: {errorContent}");
        }
    }

    public async Task<List<SubmissionDto>> GetAllAsync()
    {
     
        var fullUrl = new Uri(_client.BaseAddress!, _apiUrl);
        Console.WriteLine($"Calling API: {fullUrl}");
        
        
        var result = await _client.GetFromJsonAsync<List<SubmissionDto>>(_apiUrl);
        return result ?? new List<SubmissionDto>();
    }
}