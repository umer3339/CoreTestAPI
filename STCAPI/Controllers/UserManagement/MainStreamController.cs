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
    public class MainStreamController : ControllerBase
    {
        private readonly IGenericRepository<MainStreamModel, int> _IMainStreamRepository;
        public MainStreamController(IGenericRepository<MainStreamModel, int> mainStreamRepo)
        {
            _IMainStreamRepository = mainStreamRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> CreateMainStream(MainStreamModel model)
        {
            model.IsDeleted = false;
            model.IsActive = true;
            MainStreamModel[] dbModelArray = { model };
            var response = await _IMainStreamRepository.CreateEntity(dbModelArray);
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> GetMainStreamDetails()
        {
            var response = await _IMainStreamRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);

            return Ok(response);
        }

        [HttpPut]
        [Produces("application/json")]
        [Consumes("application/json")]

        public async Task<IActionResult> UpdateMainStreamDetails(MainStreamModel model)
        {

            var deleteModel = new MainStreamModel()
            {
                Id = model.Id,
                IsActive = false,
                IsDeleted = true
            };

            var deleteResponse = await _IMainStreamRepository.UpdateEntity(deleteModel);

            model.Id = 0;

            MainStreamModel[] dbModelArray = { model };

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
