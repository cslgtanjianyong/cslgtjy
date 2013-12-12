using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Winform_ExcelWarning
{
    public partial class ruleCon : UserControl
    {
        public FormMain _formmain;
        public syntec.Model.ExcelRule _rule;
        public ruleCon()
        {
            InitializeComponent();
        }
        private void bton_update_Click(object sender, EventArgs e)
        {
            _formmain.Hide();
            update l = new update(_rule);
            l.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchLog se = new searchLog(_rule);
            se.Show();
        }

    
    }
}
