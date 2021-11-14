using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.UserManagement;
using STCAPI.Core.ViewModel.ResponseModel;
using STCAPI.DataLayer.AdminPortal;
using STCAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.UserManagement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainStreamController : ControllerBase
    {
        private readonly IGenericRepository<MainStreamMaster, int> _IMainStreamRepository;
        private readonly IGenericRepository<StageMaster, int> _IStageMasterRepository;
        public MainStreamController(IGenericRepository<MainStreamMaster, int> mainStreamRepo,
            IGenericRepository<StageMaster, int> iStageMasterRepository)
        {
            _IMainStreamRepository = mainStreamRepo;
            _IStageMasterRepository = iStageMasterRepository;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> CreateMainStream(MainStreamMaster model)
        {
            model.IsDeleted = false;
            model.IsActive = true;
            MainStreamMaster[] dbModelArray = { model };
            var response = await _IMainStreamRepository.CreateEntity(dbModelArray);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> GetMainStreamDetails()
        {
            var mainStreamModel = await _IMainStreamRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);
            
            var stageModel= await _IStageMasterRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);

            var response= CommonServiceHelper.GetMainStreamDetail(mainStreamModel, stageModel);

            return Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> UpdateMainStreamDetails(MainStreamMaster model)
        {

            var deleteModel = await _IMainStreamRepository.GetAllEntities(x=>x.Id== model.Id);

            deleteModel.TEntities.ToList().ForEach(x => {
                x.IsActive = false;
                x.IsDeleted = true;
            
            });

            var deleteResponse = await _IMainStreamRepository.UpdateEntity(deleteModel.TEntities.First());

            model.Id = 0;

            MainStreamMaster[] dbModelArray = { model };

            var createResponse = await _IMainStreamRepository.CreateEntity(dbModelArray);

            return Ok(createResponse);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteMainStream(int id)
        {
            var response = await _IMainStreamRepository.GetAllEntities(x => x.Id == id);

            if (response.TEntities.Any())
            {
                response.TEntities.ToList().ForEach(item =>
                {
                    item.IsActive = false;
                    item.IsDeleted = true;
                });

                var deleteResponse = await _IMainStreamRepository.UpdateEntity(response.TEntities.First());

                return Ok(deleteResponse);
            }

            return BadRequest($"Invalid Report Id {id}");
        }
    }
}
