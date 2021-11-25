using STCAPI.Core.ViewModel.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public static class VATReturnValidationRule
    {
        public static IDictionary<int, (string, string)> ValidateVATReturn(List<VATRetunDetailModel> models)
        {
            IDictionary<int, (string, string)> errorResult = new Dictionary<int, (string, string)>();
            int count = 0;
            foreach (var data in models)
            {
                count++;
                if (string.IsNullOrEmpty(data.VATType))
                    errorResult.Add(count++, ("VAT Type", $"VAT Type is mandatory"));

                if (string.IsNullOrEmpty(data.VATTypeId))
                    errorResult.Add(count++, ("VAT Type Id ", $"VAT Type Id is mandatory"));

                if (string.IsNullOrEmpty(data.VATTypeName))
                    errorResult.Add(count++, ("VAT Type Name", $"VAT Type Name is mandatory"));

                if (string.IsNullOrEmpty(data.SARAmount))
                    errorResult.Add(count++, ("SAR Amount", $"SAR Amount is mandatory"));


                if (string.IsNullOrEmpty(data.SARAdjustment))
                    errorResult.Add(count++, ("SAR Adjustment", $" SAR Adjustment is mandatory"));

                if (string.IsNullOrEmpty(data.SARVATAmount))
                    errorResult.Add(count++, ("SAR VAT Amount", $"SAR VAT Amount is mandatory"));

                if (string.IsNullOrEmpty(data.VATReturnDetail))
                    errorResult.Add(count++, ("VAT Return Detail", $"VAT Return Detail is mandatory"));

                
                if (string.IsNullOrEmpty(data.UploadInvoiceDetailId))
                    errorResult.Add(count++, ("Upload Invoice DetailId", $"Upload Invoice Detail Id is mandatory"));

                 
            }
            return errorResult;

        }
    }
}
