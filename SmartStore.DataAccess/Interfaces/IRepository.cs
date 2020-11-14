using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Interfaces
{
    public interface IRepository<T, S, N>
    {
        /// <summary>
        /// Insert & Update Entity Based on id is 0 or not
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Result Entity</returns>
        T Save(T entity);

        /// <summary>
        /// Delete entity logically by id
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);

        /// <summary>
        /// Delete entity logically
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Is Deleted</returns>
        void Delete(T entity);

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Entity</returns>
        T GetEntity(object id);

        /// <summary>
        /// Get entity includes demanded navigations
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="navigations">Navigation for include</param>
        /// <returns>Entity</returns>
        T GetEntity(object id, List<N> navigations);
               

        /// <summary>
        /// Get Query of entity
        /// </summary>
        /// <returns>Query of entity</returns>
        IQueryable<T> GetDefaultQuery();

        /// <summary>
        /// Get Query of entity includes demanded navigations
        /// </summary>
        /// <param name="navigations">Navigation for include</param>
        /// <returns>Query of entity</returns>
        IQueryable<T> GetDefaultQuery(List<N> navigations);

        /// <summary>
        /// Get Query of entity based on searchObject
        /// </summary>
        /// <param name="searchObject">Entity search object</param>
        /// <returns>Query of entity</returns>
        IQueryable<T> GetDefaultQuery(S searchObject);

        /// <summary>
        /// Get Query of entity based on searchObject includes demanded navigations
        /// </summary>
        /// <param name="searchObject">Entity search object</param>
        /// <param name="navigations">Navigation for include</param>
        /// <returns>Query of entity</returns>
        IQueryable<T> GetDefaultQuery(S searchObject, List<N> navigations);

        /// <summary>
        /// Get Query of entity based on searchObject
        /// </summary>
        /// <param name="searchObject">Entity search object</param>
        /// <param name="totalCount">Return total count based on search object</param>
        /// <returns>Query of entity</returns>
        IQueryable<T> GetDefaultQuery(S searchObject, out int totalCount);

        /// <summary>
        /// Get Query of entity based on searchObject includes demanded navigations
        /// </summary>>
        /// <param name="searchObject">Entity search object</param>
        /// <param name="navigations">Navigation for include</param
        /// <param name="totalCount">Return total count based on search object</param>
        /// <returns>Query of entity</returns>
        IQueryable<T> GetDefaultQuery(S searchObject, List<N> navigations, out int totalCount);
    }
}
