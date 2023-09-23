using ImagineApps.Application.Utilities.Dtos;
using ImagineApps.Domain.Models;
using System.Text.RegularExpressions;

namespace ImagineApps.Application.Utilities.Helpers
{
    public class TXTHelper
    {
        public static FileInformationInputDto NormalizeInputData(List<string> data) 
        {
            var header = new Header();
            var details = new Detail();

            foreach (var line in data)
            {
                if (line.Substring(0,2) == HeaderConstants.IDENTIFIER)
                {
                    header.Recordtype = line.Substring(HeaderConstants.RECORDTYPE_START, HeaderConstants.RECORDTYPE_LENGTH);
                    header.CheckNumber = line.Substring(HeaderConstants.CHECK_NUMBER_START, HeaderConstants.RECORDTYPE_LENGTH);
                    header.BankId = line.Substring(HeaderConstants.BANKID_START, HeaderConstants.BANKID_LENGTH);
                    header.AccountID = line.Substring(HeaderConstants.ACCOUNTID_START, HeaderConstants.ACCOUNTID_LENGTH);
                    header.CheckDate = line.Substring(HeaderConstants.CHECKDATE_START, HeaderConstants.CHECKDATE_LENGTH);
                    header.PayeeID = line.Substring(HeaderConstants.PAYEEID_START, HeaderConstants.PAYEEID_LENGTH);
                    header.PayeeName1 = line.Substring(HeaderConstants.PAYEENAME1_START, HeaderConstants.PAYEENAME1_LENGTH);
                    header.PayeeName2 = line.Substring(HeaderConstants.PAYEENAME2_START, HeaderConstants.PAYEENAME2_LENGTH);
                    header.Address1 = line.Substring(HeaderConstants.ADDRESS1_START, HeaderConstants.ADDRESS1_LENGTH);
                    header.Address2 = line.Substring(HeaderConstants.ADDRESS2_START, HeaderConstants.ADDRESS2_LENGTH);
                    header.Address3 = line.Substring(HeaderConstants.ADDRESS3_START, HeaderConstants.ADDRESS3_LENGTH);
                    header.Address4 = line.Substring(HeaderConstants.ADDRESS4_START, HeaderConstants.ADDRESS4_LENGTH);
                    header.Address5 = line.Substring(HeaderConstants.ADDRESS5_START, HeaderConstants.ADDRESS5_LENGTH);
                    header.Checkamount = line.Substring(HeaderConstants.CHECKAMOUNT_START, HeaderConstants.CHECKAMOUNT_LENGTH);
                    header.PayorID = line.Substring(HeaderConstants.PAYORID_START, HeaderConstants.PAYORID_LENGTH);
                    header.AmountString = line.Substring(HeaderConstants.AMOUNTSTRING_START, HeaderConstants.AMOUNTSTRING_LENGTH);
                    header.TemplateID = line.Substring(HeaderConstants.TEMPLATEID_START);
                }
                else if (line.Substring(0,2) == DetailsConstants.IDENTIFIER)
                {
                    details.Recordtype = line.Substring(DetailsConstants.RECORDTYPE_START, DetailsConstants.RECORDTYPE_LENGTH);
                    details.CheckNumber = line.Substring(DetailsConstants.CHECK_NUMBER_START, DetailsConstants.RECORDTYPE_LENGTH);
                    details.BankID = line.Substring(DetailsConstants.BANKID_START, DetailsConstants.BANKID_LENGTH);
                    details.BankAccountNo = line.Substring(DetailsConstants.BANKACCOUNTNO_START, DetailsConstants.BANKACCOUNTNO_LENGTH);
                    details.CheckDate = line.Substring(DetailsConstants.CHECKDATE_START, DetailsConstants.CHECKDATE_LENGTH);
                    details.InvoiceNumber = line.Substring(DetailsConstants.INVOICENUMBER_START, DetailsConstants.INVOICENUMBER_LENGTH);
                    details.InvoiceDate = line.Substring(DetailsConstants.INVOICEDATE_START, DetailsConstants.INVOICEDATE_LENGTH);
                    details.VoucherNumber = line.Substring(DetailsConstants.VOUCHERNUMBER_START, DetailsConstants.VOUCHERNUMBER_LENGTH);
                    details.VoucherDate = line.Substring(DetailsConstants.VOUCHERDATE_START, DetailsConstants.VOUCHERDATE_LENGTH);
                    details.GrossAmount = line.Substring(DetailsConstants.GROSSAMOUNT_START, DetailsConstants.GROSSAMOUNT_LENGTH);
                    details.DiscountAmount = line.Substring(DetailsConstants.DISCOUNTAMOUNT_START, DetailsConstants.DISCOUNTAMOUNT_LENGTH);
                    details.NetAmount = line.Substring(DetailsConstants.NETAMOUNT_START, DetailsConstants.NETAMOUNT_LENGTH);
                    details.Concept = line.Substring(DetailsConstants.CONCEPT_START, DetailsConstants.CONCEPT_LENGTH);
                    details.BenefitDescription = line.Substring(DetailsConstants.BENEFITDESCRIPTION_START, DetailsConstants.BENEFITDESCRIPTION_LENGTH);
                }
            }

            var result = new FileInformationInputDto { Header = header, Details = details };

            return result;
        }

        public static (FileInformationOutputDto Result, string ErrorMessage) GetOutputData(FileInformationInputDto data, BankDto bank) 
        {
            string currency = "";

            if (data.Header.AccountID == StringConstants.ACCOUNT_01 ||
                data.Header.AccountID == StringConstants.ACCOUNT_02)
            {
                currency = StringConstants.CURRENCY_01;
            }
            else if (data.Header.AccountID == StringConstants.ACCOUNT_03 ||
                data.Header.AccountID == StringConstants.ACCOUNT_04)
            {
                currency = StringConstants.CURRENCY_02;
            }

            if (string.IsNullOrEmpty(currency)) return (null, $"The file could not be processed, no AccountID was found with the provided id: {data.Header.AccountID}.");

            var currenDate = DateTime.Now.ToString("ddMMyyyy").Replace(" ", "");
            var outputNmae = $"CHECK_AFT_{currenDate}";
            var outputLine1 = GetHeaderLine(data.Header, bank, currency);
            var outputLine2 = GetDetailsLine(data.Details);

            var result = new FileInformationOutputDto
            {
                FileName = outputNmae,
                FileLines = new List<string> { outputLine1, outputLine2 }
            };

            foreach (var line in result.FileLines)
            {
                var validation = ValidateFields(line);
                if (validation) return (null, $"The file could not be processed, please check the file fields.");
            }

            return (result, null);
        }

        private static string GetHeaderLine(Header header, BankDto bank, string currency)
        {
            var result = $@"
                                {header.Recordtype}~
                                {header.CheckNumber}~
                                {bank.Bank_Name}~
                                {bank.Address_1}~
                                {bank.Address_2}~
                                {header.AccountID}~
                                {header.CheckDate}~
                                {header.Address1}~
                                {FormatDate(header.CheckDate)}~
                                {currency}~
                                {header.PayeeName1}~
                                {header.PayeeName2}~
                                {header.Address1}~
                                {header.Address2}~
                                {header.Address3}~
                                {header.Address4}~
                                {header.Address5}~
                                {header.Checkamount}~
                                {header.PayorID}~
                                {header.AmountString}
                           ";

            return result; 
        }

        private static string GetDetailsLine(Detail details)
        {
            var result = $@"
                                {details.Recordtype}~
                                {details.InvoiceNumber}~
                                {details.VoucherNumber}~
                                {FormatDate(details.VoucherDate)}~
                                {FormatNumber(details.GrossAmount)}~
                                {FormatNumber(details.DiscountAmount)}~
                                {FormatNumber(details.NetAmount)}~
                                {details.Concept}~
                                {details.BenefitDescription}
                           ";

            return result;
        }

        private static string FormatDate(string date)
        {
            var checkdate = DateTime.ParseExact(date, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            var formattedDate = checkdate.ToString("dd MM yyyy");

            return formattedDate;
        }

        private static string FormatNumber(string numberInput)
        {
            decimal number;
            var stringNumber = numberInput.TrimEnd();

            if (decimal.TryParse(stringNumber, out number))
            {
                decimal roundedNumber = Math.Round(number, 2);
                string result = roundedNumber.ToString("0.00");
                return result;
            }

            return string.Empty;
        }

        private static bool ValidateFields(string line)
        {
            string pattern = @"~~";
            return !Regex.IsMatch(line, pattern);
        }
    }
}
