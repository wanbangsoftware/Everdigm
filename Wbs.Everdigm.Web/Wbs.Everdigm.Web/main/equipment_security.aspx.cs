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
                    initializeSessionKey();
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
            var equipment = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
            var functional = null == equipment ? EquipmentFunctional.Mechanical : (EquipmentFunctional)equipment.Functional;

            // 链接未知时，不能发送任何命令  2015/09/18 18:20
            if ((byte?)null == equipment.OnlineStyle) return;

            var link = (LinkType)equipment.OnlineStyle;
            var commands = CommandUtility.GetCommand(true);
            var html = "";
            foreach (var command in commands)
            {
                // 禁止在这里发送启用或禁用卫星命令 2015/11/26 16:35
                if (command.Flag == "satenable" || command.Flag == "satdisable")
                {
                    continue;
                }
                // 卫星链接时，不能发送以下几个命令  2015/09/16 15:50
                if (link == LinkType.SATELLITE)
                {
                    if (command.Flag == "satenable" || command.Flag == "satdisable" || command.Flag == "reset_sat")
                    {
                        continue;
                    }
                }
                else
                {
                    // 睡眠模式下禁止发送转Satellite命令  2015/09/18 11:00
                    if (command.Flag == "reset_gsm" || (equipment.OnlineStyle == (byte)LinkType.SLEEP && command.Flag == "reset_sat"))
                        continue;
                }
                if (functional == EquipmentFunctional.Mechanical)
                {
                    // 机械式的挖掘机，不显示装载机的命令
                    if (command.Title.IndexOf("Loader") < 0)
                    {
                        html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"-1\" href=\"#" + command.Flag + "\">" + command.Title + "</a></li>";
                    }
                }
                else if (functional == EquipmentFunctional.Electric)
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