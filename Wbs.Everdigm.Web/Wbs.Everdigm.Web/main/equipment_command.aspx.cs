using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

using Wbs.Everdigm.Common;

namespace Wbs.Everdigm.Web.main
{
    public partial class equipment_command : BaseEquipmentPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!IsPostBack)
            {
                if (!HasSessionLose)
                {
                    initializeSessionKey();
                    ShowCustomCommands();
                }
            }
        }
        /// <summary>
        /// 显示普通可发送的命令列表
        /// </summary>
        private void ShowCustomCommands()
        {
            var id = ParseInt(Utility.Decrypt(_key));
            var equipment = EquipmentInstance.Find(f => f.id == id && f.Deleted == false);
            var functional = null == equipment ? EquipmentFunctional.Mechanical : (EquipmentFunctional)equipment.Functional;
            var commands = CommandUtility.GetCommand(false);
            var link = (LinkType)equipment.OnlineStyle;
            var html = "";
            foreach (var command in commands)
            {
                if (functional == EquipmentFunctional.Mechanical) {
                    // 机械式的挖掘机，不显示装载机的命令
                    if (command.Title.IndexOf("Loader") < 0)
                    {
                        html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"-1\" href=\"#" + command.Flag + "\">" + command.Title + "</a></li>";
                    }
                }
                else if (functional == EquipmentFunctional.Electric)
                { 
                    // 电子式的挖掘机，不显示普通挖掘机的EPOS命令
                    if (command.Title.IndexOf("Equipment") < 0)
                    {
                        // 卫星链接时，不能发送以下几个命令  2015/09/16 15:40
                        if (link == LinkType.SATELLITE)
                        {
                            if (command.Flag == "signal" || command.Flag == "ld_daily" || command.Flag == "ld_worktime")
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (command.Flag == "ld_daily") continue;
                        }
                        html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"-1\" href=\"#" + command.Flag + "\">" + command.Title.Replace("Loader", "Equipment") + "</a></li>";
                    }
                }
                else if (functional == EquipmentFunctional.Loader)
                {
                    // 装载机不显示挖掘机的命令
                    if (command.Title.IndexOf("Equipment") < 0)
                    {
                        // 卫星链接时，不能发送以下几个命令  2015/09/16 15:40
                        if (link == LinkType.SATELLITE)
                        {
                            // 卫星链接时，不能发送信号强度、总工作时间、每日工作时间命令
                            if (command.Flag == "signal" || command.Flag == "ld_daily" || command.Flag == "ld_worktime")
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (command.Flag == "ld_daily") continue;
                        }
                        html += "<li role=\"presentation\"><a role=\"menuitem\" tabindex=\"-1\" href=\"#" + command.Flag + "\">" + command.Title + "</a></li>";
                    }
                }
            }
            menuCommands.InnerHtml = html;
        }
    }
}