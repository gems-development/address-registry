namespace osm_client;

public class OverpassApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _overpassApiUrl;

    public OverpassApiClient(string overpassApiUrl)
    {
        _httpClient = new HttpClient();
        _overpassApiUrl = overpassApiUrl;
    }

    public async Task<string> ExecuteOverpassQueryAsync(string overpassQuery)
    {
        try
        {
            var content = new StringContent(overpassQuery);
            var response = await _httpClient.PostAsync(_overpassApiUrl, content);

            if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();

            throw new Exception($"Overpass API request failed with status code: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            throw new Exception("Overpass API request failed.", ex);
        }
    }
}