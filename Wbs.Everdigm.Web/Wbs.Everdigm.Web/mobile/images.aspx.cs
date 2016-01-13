using System;

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