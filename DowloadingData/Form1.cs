using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DowloadingData.Properties;

namespace DowloadingData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private List<TradingData> listData = new List<TradingData>();
        private string formatdate = "ddd. MMM d, yyyy";
        private List<string> lisvalue = new List<string>();


        public static WebClient wClient = new WebClient();
        public static StreamWriter sw;
        public static TextWriter textWriter;
        /*I USE THIS FUNCTION TO READ WEB PAGE ON INTERNET BUT IT NOT WORK CORRECTLY ON SOME LINKS*/
        public static String readFromLink(String url)
        {
            /* IT WORK CORRECTLY IF WE SET url = "http://vn.yahoo.com/?p=us"; BUT NOT OK WITH URL BELOW*/
            //String url = "http://raovat.com/?rv=detail&idrv=527443&idcate=57&tt=vppnhatthanh@yahoo.com.vn";

            /* PLEASE HELP ME MAKE FUNCTION READFROMLINK RUNABLE WITH THIS LINK*/
            System.Net.WebClient client = new System.Net.WebClient();
            byte[] data = client.DownloadData(url);
            String html = System.Text.Encoding.UTF8.GetString(data);
            return html;


        }
        public static bool WriteTextFile(String fileName, String t)
        {

            try
            {
                textWriter = new StreamWriter(fileName);
            }
            catch (Exception)
            {
                return false;
            }

            try
            {
                textWriter.WriteLine(t);
            }
            catch (Exception)
            {
                return false;
            }
            textWriter.Close();
            return true;
        }
        private void btnRead_Click(object sender, EventArgs e)
        {
            lblAction.Text = "Tai trang web";
            var conten = readFromLink(txtURL.Text);
            txtContent.Text = conten;

            lblAction.Text = "Tai trang web thanh cong";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblAction.Text = "";
            txtURL.Text =
                "http://www.barchart.com/charts/futures/KCZ11";
            txtregex.Text = "showOHLCTooltip([^<].*)\'";
            txtregex2.Text = "showStudyTooltip([^<].*)\'";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             
                   // WriteTextFile(@"F:\Cao hoc\Luan van\source code\barchart.html", txtContent.Text);
            WriteData();

        }
     Dictionary<long,double> dicVolumn = new Dictionary<long, double>();

     private void btnRegexVolum_Click(object sender, EventArgs e)
     {
         lblAction.Text = "Lay thong tin Volum";
         txtketqua.Text = "";
         lisvalue.Clear();
         Regex Regex = new Regex(txtregex2.Text);
         var mystring = txtContent.Text;
         Regex Regex2 = new Regex("'([^<].*)'");
         var str = "";
         while (true)
         {


             Match ma = Regex.Match(mystring);

             if (ma.Value.Length <= 0)
                 break;
             var r2 = Regex2.Match(ma.Value);
             lisvalue.Add(r2.Value);
             str += "\n" + r2.Value.ToString();

             mystring = mystring.Replace(ma.Value, " ");
         }
         txtketqua.Text = str;
         if (txtketqua.Text.Length == 0)
             txtketqua.Text = "khoc co ket qu";

         lblAction.Text = "Lay thong tin Volum Thanh cong";
     }
        private void btnregex_Click(object sender, EventArgs e)
        {
            string str = "";
            lisvalue.Clear();
            txtValue2.Text = "";
            Regex Regex = new Regex(txtregex.Text);
            var mystring =txtContent.Text;
            Regex Regex2 = new Regex("'([^<].*)'");
            while (true)
            {


                Match ma = Regex.Match(mystring);
               
                if (ma.Value.Length <= 0)
                    break;
                var r2 = Regex2.Match(ma.Value);
                lisvalue.Add(r2.Value);
             str += "\n" + r2.Value.ToString();

                mystring = mystring.Replace(ma.Value, " ");
            }
            txtValue2.Text = str;
            if (txtValue2.Text.Length == 0)
                txtValue2.Text = "khoc co ket qu";

            lblAction.Text = "Loc thong tin OHLC thanh cong";
        }
       
        private void btnValue_Click(object sender, EventArgs e)
        {
            listData.Clear();
            foreach (var s in lisvalue)
            {
                var s1 = s.Replace('\'', ' ');
                 s1 = s1.Replace('[', ' ');
                 s1 = s1.Replace(']', ' ');
                var li = s1.Split(',');
                if(li.Count() < 8) continue;
                var data = new TradingData();
                var datetime = li[1].Trim() + ", " + li[2].Trim();
                DateTime day = DateTime.ParseExact(datetime,
                                  formatdate,
                                   CultureInfo.InvariantCulture);
                data.Day = day;
                data.Symbol = li[3].Trim();
                data.Open = double.Parse(li[4].Trim());
                data.High = double.Parse(li[5].Trim());
                data.Low = double.Parse(li[6].Trim());
                data.Close = double.Parse(li[7].Trim());

                if (dicVolumn.ContainsKey(day.Ticks))
                    data.Volume = dicVolumn[day.Ticks];
                listData.Add(data);
            }

            lblAction.Text = "Lay thong tin OHLC thanh cong";
        }

       
        private void btnVolumn_Click(object sender, EventArgs e)
        {
            dicVolumn.Clear();
           
            foreach (var s in lisvalue)
            {
                var s1 = s.Replace('\'', ' ');
                s1 = s1.Replace('[', ' ');
                s1 = s1.Replace(']', ' ');
                var li = s1.Split(',');
               
                var datetime = li[1].Trim() + ", " + li[2].Trim();
                DateTime day = DateTime.ParseExact(datetime,
                                  formatdate,
                                   CultureInfo.InvariantCulture);
               
                var volum = li[3].Trim();
                if(volum != "Volume") continue;
                var volumn = double.Parse(li[4].Trim());
                if (dicVolumn.ContainsKey(day.Ticks))
                {
                    var old = dicVolumn[day.Ticks];
                     if(old < volumn)
                         dicVolumn[day.Ticks] = volumn;
                    continue;
                }
                dicVolumn[day.Ticks] = volumn;
            }
            lblAction.Text = "Lay thong tin Volum thanh cong";
        }
        private void WriteData()
        {
            if(listData.Count ==0) return;
            var name = listData[0].Symbol + "_" + listData.Count.ToString();
            using (SaveFileDialog sfd = new SaveFileDialog() { FileName = name +".csv", Filter = "(*.csv)|*.csv }" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // Stream s = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None);

                    var writer = new StreamWriter(sfd.FileName);
                    writer.WriteLine("Date,Open,High,Low,Close,Volume");
                    foreach (var da in listData)
                    {
                        writer.WriteLine(da.Day.ToString("MM/dd/yyyy") + "," + da.Open.ToString() + "," + da.High.ToString() + "," + da.Low.ToString() + "," + da.Close.ToString() + "," + da.Volume.ToString());
                    }
                    writer.Close();
                }
            }
            lblAction.Text = "Ghi du lieu thanh cong";
        }


       
    }
}
