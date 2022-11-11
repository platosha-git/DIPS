using Microsoft.AspNetCore.Mvc;

namespace APIGateway
{
    [ApiController]
    [Route("/api/v1/APIGateway")]
    public class APIController : ControllerBase
    {
        //private readonly CarsWebController _carsController;
        private readonly HttpClient _httpClient;

        public APIController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        /// <summary>Get all Cars</summary>
        /// <returns>Cars information</returns>
        /// <response code="200">All Cars</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return await ProxyTo("http://localhost:8070/api/v1/cars");
        }

        private async Task<ContentResult> ProxyTo(string url)
        {
            return Content(await _httpClient.GetStringAsync(url));
        }
    }
}