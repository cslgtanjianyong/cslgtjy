using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading;

namespace Winform_ExcelWarning
{
    public partial class update : Form
    {
        private syntec.Model.ExcelRule _rule;
        public update(syntec.Model.ExcelRule rule)
        {
            _rule = rule;
            InitializeComponent();
            dataInit();
            RuleInit();
        }

        private void bton_sure_Click(object sender, EventArgs e)
        {
            //创建Rule的Bll层对象
            syntec.BLL.ExcelRule ruleBll = new syntec.BLL.ExcelRule();
            //创建Rule的Model层对象
            syntec.Model.ExcelRule ruleModel = new syntec.Model.ExcelRule();
            //给ruleModel对象赋值
            ruleModel.position = this.txtb_hang.Text.Trim() + "," + this.txtb_lei.Text.Trim();
            ListItem item = null;
            try
            {
                item = (ListItem)this.drpd_symbol.SelectedItem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("请选择符号");
                return;
            }
            ruleModel.symbol = item.ID;
            item = (ListItem)this.drpd_reportType.SelectedItem;
            if (this.txtb_aim.Text.ToString().Trim() == "")
            {
                MessageBox.Show("请输入比对数值");
            }

            ruleModel.planType = item.ID;
            ruleModel.aim = this.txtb_aim.Text.ToString().Trim();

            if (this.txtb_attachmentSource.Text.Length != 0)
            {
                ruleModel.attachmentSource = this.txtb_attachmentSource.Text.ToString();
            }
            ruleModel.email = this.txtb_email.Text.ToString().Trim();
            ruleModel.article = this.txtb_article.Text.ToString().Trim();
            ruleModel.reportSource = this.txtb_reportSource.Text.Trim() ;
            ruleModel.sheetName = this.drpd_tableName.SelectedItem.ToString();


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
                        return;
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
                        return;
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
                MessageBox.Show("规则不能执行任何一次，请重试哦亲！！");
                return;
            }
            //bool a = tools.ExcelIsOK(ruleModel);
  
            ruleModel.ID = _rule.ID;
            ruleBll.Update(ruleModel);
            MessageBox.Show("修改成功");
        }
        private void dataInit()
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
        private void RuleInit()
        {
            this.txtb_reportSource.Text = _rule.reportSource;


            DataTable _Table = tools.GetExcelTableName(this.txtb_reportSource.Text);
            if (_Table == null) 
            {
                MessageBox.Show(_rule.reportSource+_rule.sheetName+"文件不存在或者处于不可正常读取状态！！请查看！！");
                return;
            }
                for (int i = 0; i != _Table.Rows.Count; i++)
                {
                    if (_Table.Rows[i]["Table_Name"].ToString().Substring(0, 4) != "_Hid")
                    {
                        this.drpd_tableName.Items.Add(new ListItem("i", _Table.Rows[i]["Table_Name"].ToString()));
                        if (_Table.Rows[i]["Table_Name"].ToString() == _rule.sheetName)
                        {
                            this.drpd_tableName.SelectedIndex = i;
                        }
                    }

                }
            
            
          
            
            string[] ss1 = _rule.position.Split(new[] { "," }, StringSplitOptions.None);
            this.txtb_hang.Text = ss1[0].ToString();
            this.txtb_lei.Text = ss1[1].ToString();
            if (_rule.symbol == ">")
            {
                this.drpd_symbol.SelectedIndex = 0;
            }
            if (_rule.symbol == ">=")
            {
                this.drpd_symbol.SelectedIndex = 1;
            }
            if (_rule.symbol == "<")
            {
                this.drpd_symbol.SelectedIndex = 2;
            }
            if (_rule.symbol == "<=")
            {
                this.drpd_symbol.SelectedIndex = 3;
            }
            if (_rule.symbol == "=")
            {
                this.drpd_symbol.SelectedIndex = 4;
            }
            if (_rule.symbol == "!=")
            {
                this.drpd_symbol.SelectedIndex = 5;
            }
            if (_rule.symbol == "in")
            {
                this.drpd_symbol.SelectedIndex = 6;
            }
            if (_rule.symbol == "not in")
            {
                this.drpd_symbol.SelectedIndex = 7;
            }
            this.txtb_email.Text = _rule.email.Trim();
            this.txtb_aim.Text = _rule.aim;
            this.txtb_attachmentSource.Text = _rule.attachmentSource;
            this.txtb_article.Text = _rule.article;
            if (_rule.planType.Trim() == "once")
            {
                this.drpd_reportType.SelectedIndex = 0;
                this.dtme_OnceDateTime.Value = this._rule.startDate;
                DateTime d = DateTime.Now;
                d = tools.ClearHMS(d);
                d = d.AddHours(this._rule.startTime.Hours);
                d = d.AddMinutes(this._rule.startTime.Minutes);
                d = d.AddSeconds(this._rule.startTime.Seconds);
                this.dtme_OnceTime.Value = d;
                this.gbox_once.Enabled = true;
                this.gbox_more.Enabled = false;
                this.gbox_everydaymore.Enabled = false;
                this.gbox_moreCTime.Enabled = false;
            }
            else
            {
                this.drpd_reportType.SelectedIndex = 1;
                if (_rule.timeType == "D")
                {
                    this.drpd_pinlv_day.Value =Convert.ToInt16(_rule.timeSpace);
                    this.drpd_pinlv.SelectedIndex = 0;
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
                if (_rule.timeType == "W")
                {
                    this.drpd_pinlv.SelectedIndex = 1;
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
                    ss1 = this._rule.timeSpace.Split(new[] { "," }, StringSplitOptions.None);
                    this.drpd_pinlv_week.Value = Convert.ToInt16(ss1[0]);
                    foreach (string s in ss1)
                    {
                        if (s == "Monday")
                        {
                            this.cbox_pinlv_Firstday.Checked = true;
                        }
                        if (s == "Tuesday")
                        {
                            this.cbox_pinlv_Tuesday.Checked = true;
                        }
                        if (s == "Wednesday")
                        {
                            this.cbox_pinlv_Wednesday.Checked = true;
                        }
                        if (s == "Thursday")
                        {
                            this.cbox_pinlv_Thursday.Checked = true;
                        }
                        if (s == "Friday")
                        {
                            this.cbox_pinlv_Friday.Checked = true;
                        }
                        if (s == "Saturday")
                        {
                            this.cbox_pinlv_Saturday.Checked = true;
                        }
                        if (s == "Sunday")
                        {
                            this.cbox_pinlv_Sunday.Checked = true;
                        }
                    }
                }
                if (_rule.timeType == "M")
                {
                    this.drpd_pinlv.SelectedIndex = 2;
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
                    ss1 = _rule.timeSpace.Split(new[] { "," }, StringSplitOptions.None);
                    this.drpd_pinlv_day.Value = Convert.ToUInt16(ss1[0]);
                    this.drpd_pinlv_month.Value = Convert.ToUInt16(ss1[1]);
                }
                if (_rule.everydayFreSpace == "")
                {
                    DateTime d = DateTime.Now;
                    d = tools.ClearHMS(d);
                    d = d.AddHours(this._rule.startTime.Hours);
                    d = d.AddMinutes(this._rule.startTime.Minutes);
                    d = d.AddSeconds(this._rule.startTime.Seconds);
                    this.dtme_MoreTime.Value = d;
                }
                else
                {
                    this.rbtn_mtpinlv_once.Checked = false;
                    this.dtme_MoreTime.Enabled = false;
                    this.rbtn_mtpinlv_more.Checked = true;
                    this.drpd_mtpinlv_time.Enabled = true;
                    this.drpd_timeType.Enabled = true;
                    ss1 = _rule.everydayFreSpace.Split(new[] { "," }, StringSplitOptions.None);
                    if (_rule.everydayFreSpace.Substring(_rule.everydayFreSpace.Length - 1) == "u")
                    {
                        this.drpd_timeType.SelectedIndex = 0;
                    }
                    else
                    {
                        this.drpd_timeType.SelectedIndex = 1;
                    }
                    this.drpd_mtpinlv_time.Value = tools.GetNumberInt(_rule.everydayFreSpace);
                }
                this.dtme_cxTime_startTime.Value = _rule.startDate;
                DateTime nulltime = DateTime.Now;
                nulltime = tools.ClearHMS(nulltime);
                nulltime = nulltime.AddHours(this._rule.startDate.Hour);
                nulltime = nulltime.AddMinutes(this._rule.startDate.Minute);
                nulltime = nulltime.AddSeconds(this._rule.startDate.Second);
                this.dtme_lastStartTime.Value = nulltime;
                DateTime d1 = new DateTime();
                if (_rule.endDate != d1)
                {
                    this.dtme_cxTime_endTime.Value = _rule.endDate;
                    nulltime = DateTime.Now;
                    nulltime = tools.ClearHMS(nulltime);
                    nulltime = nulltime.AddHours(this._rule.endDate.Hour);
                    nulltime = nulltime.AddMinutes(this._rule.endDate.Minute);
                    nulltime = nulltime.AddSeconds(this._rule.endDate.Second);
                    this.dtme_lastEndTime.Value = nulltime;
                }
                else
                {
                    this.rbtn_cxTime_noendTime.Checked = true;
                    this.rbtn_cxTime_endTime.Checked = false;
                    this.dtme_cxTime_endTime.Enabled = false;
                    this.dtme_lastEndTime.Enabled = false;
                }
            }

        }

        private void update_Load(object sender, EventArgs e)
        {

        }

        private void bton_search_Click(object sender, EventArgs e)
        {
            this.drpd_tableName.Items.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            this.txtb_reportSource.Text = ofd.FileName;
            DataTable _Table = tools.GetExcelTableName(ofd.FileName);
            if (_Table == null) 
            {
                return;
            }
            for (int i = 0; i != _Table.Rows.Count; i++)
            {
                if (_Table.Rows[i]["Table_Name"].ToString().Substring(0, 4) != "_Hid")
                {
                    this.drpd_tableName.Items.Add(new ListItem("i", _Table.Rows[i]["Table_Name"].ToString()));
                }

            }
        }

        private void bton_fileClear_Click(object sender, EventArgs e)
        {
            this.txtb_reportSource.Text = "";

        }



        private void bton_fileBName_Click(object sender, EventArgs e)
        {

            this.bton_search.Enabled = true;
            this.drpd_tableName.Enabled = true;
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

        private void rbtn_cxTime_endTime_CheckedChanged(object sender, EventArgs e)
        {
            this.dtme_cxTime_endTime.Enabled = true;
            this.dtme_lastEndTime.Enabled = true;
        }

        private void rbtn_cxTime_noendTime_CheckedChanged_1(object sender, EventArgs e)
        {
            this.dtme_cxTime_endTime.Enabled = false;
            this.dtme_lastEndTime.Enabled = false;
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

       

        private void bton_back_Click(object sender, EventArgs e)
        {
            FormMain form = new FormMain();
            form.Show();
            this.Hide();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void bton_searchAttachment_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            this.txtb_attachmentSource.Text = ofd.FileName;
        }

       

     

       
    }
}
