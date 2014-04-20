using System;
using System.Collections.Generic;
using System.ComponentModel;

/*
 * thuc hien dao tao mang, dua ra bo trong so tot nhat.
 */

namespace ANFISPrediction
{
    public class ANFISTrainner : IDisposable 
    {
        public delegate void ErrorReportHandle(int stt, double er);
        public event ErrorReportHandle ErrorRate;

        #region mem.

        private List<ANFISData> _anfisDatas;
        private ANFISNetwork _anfisNetwork;
        private BackgroundWorker _backgroundWorker;
        private double SaiSoTBNhoNhat;
        private double[,] arrHeSoGaussCTotNhat = new double[,] {};
        private double[,] arrHeSoGaussDTotNhat = new double[,] {};
        private double[,] arrHeSoPTotNhat = new double[,] {};

        private double SaiSoGioiHan { get; set; }

        #endregion

        #region Ctor.

       public ANFISTrainner(List<ANFISData> listData, ANFISNetwork network, double errorLimited)
        {
            _anfisDatas = listData;
            _anfisNetwork = network;
            SaiSoGioiHan = errorLimited;
            Init();
        }

        private void Init()
        {
            arrHeSoGaussCTotNhat = new double[ANFISNetwork.SKM,_anfisNetwork.InputCount];
            arrHeSoGaussDTotNhat = new double[ANFISNetwork.SKM,_anfisNetwork.InputCount];
            arrHeSoPTotNhat = new double[ANFISNetwork.SKM,_anfisNetwork.InputCount + 1];
            SaiSoTBNhoNhat = 100;
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;

            _backgroundWorker.DoWork += TrainningData;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorkerOnRunWorkerCompleted;
            _backgroundWorker.ProgressChanged += BackgroundWorkerOnProgressChanged;
        }

        #endregion

        #region BackgroundWorker EventHandle

        private void BackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ErrorRate != null)
                ErrorRate(e.ProgressPercentage, (double) e.UserState);
        }

        private void BackgroundWorkerOnRunWorkerCompleted(object sender,
                                                          RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            // Đổ lại tất cả các hệ số c,d,a,b,c,P theo bộ Tốt Nhất
            _anfisNetwork.SetHeSoGaussCBetter(arrHeSoGaussCTotNhat);
            _anfisNetwork.SetHeSoGaussDBetter(arrHeSoGaussDTotNhat);
            _anfisNetwork.SetHePBetter(arrHeSoPTotNhat);
        }

        #endregion

        private void TrainningData(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            int stt = 0;
            double totalEr = 0;
            do
            {
                stt++;
                totalEr = 0;
                foreach (var anfisData in _anfisDatas)
                {
                    _anfisNetwork.SetData(anfisData);
                    _anfisNetwork.Trainning();
                    var er = _anfisNetwork.GetError();
                    totalEr += er;
                }
                double saiSoTbBoHocLanLap = -1;
                if (_anfisDatas.Count > 0) saiSoTbBoHocLanLap = totalEr / _anfisDatas.Count;
                if (saiSoTbBoHocLanLap < 0)
                {
                    _backgroundWorker.ReportProgress(stt, saiSoTbBoHocLanLap);
                    break;
                }
                // Nếu lần lặp hiện hành có Sai Số TB nhỏ hơn Sai Số TB Tốt Nhất thì cập nhật và lưu vị trí
                if (saiSoTbBoHocLanLap < SaiSoTBNhoNhat)
                {
                    SaiSoTBNhoNhat = saiSoTbBoHocLanLap;
                    arrHeSoGaussCTotNhat = _anfisNetwork.HeSoGaussC;
                    arrHeSoGaussDTotNhat = _anfisNetwork.HeSoGaussD;
                    arrHeSoPTotNhat = _anfisNetwork.HeSoP;
                }
                //report
                _backgroundWorker.ReportProgress(stt, saiSoTbBoHocLanLap);

                // Nếu lần lặp hiện hành có Sai Số TB nhỏ hơn Sai Số Giới Hạn thì thoát khỏi vòng lặp và cập nhật các Thông Số Tốt Nhất
                if (SaiSoGioiHan > 0 && saiSoTbBoHocLanLap <= SaiSoGioiHan)
                {
                    SaiSoTBNhoNhat = saiSoTbBoHocLanLap;
                    arrHeSoGaussCTotNhat = _anfisNetwork.HeSoGaussC;
                    arrHeSoGaussDTotNhat = _anfisNetwork.HeSoGaussD;
                    arrHeSoPTotNhat = _anfisNetwork.HeSoP;
                    break;
                }
                // Đổ lại tất cả các hệ số c,d,a,b,c,P theo bộ Tốt Nhất
                _anfisNetwork.SetHeSoGaussCBetter(arrHeSoGaussCTotNhat);
                _anfisNetwork.SetHeSoGaussDBetter(arrHeSoGaussDTotNhat);
                _anfisNetwork.SetHePBetter(arrHeSoPTotNhat);

            } while (!_backgroundWorker.CancellationPending);
        }

        #region Start, stop tranning

        public void StartTranning()
        {
            if (!_backgroundWorker.IsBusy)
                _backgroundWorker.RunWorkerAsync();
        }

        public void StopTranning()
        {
            if (_backgroundWorker.IsBusy)
                _backgroundWorker.CancelAsync();
        }

        #endregion

        public void Dispose()
        {
        }
    }
}
