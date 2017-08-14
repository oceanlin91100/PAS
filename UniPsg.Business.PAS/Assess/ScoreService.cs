using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;
using System;

namespace UniPsg.Business.PAS.Assess
{
    public class ScoreService
    {
        private ASSPSCORERepository db;
        private ASSPAFORGRepository db1;

        public ScoreService()
        {
            db = new ASSPSCORERepository();
            db1 = new ASSPAFORGRepository();
        }

        /// <summary>取得所有 ProjectScore 資料</summary>
        /// <returns></returns>
        public List<ProjectScoreViewModel> Get()
        {
            var DbResult = db.Get().ToList();           
            var models = new List<ProjectScoreViewModel>();

            foreach (var item in DbResult)
            {
                var result = db1.Get().Where(o => o.APRID == item.APRID && o.EMPNO == item.RVNO).FirstOrDefault();
                ProjectScoreViewModel model = new ProjectScoreViewModel();
                model.Id = item.PSID;
                model.ProjectId = item.APRID;
                model.EmployeeNo = item.EMPNO;
                model.Reviewer = item.RVNO;
                model.ReviewName = result.EMPNAME;
                model.KPIScore = item.KPISCORE;
                model.CoreScore = item.CORESCORE;
                model.ManageScore = item.MAGSCORE;
                model.BPScore = item.BPSCORE;
                model.TotalScore = item.TOTSCORE;                    
                model.RankId = item.ARID;
                model.DevlopComment = item.DEVCOMM;
                model.Comment = item.COMM;
                model.OderSerial = item.ORDER;
                model.Status = item.ASTATUS;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }

        /// <summary>取得啟用 ProjectScore 資料</summary>
        /// <returns></returns>
        public List<ProjectScoreViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(s => s.ASTATUS == status).ToList();
            var models = new List<ProjectScoreViewModel>();

            foreach (var item in DbResult)
            {
                var result = db1.Get().Where(o => o.APRID == item.APRID && o.EMPNO == item.RVNO).FirstOrDefault();
                ProjectScoreViewModel model = new ProjectScoreViewModel();
                model.Id = item.PSID;
                model.ProjectId = item.APRID;
                model.EmployeeNo = item.EMPNO;
                model.Reviewer = item.RVNO;
                model.ReviewName = result.EMPNAME;
                model.KPIScore = item.KPISCORE;
                model.CoreScore = item.CORESCORE;
                model.ManageScore = item.MAGSCORE;
                model.BPScore = item.BPSCORE;
                model.TotalScore = item.TOTSCORE;
                model.RankId = item.ARID;
                model.DevlopComment = item.DEVCOMM;
                model.Comment = item.COMM;
                model.OderSerial = item.ORDER;
                model.Status = item.ASTATUS;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }

        /// <summary>取得 ProjectScore 資料</summary>
        /// <returns></returns>
        public List<ProjectScoreViewModel> Get(int projectId, string employeeNo)
        {
            var DbResult = db.Get().Where(r => r.APRID == projectId && r.EMPNO == employeeNo).ToList();
            var models = new List<ProjectScoreViewModel>();

            foreach (var item in DbResult)
            {
                var result = db1.Get().Where(o => o.APRID == item.APRID && o.EMPNO == item.RVNO).FirstOrDefault();
                ProjectScoreViewModel model = new ProjectScoreViewModel();
                model.Id = item.PSID;
                model.ProjectId = item.APRID;
                model.EmployeeNo = item.EMPNO;
                model.Reviewer = item.RVNO;
                model.ReviewName = result.EMPNAME;
                model.KPIScore = item.KPISCORE;
                model.CoreScore = item.CORESCORE;
                model.ManageScore = item.MAGSCORE;
                model.BPScore = item.BPSCORE;
                model.TotalScore = item.TOTSCORE;
                model.RankId = item.ARID;
                model.DevlopComment = item.DEVCOMM;
                model.Comment = item.COMM;
                model.OderSerial = item.ORDER;
                model.Status = item.ASTATUS;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }
        
        /// <summary>取得單一 ProjectScore 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProjectScoreViewModel Get(string id)
        {
            var DbResult = db.GetById(id);
            var result1 = db1.Get().Where(o => o.APRID == DbResult.APRID && o.EMPNO == DbResult.RVNO).FirstOrDefault();
            ProjectScoreViewModel model = new ProjectScoreViewModel();
            model.Id = DbResult.PSID;
            model.ProjectId = DbResult.APRID;
            model.EmployeeNo = DbResult.EMPNO;
            model.Reviewer = DbResult.RVNO;
            model.ReviewName = result1.EMPNAME;
            model.KPIScore = DbResult.KPISCORE;
            model.CoreScore = DbResult.CORESCORE;
            model.ManageScore = DbResult.MAGSCORE;
            model.BPScore = DbResult.BPSCORE;
            model.TotalScore = DbResult.TOTSCORE;
            model.RankId = DbResult.ARID;
            model.DevlopComment = DbResult.DEVCOMM;
            model.Comment = DbResult.COMM;
            model.OderSerial = DbResult.ORDER;
            model.Status = DbResult.ASTATUS;
            model.Creator = DbResult.CTOR;
            model.CreatedDate = DbResult.CTDA;
            model.Modifier = DbResult.MDOR;
            model.ModifiedDate = DbResult.MDDA;
            return model;
        }

        /// <summary>新增 ProjectScore 資訊</summary>
        /// <param name="models"></param>
        public void Add(ProjectScoreViewModel models)
        {
            ASSPSCORE item = new ASSPSCORE();

            item.PSID = Guid.NewGuid().ToString();
            item.APRID = models.ProjectId;
            item.EMPNO = models.EmployeeNo;
            item.RVNO = models.Reviewer;
            item.KPISCORE = models.KPIScore;
            item.CORESCORE = models.CoreScore;
            item.MAGSCORE = models.ManageScore;
            item.BPSCORE = models.BPScore;
            item.TOTSCORE = models.TotalScore;
            item.ARID = models.RankId;
            item.DEVCOMM = models.DevlopComment;
            item.COMM = models.Comment;
            item.ORDER = models.OderSerial;
            item.ASTATUS = models.Status;
            item.DEF = "";
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 ProjectScore 資訊</summary>
        /// <param name="models"></param>
        public void Save(ProjectScoreViewModel models)
        {
            var item = db.GetById(models.Id);
            item.PSID = models.Id;
            item.APRID = models.ProjectId;
            item.EMPNO = models.EmployeeNo;
            item.RVNO = models.Reviewer;
            item.KPISCORE = models.KPIScore;
            item.CORESCORE = models.CoreScore;
            item.MAGSCORE = models.ManageScore;
            item.BPSCORE = models.BPScore;
            item.TOTSCORE = models.TotalScore;
            item.ARID = models.RankId;
            item.DEVCOMM = models.DevlopComment;
            item.COMM = models.Comment;
            item.ORDER = models.OderSerial;
            item.ASTATUS = models.Status;            
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);

        }

        /// <summary>刪除 ProjectScore 資訊</summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            var score = db.GetById(id);
            db.Delete(score.PSID);
        }

        public void AddManageFlow(int prointId, string employeeNo, List<string> employees, string editer)
        {
            int i = 0;
            foreach (string reiewer in employees)
            {
                var index = employees.FindIndex(e => e.Contains(reiewer));
                if (reiewer != employeeNo)
                {
                    ASSPSCORE item = new ASSPSCORE();
                    item.PSID = Guid.NewGuid().ToString();
                    item.APRID = prointId;
                    item.EMPNO = employeeNo;
                    item.RVNO = reiewer;
                    item.KPISCORE = 0;
                    item.CORESCORE = 0;
                    item.MAGSCORE = 0;
                    item.BPSCORE = 0;
                    item.TOTSCORE = 0;
                    item.ARID = 0;
                    item.DEVCOMM = string.Empty;
                    item.COMM = string.Empty;
                    item.ORDER = index - 1;
                    item.ASTATUS = 0;
                    item.CTOR = editer;
                    item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    item.MDOR = editer;
                    item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

                    db.Insert(item);
                }
            }

        }
    }
}



