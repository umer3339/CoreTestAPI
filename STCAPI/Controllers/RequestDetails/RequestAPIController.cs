using CommonHelper;
using MailHelper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.RequestDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STCAPI.Controllers.RequestDetails
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class RequestAPIController : ControllerBase
    {
        private readonly IGenericRepository<RequestDetailModel, int> _IRequestRepository;
        private readonly IHostingEnvironment _IhostingEnviroment;
        private readonly INotificationService _INotificationService;
        public RequestAPIController(IGenericRepository<RequestDetailModel, int> _requestRepository,
            IHostingEnvironment hostingEnvironment, INotificationService notificationService)
        {
            _IRequestRepository = _requestRepository;
            _IhostingEnviroment = hostingEnvironment;
            _INotificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRequest([FromForm] RequestDetailModel model)
        {
            var documentPaths = await new BlobHelper().UploadDocument(model.AttachmentDetails, _IhostingEnviroment);
            model.Attachments = string.Join("♥", documentPaths);
            model.CreatedDate = DateTime.Now;
            model.IsActive = true;

            var response = await _IRequestRepository.CreateEntity(new List<RequestDetailModel>() { model }.ToArray());
            if (response.ResponseStatus == Core.Entities.Common.ResponseStatus.Error)
            {

                return BadRequest("Something wents wrong Please contact admin Team");
            }
            var attachmentModel = new EmailAttachmentDetails()
            {
                Attachments = model.AttachmentDetails,
                Category = model.Category,
                Priority = model.Priority,
                Description = model.Description,
                ToEmailIds= model.Emails

            };

            var emailResponse = await _INotificationService.SendMailWithAttachment(attachmentModel);
            return Ok("New request has been created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetNewRequestDetails()
        {
            var responseData = await _IRequestRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);
            if (responseData.ResponseStatus != Core.Entities.Common.ResponseStatus.Error)
            {
                return Ok(responseData.TEntities);
            }
            return BadRequest("Something wents wrong Please contact admin Team");
        }
    }
}
