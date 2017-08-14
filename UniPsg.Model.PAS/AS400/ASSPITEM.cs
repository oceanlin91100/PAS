namespace UniPsg.Model.PAS.AS400
{
    public class ASSPITEM
    {
        #region ScopeItem 考核項目資料檔
        public int ITEMID { get; set; }
        public string ITEMNAME { get; set; }
        public decimal WEIGHT { get; set; }
        public int ASID { get; set; }
        public int KPICID { get; set; }
        public string GROUPS { get; set; }
        public int ASTATUS { get; set; }
        public string DEF { get; set; }
        public string CTOR { get; set; }
        public string CTDA { get; set; }
        public string MDOR { get; set; }
        public string MDDA { get; set; }
        #endregion
    }
}
