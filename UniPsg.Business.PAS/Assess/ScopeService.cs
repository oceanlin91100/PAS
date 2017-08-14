using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class ScopeService
    {
        private ASSPASCOPERepository db;
        public ScopeService()
        {
            db = new ASSPASCOPERepository();
        }

        /// <summary>取得所有 AssessScope 資料</summary>
        /// <returns></returns>
        public List<AssessScopeViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<AssessScopeViewModel>();
            foreach (var item in DbResult)
            {
                AssessScopeViewModel model = new AssessScopeViewModel();
                model.Id = item.ASID;
                model.Name = item.ASNAME;
                model.HasItem = item.HSITEM;
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

        /// <summary>取得啟用 AssessScope 資料</summary>
        /// <returns></returns>
        public List<AssessScopeViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(s => s.ASTATUS == status).ToList();
            var models = new List<AssessScopeViewModel>();
            foreach (var item in DbResult)
            {
                AssessScopeViewModel model = new AssessScopeViewModel();
                model.Id = item.ASID;
                model.Name = item.ASNAME;
                model.HasItem = item.HSITEM;
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

        /// <summary>取得單一 AssessScope 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssessScopeViewModel Get(int id)
        {
            var item = db.GetById(id);
            AssessScopeViewModel model = new AssessScopeViewModel();
            model.Id = item.ASID;
            model.Name = item.ASNAME;
            model.HasItem = item.HSITEM;
            model.Status = item.ASTATUS;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 AssessScope 資訊</summary>
        /// <param name="models"></param>
        public void Add(AssessScopeViewModel models)
        {
            ASSPASCOPE item = new ASSPASCOPE();
            item.ASID = db.GetLastId() + 1;
            item.ASNAME = models.Name;
            item.HSITEM = models.HasItem;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 AssessScope 資訊</summary>
        /// <param name="models"></param>
        public void Save(AssessScopeViewModel models)
        {
            var item = db.GetById(models.Id);
            item.ASID = db.GetLastId() + 1;
            item.ASNAME = models.Name;
            item.HSITEM = models.HasItem;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);
        }

        /// <summary>刪除 category 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var scope = db.GetById(id);
            db.Delete(scope.ASID);
        }
    }
}

