using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using System.Data;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Permission
{
    public class RoleService
    {
        private ASSPROLERepository db;
        public RoleService()
        {
            db = new ASSPROLERepository();
        }

        /// <summary>取得所有 Role 資料</summary>
        /// <returns></returns>
        public List<RoleViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<RoleViewModel>();
            foreach (var item in DbResult)
            {
                RoleViewModel model = new RoleViewModel();
                model.Id = item.ROLEID;
                model.Name = item.ROLENAME;
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

        /// <summary>取得啟用 Role 資料</summary>
        /// <returns></returns>
        public List<RoleViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(r => r.ASTATUS ==  status).ToList();
            var models = new List<RoleViewModel>();
            foreach (var item in DbResult)
            {
                RoleViewModel model = new RoleViewModel();
                model.Id = item.ROLEID;
                model.Name = item.ROLENAME;
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

        /// <summary>取得單一 Role 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoleViewModel Get(int id)
        {
            var item = db.GetById(id);
            RoleViewModel model = new RoleViewModel();
            model.Id = item.ROLEID;
            model.Name = item.ROLENAME;
            model.Status = item.ASTATUS;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 Role 資訊</summary>
        /// <param name="models"></param>
        public void Add(RoleViewModel models)
        {
            ASSPROLE item = new ASSPROLE();            
            item.ROLEID = db.GetLastId() + 1;
            item.ROLENAME = models.Name;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            
            db.Insert(item);
        }

        /// <summary>儲存 Role 資訊</summary>
        /// <param name="models"></param>
        public void Save(RoleViewModel models)
        {
            var item = db.GetById(models.Id);
            item.ROLEID = models.Id;
            item.ROLENAME = models.Name;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;            
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);
        }

        /// <summary>刪除 Role 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var role = db.GetById(id);
            db.Delete(role.ROLEID);
        }

        public List<HBPDUTViewModel> GetHBPDUT(string employeeNo)
        {
            List<HBPDUTViewModel> model = new List<HBPDUTViewModel>();
            HBPDUTYRepository dbWF = new HBPDUTYRepository();
            DataTable dt1 = dbWF.GetHBPDUTY(employeeNo);
            foreach (DataRow dr1 in dt1.Rows)
            {
                HBPDUTViewModel item = new HBPDUTViewModel();
                item.BRHCOD = dr1["BRHCOD"].ToString().Trim();
                item.DEPTCD = dr1["DEPTCD"].ToString().Trim();
                item.DEPTNAM = dr1["DEPTNAM"].ToString().Trim();
                item.EMPNO = dr1["EMPNO"].ToString().Trim();
                model.Add(item);
            }
            return model;
        }
    }
}