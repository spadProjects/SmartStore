using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartStore.DataAccess.Context;
using SmartStore.Model.Entities;

namespace SmartStore.Classes
{
    public class SmsService
    {
        public void Send(string To, string Body)
        {
            SmartStoreContext db = new SmartStoreContext();

            SmsIrRestful.Token tokenInstance = new SmsIrRestful.Token();

            string mobilenumber = To;
            string code = Body;


            var token = tokenInstance.GetToken("a151711852a2c88060d564d5", "09378971585");

            //SmsIrRestful.UltraFast ultraFast = new SmsIrRestful.UltraFast();

            //var uf = ultraFast.Send(token, new SmsIrRestful.UltraFastSend()
            //{
            //    Mobile = Convert.ToInt64(mobilenumber),
            //    TemplateId = 5731,

            //    ParameterArray = new List<SmsIrRestful.UltraFastParameters>()
            //    {
            //        new SmsIrRestful.UltraFastParameters()
            //        {
            //            Parameter = "ثبت نام شما با موفقیت انجام شد کد فعال سازی شما" ,
            //            ParameterValue = code
            //        }
            //    }.ToArray()
            //});


            SmsIrRestful.VerificationCode verificationCode = new SmsIrRestful.VerificationCode();

            var vc = verificationCode.Send(token, new SmsIrRestful.RestVerificationCode()
            {
                MobileNumber = mobilenumber,
                Code = code
            });

        }
    }
}