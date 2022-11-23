using AspNetCore.Http.Extensions;
using ModelsDTO.Cars;

namespace APIGateway;

public class CarsRepository : ICarsRepository
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CarsRepository> _logger;

    public CarsRepository(HttpClient httpClient, ILogger<CarsRepository> logger)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:8070");
        _logger = logger;
    }

    public async Task<PaginationCarsDTO?> FindAll(int page, int size, bool showAll)
    {
        var response = await _httpClient.GetAsync($"http://localhost:8070/api/v1/cars/?page={page}&size={size}&showAll={showAll}");
        if (!response.IsSuccessStatusCode)
            _logger.LogWarning("+Failed FindAll Cars: {statusCode}, {descriprion}", response.StatusCode, response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PaginationCarsDTO>();
    }
}