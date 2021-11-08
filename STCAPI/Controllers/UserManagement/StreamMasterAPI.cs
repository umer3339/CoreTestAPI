using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.DataLayer.AdminPortal;
using STCAPI.Helpers;
using STCAPI.ReqRespVm;
using STCAPI.ReqRespVm.AdminPortal;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace STCAPI.Controllers.AdminPortal
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StreamMasterAPI : ControllerBase
    {
        private readonly IGenericRepository<StreamMaster, int> _IStreamMasterRepository;
        private readonly IGenericRepository<MainStreamMaster, int> _IMainStreamRepository;
        public StreamMasterAPI(IGenericRepository<StreamMaster, int> streamMasterRepo, IGenericRepository<MainStreamMaster, int> mainStreamRepo)
        {
            _IMainStreamRepository = mainStreamRepo;
            _IStreamMasterRepository = streamMasterRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateStream(StreamMaster model)
        {
            var createResponse = await _IStreamMasterRepository.CreateEntity(new List<StreamMaster>() { model }.ToArray());

            return Ok(createResponse);

        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> GetStreamDetails()
        {
            var mainStreamData = await _IMainStreamRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);

            var streamData = await _IStreamMasterRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);

            List<StreamDetailVm> responseData = CommonServiceHelper.GetStreamDetails(mainStreamData, streamData);

            return Ok(responseData);


        }


        [HttpDelete]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteStream(int id)
        {
            var deleteModel = await _IStreamMasterRepository.GetAllEntities(x => x.Id == id);

            deleteModel.TEntities.ToList().ForEach(x =>
            {
                x.IsActive = false;
                x.IsDeleted = true;
            });

            var response = await _IStreamMasterRepository.DeleteEntity(deleteModel.TEntities.ToArray());

            return Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateStream(StreamMaster model)
        {
            var deleteModel = await _IStreamMasterRepository.GetAllEntities(x => x.Id == model.Id);

            deleteModel.TEntities.ToList().ForEach(data => {
                data.IsActive = false;
                data.IsDeleted = true;
            });

            var deleteResponse = await _IStreamMasterRepository.DeleteEntity(deleteModel.TEntities.ToArray());

            model.Id = 0;

            var createResponse = await _IStreamMasterRepository.CreateEntity(new List<StreamMaster>() { model}.ToArray());

            return Ok(createResponse);
        }
    }
}
