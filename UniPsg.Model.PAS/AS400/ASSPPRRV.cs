namespace UniPsg.Model.PAS.AS400
{
    public class ASSPPRRV
    {
        #region ProjectReview 自評/複評資料檔
       public string  PVID { get; set; }
        public int APRID { get; set;}
        public string EMPNO { get; set; }
        public string RVNO { get; set; }
        public int ASID { get; set; }
        public int ITEMID { get; set;}
        public string ITEMNAME { get; set; }
        public int KPICID { get; set; }
        public decimal WEIGHT { get; set; }
        public decimal RATE { get; set; }
        public decimal SCORE { get; set; }
        public decimal SCORE1 { get; set; }
        public string COMM { get; set;  }
        public string COMM1 { get; set;}
        public  string COMM2 { get; set; }
        public int ASTATUS { get; set; }
        public string DEF { get; set; }
        public string CTOR { get; set; }
        public string CTDA { get; set;}
        public string MDOR { get; set; }
        public string MDDA { get; set; }
        #endregion
    }
}
