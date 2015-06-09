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
            {
                if (!HasSessionLose)
                {
                    ShowSecurityCommands();
                }
            }
        }

        /// <summary>
        /// 显示普通可发送的命令列表
        /// </summary>
        private void ShowSecurityCommands()
        {
            var id = ParseInt(Utility.Decrypt(_key));
            var equipment = EquipmentInstance.Find(f => f.id == id);
            var functional = null == equipment ? EquipmentFunctional.Mechanical : (EquipmentFunctional)equipment.Functional;

            var commands = CommandUtility.GetCommand(true);
            var html = "";
            foreach (var command in commands)
            {
                if (functional == EquipmentFunctional.Mechanical)
                {
                    // 机械式的挖掘机，不显示装载机的命令
                    if (command.Title.IndexOf("Loader") < 0)
                    {
                        html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"-1\" href=\"#" + command.Flag + "\">" + command.Title + "</a></li>";
                    }
                }
                else if (functional == EquipmentFunctional.Electronic)
                {
                    // 电子式的挖掘机，不显示普通挖掘机的EPOS命令
                    if (command.Title.IndexOf("Security") < 0)
                    {
                        html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"-1\" href=\"#" + command.Flag + "\">" + command.Title.Replace("Loader", "Equipment") + "</a></li>";
                    }
                }
                else if (functional == EquipmentFunctional.Loader)
                {
                    // 装载机不显示挖掘机的命令
                    if (command.Title.IndexOf("Security") < 0)
                    {
                        html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"-1\" href=\"#" + command.Flag + "\">" + command.Title + "</a></li>";
                    }
                }
            }
            menuCommands.InnerHtml = html;
        }
    }
}