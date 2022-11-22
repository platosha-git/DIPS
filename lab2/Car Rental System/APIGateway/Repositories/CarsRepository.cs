using AspNetCore.Http.Extensions;
using ModelsDTO.Cars;

namespace APIGateway;

public class CarsRepository : ICarsRepository
{
    private readonly HttpClient _httpClient;

    public CarsRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:8070");
    }

    public async Task<PaginationCarsDTO> FindAll(int page, int size, bool showAll)
    {
        var response = await _httpClient.GetAsync($"/api/v1/cars/?page={page}&size={size}&showAll={showAll}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsJsonAsync<PaginationCarsDTO>();
    }
}