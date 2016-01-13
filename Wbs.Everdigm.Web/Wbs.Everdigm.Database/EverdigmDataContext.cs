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
