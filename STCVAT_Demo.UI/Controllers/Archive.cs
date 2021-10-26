using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.InvoiceDetails;

namespace STCVAT_Demo.UI.Controllers
{
    public class Archive : Controller
    {
        private readonly IGenericRepository<UploadInvoiceDetails, int> _IUploadInvoiceDetailsRepository;
        public Archive(IGenericRepository<UploadInvoiceDetails, int> IUploadInvoiceDetails)
        {
            _IUploadInvoiceDetailsRepository = IUploadInvoiceDetails;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _IUploadInvoiceDetailsRepository.GetAllEntities(x => x.IsDeleted == false);
            return View(data.TEntities);
        }

        public async Task<IActionResult> InputVATFilePartial()
        {
            return await Task.Run(() => PartialView("~/Views/Archive/_UploadInputFilePartial.cshtml"));
        }

        public async Task<IActionResult> OuputFilePartial()
        {
            return await Task.Run(() => PartialView("~/Views/Archive/_OutputVATFileUploadPartial.cshtml"));
        }

        public async Task<IActionResult> ReturnVATFilePartial()
        {
            return await Task.Run(() => PartialView("~/Views/Archive/_VATReturnDataFileUploadPartial.cshtml"));
        }

        public async Task<IActionResult> TrialBalanceFilePartial()
        {
            return await Task.Run(() => PartialView("~/Views/Archive/VATTrialBalancePartial.cshtml"));
        }
    }
}
