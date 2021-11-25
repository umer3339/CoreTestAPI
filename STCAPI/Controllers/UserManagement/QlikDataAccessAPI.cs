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
    public class QlikDataAccessAPI : ControllerBase
    {
        private readonly IGenericRepository<QlikDataAccess, int> _IQlickDataAccessRepository;

        public QlikDataAccessAPI(IGenericRepository<QlikDataAccess, int> qlickDataAccessRepo)
        {
            _IQlickDataAccessRepository = qlickDataAccessRepo;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> CreateDataAccess(QlikDataAccess model)
        {

            var filteredModel = await _IQlickDataAccessRepository.GetAllEntities(

                         x => x.StreamName.Trim().ToUpper() == model.StreamName.Trim().ToUpper()
                         && x.UserName.Trim().ToUpper() == model.UserName.Trim().ToUpper()
                         && x.AppName.Trim().ToUpper() == model.AppName.Trim().ToUpper()
                         && x.AccessLevel.Trim().ToUpper() == model.AccessLevel.Trim().ToUpper()
                         && x.DataGranularity.Trim().ToUpper() == model.DataGranularity.Trim().ToUpper()
                         && x.ActionName.Trim().ToUpper() == model.ActionName.Trim().ToUpper()

             );

            if (filteredModel.TEntities.Any())
            {
                filteredModel.TEntities.ToList().ForEach(data =>
                {
                    data.IsActive = false;
                    data.IsDeleted = true;
                });

                var deleteResponse = await _IQlickDataAccessRepository.DeleteEntity(filteredModel.TEntities.ToArray());
            }

            var response = await _IQlickDataAccessRepository.CreateEntity(new List<QlikDataAccess> (){ model }.ToArray());
            return Ok(response);
        }

        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> GetAccess()
        {
            var response = await _IQlickDataAccessRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);
            return Ok(response);
        }
    }
}
