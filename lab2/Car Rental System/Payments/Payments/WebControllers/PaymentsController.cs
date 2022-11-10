using Payments.ModelsDB;
using Payments.Repositories;

namespace Payments.WebControllers
{
    public class PaymentsController
    {
        private readonly IPaymentsRepository _paymentsRepository;

        public PaymentsController(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public List<Payment> GetAllPayments()
        {
            return _paymentsRepository.FindAll();
        }
    }
}
