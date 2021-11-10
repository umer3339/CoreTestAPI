using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.DataLayer.AdminPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace STCAPI.Controllers.AdminPortal
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StageMasterAPI : ControllerBase
    {
        private readonly IGenericRepository<StageMaster, int> _IStageMasterRepository;
        public StageMasterAPI(IGenericRepository<StageMaster, int> stageMasterRepo)
        {
            _IStageMasterRepository = stageMasterRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateStage(StageMaster model)
        {
            var response = await _IStageMasterRepository.CreateEntity(new List<StageMaster> (){ model}.ToArray());

            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetStageDetails()
        {
            var response = await _IStageMasterRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);

            return Ok(response);
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> UpdateStageDetails(StageMaster model)
        {
            var deleteModel = await _IStageMasterRepository.GetAllEntities(x => x.Id == model.Id);

            deleteModel.TEntities.ToList().ForEach(x =>
            {
                x.IsActive = false;
                x.IsDeleted = true;
            });

            var deleteResponse = await _IStageMasterRepository.DeleteEntity(deleteModel.TEntities.ToArray());

            model.Id = 0;

            var createResponse = await _IStageMasterRepository.CreateEntity(new List<StageMaster>() { model}.ToArray());

            return Ok(createResponse);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteStage(int id)
        {
            var deleteModel = await _IStageMasterRepository.GetAllEntities(x => x.Id == id);

            deleteModel.TEntities.ToList().ForEach(data =>
            {
                data.IsActive = false;
                data.IsDeleted = true;
            });

            var deleteResponse = await _IStageMasterRepository.DeleteEntity(deleteModel.TEntities.ToArray());

            return Ok(deleteResponse);
        }
    }
}
