namespace MailWarden2.Visuals
{
    partial class DebugControl
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
            Globals.ThisAddIn.DebugControl = null;
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
            this.debugtext = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // debugtext
            // 
            this.debugtext.Location = new System.Drawing.Point(26, 25);
            this.debugtext.Name = "debugtext";
            this.debugtext.ReadOnly = true;
            this.debugtext.Size = new System.Drawing.Size(734, 393);
            this.debugtext.TabIndex = 0;
            this.debugtext.Text = "";
            this.debugtext.TextChanged += new System.EventHandler(this.debugtext_TextChanged);
            // 
            // DebugControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.debugtext);
            this.Name = "DebugControl";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox debugtext;
    }
}