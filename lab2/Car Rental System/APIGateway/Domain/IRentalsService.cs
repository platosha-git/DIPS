using APIGateway.ModelsDTO;
using ModelsDTO.Rentals;

namespace APIGateway.Domain;

public interface IRentalsService
{
    Task<List<RentalResponse>?> GetAllAsync(string username);
    Task<CreateRentalResponse> RentCar(string username, CreateRentalRequest request);
}