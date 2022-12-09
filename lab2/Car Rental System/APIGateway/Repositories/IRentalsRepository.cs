using APIGateway.ModelsDTO;
using ModelsDTO.Rentals;
using ModelsDTO.Cars;

namespace APIGateway;

public interface IRentalsRepository
{
    Task<List<RentalsDTO>?> GetAllAsyncByUsername(string username);
    Task<RentalsDTO> CreateAsync(RentalsDTO rentalDTO);
}
