namespace UniPsg.Model.PAS.AS400
{
    public class ASSPRUMAP
    {
        #region RoleUserMapping 角色與使用者關係資料檔
        public int MAPID { get; set; }
        public int ROLEID { get; set; }
        public int MENUID { get; set; }
        public string EMPNO { get; set; }
        public int ASTATUS { get; set; }
        public string DEF { get; set; }
        public string CTOR { get; set; }
        public string CTDA { get; set; }
        public string MDOR { get; set; }
        public string MDDA { get; set; }

        #endregion
    }
}
