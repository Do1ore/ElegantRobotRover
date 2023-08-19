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

    public void SendCurrentPosition(RoverLocationDto locationDto)
    {
        using StringContent jsonContent = new(
            JsonSerializer.Serialize(locationDto),
            Encoding.UTF8, "application/json");

        var response = _http.PostAsync(EndpointName, jsonContent).GetAwaiter().GetResult();
    }

    public RoverLocationDto GetLastPosition()
    {
        var response = _http.GetFromJsonAsync<RoverLocationDto>(EndpointName).GetAwaiter().GetResult();

        if (response is null)
        {
            throw new ApplicationException("No data");
        }

        return response;
    }
}