using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.BLL
{
    public class AppBLL : BaseService<TB_Application>
    {
        public AppBLL() : base(new BaseRepository<TB_Application>())
        {
        }

        public override TB_Application GetObject()
        {
            return new TB_Application()
            {
                id = 0,
                Useable = false,
                CreateTime = DateTime.Now,
                Description = "",
                Download = "",
                InternalVersion = 0,
                VersionCode = 0,
                VersionName = ""
            };
        }

        public override string ToString(TB_Application entity)
        {
            return string.Format("versionCode: {0}, versionName: {1}, internalVersion: {3}", entity.VersionCode, entity.VersionName, entity.InternalVersion);
        }
    }
}
