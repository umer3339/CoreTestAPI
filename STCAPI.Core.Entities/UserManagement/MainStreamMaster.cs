using STCAPI.Core.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace STCAPI.DataLayer.AdminPortal
{
    [Table("MainStreamMaster")]
    public class MainStreamMaster : BaseModel<int>
    {
        public int StageId { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}
