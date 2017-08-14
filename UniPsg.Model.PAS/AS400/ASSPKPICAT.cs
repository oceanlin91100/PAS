namespace UniPsg.Model.PAS.AS400
{
    public class ASSPKPICAT
    {
        #region KPICategory KPI分類資料檔
        public int KPICID { get; set; }
        public string KPICNAME { get; set; }
        public int ASTATUS { get; set;}
        public string DEF { get; set; }
        public string CTOR { get; set; }
        public string CTDA { get; set; }
        public string MDOR { get; set; }
        public string MDDA { get; set;}
        #endregion
    }
}
