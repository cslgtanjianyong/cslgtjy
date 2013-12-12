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
using System.Text.RegularExpressions;
using System.Threading;

namespace Winform_ExcelWarning
{
    public partial class FormAddReportforms : Form
    {  
        public FormAddReportforms()
        {
            InitializeComponent();
        }
        //添加表名
        private void bton_search_Click(object sender, EventArgs e)
        {
            this.drpd_tableName.Items.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            this.txtb_reportSource.Text = ofd.FileName;
            if (this.txtb_reportSource.Text != "")
            {
                this.bton_filesearch.Enabled = false;
            }
            if (ofd.FileName == "") 
            {
                return;
            }
            DataTable _Table = tools.GetExcelTableName(ofd.FileName);

            try
            {
                for (int i = 0; i != _Table.Rows.Count; i++)
                {
                    if (_Table.Rows[i]["Table_Name"].ToString().Substring(0, 4) != "_Hid")
                    {
                        this.drpd_tableName.Items.Add(new ListItem("i", _Table.Rows[i]["Table_Name"].ToString()));
                    }

                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("请不要将当前输入的Excel表处于编辑状态，也不要打开不是Excel的文件！！");
                this.txtb_reportSource.Text = null;
                this.bton_filesearch.Enabled = true;

            }
          
        }

        //规则添加界面初始化
        private void FormAddReportforms_Load(object sender, EventArgs e)
        {

            Init();
        }

        //跳转回开始界面
        private void bton_back_Click(object sender, EventArgs e)
        {
            FormMain formMain = new FormMain();
            formMain.Show();
            this.Hide();
        }

        private void bton_restart_Click(object sender, EventArgs e)
        {

        }

        private void Init()
        {
            //计划类型绑定
            this.drpd_reportType.Items.Add(new ListItem("once", "执行一次"));
            this.drpd_reportType.Items.Add(new ListItem("repeat", "重复多次"));


            //栏位规则绑定
            this.drpd_symbol.Items.Add(new ListItem(">", "大于"));
            this.drpd_symbol.Items.Add(new ListItem(">=", "大于等于"));
            this.drpd_symbol.Items.Add(new ListItem("<", "小于"));
            this.drpd_symbol.Items.Add(new ListItem("<=", "小于等于"));
            this.drpd_symbol.Items.Add(new ListItem("=", "等于"));
            this.drpd_symbol.Items.Add(new ListItem("!=", "不等于"));
            this.drpd_symbol.Items.Add(new ListItem("in", "包含"));
            this.drpd_symbol.Items.Add(new ListItem("not in", "不包含"));

            //频率类型绑定
            this.drpd_pinlv.Items.Add(new ListItem(" D", "每天"));
            this.drpd_pinlv.Items.Add(new ListItem(" W", "每周"));
            this.drpd_pinlv.Items.Add(new ListItem(" M", "每月"));

            //事件类型绑定
            this.drpd_timeType.Items.Add(new ListItem("Hou", "小时"));
            this.drpd_timeType.Items.Add(new ListItem("Min", "分钟"));
        }

        private void drpd_reportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpd_reportType.SelectedItem.ToString() == "重复多次")
            {
                this.gbox_once.Enabled = false;
                this.gbox_more.Enabled = true;
                this.gbox_everydaymore.Enabled = true;
                this.gbox_moreCTime.Enabled = true;

            }
            else
            {
                this.gbox_once.Enabled = true;
                this.gbox_more.Enabled = false;
                this.gbox_everydaymore.Enabled = false;
                this.gbox_moreCTime.Enabled = false;
            }
        }

        private void setGroupReadonlyTrue(Boolean b, GroupBox g)
        {

            foreach (Control c in g.Controls)
            {
                if (b)
                {
                    c.Enabled = false;
                }
                else
                {
                    c.Enabled = true;
                }
            }
        }

        private void drpd_timeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpd_timeType.SelectedItem.ToString() == "小时")
            {
                this.drpd_mtpinlv_time.Maximum = 24;
            }
            else
            {
                this.drpd_mtpinlv_time.Maximum = 60;
            }
        }

        private void rbtn_mtpinlv_once_CheckedChanged(object sender, EventArgs e)
        {
            this.dtme_MoreTime.Enabled = true;
            this.drpd_mtpinlv_time.Enabled = false;
            this.drpd_timeType.Enabled = false;
        }

        private void rbtn_mtpinlv_more_CheckedChanged(object sender, EventArgs e)
        {
            this.dtme_MoreTime.Enabled = false;
            this.drpd_mtpinlv_time.Enabled = true;
            this.drpd_timeType.Enabled = true;
        }

        private void rbtn_cxshijian_endtime_CheckedChanged(object sender, EventArgs e)
        {
            this.dtme_cxTime_endTime.Enabled = true;
            this.dtme_lastEndTime.Enabled = true;
        }

        private void rbtn_cxTime_noendTime_CheckedChanged(object sender, EventArgs e)
        {
            this.dtme_cxTime_endTime.Enabled = false;
            this.dtme_lastEndTime.Enabled = false;
        }

        private void drpd_pinlv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpd_pinlv.SelectedItem.ToString() == "每天")
            {
                this.lbel_pinlv_day.Visible = true;
                this.lbel_pinlv_week.Visible = false;
                this.lbel_pinlv_mei.Visible = false;
                this.drpd_pinlv_month.Visible = false;
                this.lbel_pinlv_month.Visible = false;
                this.drpd_pinlv_week.Visible = false;
                this.drpd_pinlv_day.Visible = true;
                foreach (Control c in gbox_more.Controls)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.CheckBox")
                    {
                        c.Visible = false;
                    }
                }
            }
            if (this.drpd_pinlv.SelectedItem.ToString() == "每周")
            {
                this.lbel_pinlv_day.Visible = false;
                this.lbel_pinlv_week.Visible = true;
                this.lbel_pinlv_mei.Visible = false;
                this.drpd_pinlv_month.Visible = false;
                this.lbel_pinlv_month.Visible = false;
                this.drpd_pinlv_week.Visible = true;
                this.drpd_pinlv_day.Visible = false;
                foreach (Control c in gbox_more.Controls)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.CheckBox")
                    {
                        c.Visible = true;
                    }
                }
            }
            if (this.drpd_pinlv.SelectedItem.ToString() == "每月")
            {
                this.lbel_pinlv_day.Visible = true;
                this.lbel_pinlv_week.Visible = false;
                this.lbel_pinlv_mei.Visible = true;
                this.drpd_pinlv_month.Visible = true;
                this.lbel_pinlv_month.Visible = true;
                this.drpd_pinlv_week.Visible = false;
                this.drpd_pinlv_day.Visible = true;

                foreach (Control c in gbox_more.Controls)
                {
                    if (c.GetType().ToString() == "System.Windows.Forms.CheckBox")
                    {
                        c.Visible = false;
                    }
                }
            }
        }

        private void bton_searchAttachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            this.txtb_attachmentSource.Text = ofd.FileName;
        }

        private void bton_sure_Click(object sender, EventArgs e)
        {
            if (this.bton_filesearch.Enabled == false)
            {

                if (addRule(this.txtb_reportSource.Text, this.drpd_tableName.SelectedItem.ToString())) 
                {
                    MessageBox.Show("插入规则成功");
                }
            }
            else 
            {
                bool Success=true;
                string[] filenames = null;
                try
                {
                  filenames = Directory.GetFiles(this.txtb_fileBName.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("请选择Excel文件！！！！");
                    return;
                }
                List<string> names = new List<string>();
                foreach (string s in filenames)
                {

                    string[] ss = s.Split(new[] { "." }, StringSplitOptions.None);
                    if (ss[ss.Length - 1] == "xls" || ss[ss.Length - 1] == "XLS")
                    {
                        
                        DataTable _Table = tools.GetExcelTableName(s);
                        if (_Table == null)
                        {
                            MessageBox.Show("报表请不要处于编辑状态，请重试！！！！");
                            Success = false;
                            return;
                        }
                        for (int i = 0; i != _Table.Rows.Count; i++)
                        {
                            if (_Table.Rows[i]["Table_Name"].ToString().Substring(0, 4) != "_Hid")
                            {
                                if (!addRule(s, _Table.Rows[i]["Table_Name"].ToString()))
                                {
                                    Success = false;
                                    return;
                                }
                            }

                        }
                    }
                }
                if (Success)
                {
                    MessageBox.Show("插入规则成功！！");
                }
                else 
                {
                    MessageBox.Show("插入规则失败！！");
                }
            }
        }

        private bool addRule(string reportsource,string tablename)
        {
            //创建Rule的Bll层对象
            syntec.BLL.ExcelRule ruleBll = new syntec.BLL.ExcelRule();
            //创建Rule的Model层对象
            syntec.Model.ExcelRule ruleModel = new syntec.Model.ExcelRule();
            //给ruleModel对象赋值
            ruleModel.position = this.txtb_hang.Text.Trim() + "," + this.txtb_lei.Text.Trim();
             ListItem item=null;
            try
            {
              item = (ListItem)this.drpd_symbol.SelectedItem;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("请选择符号");
                return false ;
            }
            ruleModel.symbol = item.ID;
            item = (ListItem)this.drpd_reportType.SelectedItem;
            if (this.txtb_aim.Text.ToString().Trim() == "") 
            {
                MessageBox.Show("请输入比对数值");
                return false;
            }
            if (item != null)
            {
                ruleModel.planType = item.ID;
            }
            else 
            {
                MessageBox.Show("请选择检测时间类型！！！！不能为空");
                return false;
            }
            ruleModel.aim = this.txtb_aim.Text.ToString().Trim();

            if (this.txtb_attachmentSource.Text.Length != 0)
            {
                ruleModel.attachmentSource = this.txtb_attachmentSource.Text.ToString();
            }
            if (this.txtb_email.Text.ToString().Trim() == null)
            {
                MessageBox.Show("邮件不能为空！！！！");
                return false;
            }
            else 
            {
                ruleModel.email = this.txtb_email.Text.ToString().Trim();
            }
            ruleModel.article = this.txtb_article.Text.ToString().Trim();
            ruleModel.reportSource = reportsource;
            ruleModel.sheetName = tablename;
          

            //仅仅执行一次
            if (this.drpd_reportType.SelectedIndex == 0)
            {

                ruleModel.timeType = null;

                ruleModel.timeSpace = null;

                ruleModel.everydayFreSpace = null;

                TimeSpan s = new TimeSpan(this.dtme_OnceTime.Value.Hour, this.dtme_OnceTime.Value.Minute, this.dtme_OnceTime.Value.Second);

                ruleModel.startTime = s;


                ruleModel.startDate = tools.AddTwoDate(this.dtme_OnceDateTime.Value, this.dtme_OnceTime.Value);
               
                //唯一一个不要修改的。！！！！！！！！！！！！！！！！
                ruleModel.nextDoTimeDate = ruleModel.startDate;


            }
            else
            {
                ruleModel.planType = "repeat";
                if (this.rbtn_cxTime_endTime.Checked)
                {
                    DateTime endTime = new DateTime();
                    endTime = endTime.AddHours(this.dtme_lastEndTime.Value.Hour);
                    endTime = endTime.AddMinutes(this.dtme_lastEndTime.Value.Minute);
                    endTime = endTime.AddSeconds(this.dtme_lastEndTime.Value.Second);
                    ruleModel.endDate = tools.AddTwoDate(this.dtme_cxTime_endTime.Value, endTime);
                }
                if (this.rbtn_mtpinlv_once.Checked)
                {
                    ruleModel.everydayFreSpace = null;
                }
                else
                {

                    ListItem i = (ListItem)drpd_timeType.SelectedItem;
                    if (i.ID == null)
                    {
                        MessageBox.Show("请选择时间类型！！！！");
                        return false;
                    }
                    ruleModel.everydayFreSpace = this.drpd_mtpinlv_time.Value.ToString() + i.ID;
                }
                //按每日频率插入
                if (this.drpd_pinlv.SelectedIndex == 0)
                {
                    ruleModel.timeType = "D";

                    ruleModel.timeSpace = this.drpd_pinlv_day.Value.ToString();
                }
                //按每周频率插入
                if (this.drpd_pinlv.SelectedIndex == 1)
                {
                    ruleModel.timeType = "W";
                    //拼接周,星期
                    ruleModel.timeSpace = this.drpd_pinlv_week.Value.ToString() + JudgeCheckbox();

                    if (JudgeCheckbox() == "")
                    {
                        MessageBox.Show("请至少选择一个星期日期！！！！");
                        return false;
                    }
                }
                //按每月频率插入
                if (this.drpd_pinlv.SelectedIndex == 2)
                {
                    ruleModel.timeType = "M";
                    //要修改的。！！！！！！！！！！！！！！！！
                    ruleModel.timeSpace = this.drpd_pinlv_day.Value.ToString() + "," + this.drpd_pinlv_month.Value.ToString();
                }
                DateTime d = new DateTime();
                d = tools.AddTwoDate(this.dtme_cxTime_startTime.Value, this.dtme_lastStartTime.Value);

                ruleModel.startDate = d;
                TimeSpan s1 = new TimeSpan(this.dtme_MoreTime.Value.Hour, this.dtme_MoreTime.Value.Minute, this.dtme_MoreTime.Value.Second);
                ruleModel.startTime = s1;
                //在插入规则时判断第一次的执行时间 true代表第一次
                if (tools.getnextToDateTime(ruleModel, true) != null)
                {
                    ruleModel.nextDoTimeDate = (DateTime)tools.getnextToDateTime(ruleModel, true);
                }
            }

            if (ruleModel.nextDoTimeDate == null)
            {
                MessageBox.Show("规则不能执行任何一次，请重试！！");
                return false;
            }
            //bool a = tools.ExcelIsOK(ruleModel);
            //if (ruleModel.startDate < DateTime.Now) 
           // {
           //     MessageBox.Show("开始时间请选择大于现在的系统时间！！！");
           //     return false;
           // }
            ruleBll.Add(ruleModel); 
            return true;
        }

        

        private void bton_filename_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (this.folderBrowserDialog1.SelectedPath.Trim() != "")
                this.txtb_fileBName.Text = this.folderBrowserDialog1.SelectedPath.Trim();
                this.bton_search.Enabled = false;
                this.drpd_tableName.Enabled = false;
            }

        }

        private void bton_fileBName_Click(object sender, EventArgs e)
        {
            this.txtb_fileBName.Text = "";
            this.bton_search.Enabled = true;
            this.drpd_tableName.Enabled = true;
        }

        private void bton_fileClear_Click(object sender, EventArgs e)
        {
            this.txtb_reportSource.Text = "";
            this.bton_filesearch.Enabled = true;
        }

        private string JudgeCheckbox()
        {
            string a = "";
            if (this.cbox_pinlv_Firstday.Checked)
            {
                a += ",Monday";
            }
            if (this.cbox_pinlv_Tuesday.Checked)
            {
                a += ",Tuesday";
            }
            if (this.cbox_pinlv_Wednesday.Checked)
            {
                a += ",Wednesday";
            }
            if (this.cbox_pinlv_Thursday.Checked)
            {
                a += ",Thursday";
            }
            if (this.cbox_pinlv_Friday.Checked)
            {
                a += ",Friday";
            }
            if (this.cbox_pinlv_Saturday.Checked)
            {
                a += ",Saturday";
            }
            if (this.cbox_pinlv_Sunday.Checked)
            {
                a += ",Sunday";
            }
            return a;
        }

        private void txtb_email_Leave(object sender, EventArgs e)
        {
            string[] ss = this.txtb_email.Text.Trim().Split(new[] { "," }, StringSplitOptions.None);
            int i = 1;
            foreach (string a in ss)
            {
                if (!tools.HasEmail(a))
                {
                    MessageBox.Show("第" + i.ToString() + "个邮箱地址有问题哦亲！！请修改！！");
                }
                i++;
            }

        }
        private void txtb_lei_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtb_lei.Text.ToString().Trim();
            int Ucount = str.Length - Regex.Replace(str, @"[A-Z]", "").Length; //大写字母个数。
            if (Ucount < str.Length && str.Length != 0) 
            {
                MessageBox.Show("请输入大写字母哦亲！！");
                this.txtb_lei.Text = "";
              
            }
        }

        private void txtb_hang_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt16(this.txtb_hang.Text.Trim());
            }
            catch (Exception ex)
            {
                if (this.txtb_hang.Text.Length != 0)
                {
                    MessageBox.Show("请输入整数数字哦亲！！");
                }
                this.txtb_hang.Text = "";
            }
        }

        private void txtb_numberAdd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt16(this.txtb_numberAdd.Text.Trim());
            }
            catch (Exception ex)
            {
                if (this.txtb_hang.Text.Length != 0)
                {
                    MessageBox.Show("请输入整数数字哦亲！！");
                }
                this.txtb_numberAdd.Text = "";
            }
        }

        private void txtb_wordAdd_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtb_wordAdd.Text.ToString().Trim();
            int Ucount = str.Length - Regex.Replace(str, @"[A-Z]", "").Length; //大写字母个数。
            if (Ucount < str.Length && str.Length != 0)
            {
                MessageBox.Show("请输入大写字母哦亲！！");
                this.txtb_wordAdd.Text = "";
            }
        }

        private void bton_positonAdd_Click(object sender, EventArgs e)
        {
            if (this.txtb_wordAdd.Text.Trim() != "" && this.txtb_numberAdd.Text.Trim() != "")
            {
                this.txtb_article.Text += "$" + this.txtb_numberAdd.Text.Trim() + "," + this.txtb_wordAdd.Text.Trim() + "$";
            }
            else 
            {
                MessageBox.Show("栏位不能为空！！");
                return;
            }
        }

     
     
     

        

       

      
       



    }




}


