using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Core.Entities.Common
{
    public enum ResponseStatus
    {
        Created=201,
        Deleted,
        Updated,
        Success=200,
        AlreadyExists,
        Error=500
    }
}
