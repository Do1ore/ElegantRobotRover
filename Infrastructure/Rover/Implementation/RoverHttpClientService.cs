using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Infrastructure.Rover.Abstractions;

namespace Infrastructure.Rover.Implementation;

public class RoverHttpClientService : IRoverHttpClientService
{
    private readonly HttpClient _http;
    private const string EndpointName = "RoverLocation";

    public RoverHttpClientService(HttpClient http)
    {
        _http = http;
    }

    public async Task SendCurrentPositionAsync(RoverLocationDto locationDto)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(locationDto),
            Encoding.UTF8, "application/json");

        var response = await _http.PostAsync(EndpointName, jsonContent);
    }

    public async Task<RoverLocationDto> GetLastPosition()
    {
        var response = await _http.GetFromJsonAsync<RoverLocationDto>(EndpointName);

        if (response is null)
        {
            throw new ApplicationException("No data");
        }

        return response;
    }
}