using System.ComponentModel.DataAnnotations;
using Cars.ModelsDB;
using Microsoft.AspNetCore.Mvc;
using Cars.ModelsDTO;

namespace Cars.Controllers
{
    [ApiController]
    [Route("/api/v1/cars")]
    public class CarsAPIController : ControllerBase
    {
        private readonly CarsWebController _carsController;

        public CarsAPIController(CarsWebController carsController)
        {
            _carsController = carsController;
        }

        private List<CarsDTO> ListCarsDTO(List<Car> lCars)
        {
            List<CarsDTO> lCarsDTO = new List<CarsDTO>();
            foreach (var car in lCars)
            {
                CarsDTO carDTO = new CarsDTO(car);
                lCarsDTO.Add(carDTO);
            }

            return lCarsDTO;
        }

        /// <summary>Get all Cars</summary>
        /// <param name="page"> Page number </param>
        /// <param name="size"> Number of elements per page </param>
        /// <returns>Cars information</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationCarsDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCars([Range(1, int.MaxValue)] int page, [Range(1, 100)] int size)
        {
            var cars = await _carsController.GetAllCars(page, size);

            var response = new PaginationCarsDTO()
            {
                PageSize = size,
                Page = page,
                TotalElements = cars.Count,
                Cars = ListCarsDTO(cars)
            };
            
            return Ok(response);
        }
    }
}