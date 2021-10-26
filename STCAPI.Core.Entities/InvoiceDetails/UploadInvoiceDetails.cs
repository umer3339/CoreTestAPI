using STCAPI.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Core.Entities.InvoiceDetails
{
    [Table("uploadinvoicedetails")]
    public class UploadInvoiceDetails : BaseModel<int>
    {
        public string InvoiceName { get; set; }
        public string Attachments { get; set; }
       
    }
}
