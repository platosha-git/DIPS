using Newtonsoft.Json;

namespace ModelsDTO.Cars;

public class CarsDTO
{
    [JsonProperty("carUid")]
    public Guid CarUid { get; set; }
    
    [JsonProperty("brand")]
    public string Brand { get; set; }
    
    [JsonProperty("model")]
    public string Model { get; set; }
    
    [JsonProperty("registrationNumber")]
    public string RegistrationNumber { get; set; }
    
    [JsonProperty("Power")]
    public int? Power { get; set; }
    
    [JsonProperty("price")]
    public int Price { get; set; }
    
    [JsonProperty("type")]
    public string? Type { get; set; }
    
    [JsonProperty("availability")]
    public bool Availability { get; set; }
}