using CommonHelper;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using STCAPI.Core.ViewModel.RequestModel;
using STCAPI.Core.ViewModel.ResponseModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace STCAPI.Controllers.ExcelReader
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReturnVATFileValidate : ControllerBase
    {
        private readonly IHostingEnvironment _IHostingEnviroment;

        public ReturnVATFileValidate(IHostingEnvironment hostingEnvironment) 
        {
            _IHostingEnviroment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> ValidateReturnFile([FromForm] InvoiceDetail model)
        {
            var VATReturnModel = await Task.Run(() => VATReturnExcelData(model.InvoiceExcelFile));
            var errorResult = VATReturnValidationRule.ValidateVATReturn(VATReturnModel);
            return Ok(GetErrorDetails(errorResult));
        }

        private List<SubsidryErrorDetail> GetErrorDetails(IDictionary<int, (string, string)> error)
        {
            var models = new List<SubsidryErrorDetail>();
            foreach (var data in error)
            {
                var model = new SubsidryErrorDetail();
                model.PropertyName = data.Value.Item1;
                model.ErrorDetail = data.Value.Item2;

                models.Add(model);
            }
            return models;
        }

        private List<VATRetunDetailModel> VATReturnExcelData(IFormFile inputFile)
        {
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            DataSet dsexcelRecords = new DataSet();
            IExcelDataReader reader = null;
            string message = string.Empty;
            Stream stream = inputFile.OpenReadStream();
            List<VATRetunDetailModel> models = new List<VATRetunDetailModel>();

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
                            var model = new VATRetunDetailModel();
                            // model.Account = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column0"]);
                            // model.Description = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column1"]);
                            // model.BeginingBalance = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column3"]);
                            // model.Debit = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column4"]);
                            // model.Credit = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column5"]);
                            // model.Activity = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column6"]);
                            // model.EndingBalance = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column7"]);
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
