using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class OrganizationService
    {
        private ASSPAFORGRepository db;
        private ASSPAPRJRepository db1;
        private ASSPAPOSNRepository db2;
        private ASSPAUDRepository db3;
        private ASSPFWLGRepository db4;
        public OrganizationService()
        {
            db = new ASSPAFORGRepository();
            db1 = new ASSPAPRJRepository();
            db2 = new ASSPAPOSNRepository();
            db3 = new ASSPAUDRepository();
            db4 = new ASSPFWLGRepository();
        }

        public string GetEmployeeNumber(string account, int manager)
        {
            account = account.ToUpper();
            string EmployeeNumber = string.Empty;
            if (manager == 1)
            {
                var result = db.Get().Where(o => o.MAGACTNO == account).FirstOrDefault();
                EmployeeNumber = result.MAGNO;
            }
            else
            {
                var result = db.Get().Where(o => o.EMPACTNO == account).FirstOrDefault();
                EmployeeNumber = result.EMPNO;
            }
            return EmployeeNumber;
        }

        public int CheckManager(string account, int manager)
        {
            account = account.ToUpper();
            var result = db.Get().Where(o => o.MAGACTNO == account).ToList();

            if (result.Count > 0)
                return 1;
            else
                return 0;

        }

        /// <summary>取得所有 Organization 資料</summary>
        /// <returns></returns>
        public List<OrganizationViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<OrganizationViewModel>();
            foreach (var item in DbResult)
            {
                var result1 = db1.GetById(item.APRID);
                OrganizationViewModel model = new OrganizationViewModel();
                model.ProjectId = item.APRID;
                model.ProjectName = result1.APRNAME;
                model.EmployeeNo = item.EMPNO;
                model.EmployeeName = item.EMPNAME;
                model.BranchCode = item.BRCD;
                model.BranchName = item.BRNAME;
                model.DeptCode = item.DEPCD;
                model.DeptName = item.DEPNAME;
                model.TeamCode = item.TMCD;
                model.TeamName = item.TMNAME;
                model.ManagerNo = item.MAGNO;
                model.ManageName = item.MAGNAME;
                model.ManageAccount = item.MAGACTNO;
                model.FlowPath = item.FLOWPATH;
                model.NamePath = item.NAMEPATH;
                model.Status = item.ASTATUS;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }

        /// <summary>取得啟用 Organization 資料</summary>
        /// <returns></returns>
        public List<OrganizationViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(o => o.ASTATUS == status).ToList();
            var models = new List<OrganizationViewModel>();
            foreach (var item in DbResult)
            {
                var result1 = db1.GetById(item.APRID);
                OrganizationViewModel model = new OrganizationViewModel();
                model.ProjectId = item.APRID;
                model.ProjectName = result1.APRNAME;
                model.EmployeeNo = item.EMPNO;
                model.EmployeeName = item.EMPNAME;
                model.BranchCode = item.BRCD;
                model.BranchName = item.BRNAME;
                model.DeptCode = item.DEPCD;
                model.DeptName = item.DEPNAME;
                model.TeamCode = item.TMCD;
                model.TeamName = item.TMNAME;
                model.ManagerNo = item.MAGNO;
                model.ManageName = item.MAGNAME;
                model.ManageAccount = item.MAGACTNO;
                model.FlowPath = item.FLOWPATH;
                model.NamePath = item.NAMEPATH;
                model.Status = item.ASTATUS;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }

        public List<OrganizationViewModel> GetByProjectId(int projectId)
        {
            var DbResult = db.Get().Where(i => i.APRID == projectId).ToList();

            var models = new List<OrganizationViewModel>();
            foreach (var item in DbResult)
            {
                var result1 = db1.GetById(item.APRID);
                OrganizationViewModel model = new OrganizationViewModel();
                model.ProjectId = item.APRID;
                model.ProjectName = result1.APRNAME;
                model.EmployeeNo = item.EMPNO;
                model.EmployeeName = item.EMPNAME;
                model.BranchCode = item.BRCD;
                model.BranchName = item.BRNAME;
                model.DeptCode = item.DEPCD;
                model.DeptName = item.DEPNAME;
                model.TeamCode = item.TMCD;
                model.TeamName = item.TMNAME;
                model.ManagerNo = item.MAGNO;
                model.ManageName = item.MAGNAME;
                model.ManageAccount = item.MAGACTNO;
                model.FlowPath = item.FLOWPATH;
                model.NamePath = item.NAMEPATH;
                model.Status = item.ASTATUS;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }

            return models;
        }

        /// <summary>取得單一 Organization 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrganizationViewModel Get(int projectId, string employeeNo)
        {
            var item = db.GetById(projectId, employeeNo);
            var result1 = db1.GetById(item.APRID);
            OrganizationViewModel model = new OrganizationViewModel();
            model.ProjectId = item.APRID;
            model.ProjectName = result1.APRNAME;
            model.EmployeeNo = item.EMPNO;
            model.EmployeeName = item.EMPNAME;
            model.BranchCode = item.BRCD;
            model.BranchName = item.BRNAME;
            model.DeptCode = item.DEPCD;
            model.DeptName = item.DEPNAME;
            model.TeamCode = item.TMCD;
            model.TeamName = item.TMNAME;
            model.ManagerNo = item.MAGNO;
            model.ManageName = item.MAGNAME;
            model.ManageAccount = item.MAGACTNO;
            model.FlowPath = item.FLOWPATH;
            model.NamePath = item.NAMEPATH;
            model.Status = item.ASTATUS;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;

            return model;
        }

        /// <summary>新增 Organization 資訊</summary>
        /// <param name="models"></param>
        public void Add(OrganizationViewModel models)
        {
            ASSPAFORG item = new ASSPAFORG();
            item.APRID = models.ProjectId;
            item.EMPNO = models.EmployeeNo;
            item.EMPNAME = models.EmployeeName;
            item.EMPACTNO = models.Account;
            item.BRCD = models.BranchCode;
            item.BRNAME = models.BranchName;
            item.DEPCD = models.DeptCode;
            item.DEPNAME = models.DeptName;
            if (string.IsNullOrEmpty(models.TeamCode))
                item.TMCD = string.Empty;
            else
                item.TMCD = models.TeamCode;
            if (string.IsNullOrEmpty(models.TeamName))
                item.TMNAME = string.Empty;
            else
                item.TMNAME = models.TeamName;
            item.MAGNO = models.ManagerNo;
            item.MAGNAME = models.ManageName;
            item.MAGACTNO = models.ManageAccount;
            item.FLOWPATH = models.FlowPath;
            item.NAMEPATH = models.NamePath;
            item.ASTATUS = models.Status;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 Organization 資訊</summary>
        /// <param name="models"></param>
        public void Save(OrganizationViewModel models)
        {
            var DbResult = db.GetById(models.ProjectId, models.EmployeeNo);

            DbResult.APRID = models.ProjectId;
            DbResult.EMPNO = models.EmployeeNo;
            DbResult.EMPNAME = models.EmployeeName;
            DbResult.EMPACTNO = models.Account;
            DbResult.BRCD = models.BranchCode;
            DbResult.BRNAME = models.BranchName;
            DbResult.DEPCD = models.DeptCode;
            DbResult.DEPNAME = models.DeptName;
            if (string.IsNullOrEmpty(models.TeamCode))
                DbResult.TMCD = " ";
            else
                DbResult.TMCD = models.TeamCode;
            if (string.IsNullOrEmpty(models.TeamName))
                DbResult.TMNAME = " ";
            else
                DbResult.TMNAME = models.TeamName;
            var org = db.Get().Where(o => o.APRID == models.ProjectId && o.EMPNO == models.ManagerNo).FirstOrDefault();
            if (org != null)
            {
                DbResult.MAGNO = org.EMPNO;
                DbResult.MAGNAME = org.EMPNAME;
                DbResult.MAGACTNO = org.EMPACTNO;
            }
            if (DbResult.FLOWPATH != models.FlowPath)
            {
                ASSPFWLG flowlog = new ASSPFWLG();

                flowlog.FLID = db4.GetLastId() + 1;
                flowlog.PRECNT = DbResult.FLOWPATH;
                flowlog.PSTCNT = models.FlowPath;
                flowlog.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                flowlog.CTOR = models.Modifier;
                db4.Insert(flowlog);
            }
            DbResult.FLOWPATH = models.FlowPath;
            DbResult.NAMEPATH = models.NamePath;
            DbResult.ASTATUS = models.Status;
            DbResult.MDOR = models.Modifier;
            DbResult.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(DbResult, models.ProjectId, models.EmployeeNo);
        }

        /// <summary>刪除 category 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int projectId, string employeeNo)
        {
            var org = db.GetById(projectId, employeeNo);
            db.Delete(org.APRID, org.EMPNO);
        }

        /// <summary>
        /// 匯入所有組織人員
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="editer"></param>
        public void AddAll(int projectId, string editer)
        {
            try
            {
                db.Delete(projectId);
                WFPORGMRepository dbWF = new WFPORGMRepository();
                //DataTable dt1 = db2.GetUnits("102','106','110','120','130','200','310','400','510','530','600','620','630','720','800','100");

                DataTable dt1 = dbWF.GetUnits();
                foreach (DataRow dr1 in dt1.Rows)
                {
                    if (dr1["EMPNO"].ToString() != "86760")
                    {
                        DataTable dt = dbWF.GetFlow(dr1["EMPNO"].ToString());
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                ASSPAFORG item = new ASSPAFORG();
                                item.APRID = projectId;
                                item.EMPNO = dr["EMPNO"].ToString().Trim();
                                item.EMPNAME = dr["EMPNAME"].ToString().Trim();
                                item.EMPACTNO = dr["EMPACCT"].ToString().Trim();
                                item.BRCD = dr["BRHCOD"].ToString().Trim();
                                item.BRNAME = dr["BRHNAMB"].ToString().Trim();
                                item.DEPCD = dr["DEPTCD"].ToString().Trim();
                                item.DEPNAME = dr["DEPTNAM"].ToString().Trim();
                                item.TMCD = dr["TEAMCD"].ToString().Trim();
                                item.TMNAME = dr["ORGNAME"].ToString().Trim();
                                item.MAGNO = dr["MANGER"].ToString().Trim();
                                item.MAGNAME = dr["EMPNAME01"].ToString().Trim();
                                item.MAGACTNO = dr["MNGACCT"].ToString().Trim();
                                if (dr["level"].ToString() == "1")
                                {
                                    item.FLOWPATH = dr["sort_path"].ToString().Trim() + ",86760";
                                    item.NAMEPATH = dr["name_path"].ToString().Trim() + ",林寬成(86760)";
                                }
                                else
                                {
                                    item.FLOWPATH = dr["sort_path"].ToString().Trim();
                                    item.NAMEPATH = dr["name_path"].ToString().Trim();
                                }
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

                DataTable dt2 = dbWF.GetVP("86760");
                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt2.Rows)
                    {
                        ASSPAFORG item = new ASSPAFORG();
                        item.APRID = projectId;
                        item.EMPNO = dr["EMPNO"].ToString().Trim();
                        item.EMPNAME = dr["EMPNAME"].ToString().Trim();
                        item.EMPACTNO = dr["EMPACCT"].ToString().Trim();
                        item.BRCD = dr["BRHCOD"].ToString().Trim();
                        item.BRNAME = dr["BRHNAMB"].ToString().Trim();
                        item.DEPCD = dr["DEPTCD"].ToString().Trim();
                        item.DEPNAME = dr["DEPTNAM"].ToString().Trim();
                        item.TMCD = dr["TEAMCD"].ToString().Trim();
                        item.TMNAME = dr["ORGNAME"].ToString().Trim();
                        item.MAGNO = dr["MANGER"].ToString().Trim();
                        item.MAGNAME = dr["EMPNAME01"].ToString().Trim();
                        item.MAGACTNO = dr["MNGACCT"].ToString().Trim();
                        item.FLOWPATH = "86760";
                        item.NAMEPATH = "林寬成";
                        item.ASTATUS = 0;
                        item.CTOR = editer;
                        item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                        item.MDOR = editer;
                        item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

                        db.Insert(item);
                    }

                    // 取得分公司稽核人員
                    WFPPERMRepository dbPR = new WFPPERMRepository();
                    var d02 = dbPR.GetEmployees("D02");
                    foreach (DataRow item in d02.Rows)
                    {
                        var org = db.GetById(projectId, item["EMPNO"].ToString().Trim());
                        var auditor = db3.Get().Where(a => a.BRCD == org.BRCD).FirstOrDefault();
                        if (auditor != null)
                        {
                            var flowPaht = org.FLOWPATH.Split(',');

                            org.FLOWPATH = flowPaht[0] + "," + auditor.MAGNO + ",82311";

                            var namePaht = org.NAMEPATH.Split(',');

                            if (auditor.MAGNO == "78055")
                                org.NAMEPATH = namePaht[0] + ",吳清彰(78055),陳凱經(82311)";

                            if (auditor.MAGNO == "88199")
                                org.NAMEPATH = namePaht[0] + ",王續芳(88199),陳凱經(82311)";

                            if (auditor.MAGNO == "05022")
                                org.NAMEPATH = namePaht[0] + ",許文玲(05022),陳凱經(82311)";

                            db.Update(org, projectId, item["EMPNO"].ToString().Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        public List<BranchViewModel> GetBranches()
        {
            var org = db2.Get();
            var result = org.GroupBy(b => new { b.BRCD, b.BRNAME })
                      .Select(g => new
                      {
                          g.Key.BRCD,
                          g.Key.BRNAME
                      }).ToList();

            List<BranchViewModel> branches = new List<BranchViewModel>();
            foreach (var branch in result)
            {
                BranchViewModel bv = new BranchViewModel();
                bv.BranchCode = branch.BRCD.Trim();
                bv.BranchName = branch.BRNAME.Trim();
                branches.Add(bv);
            }
            return branches;
        }


        public List<DepartmentViewModel> GetDepartments()
        {
            var org = db2.Get();
            var result = org.GroupBy(d => new { d.DEPCD, d.DEPNAME })
                      .Select(g => new
                      {
                          g.Key.DEPCD,
                          g.Key.DEPNAME
                      }).ToList();

            List<DepartmentViewModel> departments = new List<DepartmentViewModel>();
            foreach (var department in result)
            {
                DepartmentViewModel dv = new DepartmentViewModel();
                dv.DeptCode = department.DEPCD.Trim();
                dv.DeptName = department.DEPNAME.Trim();
                departments.Add(dv);
            }
            return departments;
        }

        public string GetEmployeeName(string employeeNo, int projectId)
        {
            var employee = db.Get().Where(o => o.EMPNO == employeeNo && o.APRID == projectId).FirstOrDefault();
            if (employee != null)
                return employee.EMPNAME;
            else
                return string.Empty;
        }

        public string GetManagerAccount(string employeeNo, int projectId)
        {
            var employee = db.Get().Where(o => o.EMPNO == employeeNo && o.APRID == projectId).FirstOrDefault();
            if (employee != null)
                return employee.MAGACTNO;
            else
                return string.Empty;
        }

        public string GetBackAccount(string employeeNo, int projectId)
        {
            var back = db2.Get().Where(p => p.APRID == projectId && p.EMPNO == employeeNo).FirstOrDefault();
            if (back != null)
            {
                var employee = db.Get().Where(o => o.EMPNO == back.RVNO && o.APRID == projectId).FirstOrDefault();
                if (employee != null)
                    return employee.EMPACTNO;
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }


        public string GetAccount(string employeeNo, int projectId)
        {
            var employee = db.Get().Where(o => o.EMPNO == employeeNo && o.APRID == projectId).FirstOrDefault();
            if (employee != null)
                return employee.EMPACTNO;
            else
                return string.Empty;
        }

    }
}

