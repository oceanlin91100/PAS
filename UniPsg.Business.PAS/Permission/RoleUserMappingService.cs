using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Permission
{
    public class RoleUserMappingService
    {
        private ASSPRUMAPRepository db;
        private ASSPROLERepository db1;

        public RoleUserMappingService()
        {
            db = new ASSPRUMAPRepository();
            db1 = new ASSPROLERepository();
        }

        /// <summary>取得所有 RoleUserMapping 資料</summary>
        /// <returns></returns>
        public List<RoleUserMappingViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<RoleUserMappingViewModel>();
            foreach (var item in DbResult)
            {
                RoleUserMappingViewModel model = new RoleUserMappingViewModel();
                model.Id = item.MAPID;
                model.RoleId = item.ROLEID;
                model.MenuId = item.MENUID;
                model.EmployeeNo = item.EMPNO;
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

        /// <summary>取得所有 RoleUserMapping 資料</summary>
        /// <returns></returns>
        public string  GetRole(string employeeNo)
        {
            string role = "User";
            var DbResult = db.Get().Where(ru => ru.EMPNO == employeeNo).FirstOrDefault();

            if (DbResult != null)
            {
                var result = db1.GetById(DbResult.ROLEID);
                role = result.ROLENAME;
            }
            return role;
        }
        
        /// <summary>取得單一 RoleUserMapping 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleUserMappingViewModel Get(int id)
        {
            var item = db.GetById(id);
            RoleUserMappingViewModel model = new RoleUserMappingViewModel();
            model.Id = item.MAPID;
            model.RoleId = item.ROLEID;
            model.MenuId = item.MENUID;
            model.EmployeeNo = item.EMPNO;
            model.Status = item.ASTATUS;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 RoleUserMapping 資訊</summary>
        /// <param name="models"></param>
        public void Add(RoleUserMappingViewModel models)
        {
            ASSPRUMAP item = new ASSPRUMAP();
            item.MAPID = db.GetLastId() + 1;
            item.ROLEID = models.RoleId;
            item.MENUID = models.MenuId;
            item.EMPNO = models.EmployeeNo;
            item.ASTATUS = models.Status;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 RoleUserMapping 資訊</summary>
        /// <param name="models"></param>
        public void Save(RoleUserMappingViewModel models)
        {
            var item = db.GetById(models.Id);
            item.MAPID = db.GetLastId() + 1;
            item.ROLEID = models.RoleId;
            item.MENUID = models.MenuId;
            item.EMPNO = models.EmployeeNo;
            item.ASTATUS = models.Status;            
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);
        }

        /// <summary>刪除 RoleUserMapping 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var roleUserMapping = db.GetById(id);
            db.Delete(roleUserMapping.MAPID);
        }
    }
}
