using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using System;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class ReviewService
    {
        private ASSPPRRVRepository db;
        private ASSPKPICATERepository db1;
        private ASSPSCORERepository db2;
        public ReviewService()
        {
            db = new ASSPPRRVRepository();
            db1 = new ASSPKPICATERepository();
            db2 = new ASSPSCORERepository();
        }
        /// <summary>取得所有 ProjectReview 資料</summary>
        /// <returns></returns>
        public List<ProjectReviewViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<ProjectReviewViewModel>();
            foreach (var item in DbResult)
            {
                var result = db1.GetById(item.KPICID);
                ProjectReviewViewModel model = new ProjectReviewViewModel();
                model.Id = item.PVID;
                model.ProjectId = item.APRID;
                model.EmployeeNo = item.EMPNO;
                model.Reviewer = item.RVNO;
                model.ScopeId = item.ASID;
                model.ItemId = item.ITEMID;
                model.ItemName = item.ITEMNAME;
                model.KPICategoryId = item.KPICID;
                model.KPICategoryName = result.KPICNAME;
                model.Weight = item.WEIGHT;
                model.Rate = item.RATE;
                model.Score = item.SCORE;
                model.Score1 = item.SCORE1;
                model.Comment = item.COMM;
                model.Comment1 = item.COMM1;
                model.Comment2 = item.COMM2;
                model.Status = item.ASTATUS;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }

        /// <summary>取得啟用 ProjectReview 資料</summary>
        /// <returns></returns>
        public List<ProjectReviewViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(r => r.ASTATUS == status).ToList();
            var models = new List<ProjectReviewViewModel>();
            foreach (var item in DbResult)
            {
                var result = db1.GetById(item.KPICID);
                ProjectReviewViewModel model = new ProjectReviewViewModel();
                model.Id = item.PVID;
                model.ProjectId = item.APRID;
                model.EmployeeNo = item.EMPNO;
                model.Reviewer = item.RVNO;
                model.ScopeId = item.ASID;
                model.ItemId = item.ITEMID;
                model.ItemName = item.ITEMNAME;
                model.KPICategoryId = item.KPICID;
                model.KPICategoryName = result.KPICNAME;
                model.Weight = item.WEIGHT;
                model.Rate = item.RATE;
                model.Score = item.SCORE;
                model.Score1 = item.SCORE1;
                model.Comment = item.COMM;
                model.Comment1 = item.COMM1;
                model.Comment2 = item.COMM2;
                model.Status = item.ASTATUS;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }

        /// <summary>取得 ProjectReview 資料</summary>
        /// <returns></returns>
        public List<ProjectReviewViewModel> Get(int projectId, string employeeNo)
        {
            var DbResult = db.Get().Where(r => r.APRID == projectId && r.EMPNO == employeeNo).ToList();
            var models = new List<ProjectReviewViewModel>();
            foreach (var item in DbResult)
            {
                var result = db1.GetById(item.KPICID);
                ProjectReviewViewModel model = new ProjectReviewViewModel();
                model.Id = item.PVID;
                model.ProjectId = item.APRID;
                model.EmployeeNo = item.EMPNO;
                model.Reviewer = item.RVNO;
                model.ScopeId = item.ASID;
                model.ItemId = item.ITEMID;
                model.ItemName = item.ITEMNAME;
                model.KPICategoryId = item.KPICID;
                model.KPICategoryName = result.KPICNAME;
                model.Weight = item.WEIGHT;
                model.Rate = item.RATE;
                model.Score = item.SCORE;
                model.Score1 = item.SCORE1;
                model.Comment = item.COMM;
                model.Comment1 = item.COMM1;
                model.Comment2 = item.COMM2;
                model.Status = item.ASTATUS;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }

        /// <summary>取得單一 ProjectReview 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProjectReviewViewModel Get(string id)
        {
            var item = db.GetById(id);
            var result = db1.GetById(item.KPICID);
            ProjectReviewViewModel model = new ProjectReviewViewModel();
            model.Id = item.PVID;
            model.ProjectId = item.APRID;
            model.EmployeeNo = item.EMPNO;
            model.Reviewer = item.RVNO;
            model.ScopeId = item.ASID;
            model.ItemId = item.ITEMID;
            model.ItemName = item.ITEMNAME;
            model.KPICategoryId = item.KPICID;
            model.KPICategoryName = result.KPICNAME;
            model.Weight = item.WEIGHT;
            model.Rate = item.RATE;
            model.Score = item.SCORE;
            model.Score1 = item.SCORE1;
            model.Comment = item.COMM;
            model.Comment1 = item.COMM1;
            model.Comment2 = item.COMM2;
            model.Status = item.ASTATUS;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>取得單一 ProjectReview 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProjectReviewViewModel GetReviewByManager(int projectId, string employeeNo, string reviewer)
        {
            ProjectReviewViewModel model = new ProjectReviewViewModel();
            var item = db.Get().Where(r => r.APRID == projectId && r.EMPNO == employeeNo && r.RVNO == reviewer).FirstOrDefault();
            var result1 = db1.GetById(item.KPICID);
            model.Id = item.PVID;
            model.ProjectId = item.APRID;
            model.EmployeeNo = item.EMPNO;
            model.Reviewer = item.RVNO;
            model.ScopeId = item.ASID;
            model.ItemId = item.ITEMID;
            model.ItemName = item.ITEMNAME;
            model.KPICategoryId = item.KPICID;
            model.KPICategoryName = result1.KPICNAME;
            model.Weight = item.WEIGHT;
            model.Rate = item.RATE;
            model.Score = item.SCORE;
            model.Score1 = item.SCORE1;
            model.Comment = item.COMM;
            model.Comment1 = item.COMM1;
            model.Comment2 = item.COMM2;
            model.Status = item.ASTATUS;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 (ProjectReview 資訊</summary>
        /// <param name="models"></param>
        public void Add(ProjectReviewViewModel models)
        {
            ASSPPRRV item = new ASSPPRRV();
            item.PVID = Guid.NewGuid().ToString();
            item.APRID = models.ProjectId;
            item.EMPNO = models.EmployeeNo;
            item.RVNO = models.Reviewer;
            item.ASID = models.ScopeId;
            item.ITEMID = models.ItemId;
            item.ITEMNAME = models.ItemName;
            item.KPICID = models.KPICategoryId;
            item.WEIGHT = models.Weight;
            item.RATE = models.Rate;
            item.SCORE = models.Score;
            item.SCORE1 = models.Score1;
            item.COMM = models.Comment;
            item.COMM1 = models.Comment1;
            item.COMM2 = models.Comment2;
            item.ASTATUS = models.Status;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 (ProjectReview 資訊</summary>
        /// <param name="models"></param>
        public void Save(ProjectReviewViewModel models)
        {
            var item = db.GetById(models.Id);
            item.PVID = models.Id;
            item.APRID = models.ProjectId;
            item.EMPNO = models.EmployeeNo;
            item.RVNO = models.Reviewer;
            item.ASID = models.ScopeId;
            item.ITEMID = models.ItemId;
            item.ITEMNAME = models.ItemName;
            item.KPICID = models.KPICategoryId;
            item.WEIGHT = models.Weight;
            item.RATE = models.Rate;
            item.SCORE = models.Score;
            item.SCORE1 = models.Score1;
            item.COMM = models.Comment;
            item.COMM1 = models.Comment1;
            item.COMM2 = models.Comment2;
            item.ASTATUS = models.Status;           
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);
        }

        /// <summary>刪除 ProjectReview 資訊</summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            var pv = db.GetById(id);
            db.Delete(pv.PVID);
        }

        /// <summary>更新 (ProjectReview 資訊</summary>
        /// <param name="models"></param>
        public void Update(List<ProjectReviewViewModel> models, string status, string manager)
        {
            int projectId = 0;
            string employeeNo = string.Empty;
            string editer = string.Empty;
            string reviewer = string.Empty;
            foreach (var model in models)
            {
                if (model.Id == null)
                {
                    ASSPPRRV item = new ASSPPRRV();
                    item.PVID = Guid.NewGuid().ToString();
                    item.APRID = model.ProjectId;
                    item.EMPNO = model.EmployeeNo;
                    item.RVNO = model.Reviewer;
                    item.ASID = model.ScopeId;
                    item.ITEMID = model.ItemId;
                    item.ITEMNAME = model.ItemName;
                    item.KPICID = model.KPICategoryId;
                    item.WEIGHT = model.Weight;
                    item.RATE = model.Rate;
                    item.SCORE = model.Score;
                    item.SCORE1 = model.Score1;
                    item.COMM = model.Comment;
                    item.COMM1 = model.Comment1;
                    item.COMM2 = model.Comment2;
                    item.ASTATUS = model.Status;
                    item.CTOR = model.Creator;
                    item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    item.MDOR = model.Modifier;
                    item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

                    projectId = model.ProjectId;
                    employeeNo = model.EmployeeNo;
                    editer = model.Modifier;
                    reviewer = model.EmployeeNo;

                    db.Insert(item);
                }
                else
                {
                    var DbResult = db.GetById(model.Id);
                    if (DbResult == null)
                    {
                        ASSPPRRV item = new ASSPPRRV();
                        item.PVID = Guid.NewGuid().ToString();
                        item.APRID = model.ProjectId;
                        item.EMPNO = model.EmployeeNo;
                        item.RVNO = model.Reviewer;
                        item.ASID = model.ScopeId;
                        item.ITEMID = model.ItemId;
                        item.ITEMNAME = model.ItemName;
                        item.KPICID = model.KPICategoryId;
                        item.WEIGHT = model.Weight;
                        item.RATE = model.Rate;
                        item.SCORE = model.Score;
                        item.SCORE1 = model.Score1;
                        item.COMM = model.Comment;
                        item.COMM1 = model.Comment1;
                        item.COMM2 = model.Comment2;
                        item.ASTATUS = model.Status;
                        item.CTOR = model.Creator;
                        item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                        item.MDOR = model.Modifier;
                        item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

                        projectId = model.ProjectId;
                        employeeNo = model.EmployeeNo;
                        editer = model.Modifier;
                        reviewer = model.EmployeeNo;

                        db.Insert(item);
                    }
                    else
                    {
                        DbResult.PVID = model.Id;
                        DbResult.APRID = model.ProjectId;
                        DbResult.EMPNO = model.EmployeeNo;
                        DbResult.RVNO = model.Reviewer;
                        DbResult.ASID = model.ScopeId;
                        DbResult.ITEMID = model.ItemId;
                        DbResult.ITEMNAME = model.ItemName;
                        DbResult.KPICID = model.KPICategoryId;
                        DbResult.WEIGHT = model.Weight;
                        DbResult.RATE = model.Rate;
                        DbResult.SCORE = model.Score;
                        DbResult.SCORE1 = model.Score1;
                        DbResult.COMM = model.Comment;
                        DbResult.COMM1 = model.Comment1;
                        DbResult.COMM2 = model.Comment2;
                        DbResult.ASTATUS = model.Status;
                        DbResult.MDOR = model.Modifier;
                        DbResult.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

                        projectId = model.ProjectId;
                        employeeNo = model.EmployeeNo;
                        editer = model.Modifier;
                        reviewer = model.EmployeeNo;

                        db.Update(DbResult, model.Id);
                    }
                }
            }
            if (status == "reviw")
                Review(projectId, employeeNo, reviewer, editer, manager);
        }

        public void Review(int projectId, string employeeNo, string reviewer, string editer, string manager)
        {
            PersonService ps = new PersonService();
            if (manager == "0")
            {
                OrganizationService org = new OrganizationService();
                var result = org.Get(projectId, reviewer);
                var employees = result.FlowPath.Split(',').ToList();
                var index = employees.FindIndex(e => e.Contains(reviewer));
                string managerNo = employees[index + 1];
                ps.UpdateReviewer(projectId, employeeNo, managerNo, editer);                
                ScoreService service = new ScoreService();
                var scores = db2.Get().Where(s => s.APRID == projectId && s.EMPNO == employeeNo).ToList();
                if (scores.Count == 0)
                    service.AddManageFlow(projectId, employeeNo, employees, editer);
            }
            else
            {
                var scores = db2.Get().Where(s => s.APRID == projectId && s.EMPNO == employeeNo && s.RVNO == reviewer).FirstOrDefault();
                var manger = db2.Get().Where(s => s.APRID == scores.APRID && s.EMPNO == scores.EMPNO && s.ORDER == scores.ORDER + 1).FirstOrDefault();

                ps.UpdateReviewer(projectId, employeeNo, manger.RVNO, editer);
            }
        }

        public ManageUpdateViewModel GetReviews(int projectId, string empployeeNo, string reviewer)
        {
            PersonService ps = new PersonService();
            ManageUpdateViewModel model = new ManageUpdateViewModel();
            var views = db.Get().Where(r => r.APRID == projectId && r.EMPNO == empployeeNo).ToList();
            return model;
        }

        public void ManagerUpdate(ManageUpdateViewModel model, string status, string manager)
        {
            var DbResult1 = db2.Get().Where(s => s.APRID == model.ProjectId && s.EMPNO == model.EmployeeNo && s.RVNO == model.Reviewer).FirstOrDefault();
            string editer = string.Empty;
            if (DbResult1.RVNO == model.Reviewer)
            {
                foreach (var review in model.Reviews)
                {
                    var DbResult = db.GetById(review.Id);
                    DbResult.PVID = review.Id;
                    DbResult.COMM1 = review.Comment1;
                    DbResult.MDOR = review.Modifier;
                    DbResult.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

                    db.Update(DbResult, review.Id);
                }
                foreach (var score in model.Scores)
                {
                    if (DbResult1.PSID == score.Id)
                    {
                        var DbResult2 = db2.GetById(score.Id);
                        DbResult2.PSID = score.Id;
                        DbResult2.COMM = score.Comment;
                        DbResult2.MDOR = score.Modifier;
                        DbResult2.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                        editer = score.Modifier;

                        db2.Update(DbResult2, score.Id);
                    }
                }
            }
            if (status == "reviw")
            {
                var manger = db2.Get().Where(s => s.APRID == DbResult1.APRID && s.EMPNO == DbResult1.EMPNO && s.ORDER == DbResult1.ORDER + 1).FirstOrDefault();
                PersonService ps = new PersonService();
                ps.UpdateReviewer(model.ProjectId, model.EmployeeNo, manger.RVNO, editer);
                //ps.UpdateReviewer(model.ProjectId, model.EmployeeNo, "82236", "janewu");
            }
            if (status == "approve")
            {
                var manger = db2.Get().Where(s => s.APRID == DbResult1.APRID && s.EMPNO == DbResult1.EMPNO && s.ORDER == DbResult1.ORDER + 1).FirstOrDefault();
                PersonService ps = new PersonService();
                ps.UpdateReviewer(model.ProjectId, model.EmployeeNo, "00000", editer);
                //ps.UpdateReviewer(model.ProjectId, model.EmployeeNo, "82236", "janewu");
            }
        }
    }
}


