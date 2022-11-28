using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IPaymentService : IGenericService<SavePaymentViewModel, PaymentViewModel, Payment>
    {
        Task Payment(SavePaymentViewModel vm);
        Task CashAdvance(SavePaymentViewModel vm);
        Task CreditCardPayment(SavePaymentViewModel vm);
        Task<SavePaymentViewModel> TransferBetweenAccounts(SavePaymentViewModel vm);
    }
}
