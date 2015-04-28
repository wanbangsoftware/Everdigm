using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;

namespace Wbs.Everdigm.Web.mobile
{
    public partial class images : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DirectoryInfo imagesfile = new DirectoryInfo(Server.MapPath("../images/equipments"));
            lvImages.DataSource = imagesfile.GetFiles("icon_*.*", SearchOption.TopDirectoryOnly);
            lvImages.DataBind();
        }
    }
}