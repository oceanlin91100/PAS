using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class GroupService
    {
        private ASSPAGROUPRepository db;

        public GroupService()
        {
            db = new ASSPAGROUPRepository();
        }

        /// <summary>取得所有 Group 資料</summary>
        /// <returns></returns>
        public List<AssessGroupViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<AssessGroupViewModel>();
            foreach (var item in DbResult)
            {
                AssessGroupViewModel model = new AssessGroupViewModel();
                model.Id = item.AGID;
                model.Name = item.AGNAME;
                model.JobCode = item.APJPCD;
                model.UnitCode = item.APUTCD;
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

        /// <summary>取得啟用 AssessGroup 資料</summary>
        /// <returns></returns>
        public List<AssessGroupViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(g => g.ASTATUS == status).ToList();
            var models = new List<AssessGroupViewModel>();
            foreach (var item in DbResult)
            {
                AssessGroupViewModel model = new AssessGroupViewModel();
                model.Id = item.AGID;
                model.Name = item.AGNAME;
                model.JobCode = item.APJPCD;
                model.UnitCode = item.APUTCD;
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


        /// <summary>取得單一 Group 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssessGroupViewModel Get(int id)
        {
            var item = db.GetById(id);
            AssessGroupViewModel model = new AssessGroupViewModel();
            model.Id = item.AGID;
            model.Name = item.AGNAME;
            model.JobCode = item.APJPCD;
            model.UnitCode = item.APUTCD;
            model.Status = item.ASTATUS;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;

            return model;
        }

        /// <summary>新增 Group 資訊</summary>
        /// <param name="models"></param>
        public void Add(AssessGroupViewModel models)
        {
            ASSPAGROUP item = new ASSPAGROUP();
            item.AGID = db.GetLastId() + 1;
            item.AGNAME = models.Name;
            item.APJPCD = models.JobCode;
            item.APUTCD = models.UnitCode;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 Group 資訊</summary>
        /// <param name="models"></param>
        public void Save(AssessGroupViewModel models)
        {
            var item = db.GetById(models.Id);
            item.AGID = db.GetLastId() + 1;
            item.AGNAME = models.Name;
            item.APJPCD = models.JobCode;
            item.APUTCD = models.UnitCode;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);
        }

        /// <summary>刪除 Group 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var group = db.GetById(id);
            db.Delete(group.AGID);
        }
    }
}


