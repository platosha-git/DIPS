using Cars.ModelsDB;
using Cars.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace Cars.Repositories
{
    public class CarsRepository : ICarsRepository, IDisposable
    {
        private readonly CarsContext _db;
        private readonly ILogger<CarsRepository> _logger;

        public CarsRepository(CarsContext createDb, ILogger<CarsRepository> logDb)
        {
            _db = createDb;
            _logger = logDb;
        }

        public async Task<List<Car>> FindAll(int page, int size)
        {
            var cars = await _db.Cars
                .OrderBy(x => x.Price)
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            return cars;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}