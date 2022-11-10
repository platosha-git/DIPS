﻿using Rentals.ModelsDB;

namespace Rentals.Repositories
{
    public class RentalsRepository : IRentalsRepository, IDisposable
    {
        private readonly RentalContext _db;
        private readonly ILogger<RentalsRepository> _logger;

        public RentalsRepository(RentalContext createDb, ILogger<RentalsRepository> logDb)
        {
            _db = createDb;
            _logger = logDb;
        }

        public List<Rental> FindAll()
        {
            var cars = _db.Rentals.ToList();
            return cars;
        }
        

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}