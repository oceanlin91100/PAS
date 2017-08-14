using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Permission
{
    public class MenuService
    {
        private ASSPMENURepository db;
        private ASSPRMMAPRepository db1;        
        private ASSPRUMAPRepository db2;
        //private IRepostiory<RoleUserMapping> db2;

        public MenuService()
        {
            db = new ASSPMENURepository();
            db1 = new ASSPRMMAPRepository();
            db2 = new ASSPRUMAPRepository();
            //db2 = new GenericRepository<RoleUserMapping>();
        }

        /// <summary>取得所有 Menu 資料</summary>
        /// <returns></returns>
        public List<MenuViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<MenuViewModel>();
            foreach (var item in DbResult)
            {
                MenuViewModel model = new MenuViewModel();
                model.Id = item.MENUID;
                model.Name = item.MENUNAME;
                model.Controller = item.CONTR;
                model.Action = item.ACTIOM;
                model.Url = item.URL;
                model.OderSerial = item.ODER;
                model.ParentId = item.PARID;
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
        /// <summary>取得啟用 Menu 資料</summary>
        /// <returns></returns>
        public List<MenuViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(m => m.ASTATUS == status).ToList();
            var models = new List<MenuViewModel>();
            foreach (var item in DbResult)
            {
                MenuViewModel model = new MenuViewModel();
                model.Id = item.MENUID;
                model.Name = item.MENUNAME;
                model.Controller = item.CONTR;
                model.Action = item.ACTIOM;
                model.Url = item.URL;
                model.OderSerial = item.ODER;
                model.ParentId = item.PARID;
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

        /// <summary>取得單一 Menu 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MenuViewModel Get(int id)
        {
            var item = db.GetById(id);
            MenuViewModel model = new MenuViewModel();
            model.Id = item.MENUID;
            model.Name = item.MENUNAME;
            model.Controller = item.CONTR;
            model.Action = item.ACTIOM;
            model.Url = item.URL;
            model.OderSerial = item.ODER;
            model.ParentId = item.PARID;
            model.Status = item.ASTATUS;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 Menu 資訊</summary>
        /// <param name="models"></param>
        public void Add(MenuViewModel models)
        {
            ASSPMENU item = new ASSPMENU();
            item.MENUID = db.GetLastId() + 1;
            item.MENUNAME = models.Name;
            item.CONTR = models.Controller;
            item.ACTIOM = models.Action;
            item.URL = models.Url;
            item.PARID = (int)models.ParentId;
            item.ODER = models.OderSerial;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 Menu 資訊</summary>
        /// <param name="models"></param>
        public void Save(MenuViewModel models)
        {
            var item = db.GetById(models.Id);
            item.MENUID =models.Id;
            item.MENUNAME = models.Name;
            item.CONTR = models.Controller;
            item.ACTIOM = models.Action;
            item.URL = models.Url;
            item.PARID = (int)models.ParentId;
            item.ODER = models.OderSerial;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;           
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);
        }

        /// <summary>刪除 Menu 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var menu = db.GetById(id);
            db.Delete(menu.MENUID);
        }

        public List<MenuViewModel> GetRoleMenu(string users)
        {
            var roleUser = db2.Get().Where(ru => ru.EMPNO == users).FirstOrDefault();
            //var roleUser = db2.Get(ru => ru.EmployeeNo == users).FirstOrDefault();
            var roleMenu = db1.Get().Where(rm => rm.ROLEID == roleUser.ROLEID).ToList();
            //var roleMenu = db1.Get().Where(rm => rm.ROLEID == roleUser.RoleId).ToList();
            List<int> list = new List<int>();
            foreach (var item in roleMenu)
            {
                list.Add(item.MENUID);
            }
            var menu = db.Get().Where(m => list.Contains(m.MENUID)).ToList();
            var models = new List<MenuViewModel>();
            foreach (var item in menu)
            {
                MenuViewModel model = new MenuViewModel();
                model.Id = item.MENUID;
                model.Name = item.MENUNAME;
                model.Controller = item.CONTR;
                model.Action = item.ACTIOM;
                model.Url = item.URL;
                model.ParentId = item.PARID;
                model.Status = item.ASTATUS;
                model.OderSerial = item.ODER;
                model.Definition = item.DEF;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }
    }
}