using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Cars.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using AspNetCore.Http.Extensions;

namespace APIGateway.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("/api/v1/cars")]
    public class CarsAPIController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public CarsAPIController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:8080");
        }

        /// <summary>Get all Cars</summary>
        /// <returns>Cars information</returns>
        /// <response code="200">All Cars</response>
        
        /*[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationCarsDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCars([Range(1, 10)] int page, [Range(1, 10)] int size)
        {
            var response = await _httpClient.GetAsync($"/api/v1/cars/?page={page}&size={size}");
            var response2 = await response.Content.ReadAsJsonAsync<PaginationCarsDTO>();
            return Ok(response2);
        }
        */

        /*[HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationCarsDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCars([Range(1, int.MaxValue)] int page, [Range(1, 100)] int size)
        {
            var r1 = await ProxyTo("http://localhost:8070/api/v1/cars/?page={page}&size={size}");
            return Ok(r1);
        }

        private async Task<ContentResult> ProxyTo(string url)
        {
            return Content(await _httpClient.GetStringAsync(url));
        }
        */
    }
}