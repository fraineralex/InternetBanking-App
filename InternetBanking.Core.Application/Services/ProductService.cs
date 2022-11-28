using AutoMapper;
using Dapper;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Products;
using InternetBanking.Core.Application.ViewModels.Querys;
using InternetBanking.Core.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace InternetBanking.Core.Application.Services
{
    public class ProductService : GenericService<ProductSaveViewModel, ProductViewModel, Product>, IProductService
    {
        private readonly AccountNumberGenerator _numberGenerator = new();
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper) : base(productRepository, mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<StatusClientQuery> GetClientStatus()
        {
            var ConnectionString = "Server=.;Database=InternetBankingApp;MultipleActiveResultSets=True;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            var query = @"SELECT COUNT(*) ClientsActives,
                          (SELECT COUNT(*)
                          FROM [InternetBankingApp].[Identity].[Users]
                          WHERE IsVerified = 0) ClientsInatives
                          FROM [InternetBankingApp].[Identity].[Users] 
                          WHERE IsVerified = 1";

            var clientStatus = await con.QueryFirstAsync<StatusClientQuery>(query);
            return clientStatus;
        }

        public async Task<TransacctionsQuery> GetTransacctions()
        {
            var ConnectionString = "Server=.;Database=InternetBankingApp;MultipleActiveResultSets=True;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            var query = @"SELECT COUNT(*) TotalTransacctions,
                            (SELECT COUNT(*) FROM [Payments] WHERE
                            Convert(varchar(10), Created,120) = Convert(varchar(10), GETDATE(),120)) TransacctionsToday
                            FROM [Payments]";

            var transacctions = await con.QueryFirstAsync<TransacctionsQuery>(query);
            return transacctions;
        }
        public async Task<ProductsQuery> GetClientProducts()
        {
            var ConnectionString = "Server=.;Database=InternetBankingApp;MultipleActiveResultSets=True;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            var query = @"SELECT COUNT(*) TotalClientProducts
                          FROM [InternetBankingApp].[dbo].[Products]";

            var clientProducts = await con.QueryFirstAsync<ProductsQuery>(query);
            return clientProducts;
        }
        public async Task<PaymentsQuery> GetPaymentQuantities()
        {
            var ConnectionString = "Server=.;Database=InternetBankingApp;MultipleActiveResultSets=True;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";
            using var con = new SqlConnection(ConnectionString);
            con.Open();

            var query = @"SELECT * ,
	                    (SELECT COUNT(*) FROM [Payments] WHERE
	                    Convert(varchar(10), Created,120) = Convert(varchar(10), GETDATE(),120)) PaymentsToday
                        FROM [Payments]";

            var paymentsResult = await con.QueryAsync<PaymentsQuery>(query);

            List<ProductViewModel> productViewModelsList = new();
            int TotalPaymentsQuantity = 0;
            int TodayPaymentsQuantity = 0;

            foreach (var payment in paymentsResult)
            {
                ProductViewModel OriginAccount = await GetProductByNumberAccountForPayment(payment.OriginAccountNumber);
                ProductViewModel DestinationAccount = await GetProductByNumberAccountForPayment(payment.DestinationAccountNumber);

                if (OriginAccount.ClientId != DestinationAccount.ClientId)
                {
                    TotalPaymentsQuantity += 1;

                    if (payment.Created.Value.Day > DateTime.Now.AddDays(-1).Day)
                    {
                        TodayPaymentsQuantity += 1;
                    }
                }
            }

            PaymentsQuery paymentsQuery = new()
            {
                PaymentsToday = TodayPaymentsQuantity,
                TotalPayments = TotalPaymentsQuantity
            };

            return paymentsQuery;
        }
        //public async Task<ProductsQuery> GetClientProducts()
        //{
        //    var cs = "Server=.;Database=InternetBankingApp;MultipleActiveResultSets=True;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";
        //    using var con = new SqlConnection(cs);
        //    con.Open();

        //    var query = @"SELECT COUNT(*) TotalClientProducts
        //                  FROM [InternetBankingApp].[dbo].[Products]";

        //    var clientProducts = await con.QueryFirstAsync<ProductsQuery>(query);
        //    return clientProducts;
        //}
        //public async Task<PaymentsQuery> GetPaymentQuantities()
        //{
        //    var cs = "Server=.;Database=InternetBankingApp;MultipleActiveResultSets=True;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true";
        //    using var con = new SqlConnection(cs);
        //    con.Open();

        //    var query = @"SELECT COUNT(*) TotalPayments,
        //                (SELECT COUNT(*) FROM [Payments] WHERE
        //                Convert(varchar(10), Created,120) = Convert(varchar(10), GETDATE(),120)) PaymentsToday
        //                FROM [Payments]
        //                WHERE [TypeOfPayment] = 1";

        //    var paymentsResult = await con.QueryFirstAsync<PaymentsQuery>(query);
        //    return paymentsResult;
        //}

        public async Task AddSavingAccountAsync(string idUser, double amount)
        {
            var products = await ExistSavingAccountByUser(idUser);

            if (products != false)
            {
                Product saveAccount = new();
                saveAccount.ClientId = idUser;
                saveAccount.Amount = amount;
                saveAccount.AccountNumber = _numberGenerator.NumberGenerator();
                saveAccount.TypeAccountId = (int)AccountTypes.SavingAccount;

                if (await ExistCodeNumber(saveAccount.AccountNumber))
                {
                   var newAccountNumber =  _numberGenerator.NumberGenerator();
                   saveAccount.AccountNumber = newAccountNumber;
                }

                await _productRepository.AddAsync(saveAccount);
            }
            else
            {
                Product saveAccount = new();
                saveAccount.AccountNumber = _numberGenerator.NumberGenerator();
                saveAccount.ClientId = idUser;
                saveAccount.Amount = amount;
                saveAccount.TypeAccountId = (int)AccountTypes.SavingAccount;
                saveAccount.IsPrincipal = true;

                if (await ExistCodeNumber(saveAccount.AccountNumber))
                {
                    var newAccountNumber = _numberGenerator.NumberGenerator();
                    saveAccount.AccountNumber = newAccountNumber;
                }

                await _productRepository.AddAsync(saveAccount);
            }
        }
        public async Task CreateAccountAsync(string idUser, double amount, int typeAccount)
        {
            if (typeAccount == (int)AccountTypes.CreditAccount)
            {
                Product saveAccount = new();
                saveAccount.ClientId = idUser;
                saveAccount.Amount = amount;
                saveAccount.AccountNumber = _numberGenerator.NumberGenerator();
                saveAccount.TypeAccountId = (int)AccountTypes.CreditAccount;

                if (await ExistCodeNumber(saveAccount.AccountNumber))
                {
                    var newAccountNumber = _numberGenerator.NumberGenerator();
                    saveAccount.AccountNumber = newAccountNumber;
                }

                await _productRepository.AddAsync(saveAccount);
            }
            else if (typeAccount == (int)AccountTypes.LoanAccount)
            {
                Product saveAccount = new();
                saveAccount.ClientId = idUser;
                saveAccount.Amount = amount;
                saveAccount.AccountNumber = _numberGenerator.NumberGenerator();
                saveAccount.TypeAccountId = (int)AccountTypes.LoanAccount;

                if (await ExistCodeNumber(saveAccount.AccountNumber))
                {
                    var newAccountNumber = _numberGenerator.NumberGenerator();
                    saveAccount.AccountNumber = newAccountNumber;
                }
                //sumandole lo del prestamo a la cuenta principal
                await AddAmountSavingAccount(idUser, amount);
                await _productRepository.AddAsync(saveAccount);
            }
        }
        public async Task AddAmountSavingAccount(string idUser, double amount)
        {
            List<Product> savingAccounts = await GetAllProductByUser(idUser, (int)AccountTypes.SavingAccount);
            Product sAPrincipal = savingAccounts.Where(sav => sav.IsPrincipal == true).SingleOrDefault();

            sAPrincipal.Amount += amount;

            await _productRepository.UpdateAsync(sAPrincipal, sAPrincipal.Id);
        }
        public async Task<List<ProductViewModel>> GetAllProductWithInclude(string idUser)
        {
            List<Product> products = await _productRepository.GetAllWithIncludeAsync(new List<string> { "TypeAccount" });
            products = products.Where(p => p.ClientId == idUser).ToList();
            List<ProductViewModel> productsVm = _mapper.Map<List<ProductViewModel>>(products);

            return productsVm;
        }
        public async Task<List<Product>> GetAllProductByUser(string idUser, int typeAccountId)
        {
            List<Product> products = await _productRepository.GetAllAsync();
            products = products.Where(p => p.ClientId == idUser && p.TypeAccountId == typeAccountId).ToList();

            return products;
        }

        public async Task<ProductViewModel> GetProductByNumberAccountForPayment(string numberAccount, double amountToPay = -1.0)
        {
            ProductViewModel response = new();
            response.HasError = false;

            List<Product> products = await _productRepository.GetAllAsync();
            var product = products.Where(ac => ac.AccountNumber == numberAccount)
                    .FirstOrDefault();

            if (product != null)
            {
                response = _mapper.Map<ProductViewModel>(product);
                
                if (response.Amount >= amountToPay)
                {
                    return response;
                }
                else
                {
                    response.HasError = true;
                    response.Error = "La cuenta no tiene el monto suficiente para realizar ese pago";
                }
            }
            else
            {
                response.HasError = true;
                response.Error = "No existe ese numero de cuenta en el sistema";
            }

            return response;
        }
        public async Task<ProductViewModel> DeleteProductAsync(int IdProduct)
        {
            Product product = await _productRepository.GetByIdAsync(IdProduct);

            ProductViewModel responseVm = _mapper.Map<ProductViewModel>(product);


            if (product.TypeAccountId == (int)AccountTypes.SavingAccount)
            {
                await AddAmountSavingAccount(product.ClientId, product.Amount);
            }
            if (product.TypeAccountId == (int)AccountTypes.CreditAccount)
            {
                if (product.Amount != 0)
                {
                    responseVm.HasError = true;
                    responseVm.Error = "No se puede eliminar esta cuenta de credito hasta que salde lo que debe.!";
                    return responseVm;
                }
            }
            if (product.TypeAccountId == (int)AccountTypes.LoanAccount)
            {
                if (product.Amount != 0)
                {
                    responseVm.HasError = true;
                    responseVm.Error = "No se puede eliminar este prestamo hasta que salde lo que debe.!";
                    return responseVm;
                }
            }

            await _productRepository.DeleteAsync(product);
            return responseVm;
        }
        public async Task<bool> ExistProduct(int IdProduct)
        {
            List<Product> products = await _productRepository.GetAllAsync();
            bool exist = products.Any(e => e.Id == IdProduct);
            return exist;
        }
        private async Task<bool> ExistSavingAccountByUser(string idUser)
        {
            var productList = await _productRepository.GetAllAsync();

            var listViewModels = productList.Where(e => e.ClientId == idUser && e.TypeAccountId == (int)AccountTypes.SavingAccount).Select(product => new ProductViewModel
            {
                Id = product.Id,

                ClientId = product.ClientId,
                TypeAccountId = product.TypeAccountId,
                Amount = product.Amount,

            }).ToList();

            if (idUser != null)
            {
                listViewModels = listViewModels.Where(product => product.ClientId == idUser).ToList();
            }
            if (listViewModels.Count() == 0)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> ExistCodeNumber(string accountNumber)
        {
            List<Product> products = await _productRepository.GetAllAsync();
            bool exist =  products.Any(e => e.AccountNumber == accountNumber);
            return exist;
        }
    }
}
