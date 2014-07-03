using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AppTestTool.Data;
using BaseEntity.Entity;
using ZedGraph;

namespace AppTestTool
{
    public partial class CommodityResultsGraph : Form
    {
        public CommodityResultsGraph()
        {
            InitializeComponent();
            
        }

        private void Error_Load(object sender, EventArgs e)
        {
           
        }

        public void GraphInit(DateTime _predictFrom, DateTime _predictTo)
        {

            GraphPane myPane1 = DoThi_Error.GraphPane; // Khai báo sửa dụng Graph loại GraphPane;

            myPane1.Title.Text = "Đồ thị dự đoán giá Close Mã GD: KCZ13";
            myPane1.XAxis.Title.Text = "Ngày dự đoán";
            myPane1.YAxis.Title.Text = "Giá trị dự đoán";
            // Định nghĩa list để vẽ đồ thị. Để các bạn hiểu rõ cơ chế làm việc ở đây khai báo 2 list điểm <=> 2 đường đồ thị
            RollingPointPairList list6_1 = new RollingPointPairList(1000);
            RollingPointPairList list6_2 = new RollingPointPairList(1000);
            // dòng dưới là định nghĩa curve để vẽ.
            myPane1.AddCurve("Giá trị thực đo", list6_1, Color.Red, SymbolType.XCross);
            myPane1.AddCurve("Giá trị tính toán bởi mạng", list6_2, Color.Blue, SymbolType.None);


            // Định hiện thị cho trục thời gian (Trục X)
            //myPane1.XAxis.Scale.Min = 0;
            //myPane1.XAxis.Scale.Max = 10;
            //myPane1.XAxis.Scale.MinorStep = 1;
            //myPane1.XAxis.Scale.MajorStep = 1;
            myPane1.XAxis.Type = AxisType.Date;
            myPane1.XAxis.Scale.Min = new XDate(_predictFrom);  // We want to use time from now
            myPane1.XAxis.Scale.Max = new XDate(_predictTo);  // to 5 minutes per default
            //myPane1.XAxis.Scale.MinorUnit = DateUnit.Day;         // set the minimum x unit to time/seconds
            //myPane1.XAxis.Scale.MajorUnit = DateUnit.Day;         // set the maximum x unit to time/minutes
            myPane1.XAxis.Scale.Format = "MM/dd/yyyy";
            // Gọi hàm xác định cỡ trục
            myPane1.AxisChange();

        }

        public void DrawGraph( List<CommodityResults> results)
        {
           
            //ve gia tri
            LineItem curve2_1 = DoThi_Error.GraphPane.CurveList[0] as LineItem;
            LineItem curve2_2 = DoThi_Error.GraphPane.CurveList[1] as LineItem;
            //init do thi.

            // Get the PointPairList
            IPointListEdit list21 = curve2_1.Points as IPointListEdit;
            IPointListEdit list22 = curve2_2.Points as IPointListEdit;
            list21.Clear();
            list22.Clear();

            DoThi_Error.AxisChange();
            DoThi_Error.Invalidate();
            int i = 0;
            foreach (var item in results)
            {
                var xdate = new XDate(item.Date);
                list21.Add(xdate, item.ActualClose);
                list22.Add(xdate, item.PredictedClose);
                // đoạn chương trình thực hiện vẽ đồ thị
                Scale xScale = DoThi_Error.GraphPane.XAxis.Scale;
                i++;
            }
            // Vẽ đồ thị
            DoThi_Error.AxisChange();
            // Force a redraw
            DoThi_Error.Invalidate();
        }

    }
}
