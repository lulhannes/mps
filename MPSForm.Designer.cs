namespace MPS
{
    partial class MPSForm
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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.verbreekVerbindingMetOuderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verwijderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.gbMalware = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lbInfecties = new System.Windows.Forms.ListBox();
            this.gbApparaat = new System.Windows.Forms.GroupBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lblAntivirus = new System.Windows.Forms.Label();
            this.lblFirewall = new System.Windows.Forms.Label();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.cntxtNetwerk.SuspendLayout();
            this.panelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            this.gbMalware.SuspendLayout();
            this.gbApparaat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
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
            this.splitContainer.Size = new System.Drawing.Size(1076, 568);
            this.splitContainer.SplitterDistance = 906;
            this.splitContainer.TabIndex = 0;
            // 
            // cntxtNetwerk
            // 
            this.cntxtNetwerk.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.verbreekVerbindingMetOuderToolStripMenuItem,
            this.verwijderToolStripMenuItem});
            this.cntxtNetwerk.Name = "cntxtNetwerk";
            this.cntxtNetwerk.Size = new System.Drawing.Size(181, 70);
            this.cntxtNetwerk.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.cntxtNetwerk_Closed);
            this.cntxtNetwerk.Opening += new System.ComponentModel.CancelEventHandler(this.cntxtNetwerk_Opening);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem1.Text = "Nieuw...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(128, 22);
            this.toolStripMenuItem2.Text = "Switch";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.nieuweSwitchToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(128, 22);
            this.toolStripMenuItem3.Text = "Router";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.nieuweRouterToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(128, 22);
            this.toolStripMenuItem4.Text = "Laptop";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.nieuweLaptopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(128, 22);
            this.toolStripMenuItem5.Text = "Computer";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.nieuweComputerToolStripMenuItem_Click);
            // 
            // verbreekVerbindingMetOuderToolStripMenuItem
            // 
            this.verbreekVerbindingMetOuderToolStripMenuItem.Name = "verbreekVerbindingMetOuderToolStripMenuItem";
            this.verbreekVerbindingMetOuderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.verbreekVerbindingMetOuderToolStripMenuItem.Text = "Verbreek verbinding";
            this.verbreekVerbindingMetOuderToolStripMenuItem.Click += new System.EventHandler(this.verbreekVerbindingMetOuderToolStripMenuItem_Click);
            // 
            // verwijderToolStripMenuItem
            // 
            this.verwijderToolStripMenuItem.Name = "verwijderToolStripMenuItem";
            this.verwijderToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.verwijderToolStripMenuItem.Text = "Verwijder apparaat";
            this.verwijderToolStripMenuItem.Click += new System.EventHandler(this.verwijderToolStripMenuItem_Click);
            // 
            // panelInfo
            // 
            this.panelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInfo.Controls.Add(this.pbIcon);
            this.panelInfo.Controls.Add(this.gbMalware);
            this.panelInfo.Controls.Add(this.gbApparaat);
            this.panelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInfo.Location = new System.Drawing.Point(0, 0);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(166, 568);
            this.panelInfo.TabIndex = 2;
            // 
            // pbIcon
            // 
            this.pbIcon.Location = new System.Drawing.Point(3, 3);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(128, 128);
            this.pbIcon.TabIndex = 0;
            this.pbIcon.TabStop = false;
            // 
            // gbMalware
            // 
            this.gbMalware.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMalware.Controls.Add(this.btnAdd);
            this.gbMalware.Controls.Add(this.btnRemove);
            this.gbMalware.Controls.Add(this.lbInfecties);
            this.gbMalware.Location = new System.Drawing.Point(3, 361);
            this.gbMalware.Name = "gbMalware";
            this.gbMalware.Size = new System.Drawing.Size(158, 203);
            this.gbMalware.TabIndex = 10;
            this.gbMalware.TabStop = false;
            this.gbMalware.Text = "Malware";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(7, 172);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(83, 172);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(68, 23);
            this.btnRemove.TabIndex = 9;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // lbInfecties
            // 
            this.lbInfecties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfecties.FormattingEnabled = true;
            this.lbInfecties.Location = new System.Drawing.Point(7, 19);
            this.lbInfecties.Name = "lbInfecties";
            this.lbInfecties.Size = new System.Drawing.Size(144, 147);
            this.lbInfecties.TabIndex = 6;
            // 
            // gbApparaat
            // 
            this.gbApparaat.Controls.Add(this.numericUpDown2);
            this.gbApparaat.Controls.Add(this.numericUpDown1);
            this.gbApparaat.Controls.Add(this.lblAntivirus);
            this.gbApparaat.Controls.Add(this.lblFirewall);
            this.gbApparaat.Location = new System.Drawing.Point(4, 138);
            this.gbApparaat.Name = "gbApparaat";
            this.gbApparaat.Size = new System.Drawing.Size(157, 100);
            this.gbApparaat.TabIndex = 11;
            this.gbApparaat.TabStop = false;
            this.gbApparaat.Text = "Apparaat";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(65, 45);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown2.TabIndex = 5;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(65, 19);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown1.TabIndex = 4;
            // 
            // lblAntivirus
            // 
            this.lblAntivirus.AutoSize = true;
            this.lblAntivirus.Location = new System.Drawing.Point(9, 21);
            this.lblAntivirus.Name = "lblAntivirus";
            this.lblAntivirus.Size = new System.Drawing.Size(50, 13);
            this.lblAntivirus.TabIndex = 2;
            this.lblAntivirus.Text = "Antivirus:";
            // 
            // lblFirewall
            // 
            this.lblFirewall.AutoSize = true;
            this.lblFirewall.Location = new System.Drawing.Point(14, 47);
            this.lblFirewall.Name = "lblFirewall";
            this.lblFirewall.Size = new System.Drawing.Size(45, 13);
            this.lblFirewall.TabIndex = 3;
            this.lblFirewall.Text = "Firewall:";
            // 
            // MPSForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 568);
            this.Controls.Add(this.splitContainer);
            this.Name = "MPSForm";
            this.Text = "formMain";
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.cntxtNetwerk.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            this.gbMalware.ResumeLayout(false);
            this.gbApparaat.ResumeLayout(false);
            this.gbApparaat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.ContextMenuStrip cntxtNetwerk;
        private System.Windows.Forms.ToolStripMenuItem verwijderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verbreekVerbindingMetOuderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.Label lblFirewall;
        private System.Windows.Forms.Label lblAntivirus;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lbInfecties;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox gbMalware;
        private System.Windows.Forms.GroupBox gbApparaat;

    }
}