using STCAPI.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Core.Entities.Master
{
    [Table("DemoTable")]
    public class DemoTable: BaseModel<int>
    {
        public string DemoName { get; set; }
    }
}
