using STCAPI.Core.ViewModel.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public static class VATTrialBalanceValidationRule
    {
        public static IDictionary<int, (string, string)> ValidateVATTrialBalance(List<VATTrialBalanceModel> models)
        {
            IDictionary<int, (string, string)> errorResult = new Dictionary<int, (string, string)>();
            int count = 0;
            foreach (var data in models)
            {
                count++;

                if (string.IsNullOrEmpty(data.Account))
                    errorResult.Add(count++, ("Account", $"Account is mandatory"));
                if (string.IsNullOrEmpty(data.Description))
                    errorResult.Add(count++, ("Description", $"Description is mandatory"));
                if (string.IsNullOrEmpty(data.BeginingBalance))
                    errorResult.Add(count++, ("BeginingBalance", $"BeginingBalance is mandatory"));
                if (string.IsNullOrEmpty(data.Debit))
                    errorResult.Add(count++, ("Debit", $"Debit is mandatory"));
                if (string.IsNullOrEmpty(data.Credit))
                    errorResult.Add(count++, ("Credit", $"Credit is mandatory"));
                if (string.IsNullOrEmpty(data.Activity))
                    errorResult.Add(count++, ("Activity", $"Activity is mandatory"));
                if (string.IsNullOrEmpty(data.EndingBalance))
                    errorResult.Add(count++, ("EndingBalance", $"EndingBalance is mandatory"));
            }
            return errorResult;

        }
    }
}
