using MailHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.STCVATRepository;
using STCAPI.Core.Entities.STCVAT;
using STCAPI.Core.ViewModel.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Controllers.STCVAT
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class STCVATPostValidationAPI : ControllerBase
    {
        private readonly ISTCPOstValidationRepository _iSTCPOstValidationRepository;
        private readonly INotificationService _InotificationService;
        public STCVATPostValidationAPI(ISTCPOstValidationRepository sTCPOstValidationRepository, INotificationService notificationService)
        {
            _iSTCPOstValidationRepository = sTCPOstValidationRepository;
            _InotificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateForm(STCPostValidationModel model)
        {
            List<STCPostValidation> models = new List<STCPostValidation>();
            foreach (var data in model.HeaderKey)
            {
                var formModel = new STCPostValidation()
                {
                    CreatedBy = model.UserName,
                    HeaderLineKey = data,
                    PostValidation = model.PostValidation,
                    CreatedDate= DateTime.Now,
                    EmailId= model.EmailId,
                    Comment= model.Comment
                };
                models.Add(formModel);
            }

            //Code to send the email  when the post validation type is email
            var response = await _iSTCPOstValidationRepository.CreateEntity(models.ToArray());
            if (model.IsEmailSend)
            {
                var emailIds=model.EmailId.Split(";").ToList();
                _ = await _InotificationService.SendMailNotification(emailIds, model.EmailTemplate,model.EmailSubject);
            }
            return Ok(response);
        }

       
    }
}
