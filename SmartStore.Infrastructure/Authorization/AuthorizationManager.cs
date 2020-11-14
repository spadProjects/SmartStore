using SmartStore.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmartStore.Infrastructure.Authorization
{
    public class AuthorizationManager
    {
        public enum AuthorizationItemTypes
        {
            Operation = 0,
            Task = 1,
            Role = 2
        }

        public static bool HasAccess(string operationTitle)
        {
            //return true;
            var _db = new SmartStoreContext();
            var userName = HttpContext.Current.Session["UserName"].ToString();
            var allOperation = new List<string>();

            var operation = _db.AuthorizationItems.Where(w => w.Title.ToLower() == operationTitle.ToLower() &&
                                                     w.ItemType == (int)AuthorizationItemTypes.Operation).FirstOrDefault();

            if (operation == null)
                throw new Exception("Operation not found");

            var operationRelations = GetOperationRelations(operation.Id);
            
            var userHasAccess = _db.UserAuthorizations
                                   .Where(w => (operationRelations.Contains(w.AuthorizationId) || w.AuthorizationId == operation.Id) &&
                                                w.UserName == userName)
                                   .Any();

            return userHasAccess;
        }

        private static List<int> GetOperationRelations(int operationId)
        {
            var result = new List<int>();

            var tasks = GetOperationTasks(operationId);
            result.AddRange(tasks);

            var roles = GetTasksRoles(tasks); //indirect
            result.AddRange(roles);

            var _db = new SmartStoreContext();
            var opRoles = (from rr in _db.AuthorizationItemsRelations
                           join r in _db.AuthorizationItems on rr.FirstItemId equals r.Id
                           where r.ItemType == (int)AuthorizationItemTypes.Role && rr.SecondItemId == operationId
                           select r.Id).ToList();
            result.AddRange(opRoles);


            return result;
        }

        private static List<int> GetOperationTasks(int operationId)
        {
            var _db = new SmartStoreContext();

            return (from tr in _db.AuthorizationItemsRelations
                    join t in _db.AuthorizationItems on tr.FirstItemId equals t.Id
                    where t.ItemType == (int)AuthorizationItemTypes.Task && tr.SecondItemId == operationId
                    select t.Id).ToList();
        }

        private static List<int> GetTasksRoles(List<int> taskIds)
        {
            var _db = new SmartStoreContext();

            return (from rr in _db.AuthorizationItemsRelations
                    join r in _db.AuthorizationItems on rr.FirstItemId equals r.Id
                    where r.ItemType == (int)AuthorizationItemTypes.Role && taskIds.Contains(rr.SecondItemId)
                    select r.Id).ToList();
        }
    }
}
