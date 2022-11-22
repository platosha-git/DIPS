using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ModelsDTO.Cars;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("/api/v1/cars")]
    public class SystemAPIController : ControllerBase
    {
        private readonly CarsWebController _carsController;

        public SystemAPIController(CarsWebController carsController)
        {
            _carsController = carsController;
        }

        /// <summary>Get all Cars</summary>
        /// <returns>Cars information</returns>
        /// <response code="200">All Cars</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationCarsDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCars([Range(1, 10)] int page, [Range(1, 10)] int size, bool showAll)
        {
            var response = await _carsController.GetAllCars(page, size, showAll);
            return Ok(response);
        }
    }
}