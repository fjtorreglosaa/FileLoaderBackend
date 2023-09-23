namespace ImagineApps.Domain.Models
{
    public class Detail : Model
    {
        public string Recordtype { get; set; }
        public string CheckNumber { get; set; }
        public string BankID { get; set; }
        public string BankAccountNo { get; set; }
        public string CheckDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string VoucherNumber { get; set; }
        public string VoucherDate { get; set; }
        public string GrossAmount { get; set; }
        public string DiscountAmount { get; set; }
        public string NetAmount { get; set; }
        public string Concept { get; set; }
        public string BenefitDescription { get; set; }
    }
}
