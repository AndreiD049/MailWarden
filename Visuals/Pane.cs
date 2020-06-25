using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailWarden2
{
    public partial class Pane : UserControl
    {
        public WPFPane PaneWPF;
        public Pane()
        {
            InitializeComponent();
            PaneWPF = new WPFPane();
            this.Host.Child = PaneWPF;
        }

        private void elementHost1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
    }
}
