using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using AutoMapper;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Function
{
    public class BehavioralIndicatorService
    {
        private ASSPFUNBIRepository db;

        public BehavioralIndicatorService()
        {
            db = new ASSPFUNBIRepository();
        }

        /// <summary>取得所有 BehavioralIndicator 資料</summary>
        /// <returns></returns>
        public List<FunctionBehavioralIndicatorViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<FunctionBehavioralIndicatorViewModel>();
            foreach (var item in DbResult)
            {
                FunctionBehavioralIndicatorViewModel model = new FunctionBehavioralIndicatorViewModel();
                model.Id = item.FBID;
                model.ItemId = item.FBNAME;
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
        
        /// <summary>取得單一 BehavioralIndicator 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FunctionBehavioralIndicatorViewModel Get(int id)
        {
            var item = db.GetById(id);
            FunctionBehavioralIndicatorViewModel model = new FunctionBehavioralIndicatorViewModel();
            model.Id = item.FBID;
            model.ItemId = item.FBNAME;
            model.Status = item.ASTATUS;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 BehavioralIndicator 資訊</summary>
        /// <param name="models"></param>
        public void Add(FunctionBehavioralIndicatorViewModel models)
        {
            ASSPFUNBI item = new ASSPFUNBI();
            item.FBID = db.GetLastId() + 1;
            item.FBNAME = models.ItemId;           
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            db.Insert(item);

        }

        /// <summary>儲存 BehavioralIndicator 資訊</summary>
        /// <param name="models"></param>
        public void Save(FunctionBehavioralIndicatorViewModel models)
        {
            ASSPFUNBI item = new ASSPFUNBI();
            item.FBID = models.Id;
            item.FBNAME = models.ItemId;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;            
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            db.Insert(item);
        }

        /// <summary>刪除 BehavioralIndicator 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var behavioralIndicator = db.GetById(id);
            db.Delete(behavioralIndicator.FBID);
        }
    }
}
