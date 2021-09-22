using STCAPI.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Core.Entities.AdminPortal
{
    [Table ("PortalAccessDetail")]
    public class PortalAccess: BaseModel<int>
    {
        public string StreamName { get; set; }
        public string UserName { get; set; }
        public bool Dashboard { get; set; }
        public bool Form { get; set; }
        public bool Report { get; set; }
    }
}
