using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.AdminPortal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace STCAPI.Controllers.UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortalAccessAPI : ControllerBase
    {
        public readonly IGenericRepository<PortalAccess, int> _IPortalAccessRepository;

        public PortalAccessAPI(IGenericRepository<PortalAccess, int> portalAccessRepository)
        {
            _IPortalAccessRepository = portalAccessRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePortalAccess(List<PortalAccess> models)
        {
            var response = await _IPortalAccessRepository.CreateEntity(models.ToArray());
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPortalAccess()
        {
            var response = await _IPortalAccessRepository.GetAllEntities(x => x.IsActive == true && x.IsDeleted == false);
            return Ok(response.TEntities);

        }
    }
}
