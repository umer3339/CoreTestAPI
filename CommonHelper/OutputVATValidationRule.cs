using STCAPI.Core.ViewModel.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public static class OutputVATValidationRule
    {
        public static IDictionary<int, (string, string)> ValidateOutputVatData(List<OutPutVATModel> models)
        {
            IDictionary<int, (string, string)> errorResult = new Dictionary<int, (string, string)>();
            int count = 0;
            foreach (var data in models)
            {
                count++;
                if (string.IsNullOrEmpty(data.InvoiceNumber))
                    errorResult.Add(count++, ("InvoiceNumber", $"InvoiceNumber is mandatory"));
                if (string.IsNullOrEmpty(data.InvoiceDocSequence))
                    errorResult.Add(count++, ("InvoiceDocSequence", $"InvoiceDocSequence is mandatory"));
                if (string.IsNullOrEmpty(data.InvoiceSource))
                    errorResult.Add(count++, ("InvoiceSource", $"InvoiceSource is mandatory"));
                if (string.IsNullOrEmpty(data.InvoiceType))
                    errorResult.Add(count++, ("InvoiceType", $"InvoiceType is mandatory"));
                if (string.IsNullOrEmpty(data.InvoiceDate))
                    errorResult.Add(count++, ("InvoiceDate", $"InvoiceDate is mandatory"));
                if (string.IsNullOrEmpty(data.GLDate))
                    errorResult.Add(count++, ("GLDate", $"GLDate is mandatory"));
                if (string.IsNullOrEmpty(data.InvoiceAmount))
                    errorResult.Add(count++, ("InvoiceAmount", $"InvoiceAmount is mandatory"));
                if (string.IsNullOrEmpty(data.InvoiceCurrency))
                    errorResult.Add(count++, ("InvoiceCurrency", $"InvoiceCurrency is mandatory"));
                if (string.IsNullOrEmpty(data.CurrencyExchangeRate))
                    errorResult.Add(count++, ("CurrencyExchangeRate", $"CurrencyExchangeRate is mandatory"));
                if (string.IsNullOrEmpty(data.SARInvoiceAmount))
                    errorResult.Add(count++, ("SARInvoiceAmount", $"SARInvoiceAmount is mandatory"));
                if (string.IsNullOrEmpty(data.CustomerNumber))
                    errorResult.Add(count++, ("CustomerNumber", $"CustomerNumber is mandatory"));
                if (string.IsNullOrEmpty(data.CustomerName))
                    errorResult.Add(count++, ("CustomerName", $"CustomerName is mandatory"));
                if (string.IsNullOrEmpty(data.BillToAdress))
                    errorResult.Add(count++, ("BillToAdress", $"BillToAdress is mandatory"));
                if (string.IsNullOrEmpty(data.CustomerCountryName))
                    errorResult.Add(count++, ("CustomerCountryName", $"CustomerCountryName is mandatory"));
                if (string.IsNullOrEmpty(data.CustomerVATRegistrationNumber))
                    errorResult.Add(count++, ("CustomerVATRegistrationNumber", $"CustomerVATRegistrationNumber is mandatory"));
                if (string.IsNullOrEmpty(data.CustomerCommercialRegistrationNumber))
                    errorResult.Add(count++, ("CustomerCommercialRegistrationNumber", $"CustomerCommercialRegistrationNumber is mandatory"));
                if (string.IsNullOrEmpty(data.SellerNumber))
                    errorResult.Add(count++, ("SellerNumber", $"SellerNumber is mandatory"));
                if (string.IsNullOrEmpty(data.SellerVATRegistrationNumber))
                    errorResult.Add(count++, ("SellerVATRegistrationNumber", $"SellerVATRegistrationNumber is mandatory"));
                if (string.IsNullOrEmpty(data.SellerAddress))
                    errorResult.Add(count++, ("SellerAddress", $"SellerAddress is mandatory"));
                if (string.IsNullOrEmpty(data.GroupVARRegistrationNumber))
                    errorResult.Add(count++, ("GroupVARRegistrationNumber", $"GroupVARRegistrationNumber is mandatory"));
                if (string.IsNullOrEmpty(data.SellerCommercialNumber))
                    errorResult.Add(count++, ("SellerCommercialNumber", $"SellerCommercialNumber is mandatory"));
                if (string.IsNullOrEmpty(data.InvoiceLineNumber))
                    errorResult.Add(count++, ("InvoiceLineNumber", $"InvoiceLineNumber is mandatory"));
                if (string.IsNullOrEmpty(data.InvoiceLineDescription))
                    errorResult.Add(count++, ("InvoiceLineDescription", $"InvoiceLineDescription is mandatory"));
                if (string.IsNullOrEmpty(data.IssueDate))
                    errorResult.Add(count++, ("IssueDate", $"IssueDate is mandatory"));
                if (string.IsNullOrEmpty(data.Quantity))
                    errorResult.Add(count++, ("Quantity", $"Quantity is mandatory"));
                if (string.IsNullOrEmpty(data.UnitPrice))
                    errorResult.Add(count++, ("UnitPrice", $"UnitPrice is mandatory"));
                if (string.IsNullOrEmpty(data.DiscountAmount))
                    errorResult.Add(count++, ("DiscountAmount", $"DiscountAmount is mandatory"));
                if (string.IsNullOrEmpty(data.DiscountPercentage))
                    errorResult.Add(count++, ("DiscountPercentage", $"DiscountPercentage is mandatory"));
                if (string.IsNullOrEmpty(data.PaymentMethod))
                    errorResult.Add(count++, ("PaymentMethod", $"PaymentMethod is mandatory"));
                if (string.IsNullOrEmpty(data.PaymentTerm))
                    errorResult.Add(count++, ("PaymentTerm", $"PaymentTerm is mandatory"));
                if (string.IsNullOrEmpty(data.InvoiceLineAmount))
                    errorResult.Add(count++, ("InvoiceLineAmount", $"InvoiceLineAmount is mandatory"));
                if (string.IsNullOrEmpty(data.SARInvoiceLineAmount))
                    errorResult.Add(count++, ("SARInvoiceLineAmount", $"SARInvoiceLineAmount is mandatory"));
                if (string.IsNullOrEmpty(data.TaxRateName))
                    errorResult.Add(count++, ("TaxRateName", $"TaxRateName is mandatory"));
                if (string.IsNullOrEmpty(data.TaxableAmount))
                    errorResult.Add(count++, ("TaxableAmount", $"TaxableAmount is mandatory"));
                if (string.IsNullOrEmpty(data.SARTaxableAmount))
                    errorResult.Add(count++, ("SARTaxableAmount", $"SARTaxableAmount is mandatory"));
                if (string.IsNullOrEmpty(data.TaxAmount))
                    errorResult.Add(count++, ("TaxAmount", $"TaxAmount is mandatory"));
                if (string.IsNullOrEmpty(data.SARTaxAmount))
                    errorResult.Add(count++, ("SARTaxAmount", $"SARTaxAmount is mandatory"));
                if (string.IsNullOrEmpty(data.TaxClassificationCode))
                    errorResult.Add(count++, ("TaxClassificationCode", $"TaxClassificationCode is mandatory"));
                if (string.IsNullOrEmpty(data.TaxRate))
                    errorResult.Add(count++, ("TaxRate", $"TaxRate is mandatory"));
                if (string.IsNullOrEmpty(data.TaxAccount))
                    errorResult.Add(count++, ("TaxAccount", $"TaxAccount is mandatory"));
                if (string.IsNullOrEmpty(data.ContractNumber))
                    errorResult.Add(count++, ("ContractNumber", $"ContractNumber is mandatory"));
                if (string.IsNullOrEmpty(data.ContractDescription))
                    errorResult.Add(count++, ("ContractDescription", $"ContractDescription is mandatory"));
                if (string.IsNullOrEmpty(data.ContractStartDate))
                    errorResult.Add(count++, ("ContractStartDate", $"ContractStartDate is mandatory"));
                if (string.IsNullOrEmpty(data.ContractEndDate))
                    errorResult.Add(count++, ("ContractEndDate", $"ContractEndDate is mandatory"));
                if (string.IsNullOrEmpty(data.OriginalInvoice))
                    errorResult.Add(count++, ("OriginalInvoice", $"OriginalInvoice is mandatory"));
                if (string.IsNullOrEmpty(data.PONumber))
                    errorResult.Add(count++, ("PONumber", $"PONumber is mandatory"));
                if (string.IsNullOrEmpty(data.UniversalUniqueInvoiceIdentifier))
                    errorResult.Add(count++, ("UniversalUniqueInvoiceIdentifier", $"UniversalUniqueInvoiceIdentifier is mandatory"));
                if (string.IsNullOrEmpty(data.QRCode))
                    errorResult.Add(count++, ("QRCode", $"QRCode is mandatory"));
                if (string.IsNullOrEmpty(data.PreviousInvoiceNoteHash))
                    errorResult.Add(count++, ("PreviousInvoiceNoteHash", $"PreviousInvoiceNoteHash is mandatory"));
                if (string.IsNullOrEmpty(data.InvoiceTamperResistantCounterValue))
                    errorResult.Add(count++, ("InvoiceTamperResistantCounterValue", $"InvoiceTamperResistantCounterValue is mandatory"));
                if (string.IsNullOrEmpty(data.SellerName))
                    errorResult.Add(count++, ("SellerName", $"SellerName is mandatory"));
            }
            return errorResult;

        }
    }
}
