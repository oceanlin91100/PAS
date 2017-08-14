using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class CategoryService
    {
        private ASSPACATRepository db;

        public CategoryService()
        {
            db = new ASSPACATRepository();
        }

        /// <summary>取得所有 Category 資料</summary>
        /// <returns></returns>
        public List<AssessCategoryViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<AssessCategoryViewModel>();
            foreach (var item in DbResult)
            {
                AssessCategoryViewModel model = new AssessCategoryViewModel();
                model.Id = item.ACID;
                model.Name = item.ACNAME;
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

        /// <summary>取得啟用 Category 資料</summary>
        /// <returns></returns>
        public List<AssessCategoryViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(c => c.ASTATUS == status).ToList();
            var models = new List<AssessCategoryViewModel>();
            foreach (var item in DbResult)
            {
                AssessCategoryViewModel model = new AssessCategoryViewModel();
                model.Id = item.ACID;
                model.Name = item.ACNAME;
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
        public AssessCategoryViewModel Get(int id)
        {
            var DbResult = db.GetById(id);
            AssessCategoryViewModel model = new AssessCategoryViewModel();
            model.Id = DbResult.ACID;
            model.Name = DbResult.ACNAME;
            model.Status = DbResult.ASTATUS;
            model.Definition = DbResult.DEF;
            model.Creator = DbResult.CTOR;
            model.CreatedDate = DbResult.CTDA;
            model.Modifier = DbResult.MDOR;
            model.ModifiedDate = DbResult.MDDA;
            return model;
        }

        /// <summary>新增 Category 資訊</summary>
        /// <param name="models"></param>
        public void Add(AssessCategoryViewModel models)
        {
            ASSPACAT item = new ASSPACAT();
            item.ACID = db.GetLastId() + 1;
            item.ACNAME = models.Name;
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
        public void Save(AssessCategoryViewModel models)
        {
            var DbResult = db.GetById(models.Id);
            DbResult.ACID = models.Id;
            DbResult.ACNAME = models.Name;
            DbResult.ASTATUS = models.Status;
            DbResult.DEF = models.Definition;
            DbResult.MDOR = models.Modifier;
            DbResult.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(DbResult, models.Id);
        }

        /// <summary>刪除 category 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var category = db.GetById(id);
            db.Delete(category.ACID);
        }
    }
}

