using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailWarden2.Visuals
{
    public partial class DebugControl : Form
    {
        public delegate void AddMessage(string message);
        public AddMessage DebugMessage;
        public DebugControl()
        {
            InitializeComponent();
            DebugMessage = new AddMessage(_addMessage);
        }

        public void _addMessage(string message)
        {
            debugtext.AppendText(message + "\n");
            debugtext.ScrollToCaret();
        }

        private void debugtext_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
