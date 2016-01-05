﻿using System;
using System.Windows.Forms;

namespace CommodityPredictor
{
    partial class WinMarketPredictor
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
            this._labPathToSp = new System.Windows.Forms.Label();
            this._tbPathToSp = new System.Windows.Forms.TextBox();
            this._labPathToPR = new System.Windows.Forms.Label();
            this._tbPathToUSDJPY = new System.Windows.Forms.TextBox();
            this._tbMain = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._gbTrain = new System.Windows.Forms.GroupBox();
            this.lblTimeTran = new System.Windows.Forms.Label();
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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DoThi_GiaiTri = new ZedGraph.ZedGraphControl();
            this._tbPathToDow = new System.Windows.Forms.TextBox();
            this._tbPathToEURUSD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._nudHiddenUnits = new System.Windows.Forms.NumericUpDown();
            this._nudHiddenLayers = new System.Windows.Forms.NumericUpDown();
            this._labHIddenUnits = new System.Windows.Forms.Label();
            this._labHiddenLayers = new System.Windows.Forms.Label();
            this._tbPathToCommodity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this._txtNikkieIndex = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this._txtXAUUSD = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActualSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PredictedSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ErrorDifference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._tbMain.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this._gbTrain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTrainingResults)).BeginInit();
            this.tabPage1.SuspendLayout();
            this._gbPredict.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvPredictionResults)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._nudHiddenUnits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudHiddenLayers)).BeginInit();
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
            this._labPathToPR.Size = new System.Drawing.Size(229, 13);
            this._labPathToPR.TabIndex = 3;
            this._labPathToPR.Text = "Path to USD-JPY Rates (double click to select)";
            // 
            // _tbPathToUSDJPY
            // 
            this._tbPathToUSDJPY.Location = new System.Drawing.Point(13, 69);
            this._tbPathToUSDJPY.Name = "_tbPathToUSDJPY";
            this._tbPathToUSDJPY.Size = new System.Drawing.Size(250, 20);
            this._tbPathToUSDJPY.TabIndex = 4;
            this._tbPathToUSDJPY.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TbPathToPrMouseDoubleClick);
            // 
            // _tbMain
            // 
            this._tbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._tbMain.Controls.Add(this.tabPage2);
            this._tbMain.Controls.Add(this.tabPage1);
            this._tbMain.Controls.Add(this.tabPage3);
            this._tbMain.Location = new System.Drawing.Point(13, 177);
            this._tbMain.Name = "_tbMain";
            this._tbMain.SelectedIndex = 0;
            this._tbMain.Size = new System.Drawing.Size(775, 358);
            this._tbMain.TabIndex = 6;
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
            this.tabPage2.Size = new System.Drawing.Size(767, 332);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Train";
            // 
            // _gbTrain
            // 
            this._gbTrain.Controls.Add(this.lblTimeTran);
            this._gbTrain.Controls.Add(this._dtpTrainUntil);
            this._gbTrain.Controls.Add(this.label4);
            this._gbTrain.Controls.Add(this.label3);
            this._gbTrain.Controls.Add(this._dtpTrainFrom);
            this._gbTrain.Location = new System.Drawing.Point(9, 1);
            this._gbTrain.Name = "_gbTrain";
            this._gbTrain.Size = new System.Drawing.Size(750, 60);
            this._gbTrain.TabIndex = 12;
            this._gbTrain.TabStop = false;
            // 
            // lblTimeTran
            // 
            this.lblTimeTran.AutoSize = true;
            this.lblTimeTran.Location = new System.Drawing.Point(541, 38);
            this.lblTimeTran.Name = "lblTimeTran";
            this.lblTimeTran.Size = new System.Drawing.Size(55, 13);
            this.lblTimeTran.TabIndex = 15;
            this.lblTimeTran.Text = "Train Until";
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
            this._btnExport.Location = new System.Drawing.Point(682, 308);
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
            this._btnStop.Location = new System.Drawing.Point(601, 308);
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
            this._btnStartTraining.Location = new System.Drawing.Point(520, 308);
            this._btnStartTraining.Name = "_btnStartTraining";
            this._btnStartTraining.Size = new System.Drawing.Size(75, 23);
            this._btnStartTraining.TabIndex = 1;
            this._btnStartTraining.Text = "Start";
            this._btnStartTraining.UseVisualStyleBackColor = true;
            this._btnStartTraining.Click += new System.EventHandler(this.BtnStartTrainingClick);
            // 
            // _dgvTrainingResults
            // 
            this._dgvTrainingResults.AllowUserToAddRows = false;
            this._dgvTrainingResults.AllowUserToDeleteRows = false;
            this._dgvTrainingResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvTrainingResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvTrainingResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Epoch,
            this.Error});
            this._dgvTrainingResults.Location = new System.Drawing.Point(8, 67);
            this._dgvTrainingResults.Name = "_dgvTrainingResults";
            this._dgvTrainingResults.ReadOnly = true;
            this._dgvTrainingResults.RowHeadersWidth = 15;
            this._dgvTrainingResults.Size = new System.Drawing.Size(752, 240);
            this._dgvTrainingResults.TabIndex = 0;
            // 
            // Epoch
            // 
            this.Epoch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Epoch.HeaderText = "Epoch";
            this.Epoch.Name = "Epoch";
            this.Epoch.ReadOnly = true;
            // 
            // Error
            // 
            this.Error.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Error.HeaderText = "Error";
            this.Error.Name = "Error";
            this.Error.ReadOnly = true;
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
            this.tabPage1.Size = new System.Drawing.Size(767, 332);
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
            this._btnSaveResults.Location = new System.Drawing.Point(570, 303);
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
            this._btnLoad.Location = new System.Drawing.Point(6, 303);
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
            this._btnPredict.Location = new System.Drawing.Point(687, 303);
            this._btnPredict.Name = "_btnPredict";
            this._btnPredict.Size = new System.Drawing.Size(75, 23);
            this._btnPredict.TabIndex = 2;
            this._btnPredict.Text = "Predict";
            this._btnPredict.UseVisualStyleBackColor = true;
            this._btnPredict.Click += new System.EventHandler(this.BtnPredictClick);
            // 
            // _dgvPredictionResults
            // 
            this._dgvPredictionResults.AllowUserToAddRows = false;
            this._dgvPredictionResults.AllowUserToDeleteRows = false;
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
            this._dgvPredictionResults.ReadOnly = true;
            this._dgvPredictionResults.RowHeadersWidth = 15;
            this._dgvPredictionResults.Size = new System.Drawing.Size(755, 225);
            this._dgvPredictionResults.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.DoThi_GiaiTri);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(767, 332);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Graph";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // DoThi_GiaiTri
            // 
            this.DoThi_GiaiTri.Location = new System.Drawing.Point(20, 6);
            this.DoThi_GiaiTri.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.DoThi_GiaiTri.Name = "DoThi_GiaiTri";
            this.DoThi_GiaiTri.ScrollGrace = 0D;
            this.DoThi_GiaiTri.ScrollMaxX = 0D;
            this.DoThi_GiaiTri.ScrollMaxY = 0D;
            this.DoThi_GiaiTri.ScrollMaxY2 = 0D;
            this.DoThi_GiaiTri.ScrollMinX = 0D;
            this.DoThi_GiaiTri.ScrollMinY = 0D;
            this.DoThi_GiaiTri.ScrollMinY2 = 0D;
            this.DoThi_GiaiTri.Size = new System.Drawing.Size(724, 320);
            this.DoThi_GiaiTri.TabIndex = 4;
            this.DoThi_GiaiTri.Load += new System.EventHandler(this.DoThi_GiaiTri_Load);
            // 
            // _tbPathToDow
            // 
            this._tbPathToDow.Location = new System.Drawing.Point(16, 110);
            this._tbPathToDow.Name = "_tbPathToDow";
            this._tbPathToDow.Size = new System.Drawing.Size(250, 20);
            this._tbPathToDow.TabIndex = 7;
            this._tbPathToDow.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TbPathToDowMouseDoubleClick);
            // 
            // _tbPathToEURUSD
            // 
            this._tbPathToEURUSD.Location = new System.Drawing.Point(313, 70);
            this._tbPathToEURUSD.Name = "_tbPathToEURUSD";
            this._tbPathToEURUSD.Size = new System.Drawing.Size(250, 20);
            this._tbPathToEURUSD.TabIndex = 8;
            this._tbPathToEURUSD.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TbPathToNasdaqMouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Path to Dow indexes (double click to select)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Path to EUR-USD Rate (double click to select)";
            // 
            // _nudHiddenUnits
            // 
            this._nudHiddenUnits.Location = new System.Drawing.Point(622, 31);
            this._nudHiddenUnits.Name = "_nudHiddenUnits";
            this._nudHiddenUnits.Size = new System.Drawing.Size(162, 20);
            this._nudHiddenUnits.TabIndex = 11;
            this._nudHiddenUnits.ValueChanged += new System.EventHandler(this.NudHiddenUnitsValueChanged);
            // 
            // _nudHiddenLayers
            // 
            this._nudHiddenLayers.Location = new System.Drawing.Point(622, 69);
            this._nudHiddenLayers.Name = "_nudHiddenLayers";
            this._nudHiddenLayers.Size = new System.Drawing.Size(162, 20);
            this._nudHiddenLayers.TabIndex = 12;
            this._nudHiddenLayers.ValueChanged += new System.EventHandler(this.NudHiddenLayersValueChanged);
            // 
            // _labHIddenUnits
            // 
            this._labHIddenUnits.AutoSize = true;
            this._labHIddenUnits.Location = new System.Drawing.Point(619, 13);
            this._labHIddenUnits.Name = "_labHIddenUnits";
            this._labHIddenUnits.Size = new System.Drawing.Size(68, 13);
            this._labHIddenUnits.TabIndex = 13;
            this._labHIddenUnits.Text = "Hidden Units";
            // 
            // _labHiddenLayers
            // 
            this._labHiddenLayers.AutoSize = true;
            this._labHiddenLayers.Location = new System.Drawing.Point(619, 54);
            this._labHiddenLayers.Name = "_labHiddenLayers";
            this._labHiddenLayers.Size = new System.Drawing.Size(75, 13);
            this._labHiddenLayers.TabIndex = 14;
            this._labHiddenLayers.Text = "Hidden Layers";
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
            this.label7.Size = new System.Drawing.Size(204, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Path to Commodity (double click to select)";
            // 
            // _txtNikkieIndex
            // 
            this._txtNikkieIndex.Location = new System.Drawing.Point(313, 110);
            this._txtNikkieIndex.Name = "_txtNikkieIndex";
            this._txtNikkieIndex.Size = new System.Drawing.Size(250, 20);
            this._txtNikkieIndex.TabIndex = 8;
            this._txtNikkieIndex.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._txtNikkieIndex_MouseDoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(312, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(212, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Path to Nikkie Index (double click to select)";
            // 
            // _txtXAUUSD
            // 
            this._txtXAUUSD.Location = new System.Drawing.Point(313, 155);
            this._txtXAUUSD.Name = "_txtXAUUSD";
            this._txtXAUUSD.Size = new System.Drawing.Size(250, 20);
            this._txtXAUUSD.TabIndex = 8;
            this._txtXAUUSD.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this._txtXAUUSD_MouseDoubleClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(312, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(227, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Path to XAU-USD Rate (double click to select)";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(625, 110);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(162, 20);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "11";
            this.textBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TbPathToNasdaqMouseDoubleClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(625, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Input Count";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(625, 155);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(162, 20);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "1";
            this.textBox2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TbPathToNasdaqMouseDoubleClick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(625, 138);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Output Count";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(14, 1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(13, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "p";
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // ActualSP
            // 
            this.ActualSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ActualSP.HeaderText = "Actual  Close ";
            this.ActualSP.Name = "ActualSP";
            this.ActualSP.ReadOnly = true;
            // 
            // PredictedSP
            // 
            this.PredictedSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PredictedSP.HeaderText = "Predicted Close";
            this.PredictedSP.Name = "PredictedSP";
            this.PredictedSP.ReadOnly = true;
            // 
            // ErrorDifference
            // 
            this.ErrorDifference.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ErrorDifference.HeaderText = "MAE";
            this.ErrorDifference.Name = "ErrorDifference";
            this.ErrorDifference.ReadOnly = true;
            // 
            // WinMarketPredictor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 562);
            this.Controls.Add(this._tbPathToCommodity);
            this.Controls.Add(this.label7);
            this.Controls.Add(this._labHiddenLayers);
            this.Controls.Add(this._labHIddenUnits);
            this.Controls.Add(this._nudHiddenLayers);
            this.Controls.Add(this._nudHiddenUnits);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._txtXAUUSD);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this._txtNikkieIndex);
            this.Controls.Add(this._tbPathToEURUSD);
            this.Controls.Add(this._tbPathToDow);
            this.Controls.Add(this._tbMain);
            this.Controls.Add(this._tbPathToUSDJPY);
            this.Controls.Add(this._labPathToPR);
            this.Controls.Add(this._tbPathToSp);
            this.Controls.Add(this._labPathToSp);
            this.Name = "WinMarketPredictor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WinFinancialMarketPredictorFormClosing);
            this.Load += new System.EventHandler(this.WinFinancialMarketPredictorLoad);
            this._tbMain.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this._gbTrain.ResumeLayout(false);
            this._gbTrain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvTrainingResults)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this._gbPredict.ResumeLayout(false);
            this._gbPredict.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._dgvPredictionResults)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._nudHiddenUnits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._nudHiddenLayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _labPathToSp;
        private System.Windows.Forms.TextBox _tbPathToSp;
        private System.Windows.Forms.Label _labPathToPR;
        private System.Windows.Forms.TextBox _tbPathToUSDJPY;
        private System.Windows.Forms.TabControl _tbMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button _btnExport;
        private System.Windows.Forms.Button _btnStop;
        private System.Windows.Forms.Button _btnStartTraining;
        private System.Windows.Forms.DataGridView _dgvTrainingResults;
        private System.Windows.Forms.DataGridView _dgvPredictionResults;
        private System.Windows.Forms.TextBox _tbPathToDow;
        private System.Windows.Forms.TextBox _tbPathToEURUSD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
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
        private System.Windows.Forms.NumericUpDown _nudHiddenUnits;
        private System.Windows.Forms.NumericUpDown _nudHiddenLayers;
        private System.Windows.Forms.Label _labHIddenUnits;
        private System.Windows.Forms.Label _labHiddenLayers;

        private readonly Action<int, double, TrainingAlgorithm, DataGridView> addAction = new Action<int, double, TrainingAlgorithm, DataGridView>((epoch, error, algorithm, dgvTrainingResults) =>
        {
            if (dgvTrainingResults.Rows.Count == 500) dgvTrainingResults.Rows.Clear();
            int rowIndex = dgvTrainingResults.Rows.Add(epoch, error, algorithm.ToString());
            dgvTrainingResults.FirstDisplayedScrollingRowIndex = rowIndex;
        });
        private TextBox _tbPathToCommodity;
        private Label label7;
        private TabPage tabPage3;
        private ZedGraph.ZedGraphControl DoThi_GiaiTri;
        private TextBox _txtNikkieIndex;
        private Label label8;
        private TextBox _txtXAUUSD;
        private Label label9;
        private TextBox textBox1;
        private Label label10;
        private TextBox textBox2;
        private Label label11;
        private DataGridViewTextBoxColumn Epoch;
        private DataGridViewTextBoxColumn Error;
        private Label lblTimeTran;
        private Label label12;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn ActualSP;
        private DataGridViewTextBoxColumn PredictedSP;
        private DataGridViewTextBoxColumn ErrorDifference;
    }
}

