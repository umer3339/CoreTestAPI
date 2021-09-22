using AutoMapper;
using CommonHelper;
using MailHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.Common;
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
        private readonly ISTACVATFormRepository _ISTCVATFormRepository;
        private readonly IHostingEnvironment _IhostingEnviroment;
        private readonly INotificationService _InotificationService;

        public STCVATFormAPI(IMapper _mapper, ISTACVATFormRepository STCVARFormRepository,
            IHostingEnvironment hostingEnvironment, INotificationService notificationService)
        {
            _IMapper = _mapper;
            _ISTCVATFormRepository = STCVARFormRepository;
            _IhostingEnviroment = hostingEnvironment;
            _InotificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateForm(STCVATFormModel model)
        {
            List<STCVATForm> models = new List<STCVATForm>();
            foreach (var data in model.HeaderKeyIds)
            {
                var formModel = new STCVATForm()
                {
                    CreatedBy = model.UserName,
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

        [HttpPost]
        public async Task<IActionResult> CreateFormWithAttachment([FromForm] STCVATFormModel model)
        {
            var emailResponse = await SendEmailNotification(model);
            model.IsEmaiSend = emailResponse;
            var response = await InsertAdjustmentFormData(model);
            return Ok(response);
        }

        private async Task<bool> SendEmailNotification(STCVATFormModel model)
        {
            var emailResponse = await _InotificationService
                        .SendMailNotification(model.EmailTo, model.EmailTemplate, model.EmailSubject);
            return emailResponse;
        }

        private async Task<ResponseModel<STCVATForm,int>> InsertAdjustmentFormData(STCVATFormModel model)
        {
            List<STCVATForm> models = new List<STCVATForm>();

            var documentPaths = await new BlobHelper().UploadDocument(model.Images, _IhostingEnviroment);

            foreach (var data in model.HeaderKeyIds)
            {
                var formModel = new STCVATForm()
                {
                    CreatedBy = model.UserName,
                    HeaderLineKey = data,
                    ImagePath = string.Join("♥", documentPaths),
                    ReconcileApprove = model.ReconcileApprove,
                    SupplierInvoiceNumber = model.SupplierInvoiceNumber,
                    TaxClassificationCode = model.TaxClassificationCode,
                    TaxCode = model.TaxCode,
                    EmailTo = string.Join(";", model.EmailTo),
                    Comments= model.Comments,
                    IsEmailSend=model.IsEmaiSend
                };
                models.Add(formModel);
            }
            return await _ISTCVATFormRepository.CreateEntity(models.ToArray());
        }

        [HttpGet]
        public async Task<IActionResult> GetDetailWithAttachment()
        {
            string url = HttpContext.Request.Host.Value;

            var response = await _ISTCVATFormRepository.GetAllEntities(x => x.IsActive == true && x.IsDeleted == false);
            foreach (var data in response.TEntities)
            {
                List<string> ImagesPaths = new List<string>();
                foreach (var item in data.ImagePath.Split("♥"))
                {
                    ImagesPaths.Add($"http://{url}/{item}");
                }
                data.ImagesUrl = ImagesPaths;
            }
            return Ok(response);
        }
    }
}
