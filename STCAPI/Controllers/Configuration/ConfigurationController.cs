using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.Configuration;
using STCAPI.DataLayer.AdminPortal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.Configuration
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IGenericRepository<StageMaster, int> _IStageMasterRepository;
        private readonly IGenericRepository<StreamMaster, int> _IStreamMasterRepository;
        private readonly IGenericRepository<MainStreamMaster, int> _IMainStreamRepository;
       
        private readonly IGenericRepository<ConfigurationMaster, int> _IConfigurationMaster;

        public ConfigurationController(IGenericRepository<StageMaster, int> iStageMasterRepository,
            IGenericRepository<StreamMaster, int> iStreamMasterRepository,
            IGenericRepository<MainStreamMaster, int> iMainStreamRepository,
           
            IGenericRepository<ConfigurationMaster, int> iConfigurationMaster)
        {
            _IStageMasterRepository = iStageMasterRepository;
            _IStreamMasterRepository = iStreamMasterRepository;
            _IMainStreamRepository = iMainStreamRepository;
            _IConfigurationMaster = iConfigurationMaster;
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetStageDetail()
        {
            var response = await _IStageMasterRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetMainStreamDetail(int stageId)
        {
            var response = await _IMainStreamRepository.GetAllEntities(x => x.StageId == stageId);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetStreamDetail(int mainStreamId)
        {
            var response = await _IStreamMasterRepository.GetAllEntities(x => x.MainStreamId == mainStreamId);

            return Ok(response);
        }

        
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateConfiguration(ConfigurationMaster model)
        {
            var response = await _IConfigurationMaster.CreateEntity(new List<ConfigurationMaster>() { model }.ToArray());
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetConfigurationDetails(string configurationTypeId)
        {
            var response = await _IConfigurationMaster.GetAllEntities(x => x.ConfigurationType.Trim().ToUpper() 
            == configurationTypeId.Trim().ToUpper());

            return Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateConfiguration(ConfigurationMaster model)
        {
            var deleteModel = await _IConfigurationMaster.GetAllEntities(x => x.Id == model.Id);

            deleteModel.TEntities.ToList().ForEach(x =>
            {
                x.IsDeleted = true;
                x.IsActive = false;
            });

            var deleteResponse = await _IConfigurationMaster.DeleteEntity(deleteModel.TEntities.ToArray());

            model.Id = 0;

            var createResponse = await _IConfigurationMaster.CreateEntity(new List<ConfigurationMaster>() { model }.ToArray());

            return Ok(createResponse);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteConfiguration(int id)
        {
            var deleteModels = await _IConfigurationMaster.GetAllEntities(x => x.Id == id);

            deleteModels.TEntities.ToList().ForEach(x => {
                x.IsActive = false;
                x.IsDeleted= true;
            });

            var deleteResponse=await _IConfigurationMaster.DeleteEntity(deleteModels.TEntities.ToArray());

            return Ok(deleteResponse);
        }
    }
}
