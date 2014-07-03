﻿namespace AppTestTool
{
    partial class PredicDayByDay
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
            this._dtpPredictFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this._dtpPredictTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.ckbStart = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // _dtpPredictFrom
            // 
            this._dtpPredictFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpPredictFrom.Location = new System.Drawing.Point(95, 44);
            this._dtpPredictFrom.Name = "_dtpPredictFrom";
            this._dtpPredictFrom.Size = new System.Drawing.Size(135, 20);
            this._dtpPredictFrom.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Predich From";
            // 
            // _dtpPredictTo
            // 
            this._dtpPredictTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpPredictTo.Location = new System.Drawing.Point(291, 45);
            this._dtpPredictTo.Name = "_dtpPredictTo";
            this._dtpPredictTo.Size = new System.Drawing.Size(135, 20);
            this._dtpPredictTo.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "To ";
            // 
            // txtDetail
            // 
            this.txtDetail.Location = new System.Drawing.Point(23, 88);
            this.txtDetail.Multiline = true;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtDetail.Size = new System.Drawing.Size(403, 130);
            this.txtDetail.TabIndex = 17;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(116, 224);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 18;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(220, 224);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 18;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(23, 71);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(403, 11);
            this.progressBar1.TabIndex = 19;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Ứng dụng";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(80, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(337, 20);
            this.textBox2.TabIndex = 20;
            this.textBox2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox2_MouseDoubleClick);
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(301, 229);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(69, 13);
            this.lblAction.TabIndex = 16;
            this.lblAction.Text = "Predich From";
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.Location = new System.Drawing.Point(23, 224);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(75, 23);
            this.btnBaoCao.TabIndex = 22;
            this.btnBaoCao.Text = "Bao Cao";
            this.btnBaoCao.UseVisualStyleBackColor = true;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // ckbStart
            // 
            this.ckbStart.AutoSize = true;
            this.ckbStart.Checked = true;
            this.ckbStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbStart.Location = new System.Drawing.Point(423, 15);
            this.ckbStart.Name = "ckbStart";
            this.ckbStart.Size = new System.Drawing.Size(60, 17);
            this.ckbStart.TabIndex = 23;
            this.ckbStart.Text = "Restart";
            this.ckbStart.UseVisualStyleBackColor = true;
            // 
            // PredicDayByDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 262);
            this.Controls.Add(this.ckbStart);
            this.Controls.Add(this.btnBaoCao);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._dtpPredictTo);
            this.Controls.Add(this._dtpPredictFrom);
            this.Controls.Add(this.txtDetail);
            this.Controls.Add(this.progressBar1);
            this.Name = "PredicDayByDay";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.PredicDayByDay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker _dtpPredictFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker _dtpPredictTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.CheckBox ckbStart;
    }
}