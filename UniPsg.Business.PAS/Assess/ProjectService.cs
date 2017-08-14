using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class ProjectService
    {
        private ASSPAPRJRepository db;
        private ASSPACATRepository db1;
        private ASSPAPOSNRepository db2;

        public ProjectService()
        {
            db = new ASSPAPRJRepository();
            db1 = new ASSPACATRepository();
            db2 = new ASSPAPOSNRepository();
        }

        /// <summary>取得所有 AssessProject 資料</summary>
        /// <returns></returns>
        public List<AssessProjectViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<AssessProjectViewModel>();
            foreach (var item in DbResult)
            {
                var result = db1.GetById(item.ACID);
                AssessProjectViewModel model = new AssessProjectViewModel();
                model.Id = item.APRID;
                model.Name = item.APRNAME;
                model.ViewStarDate = item.VSDA;
                model.ViewEndDate = item.VEDA;
                model.FromDate = item.FRDA;
                model.Deadline = item.DLDA;
                model.AssessYear = item.AYEAR;
                model.TryDatae = item.TRYDA;
                model.Groups = item.GROUPS;
                model.Forms = item.FROMS;
                model.CategoryId = item.ACID;
                model.CategoryName = result.ACNAME;
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
        
        /// <summary>取得啟用 AssessProject 資料</summary>
        /// <returns></returns>
        public List<AssessProjectViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(p => p.ASTATUS == status).ToList();
            var models = new List<AssessProjectViewModel>();
            foreach (var item in DbResult)
            {
                var result = db1.GetById(item.ACID);
                AssessProjectViewModel model = new AssessProjectViewModel();
                model.Id = item.APRID;
                model.Name = item.APRNAME;
                model.ViewStarDate = item.VSDA;
                model.ViewEndDate = item.VEDA;
                model.FromDate = item.FRDA;
                model.Deadline = item.DLDA;
                model.AssessYear = item.AYEAR;
                model.TryDatae = item.TRYDA;
                model.Groups = item.GROUPS;
                model.Forms = item.FROMS;
                model.CategoryId = item.ACID;
                model.CategoryName = result.ACNAME;
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

        /// <summary>取得所有 AssessProject 資料(條件)</summary> 
        /// <returns></returns> 
        public List<AssessProjectViewModel> Get(string groups)
        {  
            var DbResult = db.Get().Where(p => p.GROUPS == groups).ToList();
            var models = new List<AssessProjectViewModel>();
            foreach (var item in DbResult)
            {
                var result = db1.GetById(item.ACID);
                AssessProjectViewModel model = new AssessProjectViewModel();
                model.Id = item.APRID;
                model.Name = item.APRNAME;
                model.ViewStarDate = item.VSDA;
                model.ViewEndDate = item.VEDA;
                model.FromDate = item.FRDA;
                model.Deadline = item.DLDA;
                model.AssessYear = item.AYEAR;
                model.TryDatae = item.TRYDA;
                model.Groups = item.GROUPS;
                model.Forms = item.FROMS;
                model.CategoryId = item.ACID;
                model.CategoryName = result.ACNAME;
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

        /// <summary>取得單一 AssessProject 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssessProjectViewModel Get(int id)
        {
            var item = db.GetById(id);
            var result = db1.GetById(item.ACID);  
            AssessProjectViewModel model = new AssessProjectViewModel();
            model.Id = item.APRID;
            model.Name = item.APRNAME;
            model.ViewStarDate = item.VSDA;
            model.ViewEndDate = item.VEDA;
            model.FromDate = item.FRDA;
            model.Deadline = item.DLDA;
            model.AssessYear = item.AYEAR;
            model.TryDatae = item.TRYDA;
            model.Groups = item.GROUPS;
            model.Forms = item.FROMS;
            model.CategoryId = item.ACID;
            model.CategoryName = result.ACNAME;
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
        public void Add(AssessProjectViewModel models)
        {
            ASSPAPRJ item = new ASSPAPRJ();
            item.APRID = db.GetLastId() + 1;
            item.APRNAME = models.Name;
            item.VSDA = models.ViewStarDate;
            item.VEDA = models.ViewEndDate;            
            item.FRDA = models.FromDate;
            item.DLDA = models.Deadline;
            item.AYEAR = models.AssessYear;
            item.TRYDA = models.TryDatae;
            item.GROUPS = models.Groups;
            item.FROMS= models.Forms;
            item.ACID = models.CategoryId;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 AssessProject 資訊</summary>
        /// <param name="models"></param>
        public void Save(AssessProjectViewModel models)
        {
            var item = db.GetById(models.Id);
            item.APRID = models.Id;
            item.APRNAME = models.Name;
            item.VSDA = models.ViewStarDate;
            item.VEDA = models.ViewEndDate;
            item.FRDA = models.FromDate;
            item.DLDA = models.Deadline;
            item.AYEAR = models.AssessYear;
            item.TRYDA = models.TryDatae;
            item.GROUPS = models.Groups;
            item.FROMS = models.Forms;
            item.ACID = models.CategoryId;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;           
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            if (models.Status == 2)
                db2.Update(models.Id, models.Modifier, System.DateTime.Now.ToString("yyyyMMddHHmmss"));
            
            db.Update(item, models.Id);
        }

        /// <summary>刪除 Item 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var item = db.GetById(id);
            db.Delete(item.APRID);
        }
    }
}

