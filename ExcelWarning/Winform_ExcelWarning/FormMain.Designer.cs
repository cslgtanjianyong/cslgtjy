namespace Winform_ExcelWarning
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bton_addGuize = new System.Windows.Forms.Button();
            this.lbel_fileName = new System.Windows.Forms.Label();
            this.lbel_position = new System.Windows.Forms.Label();
            this.lbel_rule = new System.Windows.Forms.Label();
            this.lbel_describe = new System.Windows.Forms.Label();
            this.lbel_startTime = new System.Windows.Forms.Label();
            this.lbel_endTime = new System.Windows.Forms.Label();
            this.lbel_state = new System.Windows.Forms.Label();
            this.bton_start = new System.Windows.Forms.Button();
            this.bton_end = new System.Windows.Forms.Button();
            this.lbel_update = new System.Windows.Forms.Label();
            this.panl_rule = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.imag_pic3 = new System.Windows.Forms.PictureBox();
            this.imag_pic2 = new System.Windows.Forms.PictureBox();
            this.imag_pic1 = new System.Windows.Forms.PictureBox();
            this.imag_logo = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbel_nextTime = new System.Windows.Forms.Label();
            this.lbel_searchLog = new System.Windows.Forms.Label();
            this.bton_refresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imag_pic3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imag_pic2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imag_pic1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imag_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // bton_addGuize
            // 
            this.bton_addGuize.Location = new System.Drawing.Point(1152, 543);
            this.bton_addGuize.Name = "bton_addGuize";
            this.bton_addGuize.Size = new System.Drawing.Size(79, 23);
            this.bton_addGuize.TabIndex = 1;
            this.bton_addGuize.Text = "添加规则";
            this.bton_addGuize.UseVisualStyleBackColor = true;
            this.bton_addGuize.Click += new System.EventHandler(this.bton_addGuize_Click);
            // 
            // lbel_fileName
            // 
            this.lbel_fileName.Location = new System.Drawing.Point(47, 121);
            this.lbel_fileName.Name = "lbel_fileName";
            this.lbel_fileName.Size = new System.Drawing.Size(70, 12);
            this.lbel_fileName.TabIndex = 5;
            this.lbel_fileName.Text = "文件名";
            // 
            // lbel_position
            // 
            this.lbel_position.Location = new System.Drawing.Point(166, 121);
            this.lbel_position.Name = "lbel_position";
            this.lbel_position.Size = new System.Drawing.Size(53, 12);
            this.lbel_position.TabIndex = 6;
            this.lbel_position.Text = "比对栏位";
            // 
            // lbel_rule
            // 
            this.lbel_rule.Location = new System.Drawing.Point(238, 122);
            this.lbel_rule.Name = "lbel_rule";
            this.lbel_rule.Size = new System.Drawing.Size(53, 12);
            this.lbel_rule.TabIndex = 7;
            this.lbel_rule.Text = "具体规则";
            // 
            // lbel_describe
            // 
            this.lbel_describe.Location = new System.Drawing.Point(415, 122);
            this.lbel_describe.Name = "lbel_describe";
            this.lbel_describe.Size = new System.Drawing.Size(110, 12);
            this.lbel_describe.TabIndex = 8;
            this.lbel_describe.Text = "频率描述";
            // 
            // lbel_startTime
            // 
            this.lbel_startTime.Location = new System.Drawing.Point(641, 122);
            this.lbel_startTime.Name = "lbel_startTime";
            this.lbel_startTime.Size = new System.Drawing.Size(110, 12);
            this.lbel_startTime.TabIndex = 9;
            this.lbel_startTime.Text = "开始日期时间";
            // 
            // lbel_endTime
            // 
            this.lbel_endTime.Location = new System.Drawing.Point(792, 121);
            this.lbel_endTime.Name = "lbel_endTime";
            this.lbel_endTime.Size = new System.Drawing.Size(110, 12);
            this.lbel_endTime.TabIndex = 10;
            this.lbel_endTime.Text = "结束日期时间";
            // 
            // lbel_state
            // 
            this.lbel_state.Location = new System.Drawing.Point(1128, 121);
            this.lbel_state.Name = "lbel_state";
            this.lbel_state.Size = new System.Drawing.Size(53, 12);
            this.lbel_state.TabIndex = 11;
            this.lbel_state.Text = "当前状态";
            // 
            // bton_start
            // 
            this.bton_start.Location = new System.Drawing.Point(86, 543);
            this.bton_start.Name = "bton_start";
            this.bton_start.Size = new System.Drawing.Size(75, 23);
            this.bton_start.TabIndex = 12;
            this.bton_start.Text = "开启检测";
            this.bton_start.UseVisualStyleBackColor = true;
            this.bton_start.Click += new System.EventHandler(this.bton_start_Click);
            // 
            // bton_end
            // 
            this.bton_end.Enabled = false;
            this.bton_end.Location = new System.Drawing.Point(388, 543);
            this.bton_end.Name = "bton_end";
            this.bton_end.Size = new System.Drawing.Size(75, 23);
            this.bton_end.TabIndex = 13;
            this.bton_end.Text = "关闭检测";
            this.bton_end.UseVisualStyleBackColor = true;
            this.bton_end.Click += new System.EventHandler(this.bton_end_Click);
            // 
            // lbel_update
            // 
            this.lbel_update.AutoSize = true;
            this.lbel_update.Location = new System.Drawing.Point(1083, 122);
            this.lbel_update.Name = "lbel_update";
            this.lbel_update.Size = new System.Drawing.Size(29, 12);
            this.lbel_update.TabIndex = 14;
            this.lbel_update.Text = "修改";
            // 
            // panl_rule
            // 
            this.panl_rule.AutoScroll = true;
            this.panl_rule.Location = new System.Drawing.Point(39, 136);
            this.panl_rule.Name = "panl_rule";
            this.panl_rule.Size = new System.Drawing.Size(1256, 345);
            this.panl_rule.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 585);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "表示规则还在有效期内";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 585);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "表示规则不会再执行";
            // 
            // imag_pic3
            // 
            this.imag_pic3.Image = global::Winform_ExcelWarning.Properties.Resources.warning;
            this.imag_pic3.Location = new System.Drawing.Point(432, 576);
            this.imag_pic3.Name = "imag_pic3";
            this.imag_pic3.Size = new System.Drawing.Size(31, 28);
            this.imag_pic3.TabIndex = 20;
            this.imag_pic3.TabStop = false;
            // 
            // imag_pic2
            // 
            this.imag_pic2.Image = global::Winform_ExcelWarning.Properties.Resources._false;
            this.imag_pic2.Location = new System.Drawing.Point(278, 576);
            this.imag_pic2.Name = "imag_pic2";
            this.imag_pic2.Size = new System.Drawing.Size(28, 28);
            this.imag_pic2.TabIndex = 18;
            this.imag_pic2.TabStop = false;
            // 
            // imag_pic1
            // 
            this.imag_pic1.Image = global::Winform_ExcelWarning.Properties.Resources._true;
            this.imag_pic1.Location = new System.Drawing.Point(86, 576);
            this.imag_pic1.Name = "imag_pic1";
            this.imag_pic1.Size = new System.Drawing.Size(28, 28);
            this.imag_pic1.TabIndex = 16;
            this.imag_pic1.TabStop = false;
            // 
            // imag_logo
            // 
            this.imag_logo.Image = global::Winform_ExcelWarning.Properties.Resources.Syntec;
            this.imag_logo.Location = new System.Drawing.Point(365, 12);
            this.imag_logo.Name = "imag_logo";
            this.imag_logo.Size = new System.Drawing.Size(337, 99);
            this.imag_logo.TabIndex = 3;
            this.imag_logo.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(469, 585);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "表示规则发生错误，请查看日志";
            // 
            // lbel_nextTime
            // 
            this.lbel_nextTime.Location = new System.Drawing.Point(949, 120);
            this.lbel_nextTime.Name = "lbel_nextTime";
            this.lbel_nextTime.Size = new System.Drawing.Size(99, 13);
            this.lbel_nextTime.TabIndex = 22;
            this.lbel_nextTime.Text = "下次执行时间";
            // 
            // lbel_searchLog
            // 
            this.lbel_searchLog.AutoSize = true;
            this.lbel_searchLog.Location = new System.Drawing.Point(1203, 122);
            this.lbel_searchLog.Name = "lbel_searchLog";
            this.lbel_searchLog.Size = new System.Drawing.Size(53, 12);
            this.lbel_searchLog.TabIndex = 23;
            this.lbel_searchLog.Text = "查看日志";
            // 
            // bton_refresh
            // 
            this.bton_refresh.Location = new System.Drawing.Point(794, 543);
            this.bton_refresh.Name = "bton_refresh";
            this.bton_refresh.Size = new System.Drawing.Size(87, 23);
            this.bton_refresh.TabIndex = 24;
            this.bton_refresh.Text = "刷新规则列表";
            this.bton_refresh.UseVisualStyleBackColor = true;
            this.bton_refresh.Click += new System.EventHandler(this.bton_refresh_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1324, 672);
            this.Controls.Add(this.bton_refresh);
            this.Controls.Add(this.lbel_searchLog);
            this.Controls.Add(this.lbel_nextTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.imag_pic3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.imag_pic2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.imag_pic1);
            this.Controls.Add(this.panl_rule);
            this.Controls.Add(this.lbel_update);
            this.Controls.Add(this.bton_end);
            this.Controls.Add(this.bton_start);
            this.Controls.Add(this.lbel_state);
            this.Controls.Add(this.lbel_endTime);
            this.Controls.Add(this.lbel_startTime);
            this.Controls.Add(this.lbel_describe);
            this.Controls.Add(this.lbel_rule);
            this.Controls.Add(this.lbel_position);
            this.Controls.Add(this.lbel_fileName);
            this.Controls.Add(this.imag_logo);
            this.Controls.Add(this.bton_addGuize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "Excel报警器";
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imag_pic3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imag_pic2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imag_pic1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imag_logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bton_addGuize;
        private System.Windows.Forms.PictureBox imag_logo;
        private System.Windows.Forms.Label lbel_fileName;
        private System.Windows.Forms.Label lbel_position;
        private System.Windows.Forms.Label lbel_rule;
        private System.Windows.Forms.Label lbel_describe;
        private System.Windows.Forms.Label lbel_startTime;
        private System.Windows.Forms.Label lbel_endTime;
        private System.Windows.Forms.Label lbel_state;
        private System.Windows.Forms.Button bton_start;
        private System.Windows.Forms.Button bton_end;
        private System.Windows.Forms.Label lbel_update;
        private System.Windows.Forms.FlowLayoutPanel panl_rule;
        private System.Windows.Forms.PictureBox imag_pic1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox imag_pic2;
        private System.Windows.Forms.PictureBox imag_pic3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbel_nextTime;
        private System.Windows.Forms.Label lbel_searchLog;
        private System.Windows.Forms.Button bton_refresh;
    }
}