namespace FacebookTool.App
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnCopyGetToken = new System.Windows.Forms.Button();
            this.txtTimeSleepCheckToken = new System.Windows.Forms.TextBox();
            this.txtNumberOfThreadGetToken = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbInfoGetToken = new System.Windows.Forms.Label();
            this.rtbInfoGetToken = new System.Windows.Forms.RichTextBox();
            this.rtbLogGetToken = new System.Windows.Forms.RichTextBox();
            this.btnStartGetToken = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnStartGetDetailPostGroup = new System.Windows.Forms.Button();
            this.tbGroupId = new System.Windows.Forms.TextBox();
            this.lbCrawlGroupPostStatus = new System.Windows.Forms.Label();
            this.rtbCrawlGroupPostException = new System.Windows.Forms.RichTextBox();
            this.rtbCrawlGroupPostInfo = new System.Windows.Forms.RichTextBox();
            this.btnStartCrawlGroupPost = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1051, 56);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(349, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(384, 38);
            this.label2.TabIndex = 2;
            this.label2.Text = "Facebook Tool All In One";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1051, 414);
            this.panel2.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1051, 414);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DarkGray;
            this.tabPage1.Controls.Add(this.btnCopyGetToken);
            this.tabPage1.Controls.Add(this.txtTimeSleepCheckToken);
            this.tabPage1.Controls.Add(this.txtNumberOfThreadGetToken);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lbInfoGetToken);
            this.tabPage1.Controls.Add(this.rtbInfoGetToken);
            this.tabPage1.Controls.Add(this.rtbLogGetToken);
            this.tabPage1.Controls.Add(this.btnStartGetToken);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1043, 385);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Get TOKEN";
            // 
            // btnCopyGetToken
            // 
            this.btnCopyGetToken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCopyGetToken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyGetToken.Location = new System.Drawing.Point(211, 7);
            this.btnCopyGetToken.Name = "btnCopyGetToken";
            this.btnCopyGetToken.Size = new System.Drawing.Size(206, 49);
            this.btnCopyGetToken.TabIndex = 4;
            this.btnCopyGetToken.Text = "Copy Get TOKEN";
            this.btnCopyGetToken.UseVisualStyleBackColor = false;
            this.btnCopyGetToken.Click += new System.EventHandler(this.btnCopyGetToken_Click);
            // 
            // txtTimeSleepCheckToken
            // 
            this.txtTimeSleepCheckToken.Location = new System.Drawing.Point(935, 323);
            this.txtTimeSleepCheckToken.Name = "txtTimeSleepCheckToken";
            this.txtTimeSleepCheckToken.Size = new System.Drawing.Size(70, 22);
            this.txtTimeSleepCheckToken.TabIndex = 3;
            this.txtTimeSleepCheckToken.Text = "100";
            // 
            // txtNumberOfThreadGetToken
            // 
            this.txtNumberOfThreadGetToken.Location = new System.Drawing.Point(768, 323);
            this.txtNumberOfThreadGetToken.Name = "txtNumberOfThreadGetToken";
            this.txtNumberOfThreadGetToken.Size = new System.Drawing.Size(44, 22);
            this.txtNumberOfThreadGetToken.TabIndex = 3;
            this.txtNumberOfThreadGetToken.Text = "9";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(834, 326);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Time Sleep";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(542, 323);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number Of Threads Get TOKEN";
            // 
            // lbInfoGetToken
            // 
            this.lbInfoGetToken.AutoSize = true;
            this.lbInfoGetToken.Location = new System.Drawing.Point(542, 349);
            this.lbInfoGetToken.Name = "lbInfoGetToken";
            this.lbInfoGetToken.Size = new System.Drawing.Size(94, 17);
            this.lbInfoGetToken.TabIndex = 2;
            this.lbInfoGetToken.Text = "InfoGetToken";
            // 
            // rtbInfoGetToken
            // 
            this.rtbInfoGetToken.Location = new System.Drawing.Point(545, 62);
            this.rtbInfoGetToken.Name = "rtbInfoGetToken";
            this.rtbInfoGetToken.Size = new System.Drawing.Size(490, 252);
            this.rtbInfoGetToken.TabIndex = 1;
            this.rtbInfoGetToken.Text = "";
            // 
            // rtbLogGetToken
            // 
            this.rtbLogGetToken.Location = new System.Drawing.Point(8, 62);
            this.rtbLogGetToken.Name = "rtbLogGetToken";
            this.rtbLogGetToken.Size = new System.Drawing.Size(528, 317);
            this.rtbLogGetToken.TabIndex = 1;
            this.rtbLogGetToken.Text = "";
            // 
            // btnStartGetToken
            // 
            this.btnStartGetToken.BackColor = System.Drawing.Color.Lime;
            this.btnStartGetToken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartGetToken.Location = new System.Drawing.Point(8, 6);
            this.btnStartGetToken.Name = "btnStartGetToken";
            this.btnStartGetToken.Size = new System.Drawing.Size(182, 50);
            this.btnStartGetToken.TabIndex = 0;
            this.btnStartGetToken.Text = "Start Get TOKEN";
            this.btnStartGetToken.UseVisualStyleBackColor = false;
            this.btnStartGetToken.Click += new System.EventHandler(this.btnStartGetToken_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Silver;
            this.tabPage2.Controls.Add(this.btnStartGetDetailPostGroup);
            this.tabPage2.Controls.Add(this.tbGroupId);
            this.tabPage2.Controls.Add(this.lbCrawlGroupPostStatus);
            this.tabPage2.Controls.Add(this.rtbCrawlGroupPostException);
            this.tabPage2.Controls.Add(this.rtbCrawlGroupPostInfo);
            this.tabPage2.Controls.Add(this.btnStartCrawlGroupPost);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1043, 385);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Crawl Group Post";
            // 
            // btnStartGetDetailPostGroup
            // 
            this.btnStartGetDetailPostGroup.BackColor = System.Drawing.Color.Lime;
            this.btnStartGetDetailPostGroup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartGetDetailPostGroup.Location = new System.Drawing.Point(875, 15);
            this.btnStartGetDetailPostGroup.Name = "btnStartGetDetailPostGroup";
            this.btnStartGetDetailPostGroup.Size = new System.Drawing.Size(142, 48);
            this.btnStartGetDetailPostGroup.TabIndex = 4;
            this.btnStartGetDetailPostGroup.Text = "Start Get Detail";
            this.btnStartGetDetailPostGroup.UseVisualStyleBackColor = false;
            this.btnStartGetDetailPostGroup.Click += new System.EventHandler(this.btnStartGetDetailPostGroup_Click);
            // 
            // tbGroupId
            // 
            this.tbGroupId.Location = new System.Drawing.Point(185, 28);
            this.tbGroupId.Name = "tbGroupId";
            this.tbGroupId.Size = new System.Drawing.Size(167, 22);
            this.tbGroupId.TabIndex = 3;
            this.tbGroupId.Text = "364997627165697";
            // 
            // lbCrawlGroupPostStatus
            // 
            this.lbCrawlGroupPostStatus.AutoSize = true;
            this.lbCrawlGroupPostStatus.Location = new System.Drawing.Point(394, 34);
            this.lbCrawlGroupPostStatus.Name = "lbCrawlGroupPostStatus";
            this.lbCrawlGroupPostStatus.Size = new System.Drawing.Size(48, 17);
            this.lbCrawlGroupPostStatus.TabIndex = 2;
            this.lbCrawlGroupPostStatus.Text = "Status";
            // 
            // rtbCrawlGroupPostException
            // 
            this.rtbCrawlGroupPostException.Location = new System.Drawing.Point(705, 75);
            this.rtbCrawlGroupPostException.Name = "rtbCrawlGroupPostException";
            this.rtbCrawlGroupPostException.Size = new System.Drawing.Size(312, 289);
            this.rtbCrawlGroupPostException.TabIndex = 1;
            this.rtbCrawlGroupPostException.Text = "";
            // 
            // rtbCrawlGroupPostInfo
            // 
            this.rtbCrawlGroupPostInfo.Location = new System.Drawing.Point(19, 75);
            this.rtbCrawlGroupPostInfo.Name = "rtbCrawlGroupPostInfo";
            this.rtbCrawlGroupPostInfo.Size = new System.Drawing.Size(680, 289);
            this.rtbCrawlGroupPostInfo.TabIndex = 1;
            this.rtbCrawlGroupPostInfo.Text = "";
            // 
            // btnStartCrawlGroupPost
            // 
            this.btnStartCrawlGroupPost.BackColor = System.Drawing.Color.Moccasin;
            this.btnStartCrawlGroupPost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartCrawlGroupPost.Location = new System.Drawing.Point(19, 18);
            this.btnStartCrawlGroupPost.Name = "btnStartCrawlGroupPost";
            this.btnStartCrawlGroupPost.Size = new System.Drawing.Size(142, 48);
            this.btnStartCrawlGroupPost.TabIndex = 0;
            this.btnStartCrawlGroupPost.Text = "Start";
            this.btnStartCrawlGroupPost.UseVisualStyleBackColor = false;
            this.btnStartCrawlGroupPost.Click += new System.EventHandler(this.btnStartCrawlGroupPost_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 470);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1051, 46);
            this.panel3.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 516);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facebook Tool";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnStartGetToken;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox rtbInfoGetToken;
        private System.Windows.Forms.RichTextBox rtbLogGetToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumberOfThreadGetToken;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbInfoGetToken;
        private System.Windows.Forms.TextBox txtTimeSleepCheckToken;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCopyGetToken;
        private System.Windows.Forms.TextBox tbGroupId;
        private System.Windows.Forms.Label lbCrawlGroupPostStatus;
        private System.Windows.Forms.RichTextBox rtbCrawlGroupPostException;
        private System.Windows.Forms.RichTextBox rtbCrawlGroupPostInfo;
        private System.Windows.Forms.Button btnStartCrawlGroupPost;
        private System.Windows.Forms.Button btnStartGetDetailPostGroup;
    }
}

