using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class AuditorService
    {
        private ASSPAUDRepository db;

        public AuditorService()
        {
            db = new ASSPAUDRepository();
        }

        /// <summary>取得所有 Auditor 資料</summary>
        /// <returns></returns>
        public List<AuditorViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<AuditorViewModel>();
            foreach (var item in DbResult)
            {
                AuditorViewModel model = new AuditorViewModel();
                model.Id = item.AUDID;
                model.BranchCode = item.BRCD;
                model.Manager = item.MAGNO;
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

        /// <summary>取得啟用 Auditor 資料</summary>
        /// <returns></returns>
        public List<AuditorViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(c => c.ASTATUS == status).ToList();
            var models = new List<AuditorViewModel>();
            foreach (var item in DbResult)
            {
                AuditorViewModel model = new AuditorViewModel();
                model.Id = item.AUDID;
                model.BranchCode = item.BRCD;
                model.Manager = item.MAGNO;
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


        /// <summary>取得單一 Auditor 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AuditorViewModel Get(int id)
        {
            var item = db.GetById(id);
            AuditorViewModel model = new AuditorViewModel();
            model.Id = item.AUDID;
            model.BranchCode = item.BRCD;
            model.Manager = item.MAGNO;
            model.Status = item.ASTATUS;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 Auditor 資訊</summary>
        /// <param name="models"></param>
        public void Add(AuditorViewModel models)
        {
            ASSPAUD item = new ASSPAUD();
            item.AUDID = db.GetLastId() + 1;
            item.BRCD = models.BranchCode;
            item.MAGNO = models.Manager;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 Auditor 資訊</summary>
        /// <param name="models"></param>
        public void Save(AuditorViewModel models)
        {
            var DbResult = db.GetById(models.Id);
            DbResult.AUDID = models.Id;
            DbResult.BRCD = models.BranchCode;
            DbResult.MAGNO = models.Manager;
            DbResult.ASTATUS = models.Status;
            DbResult.DEF = models.Definition;
            DbResult.MDOR = models.Modifier;
            DbResult.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(DbResult, models.Id);
        }

        /// <summary>刪除 Auditor 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var result = db.GetById(id);
            db.Delete(result.AUDID);
        }
    }
}

