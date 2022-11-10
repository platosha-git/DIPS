using Microsoft.AspNetCore.Mvc;
using Payments.WebControllers;
using Payments.ModelsDB;

namespace Payments.APIControllers
{
    [ApiController]
    [Route("/api/v1/payments")]
    public class ApiPaymentsController : ControllerBase
    {
        private readonly PaymentsController _paymentsController;

        public ApiPaymentsController(PaymentsController paymentsController)
        {
            _paymentsController = paymentsController;
        }

        /// <summary>Get all Payments</summary>
        /// <returns>Payments information</returns>
        /// <response code="200">All Payments</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Payment>))]
        public IActionResult GetAllPayments()
        {
            List<Payment> payments = _paymentsController.GetAllPayments();
            return Ok(payments);
        }
    }
}
