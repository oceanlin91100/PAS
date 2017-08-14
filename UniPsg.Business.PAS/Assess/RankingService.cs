using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class RankingService
    {
        private ASSPARANKRepository db;
        public RankingService()
        {
            db = new ASSPARANKRepository();
        }

        /// <summary>取得所有 Ranking 資料</summary>
        /// <returns></returns>
        public List<AssessRankingViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<AssessRankingViewModel>();
            foreach (var item in DbResult)
            {
                AssessRankingViewModel model = new AssessRankingViewModel();
                model.Id = item.ARID;
                model.Name = item.ARNAME;
                model.Weight = item.WEIGHT;
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

        /// <summary>取得啟用 AssessRanking 資料</summary>
        /// <returns></returns>
        public List<AssessRankingViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(r => r.ASTATUS == status).ToList();
            var models = new List<AssessRankingViewModel>();
            foreach (var item in DbResult)
            {
                AssessRankingViewModel model = new AssessRankingViewModel();
                model.Id = item.ARID;
                model.Name = item.ARNAME;
                model.Weight = item.WEIGHT;
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


        /// <summary>取得單一 Ranking 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssessRankingViewModel Get(int id)
        {
            var result = db.GetById(id);

            AssessRankingViewModel model = new AssessRankingViewModel();
            model.Id = result.ARID;
            model.Name = result.ARNAME;
            model.Weight = result.WEIGHT;
            model.Status = result.ASTATUS;
            model.Definition = result.DEF;
            model.Creator = result.CTOR;
            model.CreatedDate = result.CTDA;
            model.Modifier = result.MDOR;
            model.ModifiedDate = result.MDDA;

            return model;
        }

        /// <summary>新增 Ranking 資訊</summary>
        /// <param name="models"></param>
        public void Add(AssessRankingViewModel models)
        {
            ASSPARANK item = new ASSPARANK();

            item.ARID = db.GetLastId() + 1;
            item.ARNAME = models.Name;
            item.DEF = models.Definition;
            item.WEIGHT = models.Weight;
            item.ASTATUS = models.Status;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);

        }

        /// <summary>儲存 Ranking 資訊</summary>
        /// <param name="models"></param>
        public void Save(AssessRankingViewModel models)
        {
            var item = db.GetById(models.Id);

            item.ARID = models.Id;
            item.ARNAME = models.Name;
            item.DEF = models.Definition;
            item.WEIGHT = models.Weight;
            item.ASTATUS = models.Status;
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);

        }

        /// <summary>刪除 Ranking 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var ranking = db.GetById(id);
            db.Delete(ranking.ARID);
        }
    }
}

