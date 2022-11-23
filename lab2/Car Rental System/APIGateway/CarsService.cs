using ModelsDTO.Cars;

namespace APIGateway.Controllers;

public class CarsService
{
    private readonly ICarsRepository _carsRepository;

    public CarsService(ICarsRepository carsRepository)
    {
        _carsRepository = carsRepository;
    }

    public async Task<PaginationCarsDTO?> GetAllCars(int page, int size, bool showAll)
    {
        return await _carsRepository.FindAll(page, size, showAll);
    }
}