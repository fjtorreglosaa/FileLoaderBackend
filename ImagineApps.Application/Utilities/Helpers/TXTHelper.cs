using ImagineApps.Application.Utilities.Dtos;
using ImagineApps.Domain.Models;

namespace ImagineApps.Application.Utilities.Helpers
{
    public class TXTHelper
    {
        public static FileInformationInputDto NormalizeInputData(List<string> data) 
        {
            var header = new Header();
            var details = new Detail();

            try
            {
                foreach (var line in data)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var lineStart = line.Substring(0, 1);

                        if (lineStart == HeaderConstants.IDENTIFIER)
                        {
                            header.Recordtype = line.Substring(HeaderConstants.RECORDTYPE_START, HeaderConstants.RECORDTYPE_LENGTH).Trim();
                            header.CheckNumber = line.Substring(HeaderConstants.CHECK_NUMBER_START, HeaderConstants.RECORDTYPE_LENGTH).Trim();
                            header.BankId = line.Substring(HeaderConstants.BANKID_START, HeaderConstants.BANKID_LENGTH).Trim();
                            header.AccountID = line.Substring(HeaderConstants.ACCOUNTID_START, HeaderConstants.ACCOUNTID_LENGTH).Trim();
                            header.CheckDate = line.Substring(HeaderConstants.CHECKDATE_START, HeaderConstants.CHECKDATE_LENGTH).Trim();
                            header.PayeeID = line.Substring(HeaderConstants.PAYEEID_START, HeaderConstants.PAYEEID_LENGTH).Trim();
                            header.PayeeName1 = line.Substring(HeaderConstants.PAYEENAME1_START, HeaderConstants.PAYEENAME1_LENGTH).Trim();
                            header.PayeeName2 = line.Substring(HeaderConstants.PAYEENAME2_START, HeaderConstants.PAYEENAME2_LENGTH).Trim();
                            header.Address1 = line.Substring(HeaderConstants.ADDRESS1_START, HeaderConstants.ADDRESS1_LENGTH).Trim();
                            header.Address2 = line.Substring(HeaderConstants.ADDRESS2_START, HeaderConstants.ADDRESS2_LENGTH).Trim();
                            header.Address3 = line.Substring(HeaderConstants.ADDRESS3_START, HeaderConstants.ADDRESS3_LENGTH).Trim();
                            header.Address4 = line.Substring(HeaderConstants.ADDRESS4_START, HeaderConstants.ADDRESS4_LENGTH).Trim();
                            header.Address5 = line.Substring(HeaderConstants.ADDRESS5_START, HeaderConstants.ADDRESS5_LENGTH).Trim();
                            header.Checkamount = line.Substring(HeaderConstants.CHECKAMOUNT_START, HeaderConstants.CHECKAMOUNT_LENGTH).Trim();
                            header.PayorID = line.Substring(HeaderConstants.PAYORID_START, HeaderConstants.PAYORID_LENGTH).Trim();
                            header.AmountString = line.Substring(HeaderConstants.AMOUNTSTRING_START, HeaderConstants.AMOUNTSTRING_LENGTH).Trim();
                            header.TemplateID = line.Substring(HeaderConstants.TEMPLATEID_START).Trim();
                        }
                        else if (lineStart == DetailsConstants.IDENTIFIER)
                        {
                            details.Recordtype = line.Substring(DetailsConstants.RECORDTYPE_START, DetailsConstants.RECORDTYPE_LENGTH).Trim();
                            details.CheckNumber = line.Substring(DetailsConstants.CHECK_NUMBER_START, DetailsConstants.RECORDTYPE_LENGTH).Trim();
                            details.BankID = line.Substring(DetailsConstants.BANKID_START, DetailsConstants.BANKID_LENGTH).Trim();
                            details.BankAccountNo = line.Substring(DetailsConstants.BANKACCOUNTNO_START, DetailsConstants.BANKACCOUNTNO_LENGTH).Trim();
                            details.CheckDate = line.Substring(DetailsConstants.CHECKDATE_START, DetailsConstants.CHECKDATE_LENGTH).Trim();
                            details.InvoiceNumber = line.Substring(DetailsConstants.INVOICENUMBER_START, DetailsConstants.INVOICENUMBER_LENGTH).Trim();
                            details.InvoiceDate = line.Substring(DetailsConstants.INVOICEDATE_START, DetailsConstants.INVOICEDATE_LENGTH).Trim();
                            details.VoucherNumber = line.Substring(DetailsConstants.VOUCHERNUMBER_START, DetailsConstants.VOUCHERNUMBER_LENGTH).Trim();
                            details.VoucherDate = line.Substring(DetailsConstants.VOUCHERDATE_START, DetailsConstants.VOUCHERDATE_LENGTH).Trim();
                            details.GrossAmount = line.Substring(DetailsConstants.GROSSAMOUNT_START, DetailsConstants.GROSSAMOUNT_LENGTH).Trim();
                            details.DiscountAmount = line.Substring(DetailsConstants.DISCOUNTAMOUNT_START, DetailsConstants.DISCOUNTAMOUNT_LENGTH).Trim();
                            details.NetAmount = line.Substring(DetailsConstants.NETAMOUNT_START, DetailsConstants.NETAMOUNT_LENGTH).Trim();
                            details.Concept = line.Substring(DetailsConstants.CONCEPT_START, DetailsConstants.CONCEPT_LENGTH).Trim();
                            details.BenefitDescription = line.Substring(DetailsConstants.BENEFITDESCRIPTION_START).Trim();
                        }
                    }
                }

                var result = new FileInformationInputDto { Header = header, Details = details };

                return result;
            }
            catch (Exception e)
            {
                return null;
            }
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

            if (string.IsNullOrEmpty(currency)) return (null, $"Unable to continue, please check the file you are trying to upload and verify if the information and format are correct. ");

            var currentDate = DateTime.Now.ToString("ddMMyyyy").Replace(" ", "");
            var outputNmae = $"CHECK_AFT_{currentDate}";
            var outputLine1 = GetHeaderLine(data.Header, bank, currency);
            var outputLine2 = GetDetailsLine(data.Details);

            var result = new FileInformationOutputDto
            {
                FileName = outputNmae,
                FileLines = new List<string> { outputLine1, outputLine2 }
            };

            return (result, null);
        }

        private static string GetHeaderLine(Header header, BankDto bank, string currency)
        {
            var result = $"" +
                $"{header.Recordtype}~" +
                $"{header.CheckNumber}~" +
                $"{bank.Bank_Name}~" +
                $"{bank.Address_1}~" +
                $"{bank.Address_2}~" +
                $"{header.AccountID}~" +
                $"{FormatDate(header.CheckDate)}~" +
                $"{header.Address1}~" +
                $"{currency}~" +
                $"{header.PayeeName1}~" +
                $"{header.PayeeName2}~" +
                $"{header.Address1}~" +
                $"{header.Address2}~" +
                $"{header.Address3}~" +
                $"{header.Address4}~" +
                $"{header.Address5}~" +
                $"{header.Checkamount}~" +
                $"{header.PayorID}~" +
                $"{header.AmountString}";

            return result; 
        }

        private static string GetDetailsLine(Detail details)
        {
            var result = $"" +
                $"{details.Recordtype}~" +
                $"{details.InvoiceNumber}~" +
                $"{details.VoucherNumber}~" +
                $"{FormatDate(details.VoucherDate)}~" +
                $"{FormatNumber(details.GrossAmount)}~" +
                $"{FormatNumber(details.DiscountAmount)}~" +
                $"{FormatNumber(details.NetAmount)}~" +
                $"{details.Concept}~" +
                $"{details.BenefitDescription}";

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
    }
}
