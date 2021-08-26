using AutoMapper;
using STCAPI.Core.Entities.Master;
using STCAPI.Core.ViewModel.RequestModel;

namespace STCAPI.Helper
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<DemoModel, DemoTable>();
        }
    }
}
