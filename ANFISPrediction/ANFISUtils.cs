using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ANFISPrediction
{
    public class ANFISUtils
    {

        public static double UnSigmod(double y)
        {
            return (Math.Log(y) - Math.Log(Math.Abs(1 - y)));
        }
        // Hàm chuẩn hóa dạng Sigmod
        public static double Sigmod(double x)
        {
            return (double)1 / (1 + Math.Pow(Math.E, -x));
        }

        // Hàm liên thuộc dạng Gauss
        public static double Gauss(double x, double c, double d)
        {
            return Math.Pow(Math.E, (-(x - c) * (x - c) / (2 * d * d)));
        }

        public static double DecodeMinMax(double d, double mind, double maxd)
        {
            return (d * (maxd * 1.01 - mind * 0.99) + mind * 0.99);  
        }
        public static double EncodeMinMax(double d, double mind, double maxd)
        {
            return (d - mind * 0.99) / (maxd * 1.01 - mind * 0.99);
        }
        #region SerializeObject
        public static object Load(string filename)
        {
            Stream s = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
            var b = new BinaryFormatter();
            object obj = b.Deserialize(s);
            s.Close();
            return obj;
        }

        /// <summary>
        /// Save the specified object.
        /// </summary>
        /// <param name="filename">The filename to save to.</param>
        /// <param name="obj">The object to save.</param>
        public static void Save(string filename, object obj)
        {
            Stream s = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None);
            var b = new BinaryFormatter();
            b.Serialize(s, obj);
            s.Close();
        } 
        #endregion
    }
}
