using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.DataLayer.AdminPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.AdminPortal
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RawDataStreamAPI : ControllerBase
    {
        private readonly IGenericRepository<RawDataStream, int> _IRawDataStreamRepository;
        public RawDataStreamAPI(IGenericRepository<RawDataStream, int> rawDataStreamRepo)
        {
            _IRawDataStreamRepository = rawDataStreamRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateRawDataStream(RawDataStream model)
        {
            var response = await _IRawDataStreamRepository.CreateEntity(new List<RawDataStream>() { model }.ToArray());
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetRawDataStream()
        {
            var response = await _IRawDataStreamRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);
            return Ok(response);
        }

        [HttpDelete]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteRawDataStream(int id)
        {
            var deleteModel = await _IRawDataStreamRepository.GetAllEntities(x => x.Id == id);
            deleteModel.TEntities.ToList().ForEach(data => {
                data.IsActive = false;
                data.IsDeleted = true;
            });

            var response = await _IRawDataStreamRepository.DeleteEntity(deleteModel.TEntities.ToArray());
            return Ok(response);
        }

    }
}
