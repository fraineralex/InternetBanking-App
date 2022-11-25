using AutoMapper;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Identity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Services
{
    public class PaymentService : GenericService<SavePaymentViewModel, PaymentViewModel, Payment>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public PaymentService(IPaymentRepository paymentRepository, IProductService productService, IMapper mapper): base(paymentRepository, mapper)
        {
            _paymentRepository = paymentRepository;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task CashAdvance(SavePaymentViewModel vm)
        {
            ProductViewModel originCreditCard = await _productService.GetProductByNumberAccountForPayment(vm.OriginAccountNumber, vm.Amount);

            ProductViewModel accountDestination = await _productService.GetProductByNumberAccountForPayment(vm.DestinationAccountNumber);

            if (originCreditCard != null && accountDestination != null)
            {
                double amountWithInterest = vm.Amount + (vm.Amount * InterestRates.CreditCardRate);
                originCreditCard.Charge = (originCreditCard.Charge - vm.Amount);
                originCreditCard.Discharge += amountWithInterest;
                ProductSaveViewModel originCreditCardUpdated = _mapper.Map<ProductSaveViewModel>(originCreditCard);
                await _productService.Update(originCreditCardUpdated, originCreditCardUpdated.Id);

                accountDestination.Charge += vm.Amount;
                ProductSaveViewModel accountDestinationUpdated = _mapper.Map<ProductSaveViewModel>(accountDestination);
                await _productService.Update(accountDestinationUpdated, accountDestinationUpdated.Id);
            }
        }

        public async Task CreditCardPayment(SavePaymentViewModel vm)
        {
            ProductViewModel originSavingsAccount = await _productService.GetProductByNumberAccountForPayment(vm.OriginAccountNumber, vm.Amount);

            ProductViewModel creditCardDestination = await _productService.GetProductByNumberAccountForPayment(vm.DestinationAccountNumber);

            if (originSavingsAccount != null && creditCardDestination != null)
            {
                originSavingsAccount.Charge -= vm.Amount;
                originSavingsAccount.Discharge += vm.Amount;
                ProductSaveViewModel originSavingsAccountUpdated = _mapper.Map<ProductSaveViewModel>(originSavingsAccount);
                await _productService.Update(originSavingsAccountUpdated, originSavingsAccountUpdated.Id);

                double amountWithInterest = vm.Amount - (vm.Amount * InterestRates.CreditCardRate);
                creditCardDestination.Charge += amountWithInterest;
                creditCardDestination.Discharge -= vm.Amount;
                ProductSaveViewModel creditCardDestinationUpdated = _mapper.Map<ProductSaveViewModel>(creditCardDestination);
                await _productService.Update(creditCardDestinationUpdated, creditCardDestinationUpdated.Id);
            }

        }

        public async Task Payment(SavePaymentViewModel vm)
        {
            
            ProductViewModel originAccount = await _productService.GetProductByNumberAccountForPayment(vm.OriginAccountNumber, vm.Amount);

            ProductViewModel accountDestination = await _productService.GetProductByNumberAccountForPayment(vm.DestinationAccountNumber);

            if (originAccount != null && accountDestination != null)
            {
                if (accountDestination.TypeAccountId == (int)AccountTypes.SavingAccount)
                {
                    var disCharge = originAccount.Charge - vm.Amount;
                    originAccount.Charge = disCharge;
                    originAccount.Discharge += vm.Amount;
                    ProductSaveViewModel acToPayUpdated = _mapper.Map<ProductSaveViewModel>(originAccount);
                    await _productService.Update(acToPayUpdated, acToPayUpdated.Id);

                    accountDestination.Charge += vm.Amount;
                    ProductSaveViewModel acDsUpdated = _mapper.Map<ProductSaveViewModel>(accountDestination);
                    await _productService.Update(acDsUpdated, acDsUpdated.Id);
                }
                if 
                    (accountDestination.TypeAccountId == (int)AccountTypes.CreditAccount ||
                    accountDestination.TypeAccountId == (int)AccountTypes.LoanAccount)
                {
                    var disCharge = originAccount.Charge - vm.Amount;
                    originAccount.Charge = disCharge;
                    originAccount.Discharge += vm.Amount;
                    ProductSaveViewModel acToPayUpdated = _mapper.Map<ProductSaveViewModel>(originAccount);
                    await _productService.Update(acToPayUpdated, acToPayUpdated.Id);

                    accountDestination.Charge -= vm.Amount;
                    accountDestination.Discharge = vm.Amount;
                    ProductSaveViewModel acDsUpdated = _mapper.Map<ProductSaveViewModel>(accountDestination);
                    await _productService.Update(acDsUpdated, acDsUpdated.Id);
                }

                Payment payment = _mapper.Map<Payment>(vm);
                await _paymentRepository.AddAsync(payment);
                    
            }

            
        }

        public async Task<SavePaymentViewModel> TransferBetweenAccounts (SavePaymentViewModel vm)
        {
            if(vm.OriginAccountNumber == vm.DestinationAccountNumber)
            {
                vm.HasError = true;
                vm.Error = "You can not transfer to your own account, please select another account";
                return vm;
            }

            ProductViewModel originSavingAccount = await _productService.GetProductByNumberAccountForPayment(vm.OriginAccountNumber, vm.Amount);


            if (originSavingAccount.HasError)
            {
                vm.HasError = true;
                vm.Error = originSavingAccount.Error;
                return vm;
            }

            await Payment(vm);

            return vm;
        }

    }
}
