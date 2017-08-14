using System.Collections.Generic;
using System.Data;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using System;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class PersonService
    {
        private ASSPAPOSNRepository db;
        private ASSPAPRJRepository db1;
        private ASSPAFORMRepository db2;
        private ASSPAGROUPRepository db3;
        private ASSPAFORGRepository db4;
        private ASSPPRRVRepository db5;
        private ASSPKPICATERepository db6;
        private ASSPSCORERepository db7;

        public PersonService()
        {
            db = new ASSPAPOSNRepository();
            db1 = new ASSPAPRJRepository();
            db2 = new ASSPAFORMRepository();
            db3 = new ASSPAGROUPRepository();
            db4 = new ASSPAFORGRepository();
            db5 = new ASSPPRRVRepository();
            db6 = new ASSPKPICATERepository();
            db7 = new ASSPSCORERepository();
        }

        /// <summary>取得所有 AssessPerson 資料</summary>
        /// <returns></returns>
        public List<AssessPersonViewModel> Get()
        {   
            var models = new List<AssessPersonViewModel>();            
            var DbResult = db.GetPersonFlow();
            foreach (DataRow dr in DbResult.Rows)
            {   
                AssessPersonViewModel model = new AssessPersonViewModel();

                var result4 = db4.GetById(Convert.ToInt32(dr["APRID"]), dr["EMPNO"].ToString().Trim());
                model.ProjectId = Convert.ToInt32(dr["APRID"]);
                model.ProjectName = dr["APRNAME"].ToString().Trim(); 
                model.FormId = Convert.ToInt32(dr["AFID"]);
                model.FormName = dr["AFNAME"].ToString().Trim(); 
                model.GroupId = Convert.ToInt32(dr["AGID"]); 
                model.GroupName = dr["AGNAME"].ToString().Trim();
                model.EmployeeNo = dr["EMPNO"].ToString().Trim(); 
                model.Reviewer = dr["RVNO"].ToString().Trim(); 
                model.ReName = dr["ReName"].ToString().Trim(); 
                model.EmployeeName = dr["EMPNAME"].ToString().Trim();
                model.StartDate = Convert.ToInt32(dr["STADA"]); 
                model.TryDate = Convert.ToInt32(dr["TRYDA"]); 
                model.BranchCode = dr["BRCD"].ToString().Trim(); 
                model.BranchName = dr["BRNAME"].ToString().Trim();
                model.DeptCode = dr["DEPCD"].ToString().Trim();  
                model.DeptName = dr["DEPNAME"].ToString().Trim();
                model.TeamCode = result4.TMCD;
                model.TeamName = result4.TMNAME;
                model.JobCap = dr["APJCCD"].ToString().Trim(); 
                model.JobCapName = dr["APJCNAME"].ToString().Trim(); 
                model.JobPos = dr["ASJPCD"].ToString().Trim(); 
                model.JobPosName = dr["ASJPNAME"].ToString().Trim(); 
                model.Education = dr["APED"].ToString().Trim(); 
                model.EmployeeType = dr["EMPTP"].ToString().Trim(); 
                model.Status = Convert.ToInt32(dr["ASTATUS"]); 
                model.Creator = dr["CTOR"].ToString().Trim();
                model.CreatedDate = dr["CTDA"].ToString().Trim(); 
                model.Modifier = dr["MDOR"].ToString().Trim();
                model.ModifiedDate = dr["MDDA"].ToString().Trim();
                model.FlowPath = dr["FLOWPATH"].ToString().Trim(); 
                model.NamePath = dr["NAMEPATH"].ToString().Trim();                 
                models.Add(model);
            }
            return models;
        }

        /// <summary>取得 AssessPerson 資料</summary>
        /// <returns></returns>
        public List<ReviewerViewModel> GetByReviewer(string reviewer, int manager)
        {
            List<ASSPAPOSN> DbResult;
            if (manager == 1)
                DbResult = db.Get().Where(p => p.RVNO == reviewer && p.EMPNO != p.RVNO).ToList();
            else
                DbResult = db.Get().Where(p => p.ASTATUS == 0 && p.RVNO == reviewer && p.EMPNO == p.RVNO).ToList();
            var models = new List<ReviewerViewModel>();
            foreach (var item in DbResult)
            {
                var result1 = db1.GetById(item.APRID);
                var result2 = db2.GetById(item.AFID);
                var result3 = db3.GetById(item.AGID);
                ReviewerViewModel model = new ReviewerViewModel();
                model.ProjectId = item.APRID;
                model.ProjectName = result1.APRNAME;
                model.FormId = item.AFID;
                model.FormName = result2.AFNAME;
                model.GroupId = item.AGID;
                model.GroupName = result3.AGNAME;
                model.AssessYear = result1.AYEAR;
                model.EmployeeName = item.EMPNAME;
                model.EmployeeNo = item.EMPNO;                
                model.CategoryId = result1.ACID;
                models.Add(model);
            }
            return models;
        }
        
        /// <summary>取得 AssessPerson 資料</summary>
        /// <returns></returns>
        public ReviewerUpdateViewModel GetReviewer(int projectId, string employeeNo)
        {
            var result = db.GetById(projectId, employeeNo);
            var result1 = db1.GetById(projectId);
            var result2 = db2.GetById(result.AFID);
            var result3 = db3.GetById(result.AGID);     
            var result4 = db4.GetById(projectId, employeeNo);
            ReviewerUpdateViewModel model = new ReviewerUpdateViewModel();
            model.ProjectId = result.APRID;
            model.ProjectName = result1.APRNAME;
            model.FormId = result.AFID;
            model.FormName = result2.AFNAME;
            model.GroupId = result.AGID;
            model.GroupName = result3.AGNAME;
            model.ViewStarDate = result1.VSDA;
            model.ViewEndDate = result1.VEDA;
            model.EmployeeNo = result.EMPNO;
            model.EmployeeName = result.EMPNAME;
            model.Reviewer = result.RVNO;
            model.BranchCode = result.BRCD;
            model.BranchName = result.BRNAME;
            model.DeptCode = result.DEPCD;
            model.DeptName = result.DEPNAME;
            model.TeamCode = result4.TMCD;
            model.TeamName = result4.TMNAME;
            model.StartDate = result.STADA;
            model.JobCapName = result.APJCNAME;
            model.Education = result.APED;            
            model.CategoryId = result1.ACID;

            var result5 = db5.Get().Where(r => r.APRID == projectId && r.EMPNO == employeeNo).ToList();
            List<ProjectReviewViewModel> models = new List<ProjectReviewViewModel>();
            foreach (var item in result5)
            {
                ProjectReviewViewModel review = new ProjectReviewViewModel();
                var result6 = db6.GetById(item.KPICID);
                review.Id = item.PVID;
                review.ProjectId = item.APRID;
                review.EmployeeNo = item.EMPNO;
                review.Reviewer = item.RVNO;
                review.ScopeId = item.ASID;
                review.ItemId = item.ITEMID;
                review.ItemName = item.ITEMNAME;
                review.KPICategoryId = item.KPICID;
                review.KPICategoryName = result6.KPICNAME;
                review.Weight = item.WEIGHT;
                review.Rate = item.RATE;
                review.Score = item.SCORE;
                review.Score = item.SCORE1;
                review.Comment = item.COMM;
                review.Comment1 = item.COMM1;
                review.Comment2 = item.COMM2;
                review.Status = item.ASTATUS;
                review.Creator = item.CTOR;
                review.CreatedDate = item.CTDA;
                review.Modifier = item.MDOR;
                review.ModifiedDate = item.MDDA;
                models.Add(review);
            }
            model.Reviews = models;
            return model;
        }

        /// <summary>取得 AssessPerson 資料</summary>
        /// <returns></returns>
        public ManageUpdateViewModel GetManagerReview(int projectId, string employeeNo)
        {
            var result = db.GetById(projectId, employeeNo);
            var result1 = db1.GetById(projectId);
            var result2 = db2.GetById(result.AFID);
            var result3 = db3.GetById(result.AGID);
            var result4 = db4.GetById(projectId, employeeNo);
            ManageUpdateViewModel model = new ManageUpdateViewModel();
            model.ProjectId = result.APRID;
            model.ProjectName = result1.APRNAME;
            model.FormId = result.AFID;
            model.FormName = result2.AFNAME;
            model.GroupId = result.AGID;
            model.GroupName = result3.AGNAME;
            model.ViewStarDate = result1.VSDA;
            model.ViewEndDate = result1.VEDA;
            model.EmployeeNo = result.EMPNO;
            model.EmployeeName = result.EMPNAME;
            model.Reviewer = result.RVNO;               
            model.BranchCode = result.BRCD;
            model.BranchName = result.BRNAME;
            model.DeptCode = result.DEPCD;
            model.DeptName = result.DEPNAME;
            model.TeamCode = result4.TMCD;
            model.TeamName = result4.TMNAME;
            model.StartDate = result.STADA;
            model.JobCapName = result.APJCNAME;
            model.Education = result.APED;
            model.CategoryId = result1.ACID;           

            var result5 = db5.Get().Where(r => r.APRID == projectId && r.EMPNO == employeeNo).ToList();
            List<ProjectReviewViewModel> reviews = new List<ProjectReviewViewModel>();
            foreach (var item in result5)
            {
                ProjectReviewViewModel review = new ProjectReviewViewModel();
                var result6 = db6.GetById(item.KPICID);
                review.Id = item.PVID;
                review.ProjectId = item.APRID;
                review.EmployeeNo = item.EMPNO;
                review.Reviewer = item.RVNO;
                review.ScopeId = item.ASID;
                review.ItemId = item.ITEMID;
                review.ItemName = item.ITEMNAME;
                review.KPICategoryId = item.KPICID;
                review.KPICategoryName = result6.KPICNAME;
                review.Weight = item.WEIGHT;
                review.Rate = item.RATE;
                review.Score = item.SCORE;
                review.Score = item.SCORE1;
                review.Comment = item.COMM;
                review.Comment1 = item.COMM1;
                review.Comment2 = item.COMM2;
                review.Status = item.ASTATUS;
                review.Creator = item.CTOR;
                review.CreatedDate = item.CTDA;
                review.Modifier = item.MDOR;
                review.ModifiedDate = item.MDDA;
                reviews.Add(review);
            }
            model.Reviews = reviews;

            var result7 = db7.Get().Where(r => r.APRID == projectId && r.EMPNO == employeeNo).ToList();
            List<ProjectScoreViewModel> scores = new List<ProjectScoreViewModel>();
            foreach (var item in result7)
            {
                var resultOrg = db4.Get().Where(o => o.APRID == item.APRID && o.EMPNO == item.RVNO).FirstOrDefault();
                ProjectScoreViewModel score = new ProjectScoreViewModel();               
                score.Id = item.PSID;
                score.ProjectId = item.APRID;
                score.EmployeeNo = item.EMPNO;
                score.Reviewer = item.RVNO;
                if (resultOrg != null)
                    score.ReviewName = resultOrg.EMPNAME;
                else
                    score.ReviewName = string.Empty;
                score.BPScore = item.BPSCORE;
                score.KPIScore = item.KPISCORE;
                score.CoreScore = item.CORESCORE;
                score.ManageScore = item.MAGSCORE;
                score.TotalScore = item.TOTSCORE;
                score.DevlopComment = item.DEVCOMM;
                score.Comment = item.COMM;
                score.OderSerial = item.ORDER;
                score.Status = item.ASTATUS;
                score.Creator = item.CTOR;                
                score.CreatedDate = item.CTDA;
                score.Modifier = item.MDOR;
                score.ModifiedDate = item.MDDA;
                scores.Add(score);
            }
            model.Scores = scores;
            return model;
        }

        /// <summary>取得 AssessPerson 資料</summary>
        /// <returns></returns>
        public List<AssessPersonViewModel> GetByStatus(int status)
        {
            //var DbResult = db.Get().Where(p => p.ASTATUS == status).ToList();
            var models = new List<AssessPersonViewModel>();
            var DbResult = db.GetPersonFlow();
            foreach (DataRow dr in DbResult.Rows)
            {
                var result4 = db4.GetById(Convert.ToInt32(dr["APRID"]), dr["EMPNO"].ToString().Trim());
                AssessPersonViewModel model = new AssessPersonViewModel();
                model.ProjectId = Convert.ToInt32(dr["APRID"]);
                model.ProjectName = dr["APRNAME"].ToString().Trim();
                model.FormId = Convert.ToInt32(dr["AFID"]);
                model.FormName = dr["AFNAME"].ToString().Trim();
                model.GroupId = Convert.ToInt32(dr["AGID"]);
                model.GroupName = dr["AGNAME"].ToString().Trim();
                model.EmployeeNo = dr["EMPNO"].ToString().Trim();
                model.Reviewer = dr["RVNO"].ToString().Trim();
                model.ReName = dr["ReName"].ToString().Trim();
                model.EmployeeName = dr["EMPNAME"].ToString().Trim();
                model.StartDate = Convert.ToInt32(dr["STADA"]);
                model.TryDate = Convert.ToInt32(dr["TRYDA"]);
                model.BranchCode = dr["BRCD"].ToString().Trim();
                model.BranchName = dr["BRNAME"].ToString().Trim();
                model.DeptCode = dr["DEPCD"].ToString().Trim();
                model.DeptName = dr["DEPNAME"].ToString().Trim();
                model.TeamCode = result4.TMCD;
                model.TeamName = result4.TMNAME;
                model.JobCap = dr["APJCCD"].ToString().Trim();
                model.JobCapName = dr["APJCNAME"].ToString().Trim();
                model.JobPos = dr["ASJPCD"].ToString().Trim();
                model.JobPosName = dr["ASJPNAME"].ToString().Trim();
                model.Education = dr["APED"].ToString().Trim();
                model.EmployeeType = dr["EMPTP"].ToString().Trim();
                model.Status = Convert.ToInt32(dr["ASTATUS"]);
                model.Creator = dr["CTOR"].ToString().Trim();
                model.CreatedDate = dr["CTDA"].ToString().Trim();
                model.Modifier = dr["MDOR"].ToString().Trim();
                model.ModifiedDate = dr["MDDA"].ToString().Trim();
                model.FlowPath = dr["FLOWPATH"].ToString().Trim();
                model.NamePath = dr["NAMEPATH"].ToString().Trim();
                models.Add(model);
            }
            return models.Where(m => m.Status == status).ToList();
        }

        public List<AssessPersonViewModel> GetByProjectId(int projectId)
        {
            var models = new List<AssessPersonViewModel>();
            var DbResult = db.GetPersonFlow(projectId);
            foreach (DataRow dr in DbResult.Rows)
            {
                var result4 = db4.GetById(projectId, dr["EMPNO"].ToString().Trim());
                AssessPersonViewModel model = new AssessPersonViewModel();
                model.ProjectId = Convert.ToInt32(dr["APRID"]);
                model.ProjectName = dr["APRNAME"].ToString().Trim();
                model.FormId = Convert.ToInt32(dr["AFID"]);
                model.FormName = dr["AFNAME"].ToString().Trim();
                model.GroupId = Convert.ToInt32(dr["AGID"]);
                model.GroupName = dr["AGNAME"].ToString().Trim();
                model.EmployeeNo = dr["EMPNO"].ToString().Trim();
                model.Reviewer = dr["RVNO"].ToString().Trim();
                model.ReName = dr["ReName"].ToString().Trim();
                model.EmployeeName = dr["EMPNAME"].ToString().Trim();
                model.StartDate = Convert.ToInt32(dr["STADA"]);
                model.TryDate = Convert.ToInt32(dr["TRYDA"]);
                model.BranchCode = dr["BRCD"].ToString().Trim();
                model.BranchName = dr["BRNAME"].ToString().Trim();
                model.DeptCode = dr["DEPCD"].ToString().Trim();
                model.DeptName = dr["DEPNAME"].ToString().Trim();
                model.TeamCode = result4.TMCD;
                model.TeamName = result4.TMNAME;
                model.JobCap = dr["APJCCD"].ToString().Trim();
                model.JobCapName = dr["APJCNAME"].ToString().Trim();
                model.JobPos = dr["ASJPCD"].ToString().Trim();
                model.JobPosName = dr["ASJPNAME"].ToString().Trim();
                model.Education = dr["APED"].ToString().Trim();
                model.EmployeeType = dr["EMPTP"].ToString().Trim();
                model.Status = Convert.ToInt32(dr["ASTATUS"]);
                model.Creator = dr["CTOR"].ToString().Trim();
                model.CreatedDate = dr["CTDA"].ToString().Trim();
                model.Modifier = dr["MDOR"].ToString().Trim();
                model.ModifiedDate = dr["MDDA"].ToString().Trim();
                model.FlowPath = dr["FLOWPATH"].ToString().Trim();
                model.NamePath = dr["NAMEPATH"].ToString().Trim();
                models.Add(model);
            }
            return models;
        }

        /// <summary>取得單一 AssessPerson 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssessPersonViewModel Get(int projectId, string employeeNo)
        {
            var models = new List<AssessPersonViewModel>();
            AssessPersonViewModel model = new AssessPersonViewModel();
            var DbResult = db.GetPersonFlow(projectId, employeeNo);
            var result4 = db4.GetById(projectId, employeeNo);
            foreach (DataRow dr in DbResult.Rows)
            {                
                model.ProjectId = Convert.ToInt32(dr["APRID"]);
                model.ProjectName = dr["APRNAME"].ToString().Trim();
                model.FormId = Convert.ToInt32(dr["AFID"]);
                model.FormName = dr["AFNAME"].ToString().Trim();
                model.GroupId = Convert.ToInt32(dr["AGID"]);
                model.GroupName = dr["AGNAME"].ToString().Trim();
                model.EmployeeNo = dr["EMPNO"].ToString().Trim();
                model.Reviewer = dr["RVNO"].ToString().Trim();
                model.ReName = dr["ReName"].ToString().Trim();
                model.EmployeeName = dr["EMPNAME"].ToString().Trim();
                model.StartDate = Convert.ToInt32(dr["STADA"]);
                model.TryDate = Convert.ToInt32(dr["TRYDA"]);
                model.BranchCode = dr["BRCD"].ToString().Trim();
                model.BranchName = dr["BRNAME"].ToString().Trim();
                model.DeptCode = dr["DEPCD"].ToString().Trim();
                model.DeptName = dr["DEPNAME"].ToString().Trim();
                model.TeamCode = result4.TMCD;
                model.TeamName = result4.TMNAME;
                model.JobCap = dr["APJCCD"].ToString().Trim();
                model.JobCapName = dr["APJCNAME"].ToString().Trim();
                model.JobPos = dr["ASJPCD"].ToString().Trim();
                model.JobPosName = dr["ASJPNAME"].ToString().Trim();
                model.Education = dr["APED"].ToString().Trim();
                model.EmployeeType = dr["EMPTP"].ToString().Trim();
                model.Status = Convert.ToInt32(dr["ASTATUS"]);
                model.Creator = dr["CTOR"].ToString().Trim();
                model.CreatedDate = dr["CTDA"].ToString().Trim();
                model.Modifier = dr["MDOR"].ToString().Trim();
                model.ModifiedDate = dr["MDDA"].ToString().Trim();
                model.FlowPath = dr["FLOWPATH"].ToString().Trim();
                model.NamePath = dr["NAMEPATH"].ToString().Trim();
                models.Add(model);
            }
            return model;
        }

        /// <summary>新增 AssessPerson 資訊</summary>
        /// <param name="models"></param>
        public void Add(AssessPersonViewModel models)
        {
            ASSPAPOSN item = new ASSPAPOSN();

            item.APRID = models.ProjectId;
            item.AFID = models.FormId;
            item.AGID = models.GroupId;
            item.EMPNO = models.EmployeeNo;
            item.RVNO = models.Reviewer;
            item.EMPNAME = models.EmployeeName;
            item.STADA = models.StartDate;
            item.TRYDA = models.TryDate;
            item.BRCD = models.BranchCode;
            item.BRNAME = models.BranchName;
            item.DEPCD = models.DeptCode;
            item.DEPNAME = models.DeptName;
            item.APJCCD = models.JobCap;
            item.APJCNAME = models.JobCapName;
            item.ASJPCD = models.JobPos;
            item.ASJPNAME = models.JobPosName;
            if (string.IsNullOrEmpty(models.Education))
                item.APED = " ";
            else
                item.APED = models.Education;
            item.EMPTP = models.EmployeeType;
            item.ASTATUS = models.Status;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 AssessPerson 資訊</summary>
        /// <param name="models"></param>
        public void Save(AssessPersonViewModel models)
        {

            var item = db.GetById(models.ProjectId, models.EmployeeNo);

            item.APRID = models.ProjectId;
            item.AFID = models.FormId;
            item.AGID = models.GroupId;
            item.EMPNO = models.EmployeeNo;
            item.RVNO = models.Reviewer;
            item.EMPNAME = models.EmployeeName;
            item.STADA = models.StartDate;
            item.TRYDA = models.TryDate;
            item.BRCD = models.BranchCode;
            item.BRNAME = models.BranchName;
            item.DEPCD = models.DeptCode;
            item.DEPNAME = models.DeptName;
            item.APJCCD = models.JobCap;
            item.APJCNAME = models.JobCapName;
            item.ASJPCD = models.JobPos;
            item.ASJPNAME = models.JobPosName;
            if (string.IsNullOrEmpty(models.Education))
                item.APED = " ";
            else
                item.APED = models.Education;
            item.EMPTP = models.EmployeeType;
            item.ASTATUS = models.Status;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item,models.ProjectId, models.EmployeeNo);
        }


        /// <summary>更新 Reviwer AssessPerson 資訊</summary>
        /// <param name="models"></param>
        public void UpdateReviewer(int projectId, string employeeNo, string reviewer, string editer)
        { 
            var DbResult = db.GetById(projectId, employeeNo);
            DbResult.RVNO = reviewer;
            DbResult.MDOR = editer;
            DbResult.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(DbResult, projectId, employeeNo);
        }

        /// <summary>刪除 AssessPerson 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int projectId, string employeeNo)
        {
            var person = db.GetById(projectId, employeeNo);
            db.Delete(person.APRID, person.EMPNO);
        }

        public void AddAll(int projectId, string editer)
        {
            db.Delete(projectId);
            var project = db1.GetById(projectId);
            string groups = project.GROUPS;
            string[] gArray = groups.Split(',');
            foreach (var g in gArray)
            {
                var form = db2.Get().Where(f => f.ACID == project.ACID && f.AFGRP == g).FirstOrDefault();
                var group = db3.GetById(Convert.ToInt32(g));
                WFPPERMRepository dbAS400DB = new WFPPERMRepository();
                DataTable dt = new DataTable();
                string[] jArray;
                string jobCode = string.Empty;
                switch (g)
                {
                    case "1":
                    case "2":
                        jArray = group.APJPCD.Split('、');
                        jobCode = string.Join("','", jArray);
                        dt = dbAS400DB.GetEmployees(g, "", "", jobCode);
                        break;
                    case "3":
                    case "5":
                        jArray = group.APJPCD.Split('、');
                        jobCode = string.Join("','", jArray);
                        string[] uArray = group.APUTCD.Split('、');
                        string unitCode = string.Join("','", uArray);
                        dt = dbAS400DB.GetEmployees(g, "", unitCode, jobCode);
                        break;
                    case "4":
                        jArray = group.APJPCD.Split('、');
                        jobCode = string.Join("','", jArray);
                        dt = dbAS400DB.GetEmployees(g, "", "", jobCode);
                        break;
                    case "6":
                        jArray = group.APJPCD.Split('、');
                        jobCode = string.Join("','", jArray);
                        dt = dbAS400DB.GetEmployees(g, "", "", jobCode);
                        break;
                    case "7":
                        dt = dbAS400DB.GetEmployees(g, "5850", "", "");
                        break;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    ASSPAPOSN item = new ASSPAPOSN();
                    item.APRID = projectId;
                    item.AFID = form.AFID;
                    item.AGID = Convert.ToInt32(g);
                    item.EMPNO = dr["EMPNO"].ToString().Trim();
                    item.RVNO = dr["EMPNO"].ToString().Trim();
                    item.EMPNAME = dr["EMPNAME"].ToString().Trim();
                    item.STADA = Convert.ToInt32(dr["STRDAT"]);
                    item.TRYDA = Convert.ToInt32(dr["TRYDAT"]);
                    item.BRCD = dr["BRHCOD"].ToString().Trim();
                    item.BRNAME = dr["BRHNAMB"].ToString().Trim();
                    item.DEPCD = dr["DEPTCD"].ToString().Trim();
                    item.DEPNAME = dr["DEPTNAM"].ToString().Trim();
                    item.APJCCD = dr["JOBCAP"].ToString().Trim();
                    item.APJCNAME = dr["JOBCNAM"].ToString().Trim();
                    item.ASJPCD = dr["JOBPOS"].ToString().Trim();
                    item.ASJPNAME = dr["JOBPNAM"].ToString().Trim();
                    item.APED = dr["EDUHNAML"].ToString().Trim();
                    item.EMPTP = dr["EMPTYP"].ToString().Trim();
                    string tryDate = (Convert.ToInt32((project.TRYDA.ToString()).Substring(0, 4)) - 1911).ToString() + (project.TRYDA.ToString()).Substring(4, 4);
                    if (item.TRYDA < Convert.ToInt32(tryDate))
                        item.ASTATUS = 0;
                    else
                        item.ASTATUS = 1;
                    item.CTOR = editer;
                    item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                    item.MDOR = editer;
                    item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

                    db.Insert(item);
                }               
            }
        }

        public void RevievwerUpdate(ManageUpdateViewModel model, string status, string editer)
        {   
            if (status == "back")
            {
                var DbResut = db7.Get().Where(s => s.APRID == model.ProjectId && s.EMPNO == model.EmployeeNo && s.RVNO == model.Reviewer).FirstOrDefault();
                if (DbResut.ORDER == 0)
                {
                    UpdateReviewer(model.ProjectId, model.EmployeeNo, model.EmployeeNo, editer);
                }
                else
                {
                    var result = db7.Get().Where(s => s.APRID == DbResut.APRID && s.EMPNO == DbResut.EMPNO && s.ORDER == DbResut.ORDER - 1).FirstOrDefault();
                    UpdateReviewer(model.ProjectId, model.EmployeeNo, result.RVNO, editer);
                }
            }
           
            if (status == "approve")
            {
                UpdateReviewer(model.ProjectId, model.EmployeeNo, "00000", editer);
            }
        }
    }
}
