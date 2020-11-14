using SmartStore.Infrastructure.Exceptions;
using SmartStore.Models.Basement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmartStore.Service
{
    public class BaseService<T, S> where T : EntityBase<S>
    {
        public void SetLogInfo(T entity)
        {
            var CurrentUser = HttpContext.Current.Session["UserName"].ToString();

            if (string.IsNullOrEmpty(CurrentUser))
                throw new SpadException("کاربر جاری پیدا نشد");

            var idValue = 0;
            int.TryParse(entity.Id.ToString(), out idValue);

            if (entity.Id != null || idValue != 0)
            {
                entity.InsertUser = CurrentUser;
                entity.InsertDate = DateTime.Now;
            }
            else
            {
                entity.UpdateUser = CurrentUser;
                entity.UpdateDate = DateTime.Now;
            }
        }
    }
}
