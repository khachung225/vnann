// ciumac.sergiu@gmail.com
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ANFISPrediction;
using BaseEntity.Entity;
using FinancialMarketPredictor.Entities;
using FinancialMarketPredictor.Properties;
using FinancialMarketPredictor.Utilities;
using System.Configuration;
using System.Security.Permissions;

namespace FinancialMarketPredictor
{
    public partial class ANFISWinFinancialMarketPredictor : Form
    {
        #region Private member fields
        
        /// <summary>
        /// Default path to S&P csv
        /// </summary>
        private string _pathToSp = "S&P500.csv";

        /// <summary>
        /// Default path to Prime interest rates csv
        /// </summary>
        private string _pathToPrimeRates = "EUR_USD.csv";
         
        /// <summary>
        /// Predict the percentage movement from a specific date
        /// </summary>
        private readonly DateTime _predictFrom = CSVReader.ParseDate("2013-07-01");

        /// <summary>
        /// Predict the percentage movement to a specific date
        /// </summary>
        private readonly DateTime _predictTo = CSVReader.ParseDate("2013-07-12");

        /// <summary>
        /// Learn from a specific date
        /// </summary>
        private readonly DateTime _learnFrom = CSVReader.ParseDate("2012-09-13");

        /// <summary>
        /// Learn until a specific date
        /// </summary>
        private readonly DateTime _learnTo = CSVReader.ParseDate("2013-06-28"); 

        /// <summary>
        /// Maximum date that can be specified for training and predicting, specified in the AppConfig
        /// </summary>
        private readonly DateTime _maxDate;

        /// <summary>
        /// Minimum date that can be specified for training and predicting, specified in the AppConfig
        /// </summary>
        private readonly DateTime _minDate;
        
        private ANFISManager _anfisManager;
        #endregion

        private const Double ErrorLimit = 0.005;
        /// <summary>
        /// Public parameter less constructor
        /// </summary>
        public ANFISWinFinancialMarketPredictor()
        {
            InitializeComponent();
            _btnStop.Enabled = false;
            _btnExport.Enabled = false;
            try
            {
                _maxDate = CSVReader.ParseDate(ConfigurationManager.AppSettings["MaxDate"]);
                _minDate = CSVReader.ParseDate(ConfigurationManager.AppSettings["MinDate"]);
            }
            catch
            {
                _maxDate = DateTime.Now;                        /*Maximum specified in the csv files*/
                _minDate = CSVReader.ParseDate("1971-02-05");   /*Minimum specified in the csv files*/
            }

            /*Set some reasonable default values*/
            _dtpTrainFrom.Value = _learnFrom;
            _dtpTrainUntil.Value = _learnTo;
            _dtpPredictFrom.Value = _predictFrom;
            _dtpPredictTo.Value = _predictTo;

            _dtpTrainFrom.MaxDate = _dtpTrainUntil.MaxDate = _dtpPredictFrom.MaxDate = _dtpPredictTo.MaxDate = _maxDate;
            _dtpTrainFrom.MinDate = _dtpTrainUntil.MinDate = _dtpPredictFrom.MinDate = _dtpPredictTo.MinDate = _minDate;

            _anfisManager = new ANFISManager(4, ErrorLimit);
            _anfisManager.CreatNetwork();
            _anfisManager.ErrorRate += AnfisManagerOnErrorRate;
        }

        private void AnfisManagerOnErrorRate(int stt, double er)
        {
            TrainingCallback(stt,er,FinancialMarketPredictor.TrainingAlgorithm.ANFIS);
        }

        /// <summary>
        /// Load the form
        /// </summary>
        private void WinFinancialMarketPredictorLoad(object sender, EventArgs e)
        {
            SetPathsInTextBoxes();  /*Set path in textboxes*/
        }


        /// <summary>
        /// Set paths in text boxes
        /// </summary>
        private void SetPathsInTextBoxes()
        {
            if (File.Exists(Path.GetFullPath(_pathToSp)))
                _tbPathToSp.Text = Path.GetFileName(_pathToSp);
            if (File.Exists(Path.GetFullPath(_pathToPrimeRates)))
                _tbPathToPR.Text = Path.GetFileName(_pathToPrimeRates);
            //if (File.Exists(Path.GetFullPath(_pathToNasdaq)))
            //    _tbPathToNasdaq.Text = Path.GetFileName(_pathToNasdaq);
        }

        /// <summary>
        /// Training callback, invoked at each iteration
        /// </summary>
        /// <param name="epoch">Epoch number</param>
        /// <param name="error">Current error</param>
        /// <param name="algorithm">Training algorithm</param>
        private void TrainingCallback(int epoch, double error, TrainingAlgorithm algorithm)
        {
            Invoke(addAction, new object [] {epoch, error, algorithm, _dgvTrainingResults});

        }

        /// <summary>
        /// Start training button pressed
        /// </summary>
        private void BtnStartTrainingClick(object sender, EventArgs e)
        {
            if (_dgvTrainingResults.Rows.Count != 0)
                _dgvTrainingResults.Rows.Clear();

            if (!File.Exists(_tbPathToPR.Text) || !File.Exists(_tbPathToSp.Text) || !File.Exists(_tbPathToCommodity.Text))
            {
                MessageBox.Show(Resources.InputMissing, Resources.FileMissing, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_anfisManager == null)
            {
                _anfisManager = new ANFISManager(4,ErrorLimit);
            }

            DateTime trainFrom = _dtpTrainFrom.Value;
            DateTime trainTo = _dtpTrainUntil.Value;

            if (trainFrom > trainTo)
            {
                MessageBox.Show(Resources.TrainFromTrainTo, Resources.BadParameters, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _dtpTrainFrom.Focus();
                return;
            }
            FadeControls(true);
            var manager = new FinancialPredictorManager(1, 1);
            manager.Load(_tbPathToSp.Text, _tbPathToPR.Text, _tbPathToCommodity.Text,false);
            var listdata = GetData(manager, trainFrom, trainTo);
            _anfisManager.SetData(listdata);

            _anfisManager.StartTrainning();
        }
        private List<ANFISData> GetData(FinancialPredictorManager manager, DateTime trainFrom, DateTime trainTo)
        {
            // find where we are starting from
            int startIndex = -1;
            int endIndex = -1;
            foreach (var sample in manager.Samples)
            {
                if (sample.Date.CompareTo(trainFrom) < 0)
                    startIndex++;
                if (sample.Date.CompareTo(trainTo) < 0)
                    endIndex++;
            }
            var listData = new List<ANFISData>();
            // grab the actual training data from that point
            for (int i = startIndex; i < endIndex; i++)
            {
                var input = new double[4];
                var ideal = new double[1];
                manager.GetInputData(i, input);
                manager.GetOutputData(i, ideal);
                var data = new ANFISData { Ideal = ideal[0] };
                for (int j = 0; j < 4; j++)
                {
                    data.Input.Add(j,input[j]);
                }
                listData.Add(data);
            }
            return listData;
        }
        /// <summary>
        /// Predict the values
        /// </summary>
        private void BtnPredictClick(object sender, EventArgs e)
        {
            if (_dgvPredictionResults.Rows.Count != 0)
                _dgvPredictionResults.Rows.Clear();

            if (_anfisManager == null)         /*The network is untrained*/
            {
                
                if (!File.Exists(_tbPathToPR.Text) || !File.Exists(_tbPathToSp.Text) || !File.Exists(_tbPathToCommodity.Text))
                {
                    MessageBox.Show(Resources.InputMissing, Resources.FileMissing, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                switch (MessageBox.Show(Resources.UntrainedPredictorWarning, Resources.NoNetwork, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        /*Load the network*/
                        this.Cursor = Cursors.WaitCursor;
                       _anfisManager = new ANFISManager(4,ErrorLimit);
                        using (OpenFileDialog ofd = new OpenFileDialog() { FileName = "predictor.anfis", Filter = Resources.NtwrkFilter })
                        {
                            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                try
                                {
                                    _anfisManager.LoadNetWorker(Path.GetFullPath(ofd.FileName));
                                }
                                catch
                                {
                                    MessageBox.Show(Resources.ExceptionMessage, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        return;
                }
            }
            DateTime predictFrom = _dtpPredictFrom.Value;
            DateTime predictTo = _dtpPredictTo.Value;
            if (predictFrom > predictTo)
            {
                MessageBox.Show(Resources.PredictFromToWarning, Resources.BadParameters, MessageBoxButtons.OK, MessageBoxIcon.Information);
                _dtpPredictFrom.Focus();
                return;
            }

            
            List<CommodityResults> results = null;
            try
            {

                var manager = new FinancialPredictorManager(1, 1);
                manager.Load(_tbPathToSp.Text, _tbPathToPR.Text, _tbPathToCommodity.Text, false);
                var listdata = GetData(manager, predictFrom, predictTo);
                int i = 0;
                foreach (var anfisData in listdata)
                {
                    var ideal = anfisData.Ideal;
                    _anfisManager.Predic(anfisData);
                    var er = Math.Abs(anfisData.Ideal - ideal)/ideal;
                    _dgvPredictionResults.Rows.Add("T +" + i.ToString(), ideal,
                                               anfisData.Ideal.ToString(CultureInfo.InvariantCulture),
                                               er.ToString("F4", CultureInfo.InvariantCulture));
                    i++;

                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Form closing event
        /// </summary>
        private void WinFinancialMarketPredictorFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_anfisManager != null)
                _anfisManager.StopTrainning();

        }

        /// <summary>
        /// Stop training
        /// </summary>
        private void BtnStopClick(object sender, EventArgs e)
        {
            FadeControls(false);
            _anfisManager.StopTrainning();
            _btnExport.Enabled = true;
        }

        /// <summary>
        /// New path to S&P500 index is selected
        /// </summary>
        private void TbPathToSpMouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { FileName = "S&P500.csv", Filter = Resources.CsvFilter };
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _tbPathToSp.Text = Path.GetFullPath(ofd.FileName);
            }
        }

        /// <summary>
        /// New path to Prime interest rate is selected
        /// </summary>
        private void TbPathToPrMouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { FileName = "EUR_USD.csv", Filter = Resources.CsvFilter };
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _tbPathToPR.Text = Path.GetFullPath(ofd.FileName);
            }
        }
 
        private void _tbPathToCommodity_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { FileName = "92_JUL13.csv", Filter = Resources.CsvFilter };
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _tbPathToCommodity.Text = Path.GetFullPath(ofd.FileName);
            }
        }

        /// <summary>
        /// Fade controls from the main form
        /// </summary>
        /// <param name="fade">If true - fade, otherwise - restore</param>
        private void FadeControls(bool fade)
        {
            Action<bool> action = (param) =>
                                  {
                                      _tbPathToSp.Enabled = param;
                                      _tbPathToPR.Enabled = param;
                                 
                                      _btnStartTraining.Enabled = param;
                                      _btnStop.Enabled = !param;
                                      _dtpPredictFrom.Enabled = param;
                                      _dtpPredictTo.Enabled = param;
                                      _dtpTrainFrom.Enabled = param;
                                      _dtpTrainUntil.Enabled = param;
                                
                                  };
            Invoke(action, !fade);
        }

        /// <summary>
        /// Save the network, for later reuse
        /// </summary>
        private void BtnExportClick(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { FileName = "predictor.anfis", Filter = Resources.NtwrkFilter })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FileIOPermission perm = new FileIOPermission(FileIOPermissionAccess.Write, Path.GetFullPath(sfd.FileName));
                    try
                    {
                        perm.Demand();
                    }
                    catch (System.Security.SecurityException)
                    {
                        MessageBox.Show(Resources.SecurityExceptionMessage, Resources.SecurityException, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    _anfisManager.SaveNetWorker(Path.GetFullPath(sfd.FileName));
                }
            }
        }

        /// <summary>
        /// Load previously saved network
        /// </summary>
        private void BtnLoadClick(object sender, EventArgs e)
        {
            if (!File.Exists(_tbPathToSp.Text) || !File.Exists(_tbPathToCommodity.Text) ||
                    !File.Exists(_tbPathToPR.Text))
            {
                MessageBox.Show(Resources.InputMissing, Resources.FileMissing, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(_anfisManager == null)
                _anfisManager = new ANFISManager(4,ErrorLimit);
            using (OpenFileDialog ofd = new OpenFileDialog() { FileName = "predictor.anfis", Filter = Resources.NtwrkFilter })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _anfisManager.LoadNetWorker(Path.GetFullPath(ofd.FileName));
                     
                    }
                    catch (System.Security.SecurityException)
                    {
                        MessageBox.Show(Resources.SecurityExceptionFolderLevel, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch
                    {
                        MessageBox.Show(Resources.ExceptionMessage, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Save results
        /// </summary>
        private void BtnSaveResultsClick(object sender, EventArgs e)
        {
            var dgvResults = _dgvPredictionResults;
            SaveFileDialog ofd = new SaveFileDialog {Filter = Resources.CsvFilter, FileName = "results.csv"};
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CSVWriter writer = null;
                try
                {
                    writer = new CSVWriter(ofd.FileName);
                }
                catch (System.Security.SecurityException)
                {
                    MessageBox.Show( Resources.SecurityExceptionFolderLevel, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                object[,] values = new object[dgvResults.Rows.Count + 2,dgvResults.Columns.Count];
                int rowIndex = 0;
                int colIndex = 0;
                foreach (DataGridViewColumn col in dgvResults.Columns) /*Writing Column Headers*/
                {
                    values[rowIndex, colIndex] = col.HeaderText;
                    colIndex++;
                }
                rowIndex++; /*1*/

                foreach (DataGridViewRow row in dgvResults.Rows) /*Writing the values*/
                {
                    colIndex = 0;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        values[rowIndex, colIndex] = cell.Value;
                        colIndex++;
                    }
                    rowIndex++;
                }

                /*Writing the results in the last row*/
                writer.Write(values);
            }
        }
    }
}
