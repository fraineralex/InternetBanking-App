using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class PersonalTransfersViewModel
    {

        public float? Amount { get; set; }
        public int OriginAccountId { get; set; }
        public string? OriginAccountNumber { get; set; }

        public int TargetAccountNumber { get; set; }

        public string? CreatedAt { get; set; }    

    }
}
