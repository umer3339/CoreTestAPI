using STCAPI.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Core.Entities.UserManagement
{
    [Table("DataSource")]
    public class DataSource: BaseModel<int>
    {
        public int StreamId { get; set; }
        public string DataSourceName { get; set; }
    }
}
