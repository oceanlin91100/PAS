namespace UniPsg.Model.PAS.AS400
{
    public class ASSPMENU
    {
        #region Menu 選單資料檔
        public int MENUID { get; set; }
        public string MENUNAME { get; set; }
        public string CONTR { get; set; }
        public string ACTIOM { get; set; }
        public string URL { get; set; }
        public int ODER { get; set;}
        public int PARID { get; set; }
        public int ASTATUS { get; set; }
        public string DEF { get; set; }
        public string CTOR { get; set; }
        public string CTDA { get; set;}
        public string MDOR { get; set; }
        public string MDDA { get; set; }
        #endregion
    }
}
