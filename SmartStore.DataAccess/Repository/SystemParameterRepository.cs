using SmartStore.DataAccess.Common;
using SmartStore.DataAccess.Context;
using SmartStore.Interfaces;
using SmartStore.Model.Entities;
using SmartStore.Models.SearchObjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.DataAccess.Repository
{
    public class SystemParameterRepository : IRepository<SystemParameter, SystemParameterSearchObject, Navigations>
    {
        private SmartStoreContext db;

        public SystemParameterRepository()
        {
            db = SmartStoreContext.GetInstance();
        }

        #region IService
        public SystemParameter Save(SystemParameter entity)
        {
            if (entity.Id == 0)
            {
                db.SystemParameters.Add(entity);
                db.SaveChanges();
            }
            else
            {                

                db.Set<SystemParameter>().Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return entity;
        }

        public void Delete(object id)
        {
            var entity = GetEntity(id);
            Delete(entity);
        }

        public void Delete(SystemParameter entity)
        {

        }

        public SystemParameter GetEntity(object id)
        {
            return GetDefaultQuery().Where(w => w.ParameterName == id.ToString()).FirstOrDefault();
        }

        public SystemParameter GetEntity(object id, List<Navigations> navigations)
        {
            return GetEntity(id);
        }


        public IQueryable<SystemParameter> GetDefaultQuery()
        {
            var result = db.SystemParameters;
            return result.AsQueryable();
        }
        public IQueryable<SystemParameter> GetDefaultQuery(List<Navigations> navigations)
        {
            return GetDefaultQuery();
        }

        public IQueryable<SystemParameter> GetDefaultQuery(SystemParameterSearchObject searchObject)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, out totalCount);
        }
        public IQueryable<SystemParameter> GetDefaultQuery(SystemParameterSearchObject searchObject, List<Navigations> navigations)
        {
            var totalCount = 0;
            return GetDefaultQuery(searchObject, navigations, out totalCount);
        }

        public IQueryable<SystemParameter> GetDefaultQuery(SystemParameterSearchObject searchObject, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        public IQueryable<SystemParameter> GetDefaultQuery(SystemParameterSearchObject searchObject, List<Navigations> navigations, out int totalCount)
        {
            totalCount = 0;
            return GetDefaultQuery();
        }
        #endregion

        #region Methods
        #endregion
    }
}
