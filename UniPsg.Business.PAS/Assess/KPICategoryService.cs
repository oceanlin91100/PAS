using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class KPICategoryService
    {
        private ASSPKPICATERepository db;
        public KPICategoryService()
        {
            db = new ASSPKPICATERepository();
        }

        /// <summary>取得所有 Category 資料</summary>
        /// <returns></returns>
        public List<KPICategoryViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<KPICategoryViewModel>();
            foreach (var item in DbResult)
            {
                KPICategoryViewModel model = new KPICategoryViewModel();
                model.Id = item.KPICID;
                model.Name = item.KPICNAME;
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

        /// <summary>取得啟用 KPICategory 資料</summary>
        /// <returns></returns>
        public List<KPICategoryViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(c => c.ASTATUS == status).ToList();
            var models = new List<KPICategoryViewModel>();
            foreach (var item in DbResult)
            {
                KPICategoryViewModel model = new KPICategoryViewModel();
                model.Id = item.KPICID;
                model.Name = item.KPICNAME;
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

        /// <summary>取得單一 Category 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public KPICategoryViewModel Get(int id)
        {
            var item = db.GetById(id);
            KPICategoryViewModel model = new KPICategoryViewModel();
            model.Id = item.KPICID;
            model.Name = item.KPICNAME;
            model.Status = item.ASTATUS;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 Category 資訊</summary>
        /// <param name="models"></param>
        public void Add(KPICategoryViewModel models)
        {
            ASSPKPICAT item = new ASSPKPICAT();
            item.KPICID = db.GetLastId() + 1;
            item.KPICNAME = models.Name;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 Category 資訊</summary>
        /// <param name="models"></param>
        public void Save(KPICategoryViewModel models)
        {
            var item = db.GetById(models.Id);
            item.KPICID = models.Id;
            item.KPICNAME = models.Name;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);
        }

        /// <summary>刪除 category 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var category = db.GetById(id);
            db.Delete(category.KPICID);
        }
    }
}

