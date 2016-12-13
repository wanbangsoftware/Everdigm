using System;
using System.Linq;

using Wbs.Everdigm.BLL;
using Wbs.Everdigm.Database;

namespace Wbs.Everdigm.Web.main
{
    public partial class terminal_testing_content : BasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            ShowTerminalInfo();
        }
        /// <summary>
        /// 终端信息业务处理逻辑
        /// </summary>
        private TerminalBLL BLLInstance { get { return new TerminalBLL(); } }
        private EquipmentBLL EquipmentInstance { get { return new EquipmentBLL(); } }
        /// <summary>
        /// 格式化显示终端信息
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string ToString(TB_Terminal obj)
        {
            return string.Format("{0}(Sim card no.: {1}, Satellite no.: {2})", obj.Number, obj.Sim, obj.Satellite);
        }
        /// <summary>
        /// 显示正在测试的终端的基本信息
        /// </summary>
        private void ShowTerminalInfo()
        {
            if (string.IsNullOrEmpty(_key)) {
                ShowNotification("", "Could not begin the test program, paramenter is null.", false);
            }
            else
            {
                var t = BLLInstance.Find(f => f.Number.Equals(_key) && f.Delete == false);
                if (null != t)
                {
                    var mac = t.TB_Equipment.FirstOrDefault();
                    terminalInfo.InnerHtml = t.Number;
                    var link = (LinkType)t.OnlineStyle;
                    terminalContent.Value = "Sim card: " + t.Sim + "<br />Satellite: " + 
                        ((null == t.Satellite) ? "not install" : t.TB_Satellite.CardNo) + "<br />Equipment: " +
                        (null == mac ? "" : EquipmentInstance.GetFullNumber(mac)) + "<br />Link: " + link;
                    terminalCardNumber.Value = t.Sim;
                }
                else
                {
                    ShowNotification("", "No terminal like \"" + _key + "\" exists.", false);
                }
            }
        }
    }
}