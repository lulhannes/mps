namespace MPS
{
    partial class formMain
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.cntxtNetwerk = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nieuweComputerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.nieuweRouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nieuweSwitchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.cntxtNetwerk.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.White;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.ContextMenuStrip = this.cntxtNetwerk;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelInfo);
            this.splitContainer.Size = new System.Drawing.Size(1347, 818);
            this.splitContainer.SplitterDistance = 1137;
            this.splitContainer.TabIndex = 0;
            // 
            // cntxtNetwerk
            // 
            this.cntxtNetwerk.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nieuweComputerToolStripMenuItem,
            this.nieuweRouterToolStripMenuItem,
            this.nieuweSwitchToolStripMenuItem});
            this.cntxtNetwerk.Name = "cntxtNetwerk";
            this.cntxtNetwerk.Size = new System.Drawing.Size(170, 92);
            this.cntxtNetwerk.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.cntxtNetwerk_Closed);
            this.cntxtNetwerk.Opening += new System.ComponentModel.CancelEventHandler(this.cntxtNetwerk_Opening);
            // 
            // nieuweComputerToolStripMenuItem
            // 
            this.nieuweComputerToolStripMenuItem.Name = "nieuweComputerToolStripMenuItem";
            this.nieuweComputerToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.nieuweComputerToolStripMenuItem.Text = "Nieuwe computer";
            this.nieuweComputerToolStripMenuItem.Click += new System.EventHandler(this.nieuweComputerToolStripMenuItem_Click);
            // 
            // panelInfo
            // 
            this.panelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInfo.Controls.Add(this.label1);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(206, 818);
            this.panelInfo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 169);
            this.label1.TabIndex = 0;
            this.label1.Text = "Computer details:\n\nNetwerk:\n\nIP:\n\nType\n\nFirewall:\n\nVirusscanner:\n\nInfecties:";
            // 
            // nieuweRouterToolStripMenuItem
            // 
            this.nieuweRouterToolStripMenuItem.Name = "nieuweRouterToolStripMenuItem";
            this.nieuweRouterToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.nieuweRouterToolStripMenuItem.Text = "Nieuwe router";
            this.nieuweRouterToolStripMenuItem.Click += new System.EventHandler(this.nieuweRouterToolStripMenuItem_Click);
            // 
            // nieuweSwitchToolStripMenuItem
            // 
            this.nieuweSwitchToolStripMenuItem.Name = "nieuweSwitchToolStripMenuItem";
            this.nieuweSwitchToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.nieuweSwitchToolStripMenuItem.Text = "Nieuwe switch";
            this.nieuweSwitchToolStripMenuItem.Click += new System.EventHandler(this.nieuweSwitchToolStripMenuItem_Click);
            // 
            // formMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 818);
            this.Controls.Add(this.splitContainer);
            this.Name = "formMain";
            this.Text = "formMain";
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.cntxtNetwerk.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.ContextMenuStrip cntxtNetwerk;
        private System.Windows.Forms.ToolStripMenuItem nieuweComputerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nieuweRouterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nieuweSwitchToolStripMenuItem;

    }
}