using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Core.ViewModel.RequestModel
{
    public class STCVATFormModel
    {
        public List<string> HeaderKeyIds { get; set; }
        public string TaxClassificationCode { get; set; }
        public string TaxCode { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public string ReconcileApprove { get; set; }
        public string ImagePath { get; set; }
    }
}
