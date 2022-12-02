using ModelsDTO.Rentals;

namespace APIGateway;

public interface IRentalsRepository
{
    Task<PaginationRentalsDTO?> FindAllByUsername(string username);
    Task<RentalsDTO?> FindByUsernameAndUid(string username, Guid rentalUid);
}
