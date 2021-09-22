using STCAPI.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Core.Entities.STCVAT
{
    [Table("stcvat_PostValidation")]
    public class STCPostValidation: BaseModel<int>
    {
        public string HeaderLineKey { get; set; }
        public string PostValidation { get; set; }
        public string EmailId { get; set; }
        public bool  IsEmailSend { get; set; }
        public string Comment { get; set; }
    }
}
