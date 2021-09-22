using STCAPI.Core.Entities.Common;
using STCAPI.Core.Entities.Reconcilation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STAAPI.Infrastructure.Repository.STCVATRepository
{
    public interface IReconcilationSummaryRepository
    {
        Task<ResponseModel<RecincilationSummary, int>> GetAllEntities(Func<RecincilationSummary, bool> where);
        Task<ResponseModel<RecincilationSummary, int>> CreateEntity(RecincilationSummary[] model);
    }
}
