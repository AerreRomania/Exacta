using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Exacta
{
    public partial class QuantityIntervals : Form
    {
        private DataTable _finalTable = new DataTable();
        DataGridViewRow selectedRow = new DataGridViewRow();

        public QuantityIntervals()
        {
            InitializeComponent();
            Additional.DesignMyGrid(dgvQty);
        }
        private void populateGridData()
        {     
            _finalTable = new DataTable();
            _finalTable.Columns.Add("IDM", typeof(int));
            _finalTable.Columns.Add("Description");
            _finalTable.Columns.Add("Line");
            _finalTable.Columns.Add("Article");
            _finalTable.Columns.Add("Pieces/Hour");
            _finalTable.Columns.Add("Phase");
            _finalTable.Columns.Add("Operator");
            _finalTable.Columns.Add("sep1"); //7
            _finalTable.Columns.Add("Total Work Time");
            _finalTable.Columns.Add("sep2"); //9
            _finalTable.Columns.Add("Knitt Time");
            _finalTable.Columns.Add("Prep Time");
            _finalTable.Columns.Add("Stop Time");
            _finalTable.Columns.Add("Qty"); 
            _finalTable.Columns.Add("sep3"); //14
            _finalTable.Columns.Add("Avg Time");
            _finalTable.Columns.Add("Avg Qty");

            var newRow = _finalTable.NewRow();

            newRow[0] = selectedRow.Cells[0].Value.ToString();
            newRow[1] = selectedRow.Cells[1].Value.ToString();
            newRow[2] = selectedRow.Cells[2].Value.ToString();
            newRow[3] = selectedRow.Cells[3].Value.ToString();
            newRow[4] = selectedRow.Cells[4].Value.ToString();
            newRow[5] = selectedRow.Cells[6].Value.ToString();
            newRow[6] = selectedRow.Cells[7].Value.ToString();
            newRow[8] = selectedRow.Cells[9].Value.ToString();
            newRow[10] = selectedRow.Cells[11].Value.ToString();
            newRow[11] = selectedRow.Cells[12].Value.ToString();
            newRow[12] = selectedRow.Cells[13].Value.ToString();
            newRow[13] = selectedRow.Cells[14].Value.ToString();
            newRow[15] = selectedRow.Cells[selectedRow.Cells.Count - 2].Value.ToString();
            newRow[16] = selectedRow.Cells[selectedRow.Cells.Count - 1].Value.ToString();
            _finalTable.Rows.Add(newRow);

            dgvQty.DataSource = _finalTable;
            
            dgvQty.Columns["IDM"].Width = 60;
            dgvQty.Columns["Description"].Width = 150;
            dgvQty.Columns["Line"].Width = 90;
            dgvQty.Columns["Article"].Width = 130;
            dgvQty.Columns["Pieces/Hour"].Width = 80;
            dgvQty.Columns["Phase"].Width = 100;

            for(var i = 7; i <= dgvQty.ColumnCount - 1; i++)
            {
                if(i == 7 || i == 9 || i == 14)
                {
                    dgvQty.Columns[i].HeaderText = string.Empty;
                    dgvQty.Columns[i].Width = 6;
                    continue;
                }
                dgvQty.Columns[i].Width = 70;
            }
        }
        
        public void LoadData(DataGridViewRow row)
        {
            selectedRow = row;            
        }

        private void QuantityIntervals_Load(object sender, EventArgs e)
        {
            populateGridData();
            UpdateGraph();
         //  Exacta.Menu.Expiration();
        }

        private void UpdateGraph()
        {
            var _graphData = new DataTable();            

            var con = new SqlConnection(Exacta.Menu.connectionString);
            SqlCommand cmd = new SqlCommand("getMinutesBetween", con);
            cmd.CommandType = CommandType.StoredProcedure;

            int.TryParse(selectedRow.Cells[0].Value.ToString(), out var machineID);
            DateTime stDate = new DateTime(Exacta.Menu.dateFrom.Year, Exacta.Menu.dateFrom.Month,
                                           Exacta.Menu.dateFrom.Day);

            cmd.Parameters.Add("@IDM", SqlDbType.Int).Value = machineID;
            cmd.Parameters.Add("@startDate", SqlDbType.Date).Value = stDate;
            cmd.Parameters.Add("@endDate", SqlDbType.Date).Value = stDate;

            con.Open();
            var dr = cmd.ExecuteReader();
            _graphData.Load(dr);           
            dr.Close();
            con.Close();

            if (_graphData.Rows.Count <= 0)            
                return;            
            
            var myPane = new GraphPane();
            myPane = zedGraphControl1.GraphPane;

            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();
            myPane.Fill = new Fill();

            //legend
            myPane.Legend.Position = LegendPos.Bottom;
            myPane.Legend.FontSpec.Size = 20;
            myPane.Legend.Border.IsVisible = false;

            myPane.Title.Text = "Intervals between cuts";
            //myPane.BarSettings.Type = BarType.Cluster;
            myPane.Title.FontSpec.FontColor = Color.Black;

            //x axis
            myPane.XAxis.Title.Text = "Minutes";
            myPane.XAxis.Title.FontSpec.FontColor = Color.Red;
            myPane.XAxis.Title.FontSpec.Size = 20;
            myPane.XAxis.Title.FontSpec.IsBold = false;
            myPane.XAxis.Scale.MajorStep = 1;
            //myPane.XAxis.MinorTic.Size = 1;
            myPane.XAxis.Scale.Min = 7;
            myPane.XAxis.Scale.Max = 15;
            myPane.XAxis.MajorTic.IsInside = false;
            myPane.XAxis.MajorTic.IsOutside = true;
            myPane.XAxis.Type = AxisType.Date;
            myPane.XAxis.Scale.Format = "HH:mm";
            myPane.XAxis.Scale.MajorUnit = DateUnit.Minute;
            myPane.XAxis.Scale.MinorUnit = DateUnit.Minute;

            //y axis
            myPane.YAxis.Title.Text = "Quantity";
            myPane.YAxis.Title.FontSpec.FontColor = Color.Green;

            myPane.YAxis.Title.FontSpec.Size = 20;
            myPane.YAxis.Title.FontSpec.IsBold = false;

            //myPane.YAxis.Scale.MajorStep = 1;
            //myPane.YAxis.MinorTic.Size = 0;

            var lstQty = new PointPairList();
            var qt = new List<int>();
            var xAxisVals = new List<DateTime>();

            var dailyQty = _graphData.Rows.Count;

            foreach(DataRow row in _graphData.Rows)
            {
                int.TryParse(row[0].ToString(), out var minsBetween);
            
                DateTime.TryParse(row[1].ToString(), out var ts);
                XDate xScalDate = new XDate(ts);

                lstQty.Add(xScalDate, minsBetween);
                qt.Add(minsBetween);
                xAxisVals.Add(xScalDate);
            }
            myPane.YAxis.Scale.Max = qt.Max() + 4;
            myPane.XAxis.Scale.Max = xAxisVals.Max().ToOADate();
            myPane.XAxis.Scale.Min = xAxisVals.Min().ToOADate();
        
            myPane.AddHiLowBar(string.Empty, lstQty, Color.Black);

            var position = 0;
            foreach (var bar in lstQty)
            {
                if (bar.Y != 0)
                {                    
                    var barTime = xAxisVals.ElementAt(position).ToString("H:mm");
                    TextObj barLabel = new TextObj(barTime, bar.X, bar.Y + 1);
                    barLabel.FontSpec.Size = 9;
                    barLabel.FontSpec.Border.IsVisible = false;
                    myPane.GraphObjList.Add(barLabel);                   
                }
                position++;
            }

            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        private void dgvQty_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= -1)
            {
                if (e.ColumnIndex == 7 || e.ColumnIndex == 9 || e.ColumnIndex == 14)
                {
                    var rect = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                    e.Graphics.FillRectangle(new SolidBrush(dgvQty.BackgroundColor), rect);
                    e.Handled = true;
                }
            }
        }
    }

}
