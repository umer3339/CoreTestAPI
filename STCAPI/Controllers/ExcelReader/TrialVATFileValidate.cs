using CommonHelper;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STCAPI.Core.Entities.VATDetailUpload;
using STCAPI.Core.ViewModel.RequestModel;
using STCAPI.Core.ViewModel.ResponseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Controllers.ExcelReader
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrialVATFileValidate : ControllerBase
    {
        private readonly IHostingEnvironment _IHostingEnviroment;

        public TrialVATFileValidate(IHostingEnvironment hostingEnvironment)
        {
            _IHostingEnviroment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> TrialVATValidate([FromForm] InvoiceDetail model)
        {
            try
            {
                var balanceDataModel = await Task.Run(() => VATTrialBalance(model.InvoiceExcelFile));
                var errorResult = VATTrialBalanceValidationRule.ValidateVATTrialBalance(balanceDataModel);
                return Ok(GetErrorDetails(errorResult)); ;
            }
            catch (Exception ex)
            {
                return BadRequest("The Uploaded excel file do not support !!!");
            }
        }

        private List<SubsidryErrorDetail> GetErrorDetails(IDictionary<int, (string, string)> error)
        {
            var models = new List<SubsidryErrorDetail>();
            foreach (var data in error)
            {
                var model = new SubsidryErrorDetail();
                model.PropertyName = data.Value.Item1;
                model.ErrorDetail = data.Value.Item2;
                model.rowNumber = data.Key;
                models.Add(model);
            }
            return models;
        }


        private List<VATTrialBalanceModel> VATTrialBalance(IFormFile inputFile)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            DataSet dsexcelRecords = new DataSet();
            IExcelDataReader reader = null;
            string message = string.Empty;
            Stream stream = inputFile.OpenReadStream();
            List<VATTrialBalanceModel> models = new List<VATTrialBalanceModel>();

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

                        for (int i = 1; i < inputVatInvoiceDetail.Rows.Count; i++)
                        {
                            var model = new VATTrialBalanceModel();
                            model.Account = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column0"]);
                            model.Description = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column1"]);
                            model.BeginingBalance = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column3"]);
                            model.Debit = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column4"]);
                            model.Credit = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column5"]);
                            model.Activity = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column6"]);
                            model.EndingBalance = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column7"]);
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

            return models;
        }
    }
}
