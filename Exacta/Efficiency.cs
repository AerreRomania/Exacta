using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace Exacta
{
    public partial class Efficiency : Form
    {
        private string _machineDescrtiption;
        private string _machineLine;
        //private string _machineOperator;
       // private bool bx = false;
        public int SingleSelectedMachine { get; set; }
        private DataTable _finalTable = new DataTable();
        Dictionary<string, int> _totalPieces = new Dictionary<string, int>();

        public Efficiency()
        {
            InitializeComponent();
            PauseTable();
        }

        private void Efficiency_Load(object sender, EventArgs e)
        {
            if (SingleSelectedMachine <= 0) SingleSelectedMachine = -1;

            dgvEfficiency.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn c in dgvEfficiency.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvEfficiency.RowHeadersVisible = false;
                }
                dgvEfficiency.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                dgvEfficiency.GridColor = Color.Silver;
                dgvEfficiency.RowTemplate.Height = 25;
                dgvEfficiency.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised; //Sunken
                dgvEfficiency.CellBorderStyle = DataGridViewCellBorderStyle.Raised; //Sunken
                dgvEfficiency.EnableHeadersVisualStyles = false;
                dgvEfficiency.ColumnHeadersDefaultCellStyle.BackColor = Color.Gold;
                dgvEfficiency.DefaultCellStyle.BackColor = dgvEfficiency.BackgroundColor;
                dgvEfficiency.RowHeadersVisible = false;

                for (var i = 0; i <= dgvEfficiency.Columns.Count - 1; i++)
                {
                    var c = dgvEfficiency.Columns[i];
                    c.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
                    c.HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 7, FontStyle.Regular);
                }
            };
            dgvEfficiency.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEfficiency.AllowUserToAddRows = false;
            dgvEfficiency.AllowUserToDeleteRows = false;
            dgvEfficiency.AllowUserToResizeColumns = false;
            dgvEfficiency.AllowUserToResizeRows = false;
            dgvEfficiency.AllowUserToOrderColumns = false;
            dgvEfficiency.ReadOnly = true;
            dgvEfficiency.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEfficiency.MultiSelect = false;
            dgvEfficiency.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvEfficiency.ColumnHeadersHeight = this.dgvEfficiency.ColumnHeadersHeight * 2 + 20;
            dgvEfficiency.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;            
            dgvEfficiency.BackgroundColor = Color.FromArgb(235, 235, 235);
            dgvEfficiency.RowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvEfficiency.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgvEfficiency.RowHeadersDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvEfficiency.EnableHeadersVisualStyles = true;
            dgvEfficiency.BorderStyle = BorderStyle.None;
            dgvEfficiency.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvEfficiency.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEfficiency.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            dgvEfficiency.CellPainting += new DataGridViewCellPaintingEventHandler(dgvEfficiency_CellPainting);
            dgvEfficiency.Paint += new PaintEventHandler(dgvEfficiency_Paint);


            addDataTableColumns();
            dgvEfficiency.DoubleBuffered(true);
            readf();
            Hidecollumns();
            
        }
        public double startpC;
        public double endpC;
        public void readf()
        {
            string start = "";
            string end = "";
            try
            {
                var _dir = AppDomain.CurrentDomain.BaseDirectory;
                //FileStream file = new FileStream(_dir + "\\file.txt", FileMode.OpenOrCreate);
                StreamReader sr = new StreamReader(_dir + "\\file.txt");
                start = sr.ReadLine();
                end = sr.ReadLine();
                sr.Close();
                startpC = Convert.ToDouble(start.ToString());
                endpC = Convert.ToDouble(end.ToString());
            }
            catch (Exception e)
            {
                startpC = 6;
                endpC = 15;
                //MessageBox.Show("Program is not set, default is 6-15.\n To set Program go to Settings/Working time.");
            }
        }
        public class interval
        {
            public DateTime Istartp { get; set; }
            public DateTime Iendp { get; set; }

        }
        public DateTime startp = new DateTime();
        public DateTime endp = new DateTime();
        public static TimeSpan pauseT;
        public static decimal ts;
        public static List<interval> items = new List<interval>();
        public void PauseTable()
        {
            pauseT = TimeSpan.Zero;
            items.Clear();
            var pauseQ = (from pause in DatabaseTableClasses.Tables.TblPauses
                          select pause
                   ).ToList();
            foreach (var r in pauseQ)
            {

                string s = r.StartP;
                string hoursS = s.Split(':')[0];
                string minutesS = s.Split(':')[1];
                int.TryParse(hoursS, out int hs);
                int.TryParse(minutesS, out int ms);
                startp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hs, ms, 0);
                string e = r.EndP;
                string hoursE = e.Split(':')[0];
                string minutesE = e.Split(':')[1];
                int.TryParse(hoursE, out int he);
                int.TryParse(minutesE, out int me);
                endp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, he, me, 0);
                pauseT += endp - startp;
                items.Add(new interval { Istartp = startp, Iendp = endp });
            }
        }
        private void addDataTableColumns()
        {
            _finalTable = new DataTable();
            _finalTable.Columns.Add("IDM", typeof(int));
            _finalTable.Columns.Add("Description");
            _finalTable.Columns.Add("Line");
            _finalTable.Columns.Add("Article");
            _finalTable.Columns.Add("Phase");
            _finalTable.Columns.Add("Operator");
            _finalTable.Columns.Add("sep1"); //6
            //total
            _finalTable.Columns.Add("Efficiency_t");
            _finalTable.Columns.Add("sep2"); //8

            for (int i = 9; i <= 22; i++)
            {
                _finalTable.Columns.Add("Efficiency_" + i.ToString());
            }

            GetData();
            dgvEfficiency.DataSource = _finalTable;

            for (var i = 6; i <= dgvEfficiency.Columns.Count - 1; i++)
            {
                if (i == 8 || i == 6)
                {
                    dgvEfficiency.Columns[i].Width = 6;
                    dgvEfficiency.Columns[i].HeaderText = "";
                    continue;
                }
                else
                {
                    dgvEfficiency.Columns[i].HeaderText = "Efficiency";
                    dgvEfficiency.Columns[i].Width = 55;
                }                
            }

            for (int i = 0; i <= 6; i++)
            {
                //dgvEfficiency.Columns[i].DefaultCellStyle.BackColor = SystemColors.Control;

                dgvEfficiency.Columns[i].Frozen = true; 
                dgvEfficiency.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dgvEfficiency.Columns[7].Frozen = true;

            dgvEfficiency.Columns[0].Width = 40;
            dgvEfficiency.Columns[1].Width = 90;
            dgvEfficiency.Columns[2].Width = 60;
            dgvEfficiency.Columns[4].Width = 80;
            dgvEfficiency.Columns[7].Width = 55;

            var bs = new BindingSource();
            bs.DataSource = _finalTable;
            if (!(SingleSelectedMachine <= 0))
            {
                bs.Filter = string.Format("CONVERT(" + dgvEfficiency.Columns[0].DataPropertyName + ", System.String) like '%" + SingleSelectedMachine.ToString().Replace("'", "''") + "%'");

                dgvEfficiency.DataSource = bs;
                foreach (DataGridViewRow row in dgvEfficiency.Rows)
                {
                    if (row.Cells[0].Value.ToString() == SingleSelectedMachine.ToString())
                    {
                        row.Visible = true;
                    }
                    else
                    {
                        row.Visible = false;
                    }
                }
            }
        }

        private void GetData()
        {
            DataTable _finalTable = new DataTable();
            DataTable tmpTable = new DataTable();
            List<DateTime> _lstTime = new List<DateTime>();

            var newRow = tmpTable.NewRow();

            tmpTable.Columns.Add("IDM", typeof(string));
            tmpTable.Columns.Add("hourx", typeof(int));
            tmpTable.Columns.Add("ts", typeof(DateTime));
            tmpTable.Columns.Add("TIC", typeof(bool));
            tmpTable.Columns.Add("NAME", typeof(string));
            tmpTable.Columns.Add("Operatie", typeof(string));
            tmpTable.Columns.Add("PPH", typeof(double));
            tmpTable.Columns.Add("COMPONENTS", typeof(int));
            tmpTable.Columns.Add("OPERATOR", typeof(string));

            var startHour =6;
            var nextHour = 0;
            var startMachine = "0";
            var firstRead = true;
            string startName = string.Empty;
            string startOperatie = string.Empty;
            double startPph = 0;
            int startComponents = 0;
            var startOperator = string.Empty;

            object[] arrOfData = new object[] { };
            object[] arrOfAdditionalData = new object[] { };
            object[] additionArr = new object[] { };

            var con = new SqlConnection(Exacta.Menu.connectionString);
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[sp_getEfficiency]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@dateFrom", SqlDbType.NVarChar).Value = Exacta.Menu.dateFrom.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("@dateTo", SqlDbType.NVarChar).Value = Exacta.Menu.dateFrom.ToString("MM/dd/yyyy");

            con.Open();
            var dr = cmd.ExecuteReader();

            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    newRow = tmpTable.NewRow();
                    var index = 0;

                    string idm = dr[0].ToString();
                    int.TryParse(dr[1].ToString(), out var hourx);
                    DateTime.TryParse(dr[2].ToString(), out var ts);
                    bool.TryParse(dr[3].ToString(), out var tic);
                    string name = dr[4].ToString();
                    string operatie = dr[5].ToString();
                    double.TryParse(dr[6].ToString(), out var pph);
                    int.TryParse(dr[7].ToString(), out var comp);
                    var macOperator = dr[8].ToString();
                    
                    if (firstRead)
                    {
                        startMachine = idm;
                    }
                    else
                    {
                        if (startMachine != idm)
                        {
                            startHour = hourx;
                            var illegalTs = new DateTime(ts.Year, ts.Month, ts.Day, nextHour, 0, 0);
                            arrOfData = new object[] { startMachine, nextHour, illegalTs, 0, startName, startOperatie, startPph,
                                                       startComponents, startOperator };

                            if (nextHour < endpC+1)
                            {
                                if (arrOfData.Length > 0)
                                {
                                    newRow = tmpTable.NewRow();
                                    index = 0;
                                    foreach (var item in arrOfData)
                                    {
                                        newRow[index] = item;
                                        index++;
                                    }
                                    tmpTable.Rows.Add(newRow);
                                }
                                nextHour = startHour;
                            }
                        }
                    }

                    var fullTs = new DateTime(ts.Year, ts.Month, ts.Day, ts.Hour, ts.Minute, ts.Second);
                    arrOfData = new object[] { idm, hourx, fullTs, tic, name, operatie, pph, comp, macOperator };
                    arrOfAdditionalData = new object[] { };
                    var arrOfStartPointerHour = new object[] { };
                    additionArr = new object[] { };

                    var virtTs = new DateTime(fullTs.Year, fullTs.Month, fullTs.Day, nextHour, 0, 0);
                    var lastMinute = new DateTime(fullTs.Year, fullTs.Month, fullTs.Day, nextHour, 59, 59);
                    var newHour = new DateTime(fullTs.Year, fullTs.Month, fullTs.Day, hourx, 0, 0);

                    if (startHour != hourx && ts.Minute > 0)
                    {
                        arrOfStartPointerHour = new object[] { idm, hourx, newHour, 0, name, operatie, pph, comp, macOperator };
                    }

                    if (startHour != hourx && nextHour != hourx)
                    {
                        arrOfAdditionalData = new object[] { idm, nextHour, virtTs, 0, name, operatie, pph, comp, macOperator };
                        additionArr = new object[] { idm, nextHour, lastMinute, 0, name, operatie, pph, comp, macOperator };
                    }

                    if (arrOfAdditionalData.Length > 0)
                    {
                        foreach (var item in arrOfAdditionalData)
                        {
                            newRow[index] = item;
                            index++;
                        }
                        tmpTable.Rows.Add(newRow);
                    }
                    if (additionArr.Length > 0)
                    {
                        newRow = tmpTable.NewRow();
                        index = 0;
                        foreach (var item in additionArr)
                        {
                            newRow[index] = item;
                            index++;
                        }
                        tmpTable.Rows.Add(newRow);
                    }

                    if (arrOfStartPointerHour.Length > 0)
                    {
                        index = 0;
                        newRow = tmpTable.NewRow();
                        foreach (var item in arrOfStartPointerHour)
                        {
                            newRow[index] = item;
                            index++;
                        }
                        tmpTable.Rows.Add(newRow);
                    }

                    newRow = tmpTable.NewRow();
                    index = 0;
                    foreach (var item in arrOfData)
                    {
                        newRow[index] = item;
                        index++;
                    }

                    tmpTable.Rows.Add(newRow);

                    nextHour = hourx + 1;
                    startHour = hourx;
                    startMachine = idm;
                    firstRead = false;
                    startName = name;
                    startOperatie = operatie;
                    startPph = pph;
                    startComponents = comp;
                    startOperator = macOperator;
                }
            }            
            
            dr.Close();
            con.Close();

            if (tmpTable.Rows.Count <= 0)
                return;

            int numOfCuts = 0;
            int shiftCuts = 0;

            var machine = tmpTable.Rows[0][0].ToString();
            int.TryParse(tmpTable.Rows[0][1].ToString(), out var catchHour);
            var hour = catchHour;
            string article = tmpTable.Rows[0][4].ToString();
            string phase = tmpTable.Rows[0][5].ToString();
            float.TryParse(tmpTable.Rows[0][6].ToString(), out var catchPieces);
            int.TryParse(tmpTable.Rows[0][7].ToString(), out var catchComponents);

             newRow = this._finalTable.NewRow();

            foreach (DataRow row in tmpTable.Rows)
            {
                var dbMachine = row[0].ToString();
                int.TryParse(row[1].ToString(), out var dbHourx);
                bool.TryParse(row[3].ToString(), out var tic);
                var dbArticle = row[4].ToString();
                var dbPhase = row[5].ToString();
                float.TryParse(row[6].ToString(), out var piecesPerHour);
                int.TryParse(row[7].ToString(), out var components);
                var machineOperator = row[8].ToString();

                if (!_totalPieces.ContainsKey(article + "_" + phase) && !string.IsNullOrEmpty(phase) && piecesPerHour != 0)
                    _totalPieces.Add(article + "_" + phase, components * Convert.ToInt32(piecesPerHour));

                if (machine != dbMachine)
                {                   
                    this._finalTable.Rows.Add(newRow);
                    newRow = this._finalTable.NewRow();
                    shiftCuts = 0;
                    numOfCuts = 0;
                }
                else if(article != dbArticle || phase != dbPhase// ||
                        /*article != dbArticle && phase != dbPhase*/)
                {
                    this._finalTable.Rows.Add(newRow);
                    newRow = this._finalTable.NewRow();
                    numOfCuts = 0;
                }

                if (hour != dbHourx || article != dbArticle || phase != dbPhase)
                {
                    GetMachineProperties(dbMachine);
                    newRow[0] = dbMachine;
                    newRow[1] = _machineDescrtiption;
                    newRow[2] = _machineLine;
                    newRow[3] = dbArticle;
                    newRow[4] = dbPhase;
                    newRow[5] = machineOperator;

                    var ix = 0;
                    for (var h =6 ; h <= 19; h++)
                    {
                            if (hour == h)
                            {
                                ts = 60;
                            foreach (var i in items)
                                if (items.Count != 0)
                                {
                                    if (hour == i.Istartp.Hour && (hour >= i.Istartp.Hour || hour <= i.Iendp.Hour))
                                    {
                                        if (i.Iendp.Minute == 0)
                                        {
                                            ts = 60 - Convert.ToDecimal((60 - i.Istartp.Minute));
                                        }
                                        else
                                        {
                                            ts = 60 - Convert.ToDecimal((i.Iendp.Minute - i.Istartp.Minute));
                                        }
                                    }
                                }
                                else ts = 60;
                            if (piecesPerHour != 0)
                                {
                                    decimal cuts = Convert.ToDecimal(numOfCuts);
                                    decimal pph = Convert.ToDecimal(piecesPerHour * components);
                                    decimal ppm = Convert.ToDecimal(Math.Round(pph/60,2)*ts);
                                    int result = 0;
                                    result = Convert.ToInt32(Math.Round((cuts / ppm), 2) *100);
                                    newRow[9 + ix] = result.ToString() + "%";
                                    shiftCuts += numOfCuts;
                                }
                                else newRow[9 + ix] = "0%";
                            
                        }
                        ix++;
                    }
                    
                    if (piecesPerHour != 0)
                    {
                        decimal numOfShiftCuts = Convert.ToDecimal(shiftCuts);
                        decimal piecesPerShift = Convert.ToDecimal(piecesPerHour * components * 7.5);
                        int eff = 0;
                        eff = Convert.ToInt32(Math.Round((numOfShiftCuts / piecesPerShift), 2) * 100);
                        newRow[7] = eff.ToString() + "%";
                    }
                    else newRow[7] = "0%";
                    numOfCuts = 0;
                }

                if (tic)
                    numOfCuts++;

                machine = dbMachine;
                hour = dbHourx;
                article = dbArticle;
                phase = dbPhase;
                catchPieces = piecesPerHour;
                catchComponents = components;
            }
            this._finalTable.Rows.Add(newRow);
        }

        private string GetMachineProperties(string machineId)
        {
            var machineDesc = "";
            _machineDescrtiption = "";
            _machineLine = "";
            //_machineOperator = "";

            var machineProp = from machines in DatabaseTableClasses.Tables.TblMachines
                              where machines.Idm == machineId
                              select machines;

            var macList = machineProp.ToList();

            foreach (var mac in macList)
            {
                _machineDescrtiption = mac.Description;
                _machineLine = mac.Line;

                var isLineEmpty = string.IsNullOrEmpty(_machineLine);

                if (!isLineEmpty)
                {
                    var sb = new StringBuilder();

                    foreach (char ch in _machineLine.ToCharArray())
                    {
                        if (char.IsDigit(ch))
                        {
                            sb.Append(ch);
                        }
                    }
                }
                //_machineOperator = mac.Operator;
            }
            return machineDesc;
        }

        private string GetTimeFromSeconds(double seconds)
        {
            var str = "";

            var ts = TimeSpan.FromSeconds(seconds);
            var h = ts.Hours;
            var m = ts.Minutes;

            return str = ts.ToString(@"hh\:mm");
        }

        #region Merged cells

        private void dgvEfficiency_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                System.Drawing.Rectangle r2 = e.CellBounds;
                e.Graphics.FillRectangle(new SolidBrush(Color.Gold), r2);
                r2.Y += e.CellBounds.Height / 2;
                r2.Height = e.CellBounds.Height / 2;      
                e.PaintBackground(r2, true);
                e.PaintContent(r2);
                e.Handled = true;
            }
            if (e.RowIndex >= -1)
            {
                if (e.ColumnIndex == 6 || e.ColumnIndex == 8)
                {
                    var rect = new System.Drawing.Rectangle(e.CellBounds.X - 1, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                    e.Graphics.FillRectangle(new SolidBrush(dgvEfficiency.BackgroundColor), rect);
                    e.Handled = true;
                }
            }
        }

        #endregion Merged cells

        private void dgvEfficiency_Paint(object sender, PaintEventArgs e)
        {
                string[] monthes = { "Total", "6h","7h", "8h", "9h", "10h", "11h", "12h", "13h", "14h", "15h", "16h, ","17h", "18h", "19h" };
                var i = 0;

                System.Drawing.Rectangle r1 = this.dgvEfficiency.GetCellDisplayRectangle(7, -1, true);
                int w2 = this.dgvEfficiency.GetCellDisplayRectangle(7, -1, true).Width;
                r1.X += -1;
                r1.Y += 1;
                r1.Width = 55 /*r1.Width + w2 - 2*/;
                r1.Height = r1.Height / 2 /*- 6*/;
                e.Graphics.FillRectangle(new SolidBrush(Color.Gold), r1);
                var p = new Pen(new SolidBrush(Color.Gainsboro), 1);
                e.Graphics.DrawRectangle(p, r1);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(monthes[i],
                this.dgvEfficiency.ColumnHeadersDefaultCellStyle.Font,
                new SolidBrush(this.dgvEfficiency.ColumnHeadersDefaultCellStyle.ForeColor),
                r1,
                format);               
                i++;

                for (int j = 9; j < 23;)
                {
                    System.Drawing.Rectangle rr1 = this.dgvEfficiency.GetCellDisplayRectangle(j, -1, true);
                    int v2 = this.dgvEfficiency.GetCellDisplayRectangle(j, -1, true).Width;
                    rr1.X += -1;
                    rr1.Y += 1;
                    rr1.Width = 55 /*r1.Width + w2 - 2*/;
                    rr1.Height = rr1.Height / 2 /*- 6*/;
                    e.Graphics.FillRectangle(new SolidBrush(Color.Gold), rr1);
                    var pen = new Pen(new SolidBrush(Color.Gainsboro), 1);
                    e.Graphics.DrawRectangle(pen, rr1);
                    StringFormat sFormat = new StringFormat();
                    sFormat.Alignment = StringAlignment.Center;
                    sFormat.LineAlignment = StringAlignment.Center;
                    e.Graphics.DrawString(monthes[i],
                    this.dgvEfficiency.ColumnHeadersDefaultCellStyle.Font,
                    new SolidBrush(this.dgvEfficiency.ColumnHeadersDefaultCellStyle.ForeColor),
                    rr1,
                    sFormat);
                    j += 1;
                    i++;
                }                
            }
        private void PopulateGraphTotals(int rowIndex)
        {
            var myPane = new GraphPane();
            myPane = zedGraphControl2.GraphPane;

            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();
            myPane.Fill = new Fill();

            myPane.Legend.Position = LegendPos.Bottom;
            myPane.Legend.FontSpec.Size = 20;
            myPane.Legend.Border.IsVisible = false;

            myPane.XAxis.Title.Text = string.Empty;
            myPane.XAxis.Title.FontSpec.Size = 20;
            myPane.XAxis.Title.FontSpec.IsBold = false;
            myPane.XAxis.Scale.MajorStep = 0;
            myPane.XAxis.MinorTic.Size = 0;
            myPane.XAxis.MajorTic.IsInside = false;
            myPane.XAxis.MajorTic.IsOutside = true;
            myPane.XAxis.Scale.IsVisible = false;
            myPane.Legend.IsVisible = false;

            myPane.YAxis.Title.FontSpec.Size = 20;
            myPane.YAxis.Title.FontSpec.IsBold = false;

            myPane.YAxis.Scale.MajorStep = 1;
            myPane.YAxis.MinorTic.Size = 0;

            myPane.Title.Text = "Total Efficiency";
            myPane.YAxis.Title.Text = "Amount (%)";
            //myPane.XAxis.Title.Text = "Hours";
            myPane.BarSettings.Type = BarType.Stack;            
            myPane.BarSettings.ClusterScaleWidth = 60;
            zedGraphControl2.IsEnableZoom = false;
            zedGraphControl2.IsShowContextMenu = false;

            var lstEffTotals = new PointPairList();

            var value = dgvEfficiency.Rows[rowIndex].Cells[7].Value.ToString();
            int val = 0;
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Substring(0, value.Length - 1);
                int.TryParse(value, out val);
                lstEffTotals.Add(0, val);
            }
            
            myPane.YAxis.Scale.Max = val + 3;

            BarItem barItem = myPane.AddBar("Efficiency", lstEffTotals, Color.AliceBlue);

            if (val > 90)
                barItem.Color = Color.FromArgb(125, 255, 121);
            if (val >= 70 && val <= 90)
                barItem.Color = Color.FromArgb(255, 255, 70);
            if (val < 70)
                barItem.Color = Color.FromArgb(235, 105, 112);

            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));
            zedGraphControl2.AxisChange();
            zedGraphControl2.Refresh();
        }

        private void PopulateGraph(int rowIndex)
        {
            var myPane = new GraphPane();
            myPane = zedGraphControl1.GraphPane;

            myPane.CurveList.Clear();
            myPane.GraphObjList.Clear();
            myPane.Fill = new Fill();

            //legend
            myPane.Legend.Position = LegendPos.Bottom;
            myPane.Legend.FontSpec.Size = 20;
            myPane.Legend.Border.IsVisible = false;

            myPane.Title.Text = "Efficiency Per Hour";
            myPane.BarSettings.Type = BarType.Stack;

            //x axis
            myPane.XAxis.Title.Text = "Hours";
            myPane.XAxis.Title.FontSpec.Size = 20;
            myPane.XAxis.Title.FontSpec.IsBold = false;
            myPane.XAxis.Scale.MajorStep = 1;
            myPane.XAxis.MinorTic.Size = 0;
            myPane.XAxis.Scale.Min = startpC-0.5;
            myPane.XAxis.Scale.Max = endpC+0.5;
            myPane.XAxis.MajorTic.IsInside = false;
            myPane.XAxis.MajorTic.IsOutside = true;

            //y axis
            myPane.YAxis.Title.Text = "Amount (%)";
            myPane.YAxis.Title.FontSpec.Size = 20;
            myPane.YAxis.Title.FontSpec.IsBold = false;
            myPane.YAxis.Scale.MajorStep = 10;
            myPane.YAxis.MinorTic.Size = 0;
            zedGraphControl1.IsEnableZoom = false;
            zedGraphControl1.IsShowContextMenu = false;

            var lstEff = new PointPairList();
            var yAxisVals = new List<int>();

            var i = 6;
            for (var c = 9; c <= dgvEfficiency.Columns.Count - 1; c += 1)
            {
                var value = dgvEfficiency.Rows[rowIndex].Cells[c].Value.ToString();

                if(string.IsNullOrEmpty(value))
                {
                    i += 1;
                    continue;
                }

                value = value.Substring(0, value.Length - 1);
                int.TryParse(value, out var val);

                int zAxisValue = 0;

                if (val > 90)
                    zAxisValue = 200;
                if (val <= 90 && val >= 70)
                    zAxisValue = 80;
                if (val < 70)
                    zAxisValue = -150;

                yAxisVals.Add(val);
                lstEff.Add(i, val, zAxisValue);

                i += 1;
            }

            if (yAxisVals.Count <= 0)
                myPane.YAxis.Scale.Max = 3;
            else
            myPane.YAxis.Scale.Max = yAxisVals.Max() + 35;

            BarItem barItem = myPane.AddBar(string.Empty, lstEff, Color.AliceBlue);
          
            Color []colors = new Color[] { Color.FromArgb(235, 105, 112),
                                           Color.FromArgb(255, 255, 70),
                                           Color.FromArgb(125, 255, 121) };
            
            barItem.Bar.Fill = new Fill(colors);
            barItem.Bar.Fill.RangeMax = 90;
            barItem.Bar.Fill.RangeMin = 70;
            barItem.Bar.Fill.Type = FillType.GradientByZ;
            //barItem.Bar.Fill.SecondaryValueGradientColor = Color.Empty;
            foreach (var bar in lstEff)
            {
                string key = dgvEfficiency.Rows[rowIndex].Cells[3].Value.ToString() + "_" +
                             dgvEfficiency.Rows[rowIndex].Cells[4].Value.ToString();

                if (string.IsNullOrEmpty(key) || !_totalPieces.ContainsKey(key) || bar.Y == 0)
                    continue;

                var numOfPieces = _totalPieces[key];
                TextObj barLabel = new TextObj(numOfPieces.ToString(), bar.X, bar.Y + 10);
                barLabel.FontSpec.Size = 20;
                barLabel.FontSpec.Border.IsVisible = false;
                myPane.GraphObjList.Add(barLabel);
            }

            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        int rowIdx;        
        private void dgvEfficiency_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEfficiency.SelectedRows.Count <= 0 || dgvEfficiency.DataSource == null)
                return;

            rowIdx = dgvEfficiency.CurrentCell.RowIndex;
            PopulateGraph(rowIdx);
            PopulateGraphTotals(rowIdx);
        }

        private void ExportEfficiency_To_PDF()
        {
            System.Drawing.Bitmap screenshot_bmp = PrintScreen();
            iTextSharp.text.Image screenshot_pdf = iTextSharp.text.Image.GetInstance(screenshot_bmp,
                                               System.Drawing.Imaging.ImageFormat.Bmp);

            var save_file_dialog = new SaveFileDialog();
            save_file_dialog.FileName = "Efficiency " + DateTime.Now.Date.ToString("dd'-'MM'-'yyyy");
            save_file_dialog.DefaultExt = ".pdf";
            save_file_dialog.Filter = "PDF(*.pdf)|*.pdf";
            if (save_file_dialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(save_file_dialog.FileName, FileMode.Create))
                {
                    Document doc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(doc, fs);

                    float maxWidth = doc.PageSize.Width - doc.LeftMargin - doc.RightMargin;
                    float maxHeight = doc.PageSize.Height - doc.TopMargin - doc.BottomMargin;

                    if (screenshot_pdf.Height > maxHeight || screenshot_pdf.Width > maxWidth)
                        screenshot_pdf.ScaleToFit(maxWidth, maxHeight);

                    doc.Open();
                    doc.Add(screenshot_pdf);
                    doc.Close();
                    fs.Close();
                }
                save_file_dialog.Dispose();
            }
        }
        private System.Drawing.Bitmap PrintScreen()
        {
            Graphics g = this.CreateGraphics();
            var screenshot_bmp = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height, g);
            g = Graphics.FromImage(screenshot_bmp);
            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.WorkingArea.Size);

            return screenshot_bmp;
        }

        private void pbPdf_Click(object sender, EventArgs e)
        {
            ExportEfficiency_To_PDF();
        }

        private void pbPdf_MouseEnter(object sender, EventArgs e)
        {
            ControlPaint.DrawBorder(pbPdf.CreateGraphics(), pbPdf.ClientRectangle, Color.Orange, ButtonBorderStyle.Inset);
        }

        private void pbPdf_MouseLeave(object sender, EventArgs e)
        {
            pbPdf.Invalidate();
        }

        private void pbExcel_MouseEnter(object sender, EventArgs e)
        {
            ControlPaint.DrawBorder(pbExcel.CreateGraphics(), pbExcel.ClientRectangle, Color.Orange, ButtonBorderStyle.Inset);
        }

        private void pbExcel_MouseLeave(object sender, EventArgs e)
        {
            pbExcel.Invalidate();
        }

        private void ExportToExcel(DataTable dt)
        {
            Microsoft.Office.Interop.Excel.Application oexcel = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                object misValue = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Excel.Workbook obook = oexcel.Workbooks.Add(misValue);
                Microsoft.Office.Interop.Excel.Worksheet osheet = new Microsoft.Office.Interop.Excel.Worksheet();
                osheet = (Microsoft.Office.Interop.Excel.Worksheet)obook.Sheets["Sheet1"];
                int columnsCount;

                if (dt == null || (columnsCount = dt.Columns.Count) == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                object[] Header = new object[columnsCount];

                for (int i = 0; i < 7; i++)
                    Header[i] = dt.Columns[i].ColumnName;

                Header[6] = string.Empty;
                Header[7] = "Total  Work Time";
                Header[8] = string.Empty;                

                int hours = 6;
                for (var i = 6; i <= columnsCount - 1; i ++)
                {
                    Header[i] = hours + "h";
                    hours++;
                }                

                Microsoft.Office.Interop.Excel.Range HeaderRange = osheet.get_Range((Microsoft.Office.Interop.Excel.Range)
                                                                   (osheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)
                                                                   (osheet.Cells[1, columnsCount - 2]));
                HeaderRange.Value = Header;
                HeaderRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                HeaderRange.Font.Bold = true;

                int rowsCount = dt.Rows.Count;
                object[,] Cells = new object[rowsCount, columnsCount];

                for (int j = 0; j < rowsCount; j++)
                    for (int i = 0; i < columnsCount - 2; i++)
                        Cells[j, i] = dt.Rows[j][i];

                osheet.get_Range((Microsoft.Office.Interop.Excel.Range)(osheet.Cells[2, 1]),
                                (Microsoft.Office.Interop.Excel.Range)
                                (osheet.Cells[rowsCount + 1, columnsCount])).Value = Cells;
                oexcel.Columns.AutoFit();

                string filePath = string.Empty;

                var save_file_dialog = new SaveFileDialog();
                save_file_dialog.FileName = "Efficiency " + DateTime.Now.Date.ToString("dd'-'MM'-'yyyy");
                save_file_dialog.DefaultExt = ".xlsx";
                save_file_dialog.Filter = "Excel File|*.xlsx|All Files|*.*";

                if (save_file_dialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = save_file_dialog.FileName;
                    oexcel.ActiveWorkbook.SaveCopyAs(filePath);
                    oexcel.ActiveWorkbook.Saved = true;
                    oexcel.Quit();
                }
                save_file_dialog.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }
        private void Hidecollumns()
        {
            // textBOre.Text = txtchg;
            Showcollumns();
            //int x = int.Parse(textBOre.Text.ToString()) * 3 - 3;
            int x = Convert.ToInt32(endpC)+4;
            int y = 13;
            int swx = Convert.ToInt32(startpC);
            switch (swx)
            {
                case 6:
                    y = 14;
                    break;
                case 7:
                    y = 13;
                    break;
                case 8:
                    y = 12;
                    break;
            }
            for (int hideh = 9; hideh < dgvEfficiency.Columns.Count - y; hideh++)
            {
                dgvEfficiency.Columns[hideh].Visible = false;
            }
            for (int hideh =x ; hideh < dgvEfficiency.Columns.Count ; hideh++)
            {
                dgvEfficiency.Columns[hideh].Visible = false;
            }

        }
        //public static void CallHC()
        //{
        //    Intervals f = new Intervals();
        //    f.Hidecollumns();
        //}
        //public static void CallSC()
        //{
        //    Intervals f = new Intervals();
        //    f.Showcollumns();
        //}
        private void Showcollumns()
        {
            for (int i = 0; i < dgvEfficiency.Columns.Count - 2; i++)
            {
                dgvEfficiency.Columns[i].Visible = true;
            }
        }

        private void pbExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel(_finalTable);
        }
    }

    }

