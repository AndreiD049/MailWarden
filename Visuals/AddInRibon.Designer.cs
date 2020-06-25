namespace MailWarden2
{
    partial class AddInRibon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public AddInRibon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MailWarden = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.TestBtn = this.Factory.CreateRibbonButton();
            this.Unreg = this.Factory.CreateRibbonButton();
            this.ShowDebug = this.Factory.CreateRibbonButton();
            this.MailWarden.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MailWarden
            // 
            this.MailWarden.Groups.Add(this.group1);
            this.MailWarden.Label = "MW";
            this.MailWarden.Name = "MailWarden";
            // 
            // group1
            // 
            this.group1.Items.Add(this.TestBtn);
            this.group1.Items.Add(this.Unreg);
            this.group1.Items.Add(this.ShowDebug);
            this.group1.Label = "group1";
            this.group1.Name = "group1";
            // 
            // TestBtn
            // 
            this.TestBtn.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.TestBtn.Label = "Click";
            this.TestBtn.Name = "TestBtn";
            this.TestBtn.ShowImage = true;
            this.TestBtn.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.TestBtn_Click);
            // 
            // Unreg
            // 
            this.Unreg.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.Unreg.Label = "Unreg";
            this.Unreg.Name = "Unreg";
            this.Unreg.ShowImage = true;
            this.Unreg.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.Unreg_Click);
            // 
            // ShowDebug
            // 
            this.ShowDebug.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.ShowDebug.Label = "Show Debug";
            this.ShowDebug.Name = "ShowDebug";
            this.ShowDebug.ShowImage = true;
            this.ShowDebug.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ShowDebug_Click);
            // 
            // AddInRibon
            // 
            this.Name = "AddInRibon";
            this.RibbonType = "Microsoft.Outlook.Explorer";
            this.Tabs.Add(this.MailWarden);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.AddInRibon_Load);
            this.MailWarden.ResumeLayout(false);
            this.MailWarden.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab MailWarden;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton TestBtn;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton Unreg;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ShowDebug;
    }

    partial class ThisRibbonCollection
    {
        internal AddInRibon AddInRibon
        {
            get { return this.GetRibbon<AddInRibon>(); }
        }
    }
}
