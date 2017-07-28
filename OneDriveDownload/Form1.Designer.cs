namespace OneDriveDownload
{
    partial class Form1
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
            this.btnSync = new System.Windows.Forms.Button();
            this.progressLabel = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnPickFolder = new System.Windows.Forms.Button();
            this.lblFolderTarget = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnSignOut = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.overwriteAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(100, 11);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(75, 23);
            this.btnSync.TabIndex = 4;
            this.btnSync.Text = "Sync";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(12, 37);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(99, 13);
            this.progressLabel.TabIndex = 5;
            this.progressLabel.Text = "Press sync to begin";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 68);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(638, 268);
            this.treeView1.TabIndex = 6;
            // 
            // btnPickFolder
            // 
            this.btnPickFolder.Location = new System.Drawing.Point(569, 12);
            this.btnPickFolder.Name = "btnPickFolder";
            this.btnPickFolder.Size = new System.Drawing.Size(81, 22);
            this.btnPickFolder.TabIndex = 7;
            this.btnPickFolder.Text = "Pick folder...";
            this.btnPickFolder.UseVisualStyleBackColor = true;
            this.btnPickFolder.Click += new System.EventHandler(this.btnPickFolder_Click);
            // 
            // lblFolderTarget
            // 
            this.lblFolderTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFolderTarget.AutoSize = true;
            this.lblFolderTarget.Location = new System.Drawing.Point(394, 21);
            this.lblFolderTarget.Name = "lblFolderTarget";
            this.lblFolderTarget.Size = new System.Drawing.Size(99, 13);
            this.lblFolderTarget.TabIndex = 8;
            this.lblFolderTarget.Text = "(path goes      here)";
            this.lblFolderTarget.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(181, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnSignOut
            // 
            this.btnSignOut.Location = new System.Drawing.Point(262, 12);
            this.btnSignOut.Name = "btnSignOut";
            this.btnSignOut.Size = new System.Drawing.Size(82, 23);
            this.btnSignOut.TabIndex = 10;
            this.btnSignOut.Text = "Sign out";
            this.btnSignOut.UseVisualStyleBackColor = true;
            this.btnSignOut.Click += new System.EventHandler(this.btnSignOut_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(12, 11);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(82, 23);
            this.btnLogin.TabIndex = 11;
            this.btnLogin.Text = "Sign in";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // overwriteAll
            // 
            this.overwriteAll.AutoSize = true;
            this.overwriteAll.Location = new System.Drawing.Point(372, 48);
            this.overwriteAll.Name = "overwriteAll";
            this.overwriteAll.Size = new System.Drawing.Size(146, 17);
            this.overwriteAll.TabIndex = 12;
            this.overwriteAll.Text = "Overwrite all (WARNING)";
            this.overwriteAll.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 348);
            this.Controls.Add(this.overwriteAll);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnSignOut);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblFolderTarget);
            this.Controls.Add(this.btnPickFolder);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.btnSync);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnPickFolder;
        private System.Windows.Forms.Label lblFolderTarget;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnSignOut;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.CheckBox overwriteAll;
    }
}

