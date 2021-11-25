using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Core.ViewModel.RequestModel
{
    public class VATRetunDetailModel
    {
        public string VATType { get; set; }
        public decimal? VATTypeId { get; set; }
        public string VATTypeName { get; set; }
        public decimal? SARAmount { get; set; }
        public decimal? SARAdjustment { get; set; }
        public decimal? SARVATAmount { get; set; }
        public string VATReturnDetail { get; set; }
        public int? UploadInvoiceDetailId { get; set; }

    }
}
