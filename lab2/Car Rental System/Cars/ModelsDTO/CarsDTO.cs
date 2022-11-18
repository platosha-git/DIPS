using System.Text.Json.Serialization;
using Cars.ModelsDB;
using Newtonsoft.Json;

namespace Cars.ModelsDTO;

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
    
    [JsonProperty("power")]
    public int? Power { get; set; }
    
    [JsonProperty("price")]
    public int Price { get; set; }
    
    [JsonProperty("type")]
    public string? Type { get; set; }
    
    [JsonProperty("availability")]
    public bool Availability { get; set; }

    public CarsDTO(Car car)
    {
        CarUid = car.CarUid;
        Brand = car.Brand;
        Model = car.Model;
        RegistrationNumber = car.RegistrationNumber;
        Power = car.Power;
        Price = car.Price;
        Type = car.Type;
        Availability = car.Availability;
    }
}