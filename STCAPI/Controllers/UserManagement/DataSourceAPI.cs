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
    public class DataSourceAPI : ControllerBase
    {
        private readonly IGenericRepository<DataSource, int> _IDataSourceRepository;

        public DataSourceAPI(IGenericRepository<DataSource, int> dataSourceRepository)
        {
            _IDataSourceRepository = dataSourceRepository;
        }
    }
}
