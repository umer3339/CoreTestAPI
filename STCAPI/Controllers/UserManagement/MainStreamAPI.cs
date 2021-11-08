using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.DataLayer.AdminPortal;
using STCAPI.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.AdminPortal
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainStreamAPI : ControllerBase
    {
        private readonly IGenericRepository<MainStreamMaster, int> _IMainsStreamRepository;
        private readonly IGenericRepository<StageMaster, int> _IStageMasterRepository;

        public MainStreamAPI(IGenericRepository<MainStreamMaster, int> mainStreamRepo, IGenericRepository<StageMaster, int> stageMasterRepo)
        {
            _IMainsStreamRepository = mainStreamRepo;
            _IStageMasterRepository = stageMasterRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateMainStream(MainStreamMaster model)
        {
            var response = await _IMainsStreamRepository.CreateEntity( new List<MainStreamMaster>() { model }.ToArray());

            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetMainStreamDetails()
        {
            var mainStreamModel = await _IMainsStreamRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);

            var stageModel = await _IStageMasterRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);

            return Ok(CommonServiceHelper.GetMainStreamDetail(mainStreamModel, stageModel));

        }

        [HttpDelete]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteMainStream(int id)
        {
            var deleteModel = await _IMainsStreamRepository.GetAllEntities(x => x.Id == id);

            deleteModel.TEntities.ToList().ForEach(data =>
            {
                data.IsActive = false;
                data.IsDeleted = true;
            });

            var deleteResponse = await _IMainsStreamRepository.DeleteEntity(deleteModel.TEntities.ToArray());

            return Ok(deleteResponse);
        }


        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateMainStream(MainStreamMaster model)
        {

            var deleteModel = await _IMainsStreamRepository.GetAllEntities(x => x.Id == model.Id);

            deleteModel.TEntities.ToList().ForEach(data =>
            {
                data.IsActive = false;
                data.IsDeleted = true;
            });

            var deleteResponse = await _IMainsStreamRepository.DeleteEntity(deleteModel.TEntities.ToArray());

            model.Id = 0;

            var createResponse = await _IMainsStreamRepository.CreateEntity(new List<MainStreamMaster>() { model}.ToArray());

            return Ok(createResponse);
        }
    }
}
