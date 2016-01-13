using System;

using System.IO;

namespace Wbs.Everdigm.Web.main
{
    public partial class images : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DirectoryInfo imagesfile = new DirectoryInfo(Server.MapPath("../images"));
            lvImages.DataSource = imagesfile.GetFiles("img_menu_*.*", SearchOption.TopDirectoryOnly);
            lvImages.DataBind();
        }
    }
}