using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.Common;
using STCAPI.Core.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.UserManagement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainLevelAPI : ControllerBase
    {
        private readonly IGenericRepository<StageModel, int> _IStageRepository;
        private readonly IGenericRepository<MainLevel, int> _IMainLevelRepository;

        public MainLevelAPI(IGenericRepository<StageModel, int> stageRepository,
            IGenericRepository<MainLevel, int> mainLevelRepository)
        {
            _IStageRepository = stageRepository;
            _IMainLevelRepository = mainLevelRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(MainLevel model)
        {
            MainLevel[] models = new MainLevel[] { model };
            var response = await _IMainLevelRepository.CreateEntity(models);
            return GetResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(MainLevel model)
        {
            var response = await _IMainLevelRepository.UpdateEntity(model);
            return GetResult(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteModel = await _IMainLevelRepository.GetAllEntities(x => x.Id == id);
            var response = await _IMainLevelRepository.UpdateEntity(deleteModel.TEntities.First());
            return GetResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetDetails(MainLevel model)
        {
            var response = await _IMainLevelRepository.UpdateEntity(model);
            return GetResult(response);
        }

        [NonAction]
        public IActionResult GetResult(ResponseModel<MainLevel,int> result) {
            switch (result.ResponseStatus) {
                case ResponseStatus.Created:
                    return Ok(result);
                case ResponseStatus.Error:
                    return BadRequest(result);
                case ResponseStatus.Success:
                    return Ok(result);
                case ResponseStatus.Deleted:
                    return Ok(result);
                default:
                    return NoContent();
            }
        }
    }
}
