#define Debug
using System;
using System.Collections.Generic;
using System.IO;
using BaseEntity.Entity;
using BaseEntity.Utils;

namespace BaseEntity.Common
{
    /// <summary>
    /// predictor manager
    /// </summary>
    public sealed class PredictorManager
    {
        #region Private Members
      
        /// <summary>
        /// Samples
        /// </summary>
        private readonly List<CommodityIndexes> _samples = new List<CommodityIndexes>();

        /// <summary>
        /// Input size
        /// </summary>
        private readonly int _inputSize;

        /// <summary>
        /// Output size [% move]
        /// </summary>
        private readonly int _outputSize;

        #endregion

        #region Properties
    

        /// <summary>
        /// Max date for the training set
        /// </summary>
        public DateTime MaxDate { get; private set; }

        /// <summary>
        /// Min date for the training set
        /// </summary>
        public DateTime MinDate { get; private set; }

 

        #endregion

        private const string DATE_HEADER = "Date";
        private const string ADJ_CLOSE_HEADER = "Close";
        private const string ADJ_VOLUMN_HEADER = "Volume";
        private const string ADJ_OPEN_HEADER = "Open";
        private const string ADJ_HIGH_HEADER = "High";
        private const string ADJ_LOW_HEADER = "Low";
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inputSize">Input size</param>
        /// <param name="outputSize">Output size</param>
        public PredictorManager(int inputSize, int outputSize)
        {
            if (inputSize <= 0)
                throw new ArgumentException("inputSize cannot be less than 0");
            if (outputSize <= 0)
                throw new ArgumentException("outputSize cannot be less than 0");
            _inputSize = inputSize;
            _outputSize = outputSize;
            foreach (CommodityIndexe index in Enum.GetValues(typeof (CommodityIndexe)))
            {
                _dicMaxValue[(int)index] = Double.MinValue;
                _dicMinValue[(int)index] = Double.MaxValue;
            }
            MaxDate = DateTime.MaxValue;
            MinDate = DateTime.MinValue;
        }
        #endregion

        /// <summary>
        /// Get input data - S&P 500 Index, Prime Interest Rate, Dow index, Nasdaq index
        /// </summary>
        /// <param name="offset">Start index of input data</param>
        /// <param name="input">Array to be populated</param>
        /// <remarks>
        /// According to the <c>offset</c> parameter, first <c>_inputSize</c> values are drawn from the dataset 
        /// </remarks>
        public void GetInputData(int offset, double[] input)
        {
            int total = 0;
            int k = 0;
            // get SP500, prime data, NASDAQ, Dow
            for (int i = 0; i < _inputSize; i++)
            {
                CommodityIndexes sample = _samples[offset + i];
                k = 0;
                
                foreach (CommodityIndexe index in Enum.GetValues(typeof (CommodityIndexe)))
                {
                     
                    if (sample.IsSetValue((int) index))
                    {
                        input[i*total + k] = sample.GetValue((int) index);
                        k++;
                    }
                }
                if (total < 1)
                {
                    total = k;
                }
                //input[i*4]       = sample.Sp;
                //input[i*4 + 1]   = sample.PrimeInterestRate;
                //input[i*4 + 2]   = sample.Commodity;
                //input[i*4 + 3]   = sample.VolumeCommo;
            }
        }
        public void GetInputData(int offset, double[] input, out List<CommodityIndexes> dateTime)
        {
            dateTime= new List<CommodityIndexes>();
            int total = 0;
            int k = 0;
            // get SP500, prime data, NASDAQ, Dow
            for (int i = 0; i < _inputSize; i++)
            {
                CommodityIndexes sample = _samples[offset + i];
                k = 0;

                foreach (CommodityIndexe index in Enum.GetValues(typeof(CommodityIndexe)))
                {

                    if (sample.IsSetValue((int)index))
                    {
                        input[i * total + k] = sample.GetValue((int)index);
                        k++;
                    }
                }
                if (total < 1)
                {
                    total = k;
                }
                dateTime.Add(sample);
            }
        }

        /// <summary>
        /// Get output data - S&P 500 Index, Prime Interest Rate, Dow index, Nasdaq index
        /// </summary>
        /// <param name="offset">Start index of output data</param>
        /// <param name="output">Output array to be populated</param>
        /// <remarks>
        /// The value of <c>offset + _inputSize</c> indexes value are drawn from the samples data set.
        /// E.g. Consider the <c>offset</c> parameter equal to 12581. Input parameters to the network will be
        /// values from [12581..12590]. The actual values will be equal to the parameters stored in the <code>12581 + _inputSize</code>
        /// place => 12591 index.
        /// </remarks>
        public void GetOutputData(int offset, double[] output, out CommodityIndexes insample)
        {
            CommodityIndexes sample = _samples[offset + _inputSize];
            output[0] = sample.GetValue((int)CommodityIndexe.CloseIndex);
            //output[1] = sample.PrimeInterestRate;
            //output[2] = sample.Commodity;
            //output[3] = sample.VolumeCommo;
            insample = sample;
        }
        public void GetOutputData(int offset, double[] output)
        {
            CommodityIndexes sample = _samples[offset + _inputSize];
            output[0] = sample.GetValue((int)CommodityIndexe.CloseIndex);
            //output[1] = sample.PrimeInterestRate;
            //output[2] = sample.Commodity;
            //output[3] = sample.VolumeCommo;
            
        }
        #region Get indexes

        public double GetMax(int index)
        {
            return _dicMaxValue[index];
        }

        public double GetMin(int index)
        {
            return _dicMinValue[index];
        }

        public bool IsSetIndex(int index)
        {
            return _dictionary.ContainsKey(index);
        }
        public double GetIndex(DateTime date, int index)
        {
            double currentAmount = 0;

            foreach (PData data in _dictionary[index])
            {
                if (data.Date.CompareTo(date) == 0)
                    return data.Amount;
                if (data.Date.CompareTo(date) > 0)
                {
                    return currentAmount;
                }
                currentAmount = data.Amount;
            }
            return currentAmount;
        }

        #endregion

        /// <summary>
        /// Get financial samples
        /// </summary>
        public IList<CommodityIndexes> Samples
        {
            get { return _samples; }
        }

        /// <summary>
        /// Load both S&P500 and Prime Interest PrimeInterestRate file
        /// </summary>
        /// <param name="sp500Filename">S&P500 filename</param>
        /// <param name="primeFilename">Prime interest rates filename</param>
        /// <param name="pathToDow">Path to DOW indexes</param>
        /// <param name="pathToNasdaq">Path to NASDAQ indexes</param>
        public void Load(String sp500Filename, String primeFilename, String pathToDow, String pathToNasdaq)
        {
            if (!File.Exists(sp500Filename))
                throw new ArgumentException("sp500Filename targets an invalid file");
            if (!File.Exists(primeFilename))
                throw new ArgumentException("primeFilename targets an invalid file");
            if (!File.Exists(pathToDow))
                throw new ArgumentException("pathToDow targets an invalid file");
            if (!File.Exists(pathToNasdaq))
                throw new ArgumentException("pathToNasdaq targets an invalid file");
            try
            {
                Loadcsv(sp500Filename, (int)CommodityIndexe.SP500Index);
            }
            catch
            {
                throw new NotSupportedException("Loading SP500 file failed. Not supported file format. Make sure \"date\" and \"adj close\" column headers are written in the file");
            }
            try
            {
                Loadcsv(primeFilename, (int)CommodityIndexe.USDJPYIndex);
            }
            catch
            {
                throw new NotSupportedException("Loading Prime Interest Rate file failed. Not supported file format. Make sure \"date\" and \"prime\" column headers are written in the file");
            }

            try
            {
                Loadcsv(pathToDow, (int)CommodityIndexe.DowJonIndex);
            }
            catch
            {
                throw new NotSupportedException("Loading Dow indexes file failed. Not supported file format. Make sure \"date\" and \"adj close\" column headers are written in the file");
            }
            try
            {
                Loadcsv(pathToNasdaq, (int)CommodityIndexe.NikieIndex);
            }
            catch
            {
                throw new NotSupportedException("Loading Nasdaq indexes file failed. Not supported file format. Make sure \"date\" and \"adj close\" column headers are written in the file");
            }
            MaxDate = MaxDate.Subtract(new TimeSpan(_inputSize, 0, 0, 0)); /*Subtract 10 last days*/
            StitchFinancialIndexes();
            _samples.Sort();            /*Sort by date*/
            NormalizeData();
        }

        /// <summary>
        /// Load both S&P500 and Prime Interest PrimeInterestRate file
        /// </summary>
        /// <param name="sp500Filename">S&P500 filename</param>
        /// <param name="primeFilename">Prime interest rates filename</param>
        /// <param name="commoFilename">Path to DOW indexes</param>
        /// <param name="isnomalsize">Path to NASDAQ indexes</param>
        public void Load(String sp500Filename, String primeFilename, String commoFilename, bool isnomalsize = true)
        {
            if (!File.Exists(sp500Filename))
                throw new ArgumentException("sp500Filename targets an invalid file");
            if (!File.Exists(primeFilename))
                throw new ArgumentException("primeFilename targets an invalid file");
            if (!File.Exists(commoFilename))
                throw new ArgumentException("commoFilename targets an invalid file");
          
            try
            {
                Loadcsv(sp500Filename, (int)CommodityIndexe.SP500Index);
            }
            catch
            {
                throw new NotSupportedException("Loading SP500 file failed. Not supported file format. Make sure \"date\" and \"adj close\" column headers are written in the file");
            }
            try
            {
                Loadcsv(primeFilename, (int)CommodityIndexe.EURUSDIndex);
            }
            catch
            {
                throw new NotSupportedException("Loading Prime Interest Rate file failed. Not supported file format. Make sure \"date\" and \"prime\" column headers are written in the file");
            }
            try
            {
                Loadcsv(commoFilename, (int)CommodityIndexe.CloseIndex,ADJ_CLOSE_HEADER);
                //Loadcsv(commoFilename, (int)CommodityIndexe.HighIndex, ADJ_HIGH_HEADER);
                //Loadcsv(commoFilename, (int)CommodityIndexe.LowIndex, ADJ_LOW_HEADER);
                //Loadcsv(commoFilename, (int)CommodityIndexe.OpenIndex, ADJ_OPEN_HEADER);
                Loadcsv(commoFilename, (int)CommodityIndexe.VolumeIndex, ADJ_VOLUMN_HEADER);
            }
            catch
            {
                throw new NotSupportedException("Loading Commodity file failed. Not supported file format. Make sure \"date\" and \"prime\" column headers are written in the file");
            }

            MaxDate = MaxDate.Subtract(new TimeSpan(_inputSize, 0, 0, 0)); /*Subtract 10 last days*/
            StitchFinancialIndexes();
            _samples.Sort();            /*Sort by date*/
            if (isnomalsize)
             NormalizeData();
        }
        public void Load(String sp500Filename, String commoFilename, String rateusdjpy, String rateeurusd,
                                String ratexauusd, String nikkieIndex, String dowjonindex, bool isnomalsize = true)
        {
            if (!File.Exists(sp500Filename))
                throw new ArgumentException("sp500Filename targets an invalid file");
            if (!File.Exists(rateeurusd))
                throw new ArgumentException("primeFilename targets an invalid file");
            if (!File.Exists(commoFilename))
                throw new ArgumentException("commoFilename targets an invalid file");
            if (!File.Exists(rateusdjpy))
                throw new ArgumentException("rateusdjpy targets an invalid file");
            if (!File.Exists(ratexauusd))
                throw new ArgumentException("ratexauusd targets an invalid file");
            if (!File.Exists(nikkieIndex))
                throw new ArgumentException("nikkieIndex targets an invalid file");
            if (!File.Exists(dowjonindex))
                throw new ArgumentException("nikkieIndex targets an invalid file");

            try
            {
                Loadcsv(dowjonindex, (int)CommodityIndexe.DowJonIndex);
            }
            catch
            {
                throw new NotSupportedException("Loading DowJonIndex file failed. Not supported file format. Make sure \"date\" and \"adj close\" column headers are written in the file");
            }
            try
            {
                Loadcsv(nikkieIndex, (int)CommodityIndexe.NikieIndex);
            }
            catch
            {
                throw new NotSupportedException("Loading NikieIndex file failed. Not supported file format. Make sure \"date\" and \"adj close\" column headers are written in the file");
            }
            try
            {
                Loadcsv(ratexauusd, (int)CommodityIndexe.XAUUSDIndex);
            }
            catch
            {
                throw new NotSupportedException("Loading XAUUSDIndex file failed. Not supported file format. Make sure \"date\" and \"adj close\" column headers are written in the file");
            }
            try
            {
                Loadcsv(rateusdjpy, (int)CommodityIndexe.USDJPYIndex);
            }
            catch
            {
                throw new NotSupportedException("Loading USDJPYIndex file failed. Not supported file format. Make sure \"date\" and \"adj close\" column headers are written in the file");
            }
            try
            {
                Loadcsv(sp500Filename, (int)CommodityIndexe.SP500Index);
            }
            catch
            {
                throw new NotSupportedException("Loading SP500Index file failed. Not supported file format. Make sure \"date\" and \"adj close\" column headers are written in the file");
            }
            try
            {
                Loadcsv(rateeurusd, (int)CommodityIndexe.EURUSDIndex);
            }
            catch
            {
                throw new NotSupportedException("Loading EURUSDIndex  file failed. Not supported file format. Make sure \"date\" and \"prime\" column headers are written in the file");
            }
            try
            {
                Loadcsv(commoFilename, (int)CommodityIndexe.CloseIndex, ADJ_CLOSE_HEADER);
                Loadcsv(commoFilename, (int)CommodityIndexe.HighIndex, ADJ_HIGH_HEADER);
                Loadcsv(commoFilename, (int)CommodityIndexe.LowIndex, ADJ_LOW_HEADER);
                Loadcsv(commoFilename, (int)CommodityIndexe.OpenIndex, ADJ_OPEN_HEADER);
                Loadcsv(commoFilename, (int)CommodityIndexe.VolumeIndex, ADJ_VOLUMN_HEADER);
            }
            catch
            {
                throw new NotSupportedException("Loading Commodity file failed. Not supported file format. Make sure \"date\" and \"prime\" column headers are written in the file");
            }

            MaxDate = MaxDate.Subtract(new TimeSpan(_inputSize, 0, 0, 0)); /*Subtract 10 last days*/
            StitchFinancialIndexes();
            _samples.Sort();            /*Sort by date*/
            if (isnomalsize)
                NormalizeData();
        }
        #region Load .csv files region

        Dictionary<int,List<PData>> _dictionary = new Dictionary<int, List<PData>>(); 
        Dictionary<int,double> _dicMaxValue = new Dictionary<int, double>();
        Dictionary<int, double> _dicMinValue = new Dictionary<int, double>();
        /// <summary>
        /// Load S&P500 indexes file
        /// </summary>
        public void Loadcsv(String pFilename, int index, string readColumn = ADJ_CLOSE_HEADER)
        {
            _dictionary[index] = new List<PData>();

            using (CSVReader csv = new CSVReader(pFilename))
            {
                while (csv.Next())
                {
                    DateTime date = csv.GetDate(DATE_HEADER);
                    double amount = csv.GetDouble(readColumn);
                    var sample = new PData(amount, date);
                    _dictionary[index].Add(sample);
                    if (amount > _dicMaxValue[index]) _dicMaxValue[index] = amount;
                    if (amount < _dicMinValue[index]) _dicMinValue[index] = amount;
                }
                csv.Close();
                _dictionary[index].Sort();
            }
            if (_dictionary[index].Count > 0)
            {
                if (MinDate < _dictionary[index][0].Date)                //after sorting the indexes at 0 position is the lowest date in the range
                    MinDate = _dictionary[index][0].Date;
                if (MaxDate > _dictionary[index][_dictionary[index].Count - 1].Date) //Maximal date
                    MaxDate = _dictionary[index][_dictionary[index].Count - 1].Date;
            }
        }

        #endregion

        /// <summary>
        /// Get financial samples size
        /// </summary>
        public int Size
        {
            get { return _samples.Count; }
        }

        /// <summary>
        /// Stitch S&P 500 indexes to Prime Interest Rates according to the date parameter
        /// </summary>
        public void StitchFinancialIndexes()
        {
            foreach (var item in _dictionary[(int) CommodityIndexe.CloseIndex])
            {
                var ci = new CommodityIndexes {Date = item.Date};

                foreach (CommodityIndexe index in Enum.GetValues(typeof (CommodityIndexe)))
                {
                    if (IsSetIndex((int) index))
                    {
                        var amout = GetIndex(ci.Date, (int) index);
                        ci.SetValue((int) index, amout);
                    }
                }
                _samples.Add(ci);
            }
#if Debug
            string title = "Date,";
            foreach (CommodityIndexe index in Enum.GetValues(typeof (CommodityIndexe)))
            {
                title += index.ToString() + ",";
            }
            var list = new List<string>();
             
            foreach (var sample in _samples)
            { 
                var str =sample.Date.ToString("MM/dd/yyyy") + ",";
                foreach (CommodityIndexe index in Enum.GetValues(typeof (CommodityIndexe)))
                {
                   
                    if (sample.IsSetValue((int) index))
                    {
                        str += sample.GetValue((int) index).ToString();
                    }
                    str += ",";
                }
                list.Add(str);
            }

            var fileName = AppDomain.CurrentDomain.BaseDirectory + "DebugTranning.csv";
            StreamWriter textWriter = null;
             try
             {
                 textWriter = new StreamWriter(fileName);
                 textWriter.WriteLine(title);
                 foreach (var str in list)
                 {
                     textWriter.WriteLine(str);
                 }

             }
             catch (Exception)
             {

             }
             finally
             {
                 if(textWriter !=null)
                  textWriter.Close();
             }
           
#endif
        }

        /// <summary>
        /// Normalize input data
        /// </summary>
        public void NormalizeData()
        {
            foreach (CommodityIndexes t in _samples)
            {
                foreach (CommodityIndexe index in Enum.GetValues(typeof (CommodityIndexe)))
                {
                    if (t.IsSetValue((int) index))
                    {
                        var value = t.GetValue((int) index);
                        value = (value - _dicMinValue[(int)index]) / (_dicMaxValue[(int)index] - _dicMinValue[(int)index]);
                        t.SetValue((int)index,value);
                    }
                }
            }
            #if Debug
            var fileName = AppDomain.CurrentDomain.BaseDirectory + "MinMaxData.csv";
            StreamWriter textWriter = null;
            try
            {
                textWriter = new StreamWriter(fileName);
                textWriter.WriteLine("Data,Max,min");
                foreach (CommodityIndexe index in Enum.GetValues(typeof(CommodityIndexe)))
                {
                    var content = string.Format("{0},{1},{2}", index.ToString(), _dicMaxValue[(int) index],
                                                _dicMinValue[(int) index]);

                    textWriter.WriteLine(content);
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                if (textWriter != null)
                    textWriter.Close();
            }

            

#endif
        }

        /// <summary>
        /// hàm sử dụng để tìm xem ngày kế tiếp là ngày nào
        /// sử dụng cho việc tìm ngày dự đoán tiếp theo.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DateTime GetNextDate(DateTime dateTime)
        {
           
            var symbolcount = _samples.Count;
            if (symbolcount == 0) return dateTime;
            for (int i = 0; i < symbolcount; i++)
            {
                var a = _samples[i].Date.Date;
                var b = _samples[i].Date.Date.CompareTo(dateTime.Date);
               if (_samples[i].Date.Date.CompareTo(dateTime.Date) > 0)
               {
                   return _samples[i].Date.Date;
               }
            }
            return _samples[symbolcount - 1].Date.Date;
        }
    }
}
