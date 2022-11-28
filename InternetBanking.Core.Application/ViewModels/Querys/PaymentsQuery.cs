using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Querys
{
    public class PaymentsQuery
    {
        public int TotalPayments { get; set; }
        public int PaymentsToday { get; set; }
        public string? OriginAccountNumber { get; set; }
        public string? DestinationAccountNumber { get; set; }
        public DateTime? Created { get; set; }
    }
}
