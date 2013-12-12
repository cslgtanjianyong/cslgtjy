using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Winform_ExcelWarning
{
    public partial class searchLog : Form
    {
        public syntec.Model.ExcelRule _rule = new syntec.Model.ExcelRule();
        public searchLog(syntec.Model.ExcelRule rule)
        {
            _rule = rule;
            InitializeComponent();
        }

        private void searchLog_Load(object sender, EventArgs e)
        {
            syntec.BLL.Log logbll = new syntec.BLL.Log();
            List<syntec.Model.Log> rulelist = new List<syntec.Model.Log>();
            rulelist = logbll.GetModelList("RuleID = "+_rule.ID);
            foreach (syntec.Model.Log log in rulelist)
            {
                int i = 0;
                logCon rulecon = new logCon();
                rulecon.Left = 0;
                rulecon.Top = rulecon.Height * i;
                i++;
                rulecon.txtb_fileName.Text = _rule.sheetName;
                rulecon.txtb_describe.Text = log.logDescribe;
                rulecon.txtb_startTime.Text = log.logStartTime.ToString();
                this.panl_log.Controls.Add(rulecon);
            }
        }

        private void bton_back_Click(object sender, EventArgs e)
        {
            this.Hide();

        }
    }
}
