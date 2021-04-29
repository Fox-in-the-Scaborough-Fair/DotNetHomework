namespace Homework9
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Warning = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.UrlInput = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UrlGetView = new System.Windows.Forms.DataGridView();
            this.UrlText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UrlGet = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.UrlLoseView = new System.Windows.Forms.DataGridView();
            this.UrlText2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UrlLose = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UrlGetView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UrlGet)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UrlLoseView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UrlLose)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Warning);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.UrlInput);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 100);
            this.panel1.TabIndex = 0;
            // 
            // Warning
            // 
            this.Warning.AutoSize = true;
            this.Warning.Location = new System.Drawing.Point(13, 58);
            this.Warning.Name = "Warning";
            this.Warning.Size = new System.Drawing.Size(0, 12);
            this.Warning.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(349, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "启动爬虫";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "初始URL";
            // 
            // UrlInput
            // 
            this.UrlInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.UrlInput.Location = new System.Drawing.Point(109, 21);
            this.UrlInput.Name = "UrlInput";
            this.UrlInput.Size = new System.Drawing.Size(616, 21);
            this.UrlInput.TabIndex = 0;
            this.UrlInput.TextChanged += new System.EventHandler(this.UrlInput_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UrlGetView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 350);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已爬取的URL";
            // 
            // UrlGetView
            // 
            this.UrlGetView.AutoGenerateColumns = false;
            this.UrlGetView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UrlGetView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UrlText});
            this.UrlGetView.DataSource = this.UrlGet;
            this.UrlGetView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UrlGetView.Location = new System.Drawing.Point(3, 17);
            this.UrlGetView.Name = "UrlGetView";
            this.UrlGetView.RowTemplate.Height = 23;
            this.UrlGetView.Size = new System.Drawing.Size(370, 330);
            this.UrlGetView.TabIndex = 0;
            this.UrlGetView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.UrlGetView_CellContentClick);
            // 
            // UrlText
            // 
            this.UrlText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UrlText.HeaderText = "URL";
            this.UrlText.Name = "UrlText";
            this.UrlText.ReadOnly = true;
            // 
            // UrlGet
            // 
            this.UrlGet.DataSource = typeof(SimpleCrawler.MyCrawler);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.UrlLoseView);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(376, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(433, 350);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "爬取错误的URL";
            // 
            // UrlLoseView
            // 
            this.UrlLoseView.AutoGenerateColumns = false;
            this.UrlLoseView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UrlLoseView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UrlText2});
            this.UrlLoseView.DataSource = this.UrlLose;
            this.UrlLoseView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UrlLoseView.Location = new System.Drawing.Point(3, 17);
            this.UrlLoseView.Name = "UrlLoseView";
            this.UrlLoseView.RowTemplate.Height = 23;
            this.UrlLoseView.Size = new System.Drawing.Size(427, 330);
            this.UrlLoseView.TabIndex = 0;
            // 
            // UrlText2
            // 
            this.UrlText2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UrlText2.HeaderText = "URL";
            this.UrlText2.Name = "UrlText2";
            this.UrlText2.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "爬虫";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UrlGetView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UrlGet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UrlLoseView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UrlLose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UrlInput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView UrlGetView;
        private System.Windows.Forms.BindingSource UrlGet;
        private System.Windows.Forms.DataGridView UrlLoseView;
        private System.Windows.Forms.BindingSource UrlLose;
        private System.Windows.Forms.Label Warning;
        private System.Windows.Forms.DataGridViewTextBoxColumn UrlText;
        private System.Windows.Forms.DataGridViewTextBoxColumn UrlText2;
    }
}

