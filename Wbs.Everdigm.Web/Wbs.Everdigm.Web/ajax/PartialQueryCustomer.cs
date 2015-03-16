using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wbs.Everdigm.Web.ajax
{
    /// <summary>
    /// 处理客户信息的请求
    /// </summary>
    public partial class query
    {
        /// <summary>
        /// 处理查询客户信息的请求
        /// </summary>
        private void HandleCustomerQuery()
        {
            var ret = "[]";
            switch (cmd)
            {
                case "query":
                    // 查询Name或Code
                    var query = CustomerInstance.FindList(f => (f.Name.IndexOf(data) >= 0 ||
                        f.Code.IndexOf(data) >= 0) && f.Delete == false);
                    ret = JsonConverter.ToJson(query);
                    break;
                case "login":
                    // 客户登陆
                    ret = HandleCustomerLogin();
                    break;
            }
            ResponseJson(ret);
        }
        private string HandleCustomerLogin()
        {
            var ret = "{\"status\":-1,\"desc\":\"error\"}";
            var tmp = data.Trim().Split(new char[] { ',' });
            var uid = tmp[0].Trim();
            if (uid.Length > 20) uid = uid.Substring(0, 20);
            var pwd = tmp[1].ToUpper().Trim();
            if (pwd.Length > 32) pwd = pwd.Substring(0, 32);

            var query = CustomerInstance.FindList(f => f.Phone.Equals(uid) && f.Delete == false);
            var guest = query.Count() > 0 ? query.FirstOrDefault() : null;
            if (null != guest)
            {
                if (guest.Password.Equals(pwd))
                {
                    ret = "{\"status\":0,\"desc\":\"success\"}";
                    CustomerInstance.Update(f => f.id == guest.id, update => {
                        update.SignInTime = DateTime.Now;
                        update.SignInIP = Utility.GetClientIP(ctx);
                    });
                    ctx.Session[Utility.SessionNameCustomer] = guest;
                }
                else { ret = "{\"status\":-1,\"desc\":\"Password error.\"}"; }
            }
            else { ret = "{\"status\":-1,\"desc\":\"Not exist phone number.\"}"; }
            return ret;
        }
    }
}