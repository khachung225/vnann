using System;
using System.Collections.Generic;
/*
 * Dau vao la bo du lieu chua duoc chuan hoa.
 * nhiem vu chuan hoa du lieu dao data va mang
 * goi ANFISPredictor de hoc va du doan.
 * 
 */

namespace ANFISPrediction
{
    public class ANFISManager : IDisposable
    {
        public delegate void ErrorReportHandle(int stt, double er);

        public event ErrorReportHandle ErrorRate;

        private List<ANFISData> _anfisDatas;
        private List<ANFISData> _anfisPreDatas;
        private ANFISNetwork _anfisNetwork;
        private ANFISTrainner _anfisTrainner;

        private readonly Dictionary<int,ANFISMimax> _anfisMimaxes;
        private readonly ANFISMimax _anfisMimaxIdeal;
        private readonly int _inputCount;
        private readonly double _errorLimited;

        public ANFISManager(int inputCount, double errorlimited)
        {
            _inputCount = inputCount;
            _errorLimited = errorlimited;

            _anfisPreDatas = new List<ANFISData>();
            _anfisDatas = new List<ANFISData>();
            _anfisMimaxes = new Dictionary<int,ANFISMimax>();
            _anfisMimaxIdeal = new ANFISMimax { Key = 0, MaxValue = double.MinValue, MinValue = double.MaxValue };

            for (int i = 0; i < inputCount + 1; i++)
            {
              var item = new ANFISMimax{Key = i,MaxValue = double.MinValue,MinValue = double.MaxValue};
                _anfisMimaxes[i] = item;
            }
        }

        public void CreatNetwork()
        {
            _anfisNetwork = new ANFISNetwork(_inputCount);
            _anfisNetwork.initData();
        }

        /// <summary>
        /// dungf ham sigmod de chuan hoa.
        /// </summary>
        /// <param name="inputData"></param>
        public void SetData(List<ANFISData> inputData)
        {
            var listdata = new List<ANFISData>();
            foreach (var anfisData in inputData)
            {
                var data = new ANFISData {Ideal = anfisData.Ideal};
                _anfisMimaxIdeal.MinValue = Math.Min(_anfisMimaxIdeal.MinValue, data.Ideal);
                _anfisMimaxIdeal.MaxValue = Math.Max(_anfisMimaxIdeal.MinValue, data.Ideal);
                foreach (var d in anfisData.Input)
                {
                    data.Input.Add(d.Key,d.Value);
                    _anfisMimaxes[d.Key].MinValue = Math.Min(_anfisMimaxes[d.Key].MinValue, d.Value);
                    _anfisMimaxes[d.Key].MaxValue = Math.Max(_anfisMimaxes[d.Key].MinValue, d.Value);
                }
                listdata.Add(data);
            }
            _anfisPreDatas = listdata;
            PreDataProcess();
        }
        private void PreDataProcess()
        {
            var listdata = new List<ANFISData>();
            
            foreach (var anfisPreData in _anfisPreDatas)
            {
                var data = new ANFISData
                    {
                        Ideal = ANFISUtils.EncodeMinMax(anfisPreData.Ideal, _anfisMimaxIdeal.MinValue,
                                                        _anfisMimaxIdeal.MaxValue)
                    };
                foreach (var d in anfisPreData.Input)
                {
                    data.Input[d.Key] = ANFISUtils.EncodeMinMax(d.Value, _anfisMimaxes[d.Key].MinValue,
                                                                _anfisMimaxes[d.Key].MaxValue);
                }
                listdata.Add(data);
            }
            _anfisDatas = listdata;
        }
        private void AnfisTrainnerOnErrorRate(int stt, double er)
        {
            if (ErrorRate != null)
                ErrorRate(stt, er);
        }

        public void StartTrainning()
        {
            if (_anfisTrainner != null)
            {
                _anfisTrainner.ErrorRate -= AnfisTrainnerOnErrorRate;
                _anfisTrainner.Dispose();
                _anfisTrainner = null;
            }
            _anfisTrainner = new ANFISTrainner(_anfisDatas, _anfisNetwork, _errorLimited);
            _anfisTrainner.ErrorRate += AnfisTrainnerOnErrorRate;
            _anfisTrainner.StartTranning();
        }
        
        public void StopTrainning()
        {
            if (_anfisTrainner != null)
            {
                _anfisTrainner.StopTranning();
            }
        }

        public void Dispose()
        {
            if (_anfisTrainner != null)
            {
                _anfisTrainner.ErrorRate -= AnfisTrainnerOnErrorRate;
                _anfisTrainner.Dispose();
                _anfisTrainner = null;
            }
        }

        public void SaveNetWorker(string path)
        {
            ANFISUtils.Save(path, _anfisNetwork);
        }
        public void LoadNetWorker(string path)
        {
            _anfisNetwork = (ANFISNetwork)ANFISUtils.Load(path);
        }

        public void Predic(ANFISData data)
        {
            var input = new ANFISData();
            foreach (var d in data.Input)
            {
                input.Input[d.Key] = ANFISUtils.EncodeMinMax(d.Value, _anfisMimaxes[d.Key].MinValue, _anfisMimaxes[d.Key].MaxValue);
            }
            _anfisNetwork.SetData(input);
            //du doan
            var value = _anfisNetwork.Predic();
            //chuan hoa nguoc
            data.Ideal = ANFISUtils.DecodeMinMax(value, _anfisMimaxIdeal.MinValue, _anfisMimaxIdeal.MaxValue);
        }
    }
}
