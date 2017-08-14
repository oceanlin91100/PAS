using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Permission
{
    public class RoleMenuMappingService
    {
        private ASSPRMMAPRepository db;     

        public RoleMenuMappingService()
        {
            db = new ASSPRMMAPRepository();     
        }

        /// <summary>取得所有 RoleMenuMapping 資料</summary>
        /// <returns></returns>
        public List<RoleMenuMappingViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<RoleMenuMappingViewModel>();
            foreach (var item in DbResult)
            {
                RoleMenuMappingViewModel model = new RoleMenuMappingViewModel();
                model.Id = item.MAPID;
                model.RoleId = item.ROLEID;
                model.MenuId = item.MENUID;                
                model.Status = item.ASTATUS;
                model.Definition = item.DEF;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }
        
        /// <summary>取得單一 RoleMenuMapping 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleMenuMappingViewModel Get(int id)
        {
            var item = db.GetById(id);
            RoleMenuMappingViewModel model = new RoleMenuMappingViewModel();
            model.Id = item.MAPID;
            model.RoleId = item.ROLEID;
            model.MenuId = item.MENUID;
            model.Status = item.ASTATUS;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 RoleMenuMapping 資訊</summary>
        /// <param name="models"></param>
        public void Add(RoleMenuMappingViewModel models)
        {
            ASSPRMMAP item = new ASSPRMMAP();
            item.MAPID = db.GetLastId() + 1;
            item.ROLEID = models.RoleId;
            item.MENUID = models.MenuId;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            db.Insert(item);
        }

        /// <summary>儲存 RoleMenuMapping 資訊</summary>
        /// <param name="models"></param>
        public void Save(RoleMenuMappingViewModel models)
        {
            var item = db.GetById(models.Id);
            item.MAPID = models.Id;
            item.ROLEID = models.RoleId;
            item.MENUID = models.MenuId;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;         
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);
        }

        /// <summary>刪除 RoleMenuMapping 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var roleMenuMapping = db.GetById(id);
            db.Delete(roleMenuMapping.MAPID);
        }
    }
}