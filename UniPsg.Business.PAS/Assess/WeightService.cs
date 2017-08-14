using System;
using System.Collections.Generic;
using System.Linq;
using UniPsg.Model.PAS.ViewModels;
using UniPsg.Data.AS400.PAS;
using UniPsg.Model.PAS.AS400;

namespace UniPsg.Business.PAS.Assess
{
    public class WeightService
    {
        private ASSPWEIGHTRepository db;
        private ASSPAFORMRepository db1;
        private ASSPASCOPERepository db2;
        public WeightService()
        {
            db = new ASSPWEIGHTRepository();
            db1 = new ASSPAFORMRepository();
            db2 = new ASSPASCOPERepository();
        }

        /// <summary>取得所有 FormWeight 資料</summary>
        /// <returns></returns>
        public List<FormWeightViewModel> Get()
        {
            var DbResult = db.Get().ToList();
            var models = new List<FormWeightViewModel>();
            foreach (var item in DbResult)
            {
                var result1 = db1.GetById(item.AFID);
                var result2 = db2.GetById(item.ASID);
                FormWeightViewModel model = new FormWeightViewModel();
                model.Id = item.FWID;
                model.FormId = item.AFID;
                model.FormName = result1.AFNAME;
                model.ScopeId = item.ASID;
                model.ScopeName = result2.ASNAME;
                model.Weight = item.WEIGHT;
                model.Status = item.ASTATUS;
                model.Groups = result1.AFGRP;
                model.Definition = item.DEF;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }

        /// <summary>取得啟用 FormWeight 資料</summary>
        /// <returns></returns>
        public List<FormWeightViewModel> GetByStatus(int status)
        {
            var DbResult = db.Get().Where(w => w.ASTATUS == status).ToList();
            var models = new List<FormWeightViewModel>();
            foreach (var item in DbResult)
            {
                var result1 = db1.GetById(item.AFID);
                var result2 = db2.GetById(item.ASID);
                FormWeightViewModel model = new FormWeightViewModel();
                model.Id = item.FWID;
                model.FormId = item.AFID;
                model.FormName = result1.AFNAME;
                model.ScopeId = item.ASID;
                model.ScopeName = result2.ASNAME;
                model.Weight = item.WEIGHT;
                model.Status = item.ASTATUS;
                model.Groups = result1.AFGRP;
                model.Definition = item.DEF;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }
            return models;
        }      

        public List<FormWeightViewModel> GetByForm(int formId)
        {
            var DbResult = db.Get().Where(i => i.FWID == Convert.ToInt32(formId)).ToList();
            var models = new List<FormWeightViewModel>();
            foreach (var item in DbResult)
            {
                var result1 = db1.GetById(item.FWID);
                var result2 = db2.GetById(item.ASID);

                FormWeightViewModel model = new FormWeightViewModel();
                model.Id = item.FWID;
                model.FormId = item.AFID;
                model.FormName = result1.AFNAME;
                model.ScopeId = item.ASID;
                model.ScopeName = result2.ASNAME;
                model.Weight = item.WEIGHT;
                model.Status = item.ASTATUS;
                model.Groups = result1.AFGRP;
                model.Definition = item.DEF;
                model.Creator = item.CTOR;
                model.CreatedDate = item.CTDA;
                model.Modifier = item.MDOR;
                model.ModifiedDate = item.MDDA;
                models.Add(model);
            }

            return models;
        }

        /// <summary>取得單一 FormWeight 資料</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FormWeightViewModel Get(int id)
        {
            var item = db.GetById(id);
            var result1 = db1.GetById(item.AFID);
            var result2 = db2.GetById(item.ASID);
            FormWeightViewModel model = new FormWeightViewModel();
            model.Id = item.FWID;
            model.FormId = item.AFID;
            model.FormName = result1.AFNAME;
            model.ScopeId = item.ASID;
            model.ScopeName = result2.ASNAME;
            model.Weight = item.WEIGHT;
            model.Status = item.ASTATUS;
            model.Groups = result1.AFGRP;
            model.Definition = item.DEF;
            model.Creator = item.CTOR;
            model.CreatedDate = item.CTDA;
            model.Modifier = item.MDOR;
            model.ModifiedDate = item.MDDA;
            return model;
        }

        /// <summary>新增 FormWeight 資訊</summary>
        /// <param name="models"></param>
        public void Add(FormWeightViewModel models)
        {
            ASSPWEIGHT item = new ASSPWEIGHT();
            item.FWID = db.GetLastId() + 1;
            item.AFID = models.FormId;
            item.ASID = models.ScopeId;
            item.WEIGHT = models.Weight;            
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;
            item.CTOR = models.Creator;
            item.CTDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Insert(item);
        }

        /// <summary>儲存 FormWeight 資訊</summary>
        /// <param name="models"></param>
        public void Save(FormWeightViewModel models)
        {
            var item = db.GetById(models.Id);
            item.FWID = models.Id;
            item.AFID = models.FormId;
            item.ASID = models.ScopeId;
            item.WEIGHT = models.Weight;
            item.ASTATUS = models.Status;
            item.DEF = models.Definition;           
            item.MDOR = models.Modifier;
            item.MDDA = System.DateTime.Now.ToString("yyyyMMddHHmmss");

            db.Update(item, models.Id);
        }

        /// <summary>刪除 FormWeight 資訊</summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var result = db.GetById(id);
            db.Delete(result.FWID);
        }
    }
}



