using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class ItemService
    {
        private ASSPITEMRepository db;
        private ASSPKPICATERepository db1;
        private ASSPASCOPERepository db2;

        public ItemService()
        {
            db = new ASSPITEMRepository();
            db1 = new ASSPKPICATERepository();
            db2 = new ASSPASCOPERepository();
        }

        /// <summary>取得所有 Item 資料</summary>
        /// <returns></returns>
        public List<ScopeItemViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<ScopeItemViewModel>();
            foreach (var item in DbResult)
            {
                ScopeItemViewModel model = new ScopeItemViewModel();
                var result1 = db2.GetById(item.ASID);
                if (item.KPICID == 0)
                    model.KPICategoryName = "";
                else
                {
                    var result = db1.GetById(item.KPICID);
                    model.KPICategoryName = result.KPICNAME;
                }
                model.Id = item.ITEMID;
                model.ScopeId = item.ASID;
                model.ScopeName = result1.ASNAME;
                model.KPICategoryId = item.KPICID;
                model.Name = item.ITEMNAME;
                model.Weight = item.WEIGHT;
                model.Groups = item.GROUPS;
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

        /// <summary>取得啟用 Item 資料</summary>
        /// <returns></returns>
        public List<ScopeItemViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(i => i.ASTATUS == status).ToList();
            var models = new List<ScopeItemViewModel>();
            foreach (var item in DbResult)
            {
                ScopeItemViewModel model = new ScopeItemViewModel();
                var result1 = db2.GetById(item.ASID);
                if (item.KPICID == 0)
                    model.KPICategoryName = "";
                else
                {
                    var result = db1.GetById(item.KPICID);
                    model.KPICategoryName = result.KPICNAME;
                }
                model.Id = item.ITEMID;
                model.ScopeId = item.ASID;
                model.ScopeName = result1.ASNAME;
                model.KPICategoryId = item.KPICID;
                model.Name = item.ITEMNAME;
                model.Weight = item.WEIGHT;
                model.Groups = item.GROUPS;
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

        public List<ScopeItemViewModel> GetByScopeId(int scopeId)
        {
            var DbResult = db.Get().Where(i => i.ASID == scopeId).ToList();
            var models = new List<ScopeItemViewModel>();
            foreach (var item in DbResult)
            {
                ScopeItemViewModel model = new ScopeItemViewModel();
                var result1 = db2.GetById(item.ASID);
                if (item.KPICID == 0)
                    model.KPICategoryName = "";
                else
                {
                    var result = db1.GetById(item.KPICID);
                    model.KPICategoryName = result.KPICNAME;
                }
                model.Id = item.ITEMID;
                model.ScopeId = item.ASID;
                model.ScopeName = result1.ASNAME;
                model.KPICategoryId = item.KPICID;
                model.Name = item.ITEMNAME;
                model.Weight = item.WEIGHT;
                model.Groups = item.GROUPS;
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

        public List<ScopeItemViewModel> Get(int scopeId, string group)
        {
            var DbResult = db.Get().Where(i => i.ASID == scopeId && i.GROUPS.Contains(group)).ToList();
            var models = new List<ScopeItemViewModel>();
            foreach (var item in DbResult)
            {
                ScopeItemViewModel model = new ScopeItemViewModel();
                var result1 = db2.GetById(item.ASID);
                if (item.KPICID == 0)
                    model.KPICategoryName = "";
                else
                {
                    var result = db1.GetById(item.KPICID);
                    model.KPICategoryName = result.KPICNAME;
                }
                model.Id = item.ITEMID;
                model.ScopeId = item.ASID;
                model.ScopeName = result1.ASNAME;
                model.KPICategoryId = item.KPICID;
                model.Name = item.ITEMNAME;
                model.Weight = item.WEIGHT;
                model.Groups = item.GROUPS;
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

        /// <summary>取得單一 Item 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ScopeItemViewModel Get(int id)
        {
            var item = db.GetById(id);
            ScopeItemViewModel model = new ScopeItemViewModel();
            var result1 = db2.GetById(item.ASID);
            if (item.KPICID == 0)
                model.KPICategoryName = "";
            else
            {
                var result = db1.GetById(item.KPICID);
                model.KPICategoryName = result.KPICNAME;
            }
            model.Id = item.ITEMID;
            model.ScopeId = item.ASID;
            model.ScopeName = result1.ASNAME;
            model.KPICategoryId = item.KPICID;
            model.Name = item.ITEMNAME;
            model.Weight = item.WEIGHT;
            model.Groups = item.GROUPS;
            model.Status = item.ASTATUS;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 Item 資訊</summary>
        /// <param name="models"></param>
        public void Add(ScopeItemViewModel models)
        {
            ASSPITEM item = new ASSPITEM();
            item.ITEMID = db.GetLastId() + 1;
            item.ITEMNAME = models.Name;
            item.WEIGHT = models.Weight;
            item.ASID = models.ScopeId;
            item.KPICID = models.KPICategoryId;
            item.GROUPS = models.Groups;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 Item 資訊</summary>
        /// <param name="models"></param>
        public void Save(ScopeItemViewModel models)
        {
            var item = db.GetById(models.Id);
            item.ITEMID = models.Id;
            item.ITEMNAME = models.Name;
            item.WEIGHT = models.Weight;
            item.ASID = models.ScopeId;
            item.KPICID = models.KPICategoryId;
            item.GROUPS = models.Groups;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);
        }

        /// <summary>刪除 Item 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var item = db.GetById(id);
            db.Delete(item.ITEMID);
        }
    }
}

