namespace TBot
{
    partial class TBot
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genericCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.genericQuotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customQuotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableSpotifyAutosongchangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtChat = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.commandsToolStripMenuItem,
            this.logToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(512, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // commandsToolStripMenuItem
            // 
            this.commandsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.genericCommandsToolStripMenuItem,
            this.customCommandsToolStripMenuItem,
            this.genericQuotesToolStripMenuItem,
            this.customQuotesToolStripMenuItem});
            this.commandsToolStripMenuItem.Name = "commandsToolStripMenuItem";
            this.commandsToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.commandsToolStripMenuItem.Text = "Commands";
            // 
            // genericCommandsToolStripMenuItem
            // 
            this.genericCommandsToolStripMenuItem.Name = "genericCommandsToolStripMenuItem";
            this.genericCommandsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.genericCommandsToolStripMenuItem.Text = "Generic Commands";
            this.genericCommandsToolStripMenuItem.Click += new System.EventHandler(this.genericCommandsToolStripMenuItem_Click);
            // 
            // customCommandsToolStripMenuItem
            // 
            this.customCommandsToolStripMenuItem.Name = "customCommandsToolStripMenuItem";
            this.customCommandsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.customCommandsToolStripMenuItem.Text = "Custom Commands";
            this.customCommandsToolStripMenuItem.Click += new System.EventHandler(this.customCommandsToolStripMenuItem_Click);
            // 
            // genericQuotesToolStripMenuItem
            // 
            this.genericQuotesToolStripMenuItem.Name = "genericQuotesToolStripMenuItem";
            this.genericQuotesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.genericQuotesToolStripMenuItem.Text = "Generic Quotes";
            // 
            // customQuotesToolStripMenuItem
            // 
            this.customQuotesToolStripMenuItem.Name = "customQuotesToolStripMenuItem";
            this.customQuotesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.customQuotesToolStripMenuItem.Text = "Custom Quotes";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableLogToolStripMenuItem,
            this.enableSpotifyAutosongchangeToolStripMenuItem});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.logToolStripMenuItem.Text = "Settings";
            // 
            // enableLogToolStripMenuItem
            // 
            this.enableLogToolStripMenuItem.Checked = true;
            this.enableLogToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableLogToolStripMenuItem.Name = "enableLogToolStripMenuItem";
            this.enableLogToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.enableLogToolStripMenuItem.Text = "Enable Log";
            this.enableLogToolStripMenuItem.CheckedChanged += new System.EventHandler(this.enableLogToolStripMenuItem_CheckedChanged);
            this.enableLogToolStripMenuItem.Click += new System.EventHandler(this.enableLogToolStripMenuItem_Click);
            // 
            // enableSpotifyAutosongchangeToolStripMenuItem
            // 
            this.enableSpotifyAutosongchangeToolStripMenuItem.Name = "enableSpotifyAutosongchangeToolStripMenuItem";
            this.enableSpotifyAutosongchangeToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.enableSpotifyAutosongchangeToolStripMenuItem.Text = "Enable Spotify Autosongchange";
            this.enableSpotifyAutosongchangeToolStripMenuItem.ToolTipText = "Needs a restart to take effect!";
            this.enableSpotifyAutosongchangeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.enableSpotifyAutosongchangeToolStripMenuItem_CheckedChanged);
            this.enableSpotifyAutosongchangeToolStripMenuItem.Click += new System.EventHandler(this.enableSpotifyAutosongchangeToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 338);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtChat);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(504, 312);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chat";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtChat
            // 
            this.txtChat.BackColor = System.Drawing.Color.White;
            this.txtChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChat.Location = new System.Drawing.Point(3, 3);
            this.txtChat.Name = "txtChat";
            this.txtChat.ReadOnly = true;
            this.txtChat.Size = new System.Drawing.Size(498, 306);
            this.txtChat.TabIndex = 0;
            this.txtChat.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(504, 312);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Commands";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(504, 312);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Songrequest";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txtLog);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(504, 312);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Connect";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(498, 306);
            this.txtLog.TabIndex = 2;
            this.txtLog.Text = "";
            // 
            // TBot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 362);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(497, 346);
            this.Name = "TBot";
            this.Text = "SpotiBoti";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpotiBoti_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genericCommandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customCommandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genericQuotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customQuotesToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        public System.Windows.Forms.RichTextBox txtLog;
        public System.Windows.Forms.RichTextBox txtChat;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableSpotifyAutosongchangeToolStripMenuItem;

    }
}

