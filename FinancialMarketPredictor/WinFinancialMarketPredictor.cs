// ciumac.sergiu@gmail.com
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using BaseEntity.Entity;
using BaseEntity.Utils;
using FinancialMarketPredictor.Properties;
using System.Configuration;
using System.Security.Permissions;
using ZedGraph;

namespace FinancialMarketPredictor
{
    public partial class WinFinancialMarketPredictor : Form
    {
        #region Private member fields
        
        /// <summary>
        /// Default path to S&P csv
        /// </summary>
        private string _pathToSp = "$SPX_1512.csv";

        /// <summary>
        /// Default path to Prime interest rates csv
        /// </summary>
        private string _pathToEURUSD = "^EURUSD_1556.csv";
        private string _pathToUSDJPY = "^USDJPY_1556.csv";

        /// <summary>
        /// Default path to Nasdaq indexes csv
        /// </summary>
        private string _pathToNasdaq = "nasdaq.csv";

        /// <summary>
        /// Default path to Dow indexes csv
        /// </summary>
        private string _pathToDow = "$DOWI_1513.csv";

        private string _pathToCommodity = "SBN13_615.csv";

        private string _pathToXAUUSD = "^XAUUSD_1558.csv";
        private string _pathToNikkie = "$NKY_1469.csv";
   
        /// <summary>
        /// Predictor
        /// </summary>
        private PredictIndicators _predictor;

        /// <summary>
        /// Predict the percentage movement from a specific date
        /// </summary>
        private readonly DateTime _predictFrom = CSVReader.ParseDate("2013-01-01");// CSVReader.ParseDate("2013-07-01");

        /// <summary>
        /// Predict the percentage movement to a specific date
        /// </summary>
        private readonly DateTime _predictTo = CSVReader.ParseDate("2013-07-30");//CSVReader.ParseDate("2013-11-16");

        /// <summary>
        /// Learn from a specific date
        /// </summary>
        private readonly DateTime _learnFrom = CSVReader.ParseDate("2008-01-01");

        /// <summary>
        /// Learn until a specific date
        /// </summary>
        private readonly DateTime _learnTo = CSVReader.ParseDate("2012-12-31");  //CSVReader.ParseDate("2012-08-31");

        /// <summary>
        /// Maximum date that can be specified for training and predicting, specified in the AppConfig
        /// </summary>
        private readonly DateTime _maxDate;

        /// <summary>
        /// Minimum date that can be specified for training and predicting, specified in the AppConfig
        /// </summary>
        private readonly DateTime _minDate;

        /// <summary>
        /// Default parameter for hidden layers
        /// </summary>
        private int _hiddenLayers = 3;

        /// <summary>
        /// Default parameter for hidden units
        /// </summary>
        private int _hiddenUnits = 14;

        /// <summary>
        /// Check if there is a need in reloading the files
        /// </summary>
        private bool _reloadFiles = false;

        private static System.Timers.Timer aTimer;
        #endregion

        /// <summary>
        /// Public parameter less constructor
        /// </summary>
        public WinFinancialMarketPredictor()
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
            //lấy dữ liệu từ vùng nhớ static
            if (AppGlobol.IsAutoRun)
            {
                _predictFrom =  AppGlobol.PredicDate;
                _predictTo = AppGlobol.PredicDate.AddDays(2);
                _learnTo = AppGlobol.PredicDate;
                _learnTo =_learnTo.AddDays(-1);
            }

            /*Set some reasonable default values*/
            _dtpTrainFrom.Value = _learnFrom;
            _dtpTrainUntil.Value = _learnTo;
            _dtpPredictFrom.Value = _predictFrom;
            _dtpPredictTo.Value = _predictTo;

            _dtpTrainFrom.MaxDate = _dtpTrainUntil.MaxDate = _dtpPredictFrom.MaxDate = _dtpPredictTo.MaxDate = _maxDate;
            _dtpTrainFrom.MinDate = _dtpTrainUntil.MinDate = _dtpPredictFrom.MinDate = _dtpPredictTo.MinDate = _minDate;

            _nudHiddenLayers.Value = _hiddenLayers;
            _nudHiddenUnits.Value = _hiddenUnits;


            //timer
            aTimer = new System.Timers.Timer(10000);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            // Set the Interval to 2 seconds (2000 milliseconds).
            aTimer.Interval = 2000;
            label12.Text = AppGlobol.FolderPath;

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            aTimer.Elapsed -= new ElapsedEventHandler(OnTimedEvent);
             if (AppGlobol.IsAutoRun)
             {
                 while (true)
                 {


                     if (AppGlobol.Status == 7)
                     {
                         break;

                     }
                     else if (AppGlobol.Status == 6)
                     {
                         //todo:

                         AppGlobol.Status++;
                     }
                     else if (AppGlobol.Status == 5)
                     {
                         AppGlobol.Status++;
                     }
                     else if (AppGlobol.Status == 1)
                     {
                         Invoke(new MethodInvoker(delegate
                             {
                                 BtnStartTrainingClick(this, new EventArgs());
                             }));
                         AppGlobol.Status++;
                     }
                     else if (AppGlobol.Status == 3)
                     {
                         Invoke(new MethodInvoker(delegate
                             {
                                 BtnPredictClick(this, new EventArgs());
                             }));
                         AppGlobol.Status++;

                     }
                     else if (AppGlobol.Status == 4)
                     {
                         Invoke(new MethodInvoker(delegate
                             {
                                 //DoThi_GiaiTri.GetImage().Save(AppGlobol.FolderPath + "/ResultGraph.png");
                                 AppGlobol.Status++;
                             }));
                         
                     }
                     Console.WriteLine(" Count:" + AppGlobol.Status.ToString());
                     System.Threading.Thread.Sleep(100);
                 }
                 var result = new ResultRunANN
                     {
                         Counter = AppGlobol.Counter,
                         Ishoitu = AppGlobol.Ishoitu,
                         ListResult = AppGlobol.ListResult,
                         TotalMinute = AppGlobol.TimeSpan.TotalMinutes,
                         InitWeight = AppGlobol.InitWieght
                     };

                 var ketqua = JsonUtils.Serialize(result);
                 DirectionIO.WriteAllText(AppGlobol.FolderPath + "/Result.tsk", ketqua);
                 Application.Exit();
             }
        }

        /// <summary>
        /// Load the form
        /// </summary>
        private void WinFinancialMarketPredictorLoad(object sender, EventArgs e)
        {
            Text = DirectionIO.GetPath();
            SetPathsInTextBoxes();  /*Set path in textboxes*/
            GraphInit(DoThi_GiaiTri);
            if (AppGlobol.IsAutoRun)
            {
                AppGlobol.Status = 1;
                aTimer.Enabled = true;
            }
        }


        /// <summary>
        /// Set paths in text boxes
        /// </summary>
        private void SetPathsInTextBoxes()
        {
            if (File.Exists(Path.GetFullPath(_pathToSp)))
                _tbPathToSp.Text = Path.GetFileName(_pathToSp);
            if (File.Exists(Path.GetFullPath(_pathToUSDJPY)))
                _tbPathToUSDJPY.Text = Path.GetFileName(_pathToUSDJPY);
            if (File.Exists(Path.GetFullPath(_pathToDow)))
                _tbPathToDow.Text = Path.GetFileName(_pathToDow);
            if (File.Exists(Path.GetFullPath(_pathToEURUSD)))
                _tbPathToEURUSD.Text = Path.GetFileName(_pathToEURUSD);
            if (File.Exists(Path.GetFullPath(_pathToXAUUSD)))
                _txtXAUUSD.Text = Path.GetFileName(_pathToXAUUSD);
            if (File.Exists(Path.GetFullPath(_pathToNikkie)))
                _txtNikkieIndex.Text = Path.GetFileName(_pathToNikkie);
            if (File.Exists(Path.GetFullPath(_pathToCommodity)))
                _tbPathToCommodity.Text = Path.GetFileName(_pathToCommodity);
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

            if (_predictor == null)
            {
                _reloadFiles = false;
                if (!File.Exists(_tbPathToDow.Text) || !File.Exists(_tbPathToEURUSD.Text) ||
                    !File.Exists(_tbPathToUSDJPY.Text) || !File.Exists(_tbPathToSp.Text))
                {
                    MessageBox.Show(Resources.InputMissing, Resources.FileMissing, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
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
            if (_predictor == null)
            {
                _pathToSp = _tbPathToSp.Text;
                _pathToDow = _tbPathToDow.Text;
                _pathToUSDJPY = _tbPathToUSDJPY.Text;
                _pathToEURUSD = _tbPathToEURUSD.Text;
                _pathToCommodity = _tbPathToCommodity.Text;
                _pathToNikkie = _txtNikkieIndex.Text;
                _pathToXAUUSD = _txtXAUUSD.Text;
                 Cursor = Cursors.WaitCursor;
                _hiddenLayers = (int)_nudHiddenLayers.Value;
                _hiddenUnits = (int)_nudHiddenUnits.Value;
                try
                {
                   // _predictor = new PredictIndicators(_pathToSp, _pathToEURUSD, _pathToDow, _pathToNasdaq, _hiddenUnits, _hiddenLayers);
                   // _predictor = new PredictIndicators(_pathToSp, _pathToEURUSD,_pathToCommodity , _hiddenUnits, _hiddenLayers);
                    _predictor = new PredictIndicators(_pathToSp, _pathToCommodity,_pathToUSDJPY,_pathToEURUSD,
                                            _pathToXAUUSD,_pathToNikkie,_pathToDow, _hiddenUnits, _hiddenLayers);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _predictor = null;
                    return;
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
            else if (_reloadFiles) /*Reload training sets*/
            {
                _pathToSp = _tbPathToSp.Text;
                _pathToDow = _tbPathToDow.Text;
                _pathToNasdaq = _tbPathToEURUSD.Text;
                _pathToEURUSD = _tbPathToUSDJPY.Text;
                _predictor.ReloadFiles(_pathToSp, _pathToEURUSD, _pathToDow, _pathToNasdaq);
                _dtpTrainFrom.MinDate = _predictor.MinIndexDate;
                _dtpTrainUntil.MaxDate = _predictor.MaxIndexDate;
            }
            /*Verify if dates do conform with the min/max ranges*/
            if (trainFrom < _predictor.MinIndexDate)
                _dtpTrainFrom.MinDate = _dtpTrainFrom.Value = trainFrom = _predictor.MinIndexDate;
            if (trainTo > _predictor.MaxIndexDate)
                _dtpTrainUntil.MaxDate = _dtpTrainUntil.Value = trainTo = _predictor.MaxIndexDate;
            _predictor.timeTrainningSet +=PredictorOnTimeTrainningSet;
            TrainingStatus callback = TrainingCallback;
            _predictor.TrainNetworkAsync(trainFrom, trainTo, callback);
        }

        private void PredictorOnTimeTrainningSet(string timeset, TimeSpan timeSpan, bool ishoitu,int counter)
        {
            Invoke(new MethodInvoker(delegate
                {
                    lblTimeTran.Text = @"Trainning Time:" + timeset;
                    _predictor.timeTrainningSet -= PredictorOnTimeTrainningSet;
                }));
            AppGlobol.Status = 3;
            AppGlobol.Ishoitu = ishoitu;
            AppGlobol.TimeSpan = timeSpan;
            AppGlobol.Counter = counter;
        }


        /// <summary>
        /// Predict the values
        /// </summary>
        private void BtnPredictClick(object sender, EventArgs e)
        {
            if (_dgvPredictionResults.Rows.Count != 0)
                _dgvPredictionResults.Rows.Clear();

            if (_predictor == null)         /*The network is untrained*/
            {
                _reloadFiles = false;
                if (!File.Exists(_tbPathToDow.Text) || !File.Exists(_tbPathToEURUSD.Text) ||
                    !File.Exists(_tbPathToUSDJPY.Text) || !File.Exists(_tbPathToSp.Text))
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
                        _hiddenLayers = (int)_nudHiddenLayers.Value;
                        _hiddenUnits = (int)_nudHiddenUnits.Value;
                        try
                        {
                            _predictor = new PredictIndicators(_pathToSp, _pathToCommodity, _pathToUSDJPY, _pathToEURUSD,
                                            _pathToXAUUSD, _pathToNikkie, _pathToDow, _hiddenUnits, _hiddenLayers);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _predictor = null;
                            return;
                        }
                        finally
                        {
                            this.Cursor = Cursors.Default;
                        }
                        using (OpenFileDialog ofd = new OpenFileDialog() { FileName = "predictor.ntwrk", Filter = Resources.NtwrkFilter })
                        {
                            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                try
                                {
                                    _predictor.LoadNeuralNetwork(Path.GetFullPath(ofd.FileName));
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

            if (_predictor == null)
            {
                _pathToSp = _tbPathToSp.Text;
                _pathToDow = _tbPathToDow.Text;
                _pathToNasdaq = _tbPathToEURUSD.Text;
                _pathToEURUSD = _tbPathToUSDJPY.Text;
                 Cursor = Cursors.WaitCursor;
                _hiddenLayers = (int)_nudHiddenLayers.Value;
                _hiddenUnits = (int)_nudHiddenUnits.Value;
                try
                {
                    _predictor = new PredictIndicators(_pathToSp, _pathToCommodity, _pathToUSDJPY, _pathToEURUSD,
                                            _pathToXAUUSD, _pathToNikkie, _pathToDow, _hiddenUnits, _hiddenLayers);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _predictor = null;
                    return;
                }
                finally
                {
                    Cursor = Cursors.Default;
                }
            }
            List<CommodityResults> results = null;
            try
            {
                if (_reloadFiles)
                {
                    _pathToSp = _tbPathToSp.Text;
                    _pathToDow = _tbPathToDow.Text;
                    _pathToNasdaq = _tbPathToEURUSD.Text;
                    _pathToEURUSD = _tbPathToUSDJPY.Text;
                    _predictor.ReloadFiles(_pathToSp, _pathToEURUSD, _pathToDow, _pathToNasdaq);
                }
                results = _predictor.Predict(predictFrom, predictTo);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (var item in results)
            {
                _dgvPredictionResults.Rows.Add(item.Date.ToShortDateString(), item.ActualClose,
                                               item.PredictedClose.ToString("F6", CultureInfo.InvariantCulture),
                                               item.Error.ToString("F6", CultureInfo.InvariantCulture));
            }
            
            //ve do thi
            DrawGraph(DoThi_GiaiTri, results);
                _predictor.ShowError();



                AppGlobol.ListResult = results;
               
        }
        private void GraphInit(ZedGraphControl DoThi)
        {
            GraphPane myPane1 = DoThi.GraphPane; // Khai báo sửa dụng Graph loại GraphPane;

            myPane1.Title.Text = "Đồ thị dự đoán giá Close Mã GD: " + _pathToCommodity.Substring(0,5);
            myPane1.XAxis.Title.Text = "Ngày dự đoán";
            myPane1.YAxis.Title.Text = "Giá trị dự đoán";
            // Định nghĩa list để vẽ đồ thị. Để các bạn hiểu rõ cơ chế làm việc ở đây khai báo 2 list điểm <=> 2 đường đồ thị
            RollingPointPairList list6_1 = new RollingPointPairList(1000);
            RollingPointPairList list6_2 = new RollingPointPairList(1000);
            // dòng dưới là định nghĩa curve để vẽ.
          myPane1.AddCurve("Giá trị thực đo", list6_1, Color.Red, SymbolType.Diamond);
          myPane1.AddCurve("Giá trị tính toán bởi mạng", list6_2, Color.Blue, SymbolType.Star);

            // Định hiện thị cho trục thời gian (Trục X)
            //myPane1.XAxis.Scale.Min = 0;
            //myPane1.XAxis.Scale.Max = 10;
            //myPane1.XAxis.Scale.MinorStep = 1;
            //myPane1.XAxis.Scale.MajorStep = 1;
          myPane1.XAxis.Type = AxisType.Date;
          myPane1.XAxis.Scale.Min = new XDate(_predictFrom);  // We want to use time from now
          myPane1.XAxis.Scale.Max = new XDate(_predictTo);  // to 5 minutes per default
          myPane1.XAxis.Scale.MinorUnit = DateUnit.Day;         // set the minimum x unit to time/seconds
          myPane1.XAxis.Scale.MajorUnit = DateUnit.Day;         // set the maximum x unit to time/minutes
          myPane1.XAxis.Scale.Format = "MM/dd/yyyy";
            // Gọi hàm xác định cỡ trục
            myPane1.AxisChange();
        
        }

       private void DrawGraph(ZedGraphControl DoThi, List<CommodityResults> results)
        {
            //ve gia tri
            LineItem curve2_1 = DoThi.GraphPane.CurveList[0] as LineItem;
            LineItem curve2_2 = DoThi.GraphPane.CurveList[1] as LineItem;

            //init do thi.
 
            // Get the PointPairList
            IPointListEdit list21 = curve2_1.Points as IPointListEdit;
            IPointListEdit list22 = curve2_2.Points as IPointListEdit;
            list21.Clear();
            list22.Clear();
            DoThi.AxisChange();
            DoThi.Invalidate();
            int i = 0;
            foreach (var item in results)
            {
                var xdate = new XDate(item.Date);
                list21.Add(xdate, item.ActualClose);
                list22.Add(xdate, item.PredictedClose);
                // đoạn chương trình thực hiện vẽ đồ thị
                Scale xScale = DoThi.GraphPane.XAxis.Scale;
                i++;
            }
            // Vẽ đồ thị
            DoThi.AxisChange();
            // Force a redraw
            DoThi.Invalidate();
        }

        /// <summary>
        /// Form closing event
        /// </summary>
        private void WinFinancialMarketPredictorFormClosing(object sender, FormClosingEventArgs e)
        {
            if(_predictor != null)
                _predictor.AbortTraining();
        }


        #region double click

        private string GetDataFile(string filename = "*.csv")
        {
            string namefile = "";
            OpenFileDialog ofd = new OpenFileDialog() { FileName = filename, Filter = Resources.CsvFilter };
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                namefile = Path.GetFullPath(ofd.FileName);
                _reloadFiles = true;
            }
            return namefile;
        }
        /// <summary>
        /// New path to S&P500 index is selected
        /// </summary>
        private void TbPathToSpMouseDoubleClick(object sender, MouseEventArgs e)
        {
            _tbPathToSp.Text = GetDataFile(_pathToSp);
        }

        /// <summary>
        /// New path to Prime interest rate is selected
        /// </summary>
        private void TbPathToPrMouseDoubleClick(object sender, MouseEventArgs e)
        {
            _tbPathToUSDJPY.Text = GetDataFile(_pathToUSDJPY);
        }

        /// <summary>
        /// New path to Dow index is selected
        /// </summary>
        private void TbPathToDowMouseDoubleClick(object sender, MouseEventArgs e)
        {
            _tbPathToDow.Text = GetDataFile(_pathToDow);
        }

        /// <summary>
        /// Path to Nasdaq index is selected
        /// </summary>
        private void TbPathToNasdaqMouseDoubleClick(object sender, MouseEventArgs e)
        {
            _tbPathToEURUSD.Text = GetDataFile(_pathToEURUSD);

        }
        private void _tbPathToCommodity_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _tbPathToCommodity.Text = GetDataFile(_pathToCommodity);

        }

        private void _txtNikkieIndex_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _txtNikkieIndex.Text = GetDataFile(_pathToNikkie);

        }
        private void _txtXAUUSD_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            _txtXAUUSD.Text = GetDataFile(_pathToXAUUSD);
        }

        #endregion

        #region Btn Click

        /// <summary>
        /// Stop training
        /// </summary>
        private void BtnStopClick(object sender, EventArgs e)
        {
            FadeControls(false);
            _predictor.AbortTraining();
            _btnExport.Enabled = true;
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
                                      _tbPathToUSDJPY.Enabled = param;
                                      _tbPathToDow.Enabled = param;
                                      _tbPathToEURUSD.Enabled = param;
                                      _btnStartTraining.Enabled = param;
                                      _btnStop.Enabled = !param;
                                      _dtpPredictFrom.Enabled = param;
                                      _dtpPredictTo.Enabled = param;
                                      _dtpTrainFrom.Enabled = param;
                                      _dtpTrainUntil.Enabled = param;
                                      _nudHiddenLayers.Enabled = param;
                                      _nudHiddenUnits.Enabled = param;
                                  };
            Invoke(action, !fade);
        }

        /// <summary>
        /// Save the network, for later reuse
        /// </summary>
        private void BtnExportClick(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { FileName = "predictor.ntwrk", Filter = Resources.NtwrkFilter })
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
                    _predictor.ExportNeuralNetwork(Path.GetFullPath(sfd.FileName));
                }
            }
        }

        /// <summary>
        /// Load previously saved network
        /// </summary>
        private void BtnLoadClick(object sender, EventArgs e)
        {
            if (!File.Exists(_tbPathToDow.Text) || !File.Exists(_tbPathToCommodity.Text) ||
                    !File.Exists(_tbPathToUSDJPY.Text))
            {
                MessageBox.Show(Resources.InputMissing, Resources.FileMissing, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_predictor == null || _predictor.Loaded == false)
            {
                /*Load the network*/
                this.Cursor = Cursors.WaitCursor;
                _hiddenLayers = (int)_nudHiddenLayers.Value;
                _hiddenUnits = (int)_nudHiddenUnits.Value;
                try
                {
                    _predictor = new PredictIndicators(_pathToSp, _pathToCommodity, _pathToUSDJPY, _pathToEURUSD,
                                            _pathToXAUUSD, _pathToNikkie, _pathToDow, _hiddenUnits, _hiddenLayers);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _predictor = null;
                    return;
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
            using (OpenFileDialog ofd = new OpenFileDialog() { FileName = "predictor.ntwrk", Filter = Resources.NtwrkFilter })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _predictor.LoadNeuralNetwork(Path.GetFullPath(ofd.FileName));
                        _nudHiddenLayers.Value = _predictor.HiddenLayers;
                        _nudHiddenUnits.Value = _predictor.HiddenUnits;
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
            SaveFileDialog ofd = new SaveFileDialog { Filter = Resources.CsvFilter, FileName = "results.csv" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CSVWriter writer = null;
                try
                {
                    writer = new CSVWriter(ofd.FileName);
                }
                catch (System.Security.SecurityException)
                {
                    MessageBox.Show(Resources.SecurityExceptionFolderLevel, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Resources.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                object[,] values = new object[dgvResults.Rows.Count + 2, dgvResults.Columns.Count];
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

        #endregion

        /// <summary>
        /// Number of hidden units changed
        /// </summary>
        private void NudHiddenUnitsValueChanged(object sender, EventArgs e)
        {
            if(_predictor != null)
            {
                if(MessageBox.Show(Resources.ChangedNetwork, Resources.Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    _predictor = null;
                }
            }
        }

        /// <summary>
        /// Number of hidden layers changed
        /// </summary>
        private void NudHiddenLayersValueChanged(object sender, EventArgs e)
        {
            if (_predictor != null)
            {
                if (MessageBox.Show(Resources.ChangedNetwork, Resources.Warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    _predictor = null;
                }
            }
        }

        private void DoThi_GiaiTri_Load(object sender, EventArgs e)
        {

        }

    }
}
