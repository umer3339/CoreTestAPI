using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.UserManagement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Stage : ControllerBase
    {
        private readonly IGenericRepository<StageModel, int> _IStageRepository;
        public Stage(IGenericRepository<StageModel, int> stageRepository)
        {
            _IStageRepository = stageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create(StageModel model)
        {
            StageModel[] models =new  StageModel[] { model };
            var response = await _IStageRepository.CreateEntity(models);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(StageModel model)
        {
            var response = await _IStageRepository.UpdateEntity(model);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetDetail()
        {
            var response = await _IStageRepository.GetAllEntities(x=>x.IsActive== true && x.IsDeleted== false);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteModel = (await _IStageRepository.GetAllEntities(x => x.Id == id)).TEntities.FirstOrDefault();
            var response = await _IStageRepository.DeleteEntity(deleteModel);
            return Ok(response);
        }
    }
}
