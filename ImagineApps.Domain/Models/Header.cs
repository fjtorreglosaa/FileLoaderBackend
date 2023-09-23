namespace ImagineApps.Domain.Models
{
    public class Header : Model
    {
        public string Recordtype { get; set; }
        public string CheckNumber { get; set; }
        public string BankId { get; set; }
        public string AccountID { get; set; }
        public string CheckDate { get; set; }
        public string PayeeID { get; set; }
        public string PayeeName1 { get; set; }
        public string PayeeName2 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Address5 { get; set; }
        public string Checkamount { get; set; }
        public string PayorID { get; set; }
        public string AmountString { get; set; }
        public string TemplateID { get; set; }
    }
}
