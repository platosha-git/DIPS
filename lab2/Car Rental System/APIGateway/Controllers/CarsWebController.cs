using ModelsDTO.Cars;

namespace APIGateway.Controllers;

public class CarsWebController
{
    private readonly ICarsRepository _carsRepository;

    public CarsWebController(ICarsRepository carsRepository)
    {
        _carsRepository = carsRepository;
    }

    public async Task<PaginationCarsDTO> GetAllCars(int page, int size, bool showAll)
    {
        return await _carsRepository.FindAll(page, size, showAll);
    }
}