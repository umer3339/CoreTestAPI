using ExcelDataReader;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Core.Entities.Common;
using STCAPI.Core.Entities.UserManagement;
using STCAPI.Core.ViewModel.ResponseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Controllers.UserManagement
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class PortalMenuMasterAPI : ControllerBase
    {
        private readonly IGenericRepository<PortalMenuMaster, int> _IPortalMenuRepository;
        private readonly IGenericRepository<PortalAccess, int> _IPortalAccessRepository;
        public PortalMenuMasterAPI(IGenericRepository<PortalMenuMaster, int> portalMenuReposiory, IGenericRepository<PortalAccess, int> portalAcessRepo)
        {
            _IPortalMenuRepository = portalMenuReposiory;
            _IPortalAccessRepository = portalAcessRepo;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePortalMenu([FromForm] PortalMenuMaster formFile)
        {
            var models = await GetPortalMenuList(formFile.PortalFile);
            var response = await _IPortalMenuRepository.CreateEntity(models.ToArray());
            return Ok(response.ResponseStatus);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAccess(string userName)
        {
            var portalAccessModels = await _IPortalMenuRepository.GetAllEntities(x => x.IsActive && !x.IsDeleted);
            var userAccessPortalModels = await _IPortalAccessRepository.
                GetAllEntities(x => x.IsActive && !x.IsDeleted && x.UserName == userName);

            portalAccessModels.TEntities.ToList().ForEach(data =>
            {
                userAccessPortalModels.TEntities.ToList().ForEach(item =>
                {
                    if (data.Id == item.PortalId)
                    {
                        data.Flag = true;
                    }
                });

            });

            var response = GetFormattedResponse(portalAccessModels, userName);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserRight(List<PortalAccess> portalAccesses)
        {
            portalAccesses.ForEach(data =>
            {
                data.CreatedBy = "Bhavesh Deepak";
                data.CreatedDate = DateTime.Now;
                data.IsActive = true;
                data.IsDeleted = false;
            });
            var response = await _IPortalAccessRepository.CreateEntity(portalAccesses.ToArray());
            return Ok(response.Message);

        }

        /// <summary>
        /// Code to get the formatted user rights as per requirement of UI developers
        /// </summary>
        /// <param name="portalAccessDetails"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private AdminPortalResponseModel GetFormattedResponse(ResponseModel<PortalMenuMaster, int> portalAccessDetails, string userName)
        {
            var model = new AdminPortalResponseModel();
            var formList = new List<Form>();
            var dashBoardList = new List<Dashboard>();
            var reportList = new List<STCAPI.Core.ViewModel.ResponseModel.Report>();
            var subStreamList = new List<SubStream>();
            var mainStreamList = new List<MainStream>();
            var stageList = new List<Stage>();


            foreach (var stageData in portalAccessDetails.TEntities.GroupBy(x => x.Stage))
            {
                var stageModel = new Stage();
                stageModel.stageName = stageData.Key;

                foreach (var mainStreamData in stageData.GroupBy(x => x.MainStream))
                {
                    var mainStreamModel = new MainStream();
                    mainStreamModel.streamName = mainStreamData.Key;


                    foreach (var subStreamData in mainStreamData.GroupBy(x => x.Stream))
                    {
                        var subStreamModel = new SubStream();
                        subStreamModel.subStreamName = subStreamData.Key;
                        subStreamList.Add(subStreamModel);

                        foreach (var objectData in subStreamData)
                        {

                            switch (objectData.ObjectName)
                            {
                                case "Form":
                                    var formModel = new Form();
                                    formModel.accessLevel = objectData.Flag;
                                    formModel.name = "Form";
                                    formList.Add(formModel);
                                    break;
                                case "Dashboard":
                                    var dashboard = new Dashboard();
                                    dashboard.accessLevel = objectData.Flag;
                                    dashboard.name = "Dashboard";
                                    dashBoardList.Add(dashboard);
                                    break;
                                case "Report":
                                    var reportModel = new STCAPI.Core.ViewModel.ResponseModel.Report();
                                    reportModel.accessLevel = objectData.Flag;
                                    reportModel.name = "Report";
                                    reportList.Add(reportModel);
                                    break;
                            }

                        }

                        subStreamModel.form = formList;
                        subStreamModel.dashboard = dashBoardList;
                        subStreamModel.report = reportList;
                    }
                    mainStreamModel.subStream = subStreamList;
                    mainStreamList.Add(mainStreamModel);
                }
                stageModel.mainStream = mainStreamList;
                stageList.Add(stageModel);

            }
            model.directory = "Basserah";
            model.userName =userName;
            model.stages = stageList;
            return model;
        }

        /// <summary>
        /// Get the User Access right based on user name detail
        /// </summary>
        /// <param name="inputFile"></param>
        /// <returns></returns>
        private async Task<List<PortalMenuMaster>> GetPortalMenuList(IFormFile inputFile)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            DataSet dsexcelRecords = new DataSet();
            IExcelDataReader reader = null;
            string message = string.Empty;
            Stream stream = inputFile.OpenReadStream();
            List<PortalMenuMaster> models = new List<PortalMenuMaster>();

            try
            {
                if (inputFile != null)
                {
                    if (inputFile.FileName.EndsWith(".xls"))
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    else if (inputFile.FileName.EndsWith(".xlsx"))
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    else
                        message = "The file format is not supported.";

                    dsexcelRecords = reader.AsDataSet();
                    reader.Close();

                    if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                    {
                        DataTable inputVatInvoiceDetail = dsexcelRecords.Tables[0];

                        for (int i = 2; i < inputVatInvoiceDetail.Rows.Count; i++)
                        {
                            var model = new PortalMenuMaster();
                            model.Stage = Convert.ToString(inputVatInvoiceDetail.Rows[i][0]);
                            model.MainStream = Convert.ToString(inputVatInvoiceDetail.Rows[i][1]);
                            model.StreamLongName = Convert.ToString(inputVatInvoiceDetail.Rows[i][2]);
                            model.Stream = Convert.ToString(inputVatInvoiceDetail.Rows[i][3]);
                            model.ObjectName = Convert.ToString(inputVatInvoiceDetail.Rows[i][4]);
                            model.Name = Convert.ToString(inputVatInvoiceDetail.Rows[i][5]);
                            model.Url = Convert.ToString(inputVatInvoiceDetail.Rows[i][6]);
                            model.Flag = false;
                            model.CreatedBy = "Bhavesh";
                            model.IsActive = true;
                            model.IsDeleted = false;
                            model.CreatedDate = DateTime.Now;

                            models.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }

            return await Task.Run(() => models);
        }
    }
}
