namespace UniPsg.Model.PAS.AS400
{
    public class ASSPSCORE
    {
        #region ProjectScore 考核成績資料檔
        public string PSID { get; set; }
        public int APRID { get; set; }
        public string EMPNO { get; set; }
        public string RVNO { get; set; }
        public decimal KPISCORE { get; set; }
        public decimal CORESCORE { get; set; }
        public decimal MAGSCORE { get; set; }
        public decimal BPSCORE { get; set; }
        public decimal TOTSCORE { get; set; }
        public int ARID { get; set; }
        public string DEVCOMM { get; set; }
        public string COMM { get; set; }
        public int ORDER { get; set;}
        public int ASTATUS { get; set; }
        public string DEF { get; set; }
        public string CTOR { get; set; }
        public string CTDA { get; set; }
        public string MDOR { get; set; }
        public string MDDA { get; set; }
        #endregion
    }
}
