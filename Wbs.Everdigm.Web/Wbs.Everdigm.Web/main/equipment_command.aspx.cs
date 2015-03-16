using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_command : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!IsPostBack)
            { ShowCustomCommands(); }
        }
        /// <summary>
        /// 显示普通可发送的命令列表
        /// </summary>
        private void ShowCustomCommands()
        {
            var cmds = ConfigurationManager.AppSettings["Custom_Commands"].Split(new char[] { ',' });
            var html = "";
            foreach (var cmd in cmds)
            {
                var cmmds = cmd.Split(new char[] { '|' });
                html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"-1\" href=\"#" + cmmds[1] + "\">" + cmmds[0] + "</a></li>";
            }
            menuCommands.InnerHtml = html;
        }
    }
}