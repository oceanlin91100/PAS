namespace UniPsg.Model.PAS.AS400
{
    public class ASSPRMMAP
    {
        #region RoleMenuMapping 角色與選單關係資料檔
        public int MAPID { get; set; }
        public int ROLEID { get; set; }
        public int MENUID { get; set; }
        public int ASTATUS { get; set; }
        public string DEF { get; set; }
        public string CTOR { get; set; }
        public string CTDA { get; set; }
        public string MDOR { get; set; }
        public string MDDA { get; set; }
        #endregion
    }
}
