using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    /// <summary>
    /// 客户信息管理业务处理逻辑
    /// </summary>
    public class CustomerBLL : BaseService<TB_Customer>
    {
        public CustomerBLL()
            : base(new BaseRepository<TB_Customer>())
        { }
        /// <summary>
        /// 生成一个新的客户信息实体
        /// </summary>
        /// <returns></returns>
        public override TB_Customer GetObject()
        {
            return new TB_Customer()
            {
                id = 0,
                Address = "",
                IdCard = "",
                Name = "",
                Phone = "",
                Code = "",
                Answer = "",
                Password = "",
                Question = "",
                RegisterDate = DateTime.Now,
                SignInDevice = "",
                SignInIP = "",
                SignInTime = (DateTime?)null,
                Delete = false,
                Director = "",
                Fax = "",
                RegisterID = "",
                SiteAddress = ""
            };
        }
        /// <summary>
        /// 将客户信息显示为字符串
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override string ToString(TB_Customer entity)
        {
            return string.Format("id: {0}, Name: {1}, Code: {2}", entity.id, entity.Name, entity.Code);
        }
    }
}
