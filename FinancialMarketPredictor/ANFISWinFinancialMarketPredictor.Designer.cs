using System;
using System.Windows.Forms;

namespace FinancialMarketPredictor
{
    partial class ANFISWinFinancialMarketPredictor
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
            this._labPathToSp = new System.Windows.Forms.Label();
            this._tbPathToSp = new System.Windows.Forms.TextBox();
            this._labPathToPR = new System.Windows.Forms.Label();
            this._tbPathToPR = new System.Windows.Forms.TextBox();
            this._tbMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this._gbPredict = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this._dtpPredictTo = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this._dtpPredictFrom = new System.Windows.Forms.DateTimePicker();
            this._btnSaveResults = new System.Windows.Forms.Button();
            this._btnLoad = new System.Windows.Forms.Button();
            this._btnPredict = new System.Windows.Forms.Button();
            this._dgvPredictionResults = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActualSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PredictedSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorDifference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._gbTrain = new System.Windows.Forms.GroupBox();
            this._dtpTrainUntil = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._dtpTrainFrom = new System.Windows.Forms.DateTimePicker();
            this._btnExport = new System.Windows.Forms.Button();
            this._btnStop = new System.Windows.Forms.Button();
            this._btnStartTraining = new System.Windows.Forms.Button();
            this._dgvTrainingResults = new System.Windows.Forms.DataGridView();
            this.Epoch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Error = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrainingAlgorithm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._tbPathToCommodity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._tbMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this._gbPredict.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvPredictionResults)).BeginInit();
            this.tabPage2.SuspendLayout();
            this._gbTrain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTrainingResults)).BeginInit();
            this.SuspendLayout();
            // 
            // _labPathToSp
            // 
            this._labPathToSp.AutoSize = true;
            this._labPathToSp.Location = new System.Drawing.Point(13, 13);
            this._labPathToSp.Name = "_labPathToSp";
            this._labPathToSp.Size = new System.Drawing.Size(227, 13);
            this._labPathToSp.TabIndex = 0;
            this._labPathToSp.Text = "Path to SP 500 indexes (double click to select)";
            // 
            // _tbPathToSp
            // 
            this._tbPathToSp.Location = new System.Drawing.Point(13, 30);
            this._tbPathToSp.Name = "_tbPathToSp";
            this._tbPathToSp.Size = new System.Drawing.Size(250, 20);
            this._tbPathToSp.TabIndex = 1;
            this._tbPathToSp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TbPathToSpMouseDoubleClick);
            // 
            // _labPathToPR
            // 
            this._labPathToPR.AutoSize = true;
            this._labPathToPR.Location = new System.Drawing.Point(13, 53);
            this._labPathToPR.Name = "_labPathToPR";
            this._labPathToPR.Size = new System.Drawing.Size(210, 13);
            this._labPathToPR.TabIndex = 3;
            this._labPathToPR.Text = "Path to Prime Rates (double click to select)";
            // 
            // _tbPathToPR
            // 
            this._tbPathToPR.Location = new System.Drawing.Point(13, 69);
            this._tbPathToPR.Name = "_tbPathToPR";
            this._tbPathToPR.Size = new System.Drawing.Size(250, 20);
            this._tbPathToPR.TabIndex = 4;
            this._tbPathToPR.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TbPathToPrMouseDoubleClick);
            // 
            // _tbMain
            // 
            this._tbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbMain.Controls.Add(this.tabPage1);
            this._tbMain.Controls.Add(this.tabPage2);
            this._tbMain.Location = new System.Drawing.Point(13, 96);
            this._tbMain.Name = "_tbMain";
            this._tbMain.SelectedIndex = 0;
            this._tbMain.Size = new System.Drawing.Size(775, 392);
            this._tbMain.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this._gbPredict);
            this.tabPage1.Controls.Add(this._btnSaveResults);
            this.tabPage1.Controls.Add(this._btnLoad);
            this.tabPage1.Controls.Add(this._btnPredict);
            this.tabPage1.Controls.Add(this._dgvPredictionResults);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(767, 366);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Predict";
            // 
            // _gbPredict
            // 
            this._gbPredict.Controls.Add(this.label6);
            this._gbPredict.Controls.Add(this._dtpPredictTo);
            this._gbPredict.Controls.Add(this.label5);
            this._gbPredict.Controls.Add(this._dtpPredictFrom);
            this._gbPredict.Location = new System.Drawing.Point(6, 6);
            this._gbPredict.Name = "_gbPredict";
            this._gbPredict.Size = new System.Drawing.Size(755, 60);
            this._gbPredict.TabIndex = 13;
            this._gbPredict.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(287, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Predict To";
            // 
            // _dtpPredictTo
            // 
            this._dtpPredictTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpPredictTo.Location = new System.Drawing.Point(290, 32);
            this._dtpPredictTo.Name = "_dtpPredictTo";
            this._dtpPredictTo.Size = new System.Drawing.Size(250, 20);
            this._dtpPredictTo.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Predict From";
            // 
            // _dtpPredictFrom
            // 
            this._dtpPredictFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpPredictFrom.Location = new System.Drawing.Point(6, 32);
            this._dtpPredictFrom.Name = "_dtpPredictFrom";
            this._dtpPredictFrom.Size = new System.Drawing.Size(250, 20);
            this._dtpPredictFrom.TabIndex = 14;
            // 
            // _btnSaveResults
            // 
            this._btnSaveResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSaveResults.Location = new System.Drawing.Point(570, 337);
            this._btnSaveResults.Name = "_btnSaveResults";
            this._btnSaveResults.Size = new System.Drawing.Size(111, 23);
            this._btnSaveResults.TabIndex = 4;
            this._btnSaveResults.Text = "Export Results";
            this._btnSaveResults.UseVisualStyleBackColor = true;
            this._btnSaveResults.Click += new System.EventHandler(this.BtnSaveResultsClick);
            // 
            // _btnLoad
            // 
            this._btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._btnLoad.Location = new System.Drawing.Point(6, 337);
            this._btnLoad.Name = "_btnLoad";
            this._btnLoad.Size = new System.Drawing.Size(89, 23);
            this._btnLoad.TabIndex = 3;
            this._btnLoad.Text = "Load network";
            this._btnLoad.UseVisualStyleBackColor = true;
            this._btnLoad.Click += new System.EventHandler(this.BtnLoadClick);
            // 
            // _btnPredict
            // 
            this._btnPredict.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnPredict.Location = new System.Drawing.Point(687, 337);
            this._btnPredict.Name = "_btnPredict";
            this._btnPredict.Size = new System.Drawing.Size(75, 23);
            this._btnPredict.TabIndex = 2;
            this._btnPredict.Text = "Predict";
            this._btnPredict.UseVisualStyleBackColor = true;
            this._btnPredict.Click += new System.EventHandler(this.BtnPredictClick);
            // 
            // _dgvPredictionResults
            // 
            this._dgvPredictionResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvPredictionResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvPredictionResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.ActualSP,
            this.PredictedSP,
            this.ErrorDifference});
            this._dgvPredictionResults.Location = new System.Drawing.Point(6, 72);
            this._dgvPredictionResults.Name = "_dgvPredictionResults";
            this._dgvPredictionResults.Size = new System.Drawing.Size(755, 259);
            this._dgvPredictionResults.TabIndex = 1;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // ActualSP
            // 
            this.ActualSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ActualSP.HeaderText = "Actual  Close ";
            this.ActualSP.Name = "ActualSP";
            // 
            // PredictedSP
            // 
            this.PredictedSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PredictedSP.HeaderText = "Predicted Close";
            this.PredictedSP.Name = "PredictedSP";
            // 
            // ErrorDifference
            // 
            this.ErrorDifference.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ErrorDifference.HeaderText = "RMS Error";
            this.ErrorDifference.Name = "ErrorDifference";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this._gbTrain);
            this.tabPage2.Controls.Add(this._btnExport);
            this.tabPage2.Controls.Add(this._btnStop);
            this.tabPage2.Controls.Add(this._btnStartTraining);
            this.tabPage2.Controls.Add(this._dgvTrainingResults);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(767, 366);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Train";
            // 
            // _gbTrain
            // 
            this._gbTrain.Controls.Add(this._dtpTrainUntil);
            this._gbTrain.Controls.Add(this.label4);
            this._gbTrain.Controls.Add(this.label3);
            this._gbTrain.Controls.Add(this._dtpTrainFrom);
            this._gbTrain.Location = new System.Drawing.Point(9, 6);
            this._gbTrain.Name = "_gbTrain";
            this._gbTrain.Size = new System.Drawing.Size(750, 60);
            this._gbTrain.TabIndex = 12;
            this._gbTrain.TabStop = false;
            // 
            // _dtpTrainUntil
            // 
            this._dtpTrainUntil.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpTrainUntil.Location = new System.Drawing.Point(285, 32);
            this._dtpTrainUntil.Name = "_dtpTrainUntil";
            this._dtpTrainUntil.Size = new System.Drawing.Size(250, 20);
            this._dtpTrainUntil.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(282, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Train Until";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Train From";
            // 
            // _dtpTrainFrom
            // 
            this._dtpTrainFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._dtpTrainFrom.Location = new System.Drawing.Point(6, 32);
            this._dtpTrainFrom.Name = "_dtpTrainFrom";
            this._dtpTrainFrom.Size = new System.Drawing.Size(250, 20);
            this._dtpTrainFrom.TabIndex = 11;
            // 
            // _btnExport
            // 
            this._btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnExport.Location = new System.Drawing.Point(686, 337);
            this._btnExport.Name = "_btnExport";
            this._btnExport.Size = new System.Drawing.Size(75, 23);
            this._btnExport.TabIndex = 3;
            this._btnExport.Text = "Save";
            this._btnExport.UseVisualStyleBackColor = true;
            this._btnExport.Click += new System.EventHandler(this.BtnExportClick);
            // 
            // _btnStop
            // 
            this._btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnStop.Location = new System.Drawing.Point(605, 337);
            this._btnStop.Name = "_btnStop";
            this._btnStop.Size = new System.Drawing.Size(75, 23);
            this._btnStop.TabIndex = 2;
            this._btnStop.Text = "Stop";
            this._btnStop.UseVisualStyleBackColor = true;
            this._btnStop.Click += new System.EventHandler(this.BtnStopClick);
            // 
            // _btnStartTraining
            // 
            this._btnStartTraining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnStartTraining.Location = new System.Drawing.Point(524, 337);
            this._btnStartTraining.Name = "_btnStartTraining";
            this._btnStartTraining.Size = new System.Drawing.Size(75, 23);
            this._btnStartTraining.TabIndex = 1;
            this._btnStartTraining.Text = "Start";
            this._btnStartTraining.UseVisualStyleBackColor = true;
            this._btnStartTraining.Click += new System.EventHandler(this.BtnStartTrainingClick);
            // 
            // _dgvTrainingResults
            // 
            this._dgvTrainingResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvTrainingResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvTrainingResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Epoch,
            this.Error,
            this.TrainingAlgorithm});
            this._dgvTrainingResults.Location = new System.Drawing.Point(9, 72);
            this._dgvTrainingResults.Name = "_dgvTrainingResults";
            this._dgvTrainingResults.Size = new System.Drawing.Size(752, 259);
            this._dgvTrainingResults.TabIndex = 0;
            // 
            // Epoch
            // 
            this.Epoch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Epoch.HeaderText = "Epoch";
            this.Epoch.Name = "Epoch";
            // 
            // Error
            // 
            this.Error.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Error.HeaderText = "Error";
            this.Error.Name = "Error";
            // 
            // TrainingAlgorithm
            // 
            this.TrainingAlgorithm.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TrainingAlgorithm.HeaderText = "Training Algorithm";
            this.TrainingAlgorithm.Name = "TrainingAlgorithm";
            // 
            // _tbPathToCommodity
            // 
            this._tbPathToCommodity.Location = new System.Drawing.Point(313, 30);
            this._tbPathToCommodity.Name = "_tbPathToCommodity";
            this._tbPathToCommodity.Size = new System.Drawing.Size(250, 20);
            this._tbPathToCommodity.TabIndex = 16;
            this._tbPathToCommodity.Text = "92_JUL13.csv";
            this._tbPathToCommodity.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._tbPathToCommodity_MouseDoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(313, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(210, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Path to Prime Rates (double click to select)";
            // 
            // ANFISWinFinancialMarketPredictor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 500);
            this.Controls.Add(this._tbPathToCommodity);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._tbMain);
            this.Controls.Add(this._tbPathToPR);
            this.Controls.Add(this._labPathToPR);
            this.Controls.Add(this._tbPathToSp);
            this.Controls.Add(this._labPathToSp);
            this.Name = "ANFISWinFinancialMarketPredictor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main ANFIS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinFinancialMarketPredictorFormClosing);
            this.Load += new System.EventHandler(this.WinFinancialMarketPredictorLoad);
            this._tbMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this._gbPredict.ResumeLayout(false);
            this._gbPredict.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvPredictionResults)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this._gbTrain.ResumeLayout(false);
            this._gbTrain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTrainingResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _labPathToSp;
        private System.Windows.Forms.TextBox _tbPathToSp;
        private System.Windows.Forms.Label _labPathToPR;
        private System.Windows.Forms.TextBox _tbPathToPR;
        private System.Windows.Forms.TabControl _tbMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button _btnExport;
        private System.Windows.Forms.Button _btnStop;
        private System.Windows.Forms.Button _btnStartTraining;
        private System.Windows.Forms.DataGridView _dgvTrainingResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn Epoch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Error;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrainingAlgorithm;
        private System.Windows.Forms.DataGridView _dgvPredictionResults;
        private System.Windows.Forms.DateTimePicker _dtpTrainFrom;
        private System.Windows.Forms.GroupBox _gbTrain;
        private System.Windows.Forms.DateTimePicker _dtpTrainUntil;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox _gbPredict;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker _dtpPredictFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker _dtpPredictTo;
        private System.Windows.Forms.Button _btnPredict;
        private System.Windows.Forms.Button _btnLoad;
        private System.Windows.Forms.Button _btnSaveResults;

        private readonly Action<int, double, TrainingAlgorithm, DataGridView> addAction = new Action<int, double, TrainingAlgorithm, DataGridView>((epoch, error, algorithm, dgvTrainingResults) =>
                                                                                                                                                   {
                                                                                                                                                       if(epoch % 1000 ==0) dgvTrainingResults.Rows.Clear();
                                                                                                                                                       int rowIndex = dgvTrainingResults.Rows.Add(epoch, error, algorithm.ToString());
                                                                                                                                                       dgvTrainingResults.FirstDisplayedScrollingRowIndex = rowIndex;
                                                                                                                                             });
        private TextBox _tbPathToCommodity;
        private Label label7;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn ActualSP;
        private DataGridViewTextBoxColumn PredictedSP;
        private DataGridViewTextBoxColumn ErrorDifference;
    }
}

