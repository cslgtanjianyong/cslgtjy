using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Threading;



namespace Winform_ExcelWarning
{
    public partial class FormMain : Form
    {
        
        public FormMain()
        {
            InitializeComponent();
        }
        public void addFriend(string a)
        {

        }
        private void FormMain_Load(object sender, EventArgs e)
        {
          
            if (!tools.IsClick)
            {
                this.bton_start.Enabled = true;
              
            }
            else 
            {
                if (tools.IsStart)
                {
                    this.bton_start.Enabled = false;
                    this.bton_end.Enabled = true;
                }
                else 
                {
                    this.bton_start.Enabled = true;
                    this.bton_end.Enabled = false;

                }
            }
            fresh();
            tools.Isupdate = true;
        }

        private void fresh()
        {
            syntec.Model.Log log = new syntec.Model.Log();
            syntec.BLL.Log logbll = new syntec.BLL.Log();
            syntec.BLL.ExcelRule rulebll = new syntec.BLL.ExcelRule();
            List<syntec.Model.ExcelRule> rulelist = new List<syntec.Model.ExcelRule>();
            rulelist = rulebll.GetModelList("1=1");
            int i = 0;
            DateTime nulltime = new DateTime();
            foreach (syntec.Model.ExcelRule rule in rulelist)
            {
                
                ruleCon rulecon = new ruleCon();
                rulecon._formmain = this;
                rulecon._rule = rule;
                rulecon.Left = 0;
                rulecon.Top = rulecon.Height * i;
                i++;
                rulecon.txtb_fileName.Text = rule.sheetName;
                rulecon.txtb_position.Text = rule.position;
                rulecon.txtb_rule.Text = rule.symbol + rule.aim;
                if (rule.symbol == "in")
                {
                    rulecon.txtb_rule.Text = "包括" + rule.aim;
                }
                if (rule.symbol == "not in") 
                {
                    rulecon.txtb_rule.Text = "不包括" + rule.aim;
                }
                rulecon.txtb_describe.Text = tools.describe(rule);
                rulecon.txtb_startTime.Text = rule.startDate.ToString();
                if (rule.endDate == nulltime)
                {
                    rulecon.txtb_endTime.Text = "无结束日期";
                }
                else
                {
                    rulecon.txtb_endTime.Text = rule.endDate.ToString();
                }
                if (rule.nextDoTimeDate != null && rule.nextDoTimeDate>=DateTime.Now.AddMinutes(-3))
                {
                    rulecon.imag_state.Image = Image.FromFile(Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 10) + "\\Resources\\true.jpg");
                }
                if (rule.nextDoTimeDate == null)
                {
                    rulecon.imag_state.Image = Image.FromFile(Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length-10) + "\\Resources\\false.jpg");
                }
                List<syntec.Model.Log> logs = new List<syntec.Model.Log>();
                logs = logbll.GetModelList("RuleID= " + rule.ID + " and StateID=" + " 2");
                if (logs.Count != 0) 
                {
                    rulecon.imag_state.Image = Image.FromFile(Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 10) + "\\Resources\\warning.jpg");
                }
           
                if (rule.nextDoTimeDate == null)
                {
                    rulecon.txtb_nextTime.Text = "已经无法继续执行了！！";
                }
                else
                {
                    rulecon.txtb_nextTime.Text = rule.nextDoTimeDate.ToString();
                }
                panl_rule.Controls.Add(rulecon);
            }
        }


        private void bton_addGuize_Click(object sender, EventArgs e)
        {
            FormAddReportforms formAddReportforms = new FormAddReportforms();
            formAddReportforms.Show();
            this.Hide();
        }

        private void bton_searchGuize_Click(object sender, EventArgs e)
        {
            MessageBox.Show(DateTime.Now.ToString());
        }

        private void bton_start_Click(object sender, EventArgs e)
        {
            tools.IsStart = true;
            tools.IsClick = true;
            ThreadExcel lis = new ThreadExcel(this);
            tools.thread = new Thread(new ThreadStart(lis.start));
            tools.thread.IsBackground = true;
            tools.thread.Start();
            MessageBox.Show("检测已经开始");
            this.bton_start.Enabled = false;
            this.bton_end.Enabled = true;


           // syntec.BLL.ExcelRule ebll = new syntec.BLL.ExcelRule();
           // ebll.GetModelList("1=1");
           // foreach (syntec.Model.ExcelRule rule in ebll.GetModelList("1=1"))
           // {
           //     bool a = tools.ReadExcelToTable(rule);
           //     DateTime d = Convert.ToDateTime(tools.getnextToDateTime(rule, false));
           // }
        }

        private void bton_end_Click(object sender, EventArgs e)
        {
            tools.IsStart = false;
            tools.thread.Abort();
            this.bton_start.Enabled = true;
            this.bton_end.Enabled = false;
            MessageBox.Show("检测已经终止");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void imag_logo_Click(object sender, EventArgs e)
        {

        }

        private void bton_refresh_Click(object sender, EventArgs e)
        {
            this.panl_rule.Controls.Clear();
            fresh();
        }

     

    }
}
