using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.STCVAT;
using STCAPI.Core.ViewModel.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.STCVAT
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class STCVATFormAPI : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IGenericRepository<STCVATForm, int> _ISTCVATFormRepository;

        public STCVATFormAPI(IMapper _mapper, IGenericRepository<STCVATForm, int> STCVARFormRepository)
        {
            _IMapper = _mapper;
            _ISTCVATFormRepository = STCVARFormRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateForm(STCVATFormModel model)
        {
            List<STCVATForm> models = new List<STCVATForm>();
            foreach (var data in model.HeaderKeyIds)
            {
                var formModel = new STCVATForm()
                {
                    CreatedBy = 1,
                    HeaderLineKey = data,
                    ImagePath = model.ImagePath,
                    ReconcileApprove = model.ReconcileApprove,
                    SupplierInvoiceNumber = model.SupplierInvoiceNumber,
                    TaxClassificationCode = model.TaxClassificationCode,
                    TaxCode = model.TaxCode
                };
                models.Add(formModel);
            }

            var response = await _ISTCVATFormRepository.CreateEntity(models.ToArray());
            return Ok(response);
        }
    }
}
