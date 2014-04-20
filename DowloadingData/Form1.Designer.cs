namespace DowloadingData
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
            this.btnRead = new System.Windows.Forms.Button();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtregex = new System.Windows.Forms.TextBox();
            this.btnregex = new System.Windows.Forms.Button();
            this.txtketqua = new System.Windows.Forms.TextBox();
            this.txtregex2 = new System.Windows.Forms.TextBox();
            this.txtValue2 = new System.Windows.Forms.TextBox();
            this.btnValue = new System.Windows.Forms.Button();
            this.btnVolumn = new System.Windows.Forms.Button();
            this.btnRegexVolum = new System.Windows.Forms.Button();
            this.lblAction = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRead
            // 
            this.btnRead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRead.Location = new System.Drawing.Point(836, 27);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "LoadPage";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // txtURL
            // 
            this.txtURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtURL.Location = new System.Drawing.Point(12, 29);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(802, 20);
            this.txtURL.TabIndex = 1;
            // 
            // txtContent
            // 
            this.txtContent.AcceptsReturn = true;
            this.txtContent.AcceptsTab = true;
            this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContent.Location = new System.Drawing.Point(12, 60);
            this.txtContent.Multiline = true;
            this.txtContent.Name = "txtContent";
            this.txtContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContent.Size = new System.Drawing.Size(184, 413);
            this.txtContent.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(926, 26);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtregex
            // 
            this.txtregex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtregex.Location = new System.Drawing.Point(12, 3);
            this.txtregex.Name = "txtregex";
            this.txtregex.Size = new System.Drawing.Size(302, 20);
            this.txtregex.TabIndex = 1;
            // 
            // btnregex
            // 
            this.btnregex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnregex.Location = new System.Drawing.Point(836, -1);
            this.btnregex.Name = "btnregex";
            this.btnregex.Size = new System.Drawing.Size(75, 23);
            this.btnregex.TabIndex = 0;
            this.btnregex.Text = "Regex";
            this.btnregex.UseVisualStyleBackColor = true;
            this.btnregex.Click += new System.EventHandler(this.btnregex_Click);
            // 
            // txtketqua
            // 
            this.txtketqua.AcceptsReturn = true;
            this.txtketqua.AcceptsTab = true;
            this.txtketqua.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtketqua.Location = new System.Drawing.Point(205, 86);
            this.txtketqua.Multiline = true;
            this.txtketqua.Name = "txtketqua";
            this.txtketqua.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtketqua.Size = new System.Drawing.Size(779, 189);
            this.txtketqua.TabIndex = 2;
            this.txtketqua.Text = "showOHLCTooltip(event, \'A\', \'[Fri. Dec 16, 2011]\', \'KCZ11\', \'213.4000000\', \'214.2" +
    "500000\', \'210.9500000\', \'210.9500000\'";
            // 
            // txtregex2
            // 
            this.txtregex2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtregex2.Location = new System.Drawing.Point(320, 3);
            this.txtregex2.Name = "txtregex2";
            this.txtregex2.Size = new System.Drawing.Size(302, 20);
            this.txtregex2.TabIndex = 1;
            // 
            // txtValue2
            // 
            this.txtValue2.AcceptsReturn = true;
            this.txtValue2.AcceptsTab = true;
            this.txtValue2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue2.Location = new System.Drawing.Point(205, 281);
            this.txtValue2.Multiline = true;
            this.txtValue2.Name = "txtValue2";
            this.txtValue2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtValue2.Size = new System.Drawing.Size(779, 184);
            this.txtValue2.TabIndex = 2;
            // 
            // btnValue
            // 
            this.btnValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValue.Location = new System.Drawing.Point(917, 1);
            this.btnValue.Name = "btnValue";
            this.btnValue.Size = new System.Drawing.Size(84, 23);
            this.btnValue.TabIndex = 0;
            this.btnValue.Text = "GetDataOHLC";
            this.btnValue.UseVisualStyleBackColor = true;
            this.btnValue.Click += new System.EventHandler(this.btnValue_Click);
            // 
            // btnVolumn
            // 
            this.btnVolumn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVolumn.Location = new System.Drawing.Point(746, 2);
            this.btnVolumn.Name = "btnVolumn";
            this.btnVolumn.Size = new System.Drawing.Size(84, 23);
            this.btnVolumn.TabIndex = 0;
            this.btnVolumn.Text = "GetDataVolumn";
            this.btnVolumn.UseVisualStyleBackColor = true;
            this.btnVolumn.Click += new System.EventHandler(this.btnVolumn_Click);
            // 
            // btnRegexVolum
            // 
            this.btnRegexVolum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegexVolum.Location = new System.Drawing.Point(651, 2);
            this.btnRegexVolum.Name = "btnRegexVolum";
            this.btnRegexVolum.Size = new System.Drawing.Size(75, 23);
            this.btnRegexVolum.TabIndex = 0;
            this.btnRegexVolum.Text = "Regex";
            this.btnRegexVolum.UseVisualStyleBackColor = true;
            this.btnRegexVolum.Click += new System.EventHandler(this.btnRegexVolum_Click);
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(202, 63);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(35, 13);
            this.lblAction.TabIndex = 3;
            this.lblAction.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 485);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.txtValue2);
            this.Controls.Add(this.txtketqua);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.txtregex2);
            this.Controls.Add(this.txtregex);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnVolumn);
            this.Controls.Add(this.btnValue);
            this.Controls.Add(this.btnRegexVolum);
            this.Controls.Add(this.btnregex);
            this.Controls.Add(this.btnRead);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtregex;
        private System.Windows.Forms.Button btnregex;
        public System.Windows.Forms.TextBox txtketqua;
        public System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.TextBox txtregex2;
        public System.Windows.Forms.TextBox txtValue2;
        private System.Windows.Forms.Button btnValue;
        private System.Windows.Forms.Button btnVolumn;
        private System.Windows.Forms.Button btnRegexVolum;
        private System.Windows.Forms.Label lblAction;
    }
}

