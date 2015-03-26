using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Wbs.Everdigm.Database
{
    public partial class EverdigmDataContext
    {
        public EverdigmDataContext() :
            base(null == ConfigurationManager.ConnectionStrings["EverdigmDatabaseConnectionString"] ?
            ConfigurationManager.AppSettings["EverdigmDatabaseConnectionString"] :
            ConfigurationManager.ConnectionStrings["EverdigmDatabaseConnectionString"].ToString())
        {
            OnCreated();
        }
    }
}
