using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Wbs.Everdigm.Common;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_security : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!IsPostBack)
            { ShowSecurityCommands(); }
        }

        /// <summary>
        /// 显示普通可发送的命令列表
        /// </summary>
        private void ShowSecurityCommands()
        {
            var commands = CommandUtility.GetCommand(true);
            var html = "";
            foreach (var command in commands)
            {
                html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"-1\" href=\"#" + command.Flag + "\">" + command.Title + "</a></li>";
            }
            menuCommands.InnerHtml = html;
        }
    }
}