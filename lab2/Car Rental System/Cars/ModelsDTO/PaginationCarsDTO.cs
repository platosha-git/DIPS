using Newtonsoft.Json;

namespace Cars.ModelsDTO;

public class PaginationCarsDTO
{
    [JsonProperty("page")]
    public int Page { get; set; }
    
    [JsonProperty("pageSize")]
    public int PageSize { get; set; }
    
    [JsonProperty("totalElements")]
    public int TotalElements { get; set; }
    
    [JsonProperty("items")]
    public List<CarsDTO> Cars { get; set; }
}