using STCAPI.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Core.Entities.UserManagement
{
    [Table("Stream")]
    public class StreamModel:BaseModel<int>
    {
        public int MainLevelId { get; set; }
        public string StreamName { get; set; }
    }
}
