﻿using CommonHelper;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using STAAPI.Infrastructure.Repository.GenericRepository;
using STCAPI.Controllers.ExcelReader;
using STCAPI.Core.Entities.Enums;
using STCAPI.Core.Entities.VATDetailUpload;
using STCAPI.Core.ViewModel.RequestModel;
using STCVAT_Demo.UI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace STCVAT_Demo.UI.Controllers
{
    //public class HomeController : Controller
    //{
    //    private readonly IHostingEnvironment _IHostingEnviroment;
    //    private readonly IGenericRepository<InputVATDataFile, int> _IInputVatDataFileRepository;
    //    private readonly IGenericRepository<VATTrailBalanceModel, int> _IVatTrialBalanceModelRepository;
    //    private readonly IGenericRepository<STCVATOutputModel, int> _ISTCVATOutputModelRepository;
    //    private readonly IGenericRepository<VATReturnModel, int> _IVATReturnModelRepository;
    //    private readonly IGenericRepository<UploadInvoiceDetail, int> _IUploadInvoiceRepository;
    //    private readonly ILogger<ReadExcelData> _logger;

    //    public HomeController(IHostingEnvironment hostingEnvironment,
    //       IGenericRepository<InputVATDataFile, int> inputVATDatFileRepository,
    //       IGenericRepository<VATTrailBalanceModel, int> vatTrialBalanceRepository,
    //       IGenericRepository<STCVATOutputModel, int> stcVatOutModelRepo,
    //       IGenericRepository<VATReturnModel, int> vatReturnModelRepository,
    //       IGenericRepository<UploadInvoiceDetail, int> uploadInvoiceRepo, ILogger<ReadExcelData> logger)
    //    {
    //        _IHostingEnviroment = hostingEnvironment;
    //        _IInputVatDataFileRepository = inputVATDatFileRepository;
    //        _IVatTrialBalanceModelRepository = vatTrialBalanceRepository;
    //        _ISTCVATOutputModelRepository = stcVatOutModelRepo;
    //        _IVATReturnModelRepository = vatReturnModelRepository;
    //        _IUploadInvoiceRepository = uploadInvoiceRepo;
    //        _logger = logger;

    //    }

    //    public IActionResult Index()
    //    {
    //        return View();
    //    }
    //    [HttpPost]
    //    public async Task<IActionResult> UploadFile(IFormFile inputVATDatafile,
    //        IFormFile outputVATDatafile, IFormFile VATReturnDataFile, IFormFile VATTrailBalanceDataFile, string validateSubmit, string finalSubmit)
    //    {
    //        IDictionary<int, (string, string)> errorResult = new Dictionary<int, (string, string)>();
    //        switch (validateSubmit)
    //        {
    //            case "Input":
    //                var inputVATModel = await Task.Run(() => InputVATExcelData(inputVATDatafile));
    //                errorResult = InputVATvalidationRule.ValidateInputVatData(inputVATModel);
    //                break;
    //            case "btnoutputVATDatafile":
    //                var OutputVATModel = await Task.Run(() => OutputVATExcelFle(outputVATDatafile));
    //                errorResult = OutputVATValidationRule.ValidateOutputVatData(OutputVATModel);
    //                break;
    //            case "btnVATReturnDatafile":
    //                var VATReturnModel = await Task.Run(() => VATReturnExcelData(VATReturnDataFile));
    //                errorResult = VATReturnValidationRule.ValidateVATReturn(VATReturnModel);
    //                break;
    //            case "btnVATTrailBalanceDataFile":
    //                var OutputVATTrailModel = await Task.Run(() => VATTrialBalance(VATTrailBalanceDataFile));
    //                IDictionary<int, (int,string, string)> trialBalanceError = VATTrialBalanceValidationRule.ValidateVATTrialBalance(OutputVATTrailModel);
    //                break;

    //        }
    //        return PartialView("~/Views/Home/GetErrorlist.cshtml", errorResult);
    //    }


    //    public async Task<IActionResult> GetDetails(string fileName)
    //    {
    //        switch (fileName)
    //        {
    //            case "input":
    //                ViewBag.PageName = "Input VAT File Details.";
    //                break;
    //            case "output":
    //                ViewBag.PageName = "Output VAT File Details.";
    //                break;
    //            case "return":
    //                ViewBag.PageName = "Return VAT File Details.";
    //                break;
    //            case "trial":
    //                ViewBag.PageName = "Trial Balance VAT File Details.";
    //                break;
    //            default:
    //                ViewBag.PageName = "Input VAT File Details.";
    //                break;
    //        }
    //        var data = await _IInputVatDataFileRepository.GetAllEntities(x => x.IsDeleted == true);
    //        return View(data.TEntities);
    //    }
    //    public IActionResult Privacy()
    //    {
    //        return View();
    //    }

    //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //    }

    //    #region PrivateMethod
    //    [NonAction]
    //    public async Task UploadData(InvoiceDetail model)
    //    {

    //        try
    //        {
    //            IDictionary<int, (string, string)> errorResult = new Dictionary<int, (string, string)>();
    //            var attachmentList = await new BlobHelper().UploadDocument(model.AttachmentList, _IHostingEnviroment);
    //            var invoiceFiles = new List<IFormFile>() { model.InvoiceExcelFile };

    //            _logger.LogInformation("File attachment has been done", DateTime.Now);


    //            var uploadFileDetails = await new BlobHelper().UploadDocument(invoiceFiles, _IHostingEnviroment);

    //            var uploadInvoiceId = await CreateUploadInvocie(model, attachmentList);

    //            switch (model.ExcelType)
    //            {
    //                case VATExcelType.InputDataFile:
    //                    var inputVATModel = await Task.Run(() => InputVATExcelData(model.InvoiceExcelFile));
    //                    errorResult = InputVATvalidationRule.ValidateInputVatData(inputVATModel);
    //                    var dbModels = await ConvertInputModelToDBModel(inputVATModel);
    //                    dbModels.ForEach(data =>
    //                    {
    //                        data.UploadInvoiceDetailId = uploadInvoiceId;
    //                    });
    //                    var inputDataFileResponse = await CreateInputVATDetail(dbModels, model.UserName);
    //                    break;

    //                case VATExcelType.OutputDataFile:
    //                    var outputVATModel = await Task.Run(() => OutputVATExcelFle(model.InvoiceExcelFile));
    //                    var dtoModel = await DTOOutModelToOutputDataModel(outputVATModel);
    //                    dtoModel.ForEach(data =>
    //                    {
    //                        data.UploadInvoiceDetailId = uploadInvoiceId;
    //                    });

    //                    var dbResponse = await CreateSTCOutputModel(dtoModel, model.UserName);
    //                    break;

    //                case VATExcelType.VATReturnDataFile:
    //                    var returnModel = await GetVATReturnDetail(model.InvoiceExcelFile);
    //                    var dbReturnModels = await DTOConvertModelTODBObject(returnModel);
    //                    dbReturnModels.Item2.ForEach(data =>
    //                    {
    //                        data.UploadInvoiceDetailId = uploadInvoiceId;
    //                    });

    //                    var dbReturnResponse = await CreateVATReturnData(dbReturnModels.Item2, model.UserName);
    //                    break;

    //                case VATExcelType.VATTrialBalanceDataFile:
    //                    var balanceDataModel = await Task.Run(() => VATTrialBalance(model.InvoiceExcelFile));
    //                    var trialDBModels = await ConvertVATTrialBalanceModelToDBModel(balanceDataModel);
    //                    trialDBModels.ForEach(data =>
    //                    {
    //                        data.UploadInvoiceDetailId = uploadInvoiceId;
    //                    });
    //                    var trialDataResponse = await CreateTrialBalance(trialDBModels, model.UserName);
    //                    break;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            string exception = ex.Message;
    //        }
    //    }
    //    private async Task<int> CreateUploadInvocie(InvoiceDetail model, List<string> attchementList)
    //    {
    //        var invoiceModel = new UploadInvoiceDetail()
    //        {
    //            Attachments = string.Join(";", attchementList),
    //            InvoiceName = model.InvoiceName,
    //            CreatedBy = model.UserName,
    //            CreatedDate = DateTime.Now,
    //            IsActive = true,
    //            IsDeleted = false
    //        };


    //        var response = await _IUploadInvoiceRepository.CreateEntity(new List<UploadInvoiceDetail>() { invoiceModel }.ToArray());
    //        return await Task.Run(() => 0);
    //    }

    //    private List<InputVATFileVm> InputVATExcelData(IFormFile inputFile)
    //    {
    //        Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    //        DataSet dsexcelRecords = new DataSet();
    //        IExcelDataReader reader = null;
    //        string message = string.Empty;
    //        Stream stream = inputFile.OpenReadStream();
    //        List<InputVATFileVm> models = new List<InputVATFileVm>();

    //        try
    //        {
    //            if (inputFile != null)
    //            {
    //                if (inputFile.FileName.EndsWith(".xls"))
    //                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
    //                else if (inputFile.FileName.EndsWith(".xlsx"))
    //                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
    //                else
    //                    message = "The file format is not supported.";

    //                dsexcelRecords = reader.AsDataSet();
    //                reader.Close();

    //                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
    //                {
    //                    DataTable inputVatInvoiceDetail = dsexcelRecords.Tables[0];

    //                    for (int i = 2; i < inputVatInvoiceDetail.Rows.Count; i++)
    //                    {
    //                        var model = new InputVATFileVm();
    //                        model.InvoiceType = Convert.ToString(inputVatInvoiceDetail.Rows[i][0]);
    //                        model.InvoiceSource = Convert.ToString(inputVatInvoiceDetail.Rows[i][1]);
    //                        model.InvoiceNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][2]);
    //                        model.InvoiceDocNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][3]);
    //                        model.InvoiceDate = Convert.ToString(inputVatInvoiceDetail.Rows[i][4]);
    //                        model.GLDate = Convert.ToString(inputVatInvoiceDetail.Rows[i][5]);
    //                        model.TotalInvoiceAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][6]);
    //                        model.InvoiceCurrency = Convert.ToString(inputVatInvoiceDetail.Rows[i][7]);
    //                        model.CurrencyExchangeRate = Convert.ToString(inputVatInvoiceDetail.Rows[i][8]);
    //                        model.SARInvoiceAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][9]);
    //                        model.SuppierNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][10]);
    //                        model.SupplierName = Convert.ToString(inputVatInvoiceDetail.Rows[i][11]);
    //                        model.SupplierSite = Convert.ToString(inputVatInvoiceDetail.Rows[i][12]);
    //                        model.SupplierAddress = Convert.ToString(inputVatInvoiceDetail.Rows[i][13]);
    //                        model.SupplierCountry = Convert.ToString(inputVatInvoiceDetail.Rows[i][14]);
    //                        model.SupplierBankAccount = Convert.ToString(inputVatInvoiceDetail.Rows[i][15]);
    //                        model.SupplierVATRegistrationNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][16]);
    //                        model.SupplierVATGroupRegistrationNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][17]);
    //                        model.InvoiceStatus = Convert.ToString(inputVatInvoiceDetail.Rows[i][18]);
    //                        model.PaymentStatus = Convert.ToString(inputVatInvoiceDetail.Rows[i][19]);

    //                        model.PaymentAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][20]);
    //                        model.PaymentMethod = Convert.ToString(inputVatInvoiceDetail.Rows[i][21]);
    //                        model.PaymentTerm = Convert.ToString(inputVatInvoiceDetail.Rows[i][22]);
    //                        model.InvoiceLineNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][23]);
    //                        model.InvoiceLineDescription = Convert.ToString(inputVatInvoiceDetail.Rows[i][24]);
    //                        model.PONumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][25]);
    //                        model.PoDate = Convert.ToString(inputVatInvoiceDetail.Rows[i][26]);
    //                        model.ReleaseDate = Convert.ToString(inputVatInvoiceDetail.Rows[i][27] ?? DateTime.Now.AddYears(100));
    //                        model.ReceiptNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][28]);
    //                        model.PoItemNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][29]);
    //                        model.PoItemDescription = Convert.ToString(inputVatInvoiceDetail.Rows[i][30]);
    //                        model.InvoiceSource = Convert.ToString(inputVatInvoiceDetail.Rows[i][31]);
    //                        model.Quantity = Convert.ToString(inputVatInvoiceDetail.Rows[i][32]);
    //                        model.UnitPrice = Convert.ToString(inputVatInvoiceDetail.Rows[i][33]);
    //                        model.DiscountAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][34]);
    //                        model.DiscountPercentage = Convert.ToString(inputVatInvoiceDetail.Rows[i][35]);
    //                        model.ContractNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][36]);
    //                        model.ContractStartDate = Convert.ToString(inputVatInvoiceDetail.Rows[i][37] ?? DateTime.Now.AddYears(100));
    //                        model.ContractEndDate = Convert.ToString(inputVatInvoiceDetail.Rows[i][38] ?? DateTime.Now.AddYears(100));
    //                        model.OriginalInvoiceNumberForDebitCreditNote = Convert.ToString(inputVatInvoiceDetail.Rows[i][39]);

    //                        model.TaxLineNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][40]);
    //                        model.ProductType = Convert.ToString(inputVatInvoiceDetail.Rows[i][41]);
    //                        model.TaxCode = Convert.ToString(inputVatInvoiceDetail.Rows[i][42]);
    //                        model.TaxRateCode = Convert.ToString(inputVatInvoiceDetail.Rows[i][43]);
    //                        model.TaxRate = Convert.ToString(inputVatInvoiceDetail.Rows[i][44]);
    //                        model.TaxableAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][45]);
    //                        model.SARTaxableAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][46]);
    //                        model.RecoverableTaxableAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][47]);
    //                        model.SARRecoverableTaxableAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][48]);
    //                        model.NonRecoverableTaxAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][49]);
    //                        model.SARNonRecoverableTaxAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][50]);
    //                        models.Add(model);
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            message = ex.Message;
    //            return null;
    //        }

    //        return models;
    //    }

    //    private List<OutPutVATModel> OutputVATExcelFle(IFormFile inputFile)
    //    {
    //        Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    //        DataSet dsexcelRecords = new DataSet();
    //        IExcelDataReader reader = null;
    //        string message = string.Empty;
    //        Stream stream = inputFile.OpenReadStream();
    //        List<OutPutVATModel> models = new List<OutPutVATModel>();

    //        try
    //        {
    //            if (inputFile != null)
    //            {
    //                if (inputFile.FileName.EndsWith(".xls"))
    //                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
    //                else if (inputFile.FileName.EndsWith(".xlsx"))
    //                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
    //                else
    //                    message = "The file format is not supported.";

    //                dsexcelRecords = reader.AsDataSet();
    //                reader.Close();

    //                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
    //                {
    //                    DataTable inputVatInvoiceDetail = dsexcelRecords.Tables[0];
    //                    //var newDT = DataTableHelper.MakeFirstRowAsColumnName(inputVatInvoiceDetail);
    //                    //var detailModels = ConvertDataTableToList.ConvertDataTable<OutPutVATModel>(newDT);

    //                    for (int i = 2; i < inputVatInvoiceDetail.Rows.Count; i++)
    //                    {
    //                        var model = new OutPutVATModel();
    //                        model.InvoiceNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][0]);
    //                        model.InvoiceDocSequence = Convert.ToString(inputVatInvoiceDetail.Rows[i][1]);
    //                        model.InvoiceSource = Convert.ToString(inputVatInvoiceDetail.Rows[i][2]);
    //                        model.InvoiceType = Convert.ToString(inputVatInvoiceDetail.Rows[i][3]);
    //                        model.InvoiceDate = Convert.ToString(inputVatInvoiceDetail.Rows[i][4]);
    //                        model.GLDate = Convert.ToString(inputVatInvoiceDetail.Rows[i][5]);
    //                        model.InvoiceAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][6]);
    //                        model.InvoiceCurrency = Convert.ToString(inputVatInvoiceDetail.Rows[i][7]);
    //                        model.CurrencyExchangeRate = Convert.ToString(inputVatInvoiceDetail.Rows[i][8]);
    //                        model.SARInvoiceAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][9]);
    //                        model.CustomerNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][10]);
    //                        model.CustomerName = Convert.ToString(inputVatInvoiceDetail.Rows[i][11]);
    //                        model.BillToAdress = Convert.ToString(inputVatInvoiceDetail.Rows[i][12]);
    //                        model.CustomerCountryName = Convert.ToString(inputVatInvoiceDetail.Rows[i][13]);
    //                        model.CustomerVATRegistrationNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][14]);
    //                        model.CustomerCommercialRegistrationNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][15]);
    //                        model.SellerNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][16]);
    //                        model.SellerVATRegistrationNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][17]);
    //                        model.SellerAddress = Convert.ToString(inputVatInvoiceDetail.Rows[i][18]);
    //                        model.GroupVARRegistrationNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][19]);
    //                        model.SellerCommercialNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][20]);
    //                        model.InvoiceLineNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][21]);

    //                        model.InvoiceLineDescription = Convert.ToString(inputVatInvoiceDetail.Rows[i][22]);
    //                        model.IssueDate = Convert.ToString(inputVatInvoiceDetail.Rows[i][23]);
    //                        model.Quantity = Convert.ToString(inputVatInvoiceDetail.Rows[i][24]);
    //                        model.UnitPrice = Convert.ToString(inputVatInvoiceDetail.Rows[i][25]);
    //                        model.DiscountAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][26]);
    //                        model.DiscountPercentage = Convert.ToString(inputVatInvoiceDetail.Rows[i][27]);
    //                        model.PaymentMethod = Convert.ToString(inputVatInvoiceDetail.Rows[i][28]);
    //                        model.PaymentTerm = Convert.ToString(inputVatInvoiceDetail.Rows[i][29]);
    //                        model.InvoiceLineAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][30]);
    //                        model.SARInvoiceLineAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][31]);
    //                        model.TaxRateName = Convert.ToString(inputVatInvoiceDetail.Rows[i][32]);
    //                        model.TaxableAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][33]);
    //                        model.SARTaxableAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][34]);
    //                        model.TaxAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][35]);
    //                        model.SARTaxAmount = Convert.ToString(inputVatInvoiceDetail.Rows[i][36]);
    //                        model.TaxClassificationCode = Convert.ToString(inputVatInvoiceDetail.Rows[i][37]);
    //                        model.TaxRate = Convert.ToString(inputVatInvoiceDetail.Rows[i][38]);
    //                        model.TaxAccount = Convert.ToString(inputVatInvoiceDetail.Rows[i][39]);
    //                        model.ContractNumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][40]);
    //                        model.ContractDescription = Convert.ToString(inputVatInvoiceDetail.Rows[i][41]);

    //                        model.ContractStartDate = Convert.ToString(inputVatInvoiceDetail.Rows[i][42]);
    //                        model.ContractEndDate = Convert.ToString(inputVatInvoiceDetail.Rows[i][43]);
    //                        model.OriginalInvoice = Convert.ToString(inputVatInvoiceDetail.Rows[i][44]);
    //                        model.PONumber = Convert.ToString(inputVatInvoiceDetail.Rows[i][45]);
    //                        model.UniversalUniqueInvoiceIdentifier = Convert.ToString(inputVatInvoiceDetail.Rows[i][46]);
    //                        model.QRCode = Convert.ToString(inputVatInvoiceDetail.Rows[i][47]);
    //                        model.PreviousInvoiceNoteHash = Convert.ToString(inputVatInvoiceDetail.Rows[i][48]);
    //                        model.InvoiceTamperResistantCounterValue = Convert.ToString(inputVatInvoiceDetail.Rows[i][49]);
    //                        models.Add(model);
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            message = ex.Message;
    //            return null;
    //        }

    //        return models;
    //    }

    //    private List<VATTrialBalanceModel> VATTrialBalance(IFormFile inputFile)
    //    {
    //        Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    //        DataSet dsexcelRecords = new DataSet();
    //        IExcelDataReader reader = null;
    //        string message = string.Empty;
    //        Stream stream = inputFile.OpenReadStream();
    //        List<VATTrialBalanceModel> models = new List<VATTrialBalanceModel>();

    //        try
    //        {
    //            if (inputFile != null)
    //            {
    //                if (inputFile.FileName.EndsWith(".xls"))
    //                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
    //                else if (inputFile.FileName.EndsWith(".xlsx"))
    //                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
    //                else
    //                    message = "The file format is not supported.";

    //                dsexcelRecords = reader.AsDataSet();
    //                reader.Close();

    //                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
    //                {
    //                    DataTable inputVatInvoiceDetail = dsexcelRecords.Tables[0];

    //                    for (int i = 1; i < inputVatInvoiceDetail.Rows.Count; i++)
    //                    {
    //                        var model = new VATTrialBalanceModel();
    //                        model.Account = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column0"]);
    //                        model.Description = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column1"]);
    //                        model.BeginingBalance = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column3"]);
    //                        model.Debit = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column4"]);
    //                        model.Credit = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column5"]);
    //                        model.Activity = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column6"]);
    //                        model.EndingBalance = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column7"]);
    //                        models.Add(model);
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            message = ex.Message;
    //            return null;
    //        }

    //        return models;
    //    }
    //    private List<VATRetunDetailModel> VATReturnExcelData(IFormFile inputFile)
    //    {
    //        Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    //        DataSet dsexcelRecords = new DataSet();
    //        IExcelDataReader reader = null;
    //        string message = string.Empty;
    //        Stream stream = inputFile.OpenReadStream();
    //        List<VATRetunDetailModel> models = new List<VATRetunDetailModel>();

    //        try
    //        {
    //            if (inputFile != null)
    //            {
    //                if (inputFile.FileName.EndsWith(".xls"))
    //                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
    //                else if (inputFile.FileName.EndsWith(".xlsx"))
    //                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
    //                else
    //                    message = "The file format is not supported.";

    //                dsexcelRecords = reader.AsDataSet();
    //                reader.Close();

    //                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
    //                {
    //                    DataTable inputVatInvoiceDetail = dsexcelRecords.Tables[0];

    //                    for (int i = 1; i < inputVatInvoiceDetail.Rows.Count; i++)
    //                    {
    //                        var model = new VATRetunDetailModel();
    //                        // model.Account = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column0"]);
    //                        // model.Description = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column1"]);
    //                        // model.BeginingBalance = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column3"]);
    //                        // model.Debit = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column4"]);
    //                        // model.Credit = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column5"]);
    //                        // model.Activity = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column6"]);
    //                        // model.EndingBalance = Convert.ToString(inputVatInvoiceDetail.Rows[i]["Column7"]);
    //                        models.Add(model);
    //                    }
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            message = ex.Message;
    //            return null;
    //        }

    //        return models;
    //    }

    //    private async Task<List<InputVATDataFile>> ConvertInputModelToDBModel(List<InputVATFileVm> models)
    //    {
    //        List<InputVATDataFile> dbModels = new List<InputVATDataFile>();
    //        models.ForEach(item =>
    //        {
    //            var dbModel = new InputVATDataFile();
    //            dbModel.InvoiceType = item.InvoiceType;
    //            dbModel.InvoiceSource = item.InvoiceSource;
    //            dbModel.InvoiceNumber = item.InvoiceNumber;
    //            dbModel.InvoiceDocNumber = item.InvoiceDocNumber;
    //            dbModel.InvoiceDate = item.InvoiceDate.GetDefaultIfStringNull<DateTime>();
    //            dbModel.GLDate = item.GLDate.GetDefaultIfStringNull<DateTime>();
    //            dbModel.TotalInvoiceAmount = item.TotalInvoiceAmount.GetDefaultIfStringNull<decimal>();
    //            dbModel.InvoiceCurrency = item.InvoiceCurrency;
    //            dbModel.CurrencyExchangeRate = item.CurrencyExchangeRate.GetDefaultIfStringNull<decimal>();
    //            dbModel.SARInvoiceAmount = item.SARInvoiceAmount.GetDefaultIfStringNull<decimal>();
    //            dbModel.SuppierNumber = item.SuppierNumber.GetDefaultIfStringNull<int>();
    //            dbModel.SupplierName = item.SupplierName.GetDefaultIfStringNull<string>();
    //            dbModel.SupplierSite = item.SupplierSite.GetDefaultIfStringNull<string>();
    //            dbModel.SupplierAddress = item.SupplierAddress.GetDefaultIfStringNull<string>();
    //            dbModel.SupplierCountry = item.SupplierCountry.GetDefaultIfStringNull<string>();
    //            dbModel.SupplierBankAccount = item.SupplierBankAccount.GetDefaultIfStringNull<string>();
    //            dbModel.SupplierVATRegistrationNumber = item.SupplierVATRegistrationNumber.GetDefaultIfStringNull<string>();
    //            dbModel.SupplierVATGroupRegistrationNumber = item.SupplierVATGroupRegistrationNumber.GetDefaultIfStringNull<string>();
    //            dbModel.InvoiceStatus = item.InvoiceStatus.GetDefaultIfStringNull<string>();
    //            dbModel.PaymentStatus = item.PaymentStatus.GetDefaultIfStringNull<string>();
    //            dbModel.PaymentAmount = item.PaymentAmount.GetDefaultIfStringNull<decimal>();
    //            dbModel.PaymentMethod = item.PaymentMethod.GetDefaultIfStringNull<string>();
    //            dbModel.PaymentTerm = item.PaymentTerm.GetDefaultIfStringNull<string>();
    //            dbModel.InvoiceLineNumber = item.InvoiceLineNumber.GetDefaultIfStringNull<int>();
    //            dbModel.InvoiceLineDescription = item.InvoiceLineDescription.GetDefaultIfStringNull<string>();
    //            dbModel.PONumber = item.PONumber.GetDefaultIfStringNull<string>();
    //            dbModel.PoDate = item.PoDate.GetDefaultIfStringNull<DateTime>();
    //            dbModel.ReleaseDate = item.ReleaseDate.GetDefaultIfStringNull<DateTime>();
    //            dbModel.ReceiptNumber = item.ReceiptNumber.GetDefaultIfStringNull<string>();
    //            dbModel.ReceiptDate = item.ReceiptDate.GetDefaultIfStringNull<DateTime>();
    //            dbModel.PoItemNumber = item.PoItemNumber.GetDefaultIfStringNull<string>();
    //            dbModel.PoItemDescription = item.PoItemDescription.GetDefaultIfStringNull<string>();
    //            dbModel.Quantity = item.Quantity.GetDefaultIfStringNull<decimal>();
    //            dbModel.UnitPrice = item.UnitPrice.GetDefaultIfStringNull<decimal>();
    //            dbModel.DiscountAmount = item.DiscountAmount.GetDefaultIfStringNull<decimal>();
    //            dbModel.DiscountPercentage = item.DiscountPercentage.GetDefaultIfStringNull<decimal>();
    //            dbModel.ContractNumber = item.ContractNumber.GetDefaultIfStringNull<string>();
    //            dbModel.ContractStartDate = item.ContractStartDate.GetDefaultIfStringNull<DateTime>();
    //            dbModel.ContractEndDate = item.ContractEndDate.GetDefaultIfStringNull<DateTime>();
    //            dbModel.OriginalInvoiceNumberForDebitCreditNote = item.OriginalInvoiceNumberForDebitCreditNote;
    //            dbModel.TaxLineNumber = item.TaxLineNumber.GetDefaultIfStringNull<int>();
    //            dbModel.ProductType = item.ProductType;
    //            dbModel.TaxCode = item.TaxCode;
    //            dbModel.TaxRateCode = item.TaxRateCode;
    //            dbModel.TaxRate = item.TaxRate.GetDefaultIfStringNull<int>();
    //            dbModel.TaxableAmount = item.TaxableAmount.GetDefaultIfStringNull<decimal>();
    //            dbModel.SARTaxableAmount = item.SARTaxableAmount.GetDefaultIfStringNull<decimal>();
    //            dbModel.RecoverableTaxableAmount = item.RecoverableTaxableAmount.GetDefaultIfStringNull<decimal>();
    //            dbModel.SARRecoverableTaxableAmount = item.SARRecoverableTaxableAmount.GetDefaultIfStringNull<decimal>();
    //            dbModel.NonRecoverableTaxAmount = item.NonRecoverableTaxAmount.GetDefaultIfStringNull<decimal>();
    //            dbModel.SARNonRecoverableTaxAmount = item.SARNonRecoverableTaxAmount.GetDefaultIfStringNull<decimal>();
    //            dbModel.RecoverableTaxGLAccountNumber = item.RecoverableTaxGLAccountNumber;

    //            dbModels.Add(dbModel);
    //        });

    //        return await Task.Run(() => dbModels);

    //    }

    //    private async Task<bool> CreateInputVATDetail(List<InputVATDataFile> models, string userName)
    //    {
    //        models.ForEach(item =>
    //        {
    //            item.CreatedBy = userName;
    //        });

    //        var response = await _IInputVatDataFileRepository.CreateEntity(models.ToArray());
    //        return true;
    //    }

    //    private async Task<List<VATTrailBalanceModel>> ConvertVATTrialBalanceModelToDBModel(List<VATTrialBalanceModel> models)
    //    {
    //        var dbModels = new List<VATTrailBalanceModel>();
    //        foreach (var data in models)
    //        {
    //            var dbModel = new VATTrailBalanceModel();
    //            dbModel.Account = data.Account.GetDefaultIfStringNull<string>();
    //            dbModel.Description = data.Description.GetDefaultIfStringNull<string>();
    //            dbModel.BeginingBalance = data.BeginingBalance.GetDefaultIfStringNull<decimal>();
    //            dbModel.Debit = data.Debit.GetDefaultIfStringNull<decimal>();
    //            dbModel.Credit = data.Credit.GetDefaultIfStringNull<decimal>();
    //            dbModel.Activity = data.Activity.GetDefaultIfStringNull<decimal>();
    //            dbModel.EndingBalance = data.EndingBalance.GetDefaultIfStringNull<decimal>();

    //            dbModels.Add(dbModel);

    //        }

    //        return await Task.Run(() => dbModels);

    //    }

    //    private async Task<bool> CreateTrialBalance(List<VATTrailBalanceModel> models, string userName)
    //    {
    //        models.ForEach(item =>
    //        {
    //            item.CreatedBy = userName;
    //        });

    //        var response = await _IVatTrialBalanceModelRepository.CreateEntity(models.ToArray());
    //        return true;
    //    }

    //    private async Task<List<STCVATOutputModel>> DTOOutModelToOutputDataModel(List<OutPutVATModel> models)
    //    {
    //        var dtoModels = new List<STCVATOutputModel>();
    //        models.ForEach(item =>
    //        {

    //            var dtoModel = new STCVATOutputModel();
    //            dtoModel.InvoiceNumber = item.InvoiceNumber.GetDefaultIfStringNull<string>();
    //            dtoModel.InvoiceDocSequence = item.InvoiceDocSequence.GetDefaultIfStringNull<string>();
    //            dtoModel.InvoiceSource = item.InvoiceSource.GetDefaultIfStringNull<string>();
    //            dtoModel.InvoiceType = item.InvoiceType.GetDefaultIfStringNull<string>();
    //            dtoModel.InvoiceDate = item.InvoiceDate.GetDefaultIfStringNull<DateTime>();
    //            dtoModel.GLDate = item.GLDate.GetDefaultIfStringNull<DateTime>();
    //            dtoModel.InvoiceAmount = item.InvoiceAmount.GetDefaultIfStringNull<decimal>();
    //            dtoModel.InvoiceCurrency = item.InvoiceCurrency.GetDefaultIfStringNull<string>();
    //            dtoModel.CurrencyExchangeRate = item.CurrencyExchangeRate.GetDefaultIfStringNull<decimal>();
    //            dtoModel.SARInvoiceAmount = item.SARInvoiceAmount.GetDefaultIfStringNull<decimal>();

    //            dtoModel.CustomerNumber = item.CustomerNumber.GetDefaultIfStringNull<string>();
    //            dtoModel.CustomerName = item.CustomerName.GetDefaultIfStringNull<string>();
    //            dtoModel.BillToAddress = item.BillToAdress.GetDefaultIfStringNull<string>();
    //            dtoModel.CustomerCountryName = item.CustomerCountryName.GetDefaultIfStringNull<string>();
    //            dtoModel.CustomerVATRegistrationNumber = item.CustomerVATRegistrationNumber.GetDefaultIfStringNull<string>();
    //            dtoModel.CustomerCommercialRegistrationNumber = item.CustomerCommercialRegistrationNumber.GetDefaultIfStringNull<string>();
    //            dtoModel.SellerName = item.SellerName.GetDefaultIfStringNull<string>();
    //            dtoModel.SellerVATRegistrationNumber = item.SellerVATRegistrationNumber.GetDefaultIfStringNull<string>();
    //            dtoModel.SellerAddress = item.SellerAddress.GetDefaultIfStringNull<string>();
    //            dtoModel.GroupVATRegistrationNumber = item.GroupVARRegistrationNumber.GetDefaultIfStringNull<string>();

    //            dtoModel.SellerCommercialNumber = item.SellerCommercialNumber.GetDefaultIfStringNull<string>();
    //            dtoModel.InvoiceLineNumber = item.InvoiceLineNumber.GetDefaultIfStringNull<string>();
    //            dtoModel.InvoiceLineDescription = item.InvoiceLineDescription.GetDefaultIfStringNull<string>();
    //            dtoModel.IssueDate = item.IssueDate.GetDefaultIfStringNull<string>();
    //            dtoModel.Quantity = item.Quantity.GetDefaultIfStringNull<decimal>();
    //            dtoModel.UnitPrice = item.UnitPrice.GetDefaultIfStringNull<decimal>();
    //            dtoModel.DiscountAmount = item.DiscountAmount.GetDefaultIfStringNull<decimal>();
    //            dtoModel.DiscountPercentage = item.DiscountPercentage.GetDefaultIfStringNull<decimal>();
    //            dtoModel.PaymentMethod = item.PaymentMethod.GetDefaultIfStringNull<string>();
    //            dtoModel.PaymentTerm = item.PaymentTerm.GetDefaultIfStringNull<string>();


    //            dtoModel.InvoiceLineAmount = item.InvoiceLineAmount.GetDefaultIfStringNull<decimal>();
    //            dtoModel.SARInvoiceLineAmount = item.SARInvoiceLineAmount.GetDefaultIfStringNull<decimal>();
    //            dtoModel.TaxRateName = item.TaxRateName.GetDefaultIfStringNull<string>();
    //            dtoModel.TaxableAmount = item.TaxableAmount.GetDefaultIfStringNull<decimal>();
    //            dtoModel.SARTaxAmount = item.SARTaxAmount.GetDefaultIfStringNull<decimal>();
    //            dtoModel.TaxAmount = item.TaxAmount.GetDefaultIfStringNull<decimal>();
    //            dtoModel.SARTaxAmount = item.SARTaxAmount.GetDefaultIfStringNull<decimal>();
    //            dtoModel.TaxClassificationCode = item.TaxClassificationCode.GetDefaultIfStringNull<string>();
    //            dtoModel.TaxRate = item.TaxRate.GetDefaultIfStringNull<decimal>();
    //            dtoModel.TaxAccount = item.TaxAccount.GetDefaultIfStringNull<string>();


    //            dtoModel.ContractNumber = item.ContractNumber.GetDefaultIfStringNull<string>();
    //            dtoModel.ContractDescription = item.ContractDescription.GetDefaultIfStringNull<string>();

    //            dtoModel.ContractStartDate = item.ContractStartDate.GetDefaultIfStringNull<DateTime>();
    //            dtoModel.ContractEndDate = item.ContractEndDate.GetDefaultIfStringNull<DateTime>();
    //            dtoModel.OriginalInvoice = item.OriginalInvoice.GetDefaultIfStringNull<string>();
    //            dtoModel.PoNumber = item.PONumber.GetDefaultIfStringNull<string>();
    //            dtoModel.UniversalUniqueInvoiceIndentifier = item.UniversalUniqueInvoiceIdentifier.GetDefaultIfStringNull<string>();
    //            dtoModel.QRCode = item.QRCode.GetDefaultIfStringNull<string>();
    //            dtoModel.PreviousInvoiceNoteHash = item.PreviousInvoiceNoteHash.GetDefaultIfStringNull<string>();
    //            dtoModel.InvoiceTamperResistantCounterValue = item.InvoiceTamperResistantCounterValue.GetDefaultIfStringNull<string>();


    //            dtoModels.Add(dtoModel);
    //        });

    //        return await Task.Run(() => dtoModels);
    //    }

    //    private async Task<bool> CreateSTCOutputModel(List<STCVATOutputModel> models, string userName)
    //    {
    //        models.ForEach(data =>
    //        {
    //            data.CreatedBy = userName;
    //            data.CreatedDate = DateTime.Now;

    //        });
    //        var response = await _ISTCVATOutputModelRepository.CreateEntity(models.ToArray());
    //        return true;
    //    }

    //    private async Task<List<VATRetunDetailModel>> GetVATReturnDetail(IFormFile fileData)
    //    {

    //        Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    //        DataSet dsexcelRecords = new DataSet();
    //        IExcelDataReader reader = null;
    //        string message = string.Empty;
    //        Stream stream = fileData.OpenReadStream();
    //        List<VATRetunDetailModel> models = new List<VATRetunDetailModel>();

    //        try
    //        {
    //            if (fileData != null)
    //            {
    //                if (fileData.FileName.EndsWith(".xls"))
    //                    reader = ExcelReaderFactory.CreateBinaryReader(stream);
    //                else if (fileData.FileName.EndsWith(".xlsx"))
    //                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
    //                else
    //                    message = "The file format is not supported.";

    //                dsexcelRecords = reader.AsDataSet();
    //                reader.Close();

    //                if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
    //                {
    //                    DataTable inputVatInvoiceDetail = dsexcelRecords.Tables[0];
    //                    var newDT = DataTableHelper.MakeFirstRowAsColumnName(inputVatInvoiceDetail);
    //                    var detailModels = ConvertDataTableToList.ConvertDataTable<VATRetunDetailModel>(newDT);
    //                    models = detailModels.Item2;
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            message = ex.Message;
    //            return null;
    //        }

    //        return await Task.Run(() => models);
    //    }

    //    private async Task<(string, List<VATReturnModel>)> DTOConvertModelTODBObject(List<VATRetunDetailModel> models)
    //    {
    //        var dbModels = new List<VATReturnModel>();
    //        int count = 0;
    //        string vatType = "VAT on Sales";
    //        string vatrTypeDetail = string.Empty;
    //        try
    //        {
    //            foreach (var data in models)
    //            {
    //                count++;
    //                if (count > 5)
    //                {
    //                    if (data.Column2.Contains("Purchases"))
    //                    {
    //                        vatType = "VAT on Purchases";
    //                    }
    //                    var dbModel = new VATReturnModel();
    //                    dbModel.VATType = vatType;
    //                    dbModel.VATTypeId = data.Column4.GetDefaultIfStringNull<decimal>();
    //                    dbModel.VATTypeName = data.Column6;
    //                    dbModel.SARAmount = data.Column8.GetDefaultIfStringNull<decimal>();
    //                    dbModel.SARAdjustment = data.Column10.GetDefaultIfStringNull<decimal>();
    //                    dbModel.SARVATAmount = data.Column12.GetDefaultIfStringNull<decimal>();
    //                    dbModels.Add(dbModel);
    //                }
    //                if (count == 2)
    //                {
    //                    vatrTypeDetail = data.Column8;
    //                }
    //            }

    //            dbModels.ForEach(data =>
    //            {
    //                data.VATReturnDetail = vatrTypeDetail;
    //                data.CreatedDate = DateTime.Now;
    //            });
    //        }
    //        catch (Exception ex)
    //        {
    //            string message = ex.Message;
    //            return (message, null);
    //        }
    //        return await Task.Run(() => ("", dbModels));
    //    }

    //    private async Task<bool> CreateVATReturnData(List<VATReturnModel> models, string userName)
    //    {
    //        models.ForEach(data =>
    //        {
    //            data.CreatedBy = userName;
    //        });

    //        models.RemoveAll(data => string.IsNullOrEmpty(data.VATTypeName));

    //        var response = await _IVATReturnModelRepository.CreateEntity(models.ToArray());
    //        return true;
    //    }

    //    #endregion
    //}
}
