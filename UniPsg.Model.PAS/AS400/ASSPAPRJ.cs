namespace UniPsg.Model.PAS.AS400
{
    public class ASSPAPRJ
    {
        #region AssessProject 考核專案資料檔
        public int APRID { get; set; }
        public string APRNAME { get; set; }
        public int VSDA { get; set; }
        public int VEDA { get; set; }
        public int FRDA { get; set; }
        public int DLDA { get; set; }
        public int AYEAR { get; set; }
        public int TRYDA { get; set; }
        public string GROUPS { get; set; }
        public string FROMS { get; set; }
        public int ACID { get; set; }
        public int ASTATUS { get; set; }
        public string DEF { get; set; }
        public string CTOR { get; set; }
        public string CTDA { get; set; }
        public string MDOR { get; set; }
        public string MDDA { get; set; }
        #endregion
    }
}
