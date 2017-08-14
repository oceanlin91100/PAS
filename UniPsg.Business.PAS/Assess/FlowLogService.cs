using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class FlowLogService
    {
        private ASSPFWLGRepository db;

        public FlowLogService()
        {
            db = new ASSPFWLGRepository();
        }

        /// <summary>取得所有 Auditor 資料</summary>
        /// <returns></returns>
        public List<FlowLogViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<FlowLogViewModel>();
            foreach (var item in DbResult)
            {
                FlowLogViewModel model = new FlowLogViewModel();
                model.Id = item.FLID;
                model.BeforContent = item.PRECNT;
                model.AfterContent = item.PSTCNT;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                models.Add(model);
            }
            return models;
        }

        /// <summary>取得單一 Auditor 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FlowLogViewModel Get(int id)
        {
            var item = db.GetById(id);
            FlowLogViewModel model = new FlowLogViewModel();
            model.Id = item.FLID;
            model.BeforContent = item.PRECNT;
            model.AfterContent = item.PSTCNT;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            return model;
        }

        /// <summary>新增 Auditor 資訊</summary>
        /// <param name="models"></param>
        public void Add(FlowLogViewModel models)
        {
            ASSPFWLG item = new ASSPFWLG();
            item.FLID = db.GetLastId() + 1;
            item.PRECNT = models.BeforContent;
            item.PSTCNT = models.AfterContent;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 Auditor 資訊</summary>
        /// <param name="models"></param>
        public void Save(FlowLogViewModel models)
        {
            var DbResult = db.GetById(models.Id);
            DbResult.FLID = models.Id;
            DbResult.PSTCNT = models.AfterContent;
            DbResult.PRECNT = models.BeforContent;

            db.Update(DbResult, models.Id);
        }

        /// <summary>刪除 Auditor 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var result = db.GetById(id);
            db.Delete(result.FLID);
        }
    }
}


