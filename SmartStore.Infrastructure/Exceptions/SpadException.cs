using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SmartStore.Infrastructure.Exceptions
{
    public class SpadException : Exception
    {
        public SpadException(string message)
            : base(message)
        {
        }

        public SpadException(string message, Exception innerException, params object[] args)
            : base(String.Format(message, args), innerException)
        {
        }

        public SpadException(string message, params object[] args)
            : base(String.Format(message, args))
        {
        }

        public SpadException(bool useResource, string message, params object[] args)
            : base(String.Format(message, args))
        {
        }
        //public object DataObject
        //{
        //    set
        //    {
        //        var ps = value.GetType().GetProperties();
        //        foreach (var p in ps)
        //            base.Data.Add(p.Name, value.GetPropertyValue(p.Name));
        //    }
        //}
    }
}
