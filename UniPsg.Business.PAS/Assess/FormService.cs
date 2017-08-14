using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class FormService
    {
        private ASSPAFORMRepository db;
        private ASSPACATRepository db1;

        public FormService()
        {
            db = new ASSPAFORMRepository();
            db1 = new ASSPACATRepository();
        }

        /// <summary>取得所有 AssessForm 資料</summary>
        /// <returns></returns>
        public List<AssessFormViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<AssessFormViewModel>();
            foreach (var item in DbResult)
            {
                var result = db1.GetById(item.ACID);
                AssessFormViewModel model = new AssessFormViewModel();
                model.Id = item.AFID;
                model.Name = item.AFNAME;
                model.CategoryId = item.ACID;
                model.CategoryName = result.ACNAME;
                model.Groups = item.AFGRP;
                model.Items = item.AFITEM;
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

        /// <summary>取得啟用 AssessForm 資料</summary>
        /// <returns></returns>
        public List<AssessFormViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(f => f.ASTATUS == status).ToList();
            var models = new List<AssessFormViewModel>();
            foreach (var item in DbResult)
            {
                var result = db1.GetById(item.ACID);
                AssessFormViewModel model = new AssessFormViewModel();
                model.Id = item.AFID;
                model.Name = item.AFNAME;
                model.CategoryId = item.ACID;
                model.CategoryName = result.ACNAME;
                model.Groups = item.AFGRP;
                model.Items = item.AFITEM;
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

        /// <summary>取得 AssessForm 資料</summary>
        /// <returns></returns>
        public List<AssessFormViewModel> GetByCategory(int categoryId)
        {
            var DbResult = db.Get().Where(f => f.ACID == categoryId).ToList();
            var models = new List<AssessFormViewModel>();
            foreach (var item in DbResult)
            {
                var result = db1.GetById(item.ACID);
                AssessFormViewModel model = new AssessFormViewModel();
                model.Id = item.AFID;
                model.Name = item.AFNAME;
                model.CategoryId = item.ACID;
                model.CategoryName = result.ACNAME;
                model.Groups = item.AFGRP;
                model.Items = item.AFITEM;
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

        /// <summary>取得單一 AssessForm 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssessFormViewModel Get(int id)
        {
            var item = db.GetById(id);
            var result = db1.GetById(item.ACID);
            AssessFormViewModel model = new AssessFormViewModel();
            model.Id = item.AFID;
            model.Name = item.AFNAME;
            model.CategoryId = item.ACID;
            model.CategoryName = result.ACNAME;
            model.Groups = item.AFGRP;
            model.Items = item.AFITEM;
            model.Status = item.ASTATUS;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 AssessForm 資訊</summary>
        /// <param name="models"></param>
        public void Add(AssessFormViewModel models)
        {
            ASSPAFORM item = new ASSPAFORM();
            item.AFID = db.GetLastId() + 1;
            item.AFNAME = models.Name;
            item.ACID = models.CategoryId;
            item.AFGRP = models.Groups;
            item.AFITEM = models.Items;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 AssessForm 資訊</summary>
        /// <param name="models"></param>
        public void Save(AssessFormViewModel models)
        {
            var item = db.GetById(models.Id);
            item.AFID = models.Id;
            item.AFNAME = models.Name;
            item.ACID = models.CategoryId;
            item.AFGRP = models.Groups;
            item.AFITEM = models.Items;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);
        }

        /// <summary>刪除 AssessForm 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var form = db.GetById(id);
            db.Delete(form.AFID);
        }
    }
}


