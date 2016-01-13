using System;

namespace Wbs.Everdigm.Web.mobile
{
    public partial class static_google_map : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            var lat = double.Parse(GetParamenter("lat"));
            var lon = double.Parse(GetParamenter("lon"));
            var width = int.Parse(GetParamenter("width"));
            var height = int.Parse(GetParamenter("height"));
            StaticGoogleMap map = new StaticGoogleMap();
            map.Height = height;
            map.Width = width;
            map.Latitude = lat;
            map.Longitude = lon;
            map.Chart(Response.OutputStream);
        }
    }
}