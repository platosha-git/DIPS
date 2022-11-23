using Newtonsoft.Json;

namespace ModelsDTO.Cars;

public class PaginationCarsDTO
{
    [JsonProperty("page")]
    public int Page { get; set; }
    [JsonProperty("pageSize")]
    public int PageSize { get; set; }
    [JsonProperty("totalElements")]
    public int TotalElements { get; set; }
    [JsonProperty("cars")]
    public List<CarsDTO> Cars { get; set; }
}