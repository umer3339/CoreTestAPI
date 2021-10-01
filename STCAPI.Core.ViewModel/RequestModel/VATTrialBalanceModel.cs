namespace STCAPI.Core.ViewModel.RequestModel
{
    public class VATTrialBalanceModel
    {
        public string Account { get; set; }
        public string Description { get; set; }
        public string BeginingBalance { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string Activity { get; set; }
        public string EndingBalance { get; set; }
    }
}
