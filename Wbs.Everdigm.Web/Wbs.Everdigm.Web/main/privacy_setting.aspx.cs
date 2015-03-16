using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wbs.Everdigm.Web.main
{
    public partial class privacy_setting : BaseAccountPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!HasSessionLose)
            {
                if (!IsPostBack)
                { showEdit(); }
            }
        }

        private void showEdit()
        {
            txtQuestion.Value = Account.Question;
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (!HasSessionLose)
            {
                Account.Question = txtQuestion.Value.Trim();
                Account.Answer = Utility.MD5(txtAnswer.Value.Trim());
                Update(Account);

                SaveHistory(new Database.TB_AccountHistory
                {
                    ActionId = ActionInstance.Find(f => f.Name.Equals("EditPrivacy")).id
                });

                ShowNotification("./account_history.aspx", "You have changed your privacy question successfully.");
            }
        }
    }
}