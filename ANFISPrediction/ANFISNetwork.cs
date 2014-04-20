using System;
using System.Collections.Generic;

namespace ANFISPrediction
{
    [Serializable]
    public class ANFISNetwork
    {
        public const double HSH = 0.005;
        public const int SKM = 3;

        private double HSG_c;
        private double HSG_d;
        private double[] Lop1 = new double[] { };
        private double[,] Lop2 = new double[,] { };
        private double[] Lop3 = new double[] { };
        private double[] Lop4 = new double[] { };
        private double[] Lop5 = new double[] { };
        private double Lop6;
        private double[,] arrHeSoGaussC = new double[,] { };
        private double[,] arrHeSoGaussD = new double[,] { };
        private static double[,] arrHeSoP = new double[,] { };
        private ANFISData _data;
        public int InputCount { get; set; }

        private double TongLop3 =0;
        #region Ctor.
        /// <summary>
        /// can goi initData sau khi new mang
        /// </summary>
        /// <param name="inputcout"></param>
        public ANFISNetwork(int inputcout)
        {
            InputCount = inputcout;
            Lop1 = new double[inputcout];
            Lop2 = new double[inputcout, SKM];
            Lop3 = new double[SKM];
            Lop4 = new double[SKM];
            Lop5 = new double[SKM];

            arrHeSoGaussC = new double[SKM, inputcout];
            arrHeSoGaussD = new double[SKM, inputcout];
            arrHeSoP = new double[SKM, inputcout + 1];
        }
         #endregion

        #region set Data
        /// <summary>
        /// goi sau khi new mang. de init data cho mang
        /// </summary>
        public void initData()
        {
            var rd = new Random();
            for (int i = 0; i < SKM; i++)
                for (int j = 0; j < InputCount; j++)
                {
                    arrHeSoGaussC[i, j] = rd.NextDouble();
                    arrHeSoGaussD[i, j] = rd.NextDouble();
                }
            for (int i = 0; i < SKM; i++)
                for (int j = 0; j < InputCount + 1; j++) arrHeSoP[i, j] = rd.NextDouble();
        }

        public void SetData(ANFISData data)
        {
            _data = new ANFISData{Ideal = data.Ideal,Input = data.Input};
        }

        public void SetHeSoGaussCBetter(double[,] heSoGaussC)
        {
            arrHeSoGaussC = HeSoGaussC;
        }
        public void SetHeSoGaussDBetter(double[,] heSoGaussD)
        {
            arrHeSoGaussD = HeSoGaussD;
        }
        public void SetHePBetter(double[,] heSoP)
        {
            arrHeSoP = heSoP;
        }
        #endregion

        #region GetData
        public double[,] HeSoGaussC { get { return arrHeSoGaussC; } }
        public double[,] HeSoGaussD { get { return arrHeSoGaussD; } }
        public double[,] HeSoP { get { return arrHeSoP; } }
        #endregion

        #region layout network
        private void Layout1()
        {
            int i = 0; 
            foreach (var d in _data.Input)
            {
                Lop1[i] = d.Value;
                i++;
            }
        }

        private void Layout2()
        {
            for (int i = 0; i < InputCount; i++)
            {
                for (int j = 0; j < SKM; j++)
                {
                    Lop2[i, j] = ANFISUtils.Gauss(Lop1[i], arrHeSoGaussC[j, i], arrHeSoGaussD[j, i]);

                }
            }
        }

        private void Layout3()
        {
            TongLop3 = 0;
            for (int i = 0; i < SKM; i++)
            {
                Lop3[i] = 1;
                for (int j = 0; j < InputCount; j++)
                {
                    Lop3[i] *= Lop2[j, i];
                    TongLop3 += Lop3[i];
                }
            }
        }

        private void Layout4()
        {
            for (int i = 0; i < SKM; i++)
            {
                Lop4[i] = Lop3[i] / TongLop3;
            }
        }

        private void Layout5()
        {
            double Sum = 0;
            for (int i = 0; i < SKM; i++)
            {
                Sum = 0;
                for (int j = 0; j < InputCount; j++)
                {
                    Sum += arrHeSoP[i, j + 1] * Lop1[j];
                    Lop5[i] = Lop4[i] * (arrHeSoP[i, 0] + Sum);
                }
            }
        }

        private double Layout6()
        {
            Lop6 = 0;
            for (int i = 0; i < SKM; i++)
            {
                Lop6 += Lop4[i] * Lop5[i];
            }
            return Lop6;
        }
        #endregion

        private void UpdateWeight()
        {
            for (int i = 0; i < InputCount; i++)
            {
                for (int j = 0; j < SKM; j++)
                {
                    double temp = Lop4[j] * (Lop6 - _data.Ideal) * Lop1[i];
                    arrHeSoP[j, i + 1] = arrHeSoP[j, i + 1] - HSH * temp;
                }
                for (int j = 0; j < SKM; j++) //X0=1
                {
                    arrHeSoP[j, 0] -= Lop4[j] * (Lop6 - _data.Ideal);
                }
            }

           //Hàm Gauss, cập nhật c,d
            for (int i = 0; i < InputCount; i++)
            {
                for (int j = 0; j < SKM; j++)
                {
                    double temp_c = 0, temp_d = 0;
                    temp_c = Lop4[j]*(Lop6 - _data.Ideal)*(Lop5[j] - Lop6)*(Lop1[i] - arrHeSoGaussC[j, i])/
                             Math.Pow(arrHeSoGaussD[j, i], 2);
                    temp_d = Lop4[j]*(Lop6 - _data.Ideal)*(Lop5[j] - Lop6)*Math.Pow(Lop1[i] - arrHeSoGaussC[j, i], 2)/
                             Math.Pow(arrHeSoGaussD[j, i], 3);
                    arrHeSoGaussC[j, i] -= HSG_c*temp_c;
                    arrHeSoGaussD[j, i] -= HSG_d*temp_d;
                }
            }
        }
      
        public double Predic()
        {
            Layout1();
            Layout2();
            Layout3();
            Layout4();
            Layout5();
           return Layout6();
        }

        public void Trainning()
        {
            Layout1();
            Layout2();
            Layout3();
            Layout4();
            Layout5();
           var output= Layout6();
            UpdateWeight();
        }

        public double GetError()
        {
            return Math.Abs(Lop6 - _data.Ideal)/_data.Ideal;
        }
    }
}
