using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartStore.Infrastructure.Logging
{
    public enum LogType : int
    {
        Info = 0,
        Error = 1,
    }

    public static class LogManagement
    {
        //private static LogRepository _repository = new LogRepository();

        public static void Logging(string Message, int LogType, string Event, string Description)
        {
            //var user = HttpContext.Current.Session["UserName"].ToString();
            //var logEntity = new SpadTransportation.Model.Entities.Base.Log.Log()
            //{
            //    Message = Message,
            //    LogType = LogType,
            //    Event = Event,
            //    Description = Description,
            //    InsertDate = DateTime.Now,
            //    InsertUser = user,
            //    IpAddress = "1.1.1.1"
            //};

            //_repository.Create(logEntity);
        }
    }
}
