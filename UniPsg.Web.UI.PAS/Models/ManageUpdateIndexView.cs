using UniPsg.Model.PAS.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace UniPsg.Web.UI.PAS.Models
{
    public class ManageUpdateIndexView
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int FormId { get; set; }
        public string FormName { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int ViewStarDate { get; set; }
        public int ViewEndDate { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public string Reviewer { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
        public int StartDate { get; set; }
        public string JobCapName { get; set; }
        public string Education { get; set; }
        public int CategoryId { get; set; }

        public List<ProjectReviewViewModel> Reviews1 { get; set; }
        public List<ProjectReviewViewModel> Reviews2 { get; set; }
        public List<ProjectReviewViewModel> Reviews3 { get; set; }
        public List<ProjectReviewViewModel> Reviews4 { get; set; }
        public List<ProjectScoreViewModel> Scores { get; set; }
    }
}