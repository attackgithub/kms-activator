using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kms_activator
{
    public partial class DebugInfo : Form
    {
        public DebugInfo()
        {
            InitializeComponent();
        }

        public void WriteLine(string text)
        {
            textBox1.AppendText(text + "\r\n");
        }

        private void DebugInfo_Load(object sender, EventArgs e)
        {

        }

        private void DebugInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.uncheck_debug_option();
        }
    }
}
