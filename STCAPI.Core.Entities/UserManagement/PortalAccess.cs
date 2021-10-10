using STCAPI.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Core.Entities.UserManagement
{
    [Table("PortalUserAccess")]
    public class PortalAccess: BaseModel<int>
    {
        public int PortalId { get; set; }
        public string UserName { get; set; }
    }
}
