namespace UniPsg.Model.PAS.AS400
{
    public class ASSPARANK
    {
        #region AssessRanking 考核等第資料檔
        public int ARID { get; set; }
        public string ARNAME { get; set; }
        public decimal WEIGHT { get; set; }
        public int ASTATUS { get; set;}
        public string DEF { get; set;}
        public string CTOR { get; set; }
        public string CTDA { get; set; }
        public string MDOR { get; set; }
        public string MDDA { get; set; }
        #endregion 
    }
}
