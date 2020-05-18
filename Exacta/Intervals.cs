using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace Exacta
{
    public partial class Intervals : Form
    {
        private DataTable _finalTable = new DataTable();
        private Dictionary<string, int> _totalPieces = new Dictionary<string, int>();
        private string _machineDescrtiption;
        private string _machineLine;
        // private string _machineOperator;
        private bool bx = false;
        public int SingleSelectedMachine { get; set; }
        public string selectedRow = string.Empty;

        public Intervals()
        {
            InitializeComponent();
            PauseTable();

        }
        public class interval
        {
            public DateTime Istartp { get; set; }
            public DateTime Iendp { get; set; }
        }

        public void Intervals_Load(object sender, EventArgs e)
        {
            readf();
            if (SingleSelectedMachine <= 0) SingleSelectedMachine = -1;

            dgvIntervals.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn c in dgvIntervals.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvIntervals.RowHeadersVisible = false;
                }
                dgvIntervals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            };
            dgvIntervals.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvIntervals.AllowUserToAddRows = false;
            dgvIntervals.AllowUserToDeleteRows = false;
            dgvIntervals.AllowUserToResizeColumns = false;
            dgvIntervals.AllowUserToResizeRows = false;
            dgvIntervals.AllowUserToOrderColumns = false;
            dgvIntervals.ReadOnly = true;
            dgvIntervals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIntervals.MultiSelect = false;

            dgvIntervals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvIntervals.ColumnHeadersHeight = this.dgvIntervals.ColumnHeadersHeight * 2 + 20;
            dgvIntervals.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            dgvIntervals.CellPainting += new DataGridViewCellPaintingEventHandler(dgvIntervals_CellPainting);
            dgvIntervals.Paint += new PaintEventHandler(dgvIntervals_Paint);
            dgvIntervals.Scroll += new ScrollEventHandler(dgvIntervals_Scroll);
            dgvIntervals.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgvIntervals_ColumnWidthChanged);
            dgvIntervals.SelectionChanged += dgvIntervals_SelectedIndexChange;

            addDataTableColumns();
            dgvIntervals.DoubleBuffered(true);

            btnCuts.BackColor = Color.AliceBlue;
            btnKnitt.BackColor = Color.FromArgb(145, 255, 145);
            btnPrep.BackColor = Color.FromArgb(255, 255, 111);
            btnStop.BackColor = Color.FromArgb(243, 139, 145);
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
                //MessageBox.Show(e.ToString());
            }
        }
        public DateTime startp=new DateTime();
        public DateTime endp =new DateTime();
        public static TimeSpan pauseT;

        public static List<interval> items = new List<interval>();
        public void PauseTable()
        {           pauseT = TimeSpan.Zero;
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
            _finalTable.Columns.Add("Order");
            _finalTable.Columns.Add("Pieces/Hour");
            _finalTable.Columns.Add("Pieces/Shift"); //6
            _finalTable.Columns.Add("Phase");
            _finalTable.Columns.Add("Operator");
            _finalTable.Columns.Add("sep1"); //9
            _finalTable.Columns.Add("Total Work Time"); //10
            _finalTable.Columns.Add("sep2"); //11
            //total
            _finalTable.Columns.Add("Knitt_t");
            _finalTable.Columns.Add("Preparation_t");
            _finalTable.Columns.Add("Stop_t");
            _finalTable.Columns.Add("Cuts_t");
            //before hours
            _finalTable.Columns.Add("sep3"); //16

            for (var i = 17; i < 31; i++)
            {
                _finalTable.Columns.Add("Knitt_" + i.ToString());
                _finalTable.Columns.Add("Preparation_" + i.ToString());
                _finalTable.Columns.Add("Stop_" + i.ToString());
                _finalTable.Columns.Add("Cuts_" + i.ToString());
            }
            _finalTable.Columns.Add("AvgQtyTime");
            _finalTable.Columns.Add("AvgQty");

            GetData();
            dgvIntervals.DataSource = _finalTable;

            dgvIntervals.Columns[dgvIntervals.Columns.Count - 1].Visible = false;
            dgvIntervals.Columns[dgvIntervals.Columns.Count - 2].Visible = false;


            for (var i = 9; i <= dgvIntervals.Columns.Count - 1; i++)
            {
                if (i == 16 || i == 11 || i == 9)
                {
                    dgvIntervals.Columns[i].Width = 6;
                    dgvIntervals.Columns[i].HeaderText = "";
                    continue;
                }
                dgvIntervals.Columns[i].Width = 50;
            }
            for (var i = 12; i <= dgvIntervals.Columns.Count - 1; i += 4)
            {
                if (i == 16)
                    i += 1;
                dgvIntervals.Columns[i].HeaderText = "Knitt Time";
            }
            for (var i = 13; i <= dgvIntervals.Columns.Count - 1; i += 4)
            {
                if (i == 17)
                    i += 1;
                dgvIntervals.Columns[i].HeaderText = "Prep Time";
            }
            for (var i = 14; i <= dgvIntervals.Columns.Count - 1; i += 4)
            {
                if (i == 18)
                    i += 1;
                dgvIntervals.Columns[i].HeaderText = "Stop Time";
            }
            for (var i = 15; i <= dgvIntervals.Columns.Count - 1; i += 4)
            {
                if (i == 19)
                    i += 1;
                dgvIntervals.Columns[i].HeaderText = "Qty";
            }

            foreach (DataGridViewColumn column in dgvIntervals.Columns)
            {
                if ((column.HeaderText).Equals("Knitt Time"))
                    column.DefaultCellStyle.BackColor = Color.FromArgb(145, 255, 145);

                if ((column.HeaderText).Equals("Prep Time"))
                    column.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 111);

                if ((column.HeaderText).Equals("Qty"))
                    column.DefaultCellStyle.BackColor = Color.AliceBlue;

                if ((column.HeaderText).Equals("Stop Time"))
                    column.DefaultCellStyle.BackColor = Color.FromArgb(243, 139, 145);
            }

            for (int i = 0; i <= 11; i++)
            {
                dgvIntervals.Columns[i].DefaultCellStyle.BackColor = SystemColors.Control;

                dgvIntervals.Columns[i].Frozen = true;
                dgvIntervals.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            for (int i = 11; i <= 15; i++)
            {
                dgvIntervals.Columns[i].Frozen = true;
            }

            dgvIntervals.Columns["IDM"].Width = 40;
            dgvIntervals.Columns["Description"].Width = 90;
            dgvIntervals.Columns["Line"].Width = 60;
            dgvIntervals.Columns["Order"].Width = 70;
            dgvIntervals.Columns["Phase"].Width = 80;
            dgvIntervals.Columns["Pieces/Hour"].Width = 70;
            dgvIntervals.Columns["Pieces/Shift"].Width = 70;

            for (int i = 0; i <= 8; i++)
                DrawButtonInDGVHeader(i);

            var bs = new BindingSource();
            bs.DataSource = _finalTable;
            if (!(SingleSelectedMachine <= 0))
            {
                bs.Filter = string.Format("CONVERT(" + dgvIntervals.Columns[0].DataPropertyName + ", System.String) like '%" + SingleSelectedMachine.ToString().Replace("'", "''") + "%'");

                dgvIntervals.DataSource = bs;
                foreach (DataGridViewRow row in dgvIntervals.Rows)
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
        public bool pauseBett = false;
        private void GetData()
        {
            DataTable tmpTable = new DataTable();
            List<DateTime> _lstTime = new List<DateTime>();
            var newRow = tmpTable.NewRow();
            tmpTable.Columns.Add("IDM", typeof(string));
            tmpTable.Columns.Add("hourx", typeof(int));
            tmpTable.Columns.Add("ts", typeof(DateTime));
            tmpTable.Columns.Add("TIC", typeof(bool));
            tmpTable.Columns.Add("CIC", typeof(bool));
            tmpTable.Columns.Add("MVC", typeof(bool));
            tmpTable.Columns.Add("MLC", typeof(bool));
            tmpTable.Columns.Add("NAME", typeof(string));
            tmpTable.Columns.Add("Operatie", typeof(string));
            tmpTable.Columns.Add("ALF", typeof(bool));
            tmpTable.Columns.Add("ALM", typeof(bool));
            tmpTable.Columns.Add("ALS", typeof(bool));
            tmpTable.Columns.Add("PPH", typeof(double));
            tmpTable.Columns.Add("COMPONENTS", typeof(int));
            tmpTable.Columns.Add("Ordernr", typeof(int));
            tmpTable.Columns.Add("OPEARTOR", typeof(string));

            var startHour = 6;
            var nextHour = 0;
            var startMachine = "0";
            var firstRead = true;
            string startName = string.Empty;
            string startOperatie = string.Empty;
            double startPph = 0;
            int startComponents = 0;
            int startOrdernr = 0;
            var startOperator = string.Empty;

            object[] arrOfData = new object[] { };
            object[] arrOfAdditionalData = new object[] { };
            object[] additionArr = new object[] { };

            var con = new SqlConnection(Exacta.Menu.connectionString);
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "[spGetDataPerHour]";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@startDate", SqlDbType.NVarChar).Value = Exacta.Menu.dateFrom.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("@endDate", SqlDbType.NVarChar).Value = Exacta.Menu.dateFrom.ToString("MM/dd/yyyy");

            con.Open();
            var dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    newRow = tmpTable.NewRow();
                    var index = 0;

                    int.TryParse(dr[1].ToString(), out var hourx);
                    DateTime.TryParse(dr[2].ToString(), out var ts);
                    bool.TryParse(dr[3].ToString(), out var tic);
                    bool.TryParse(dr[4].ToString(), out var cic);
                    bool.TryParse(dr[5].ToString(), out var mvc);
                    bool.TryParse(dr[6].ToString(), out var mlc);
                    string name = dr[7].ToString();
                    string operatie = dr[8].ToString();
                    bool.TryParse(dr[9].ToString(), out var alf);
                    bool.TryParse(dr[10].ToString(), out var alm);
                    bool.TryParse(dr[11].ToString(), out var als);
                    double.TryParse(dr[12].ToString(), out var pph);
                    int.TryParse(dr[13].ToString(), out var comp);
                    int.TryParse(dr[14].ToString(), out var ord);
                    var operators = dr[15].ToString();
                    string idm = dr[0].ToString();
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
                            arrOfData = new object[] { startMachine, nextHour, illegalTs, 0, 0, 0, 0, startName, startOperatie, 0, 0, 0 ,
                                                       startPph, startComponents, startOrdernr, startOperator};

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
                    arrOfData = new object[] { idm, hourx, fullTs, tic, cic, mvc, mlc, name, operatie, alf, alm, als, pph, comp, ord, operators };
                    arrOfAdditionalData = new object[] { };
                    var arrOfStartPointerHour = new object[] { };
                    additionArr = new object[] { };

                    var virtTs = new DateTime(fullTs.Year, fullTs.Month, fullTs.Day, nextHour, 0, 0);
                    var lastMinute = new DateTime(fullTs.Year, fullTs.Month, fullTs.Day, nextHour, 59, 59);
                    var newHour = new DateTime(fullTs.Year, fullTs.Month, fullTs.Day, hourx, 0, 0);

                    if (startHour != hourx && ts.Minute > 0)
                    {
                        arrOfStartPointerHour = new object[] { idm, hourx, newHour, 0, 0, 0, 0, name, operatie, 0, 0, 0, pph, comp, ord, operators };
                    }

                    if (startHour != hourx && nextHour != hourx)
                    {
                        arrOfAdditionalData = new object[] { idm, nextHour, virtTs, 0, 0, 0, 0, name, operatie, 0, 0, 0, pph, comp, ord, operators };
                        additionArr = new object[] { idm, nextHour, lastMinute, 0, 0, 0, 0, name, operatie, 0, 0, 0, pph, comp, ord, operators };
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
                    startOrdernr = ord;
                    startOperator = operators;
                }
            }
            var c = tmpTable.Rows.Count;
            dr.Close();
            con.Close();
            if (tmpTable.Rows.Count <= 0)
                return;
            List<DateTime> lstStop = new List<DateTime>();
            var  minutesStop = 0.0;
            List<DateTime> lstKnitt = new List<DateTime>();
            var minutesKnitt = 0.0;
            List<DateTime> lstPrep = new List<DateTime>();
            var minutesPrep = 0.0;

            List<double> lstAvgKnitt = new List<double>();
            var hoursWorked = 0;
            List<int> macQtyPerHour = new List<int>();

            List<DateTime> lstAls = new List<DateTime>();
            var minutesAls = 0.0;
            List<DateTime> lstAlf = new List<DateTime>();
            var minutesAlf = 0.0;
            List<DateTime> lstAlm = new List<DateTime>();
            var minutesAlm = 0.0;

            var stopTot = 0.0;
            var knittTot = 0.0;
            var prepTot = 0.0;
            var cutsTot = 0;

            // double shiftHours = 12.5 - pauseT.TotalHours;
            double shiftHours = endpC - startpC - pauseT.TotalHours;

            var machineCuts = 0;

            var machine = tmpTable.Rows[0][0].ToString();
            int.TryParse(tmpTable.Rows[0][1].ToString(), out var catchHour);
            var hour = catchHour;

            newRow = _finalTable.NewRow();

            var prod = new Production();
          
            _totalPieces = new Dictionary<string, int>();
            foreach (DataRow row in tmpTable.Rows)
            {
                
                var dbMachine = row[0].ToString();

                int.TryParse(row[1].ToString(), out var dbHourx); //last readed hour
                var dbEventTime = Convert.ToDateTime(row[2]);

                //alarms for stop time
                bool.TryParse(row[3].ToString(), out var dbTic);
                bool.TryParse(row[4].ToString(), out var dbCic);
                bool.TryParse(row[5].ToString(), out var dbMvc);
                bool.TryParse(row[6].ToString(), out var dbMlc);
                var article = row[7].ToString();
                var phase = row[8].ToString();
                bool.TryParse(row[9].ToString(), out var dbAlf);
                bool.TryParse(row[10].ToString(), out var dbAlm);
                bool.TryParse(row[11].ToString(), out var dbAls);
                double.TryParse(row[12].ToString(), out var piecesPerHour);
                int.TryParse(row[13].ToString(), out var components);
                int.TryParse(row[14].ToString(), out var ord);
                var operators = row[15].ToString();

                if (!_totalPieces.ContainsKey(article + "_" + phase) &&
                   !string.IsNullOrEmpty(phase) && piecesPerHour != 0)
                    _totalPieces.Add(article + "_" + phase, components * Convert.ToInt32(piecesPerHour));

                if (machine != dbMachine)
                {

                    if (lstStop.Count > 0)
                    { 
                        var curTime = dbEventTime;
                        var stop = lstStop.First();
                        minutesStop += curTime.Subtract(stop).TotalSeconds;
                        lstStop.Clear();
                    }
                    if (lstPrep.Count > 0)
                    {
                        var curTime = dbEventTime;
                        var stop = lstPrep.First();
                        minutesPrep += curTime.Subtract(stop).TotalSeconds;
                        lstPrep.Clear();
                    }
                    if (lstKnitt.Count > 0)
                    {
                        var curTime = dbEventTime;
                        var stop = lstKnitt.First();
                        minutesKnitt += curTime.Subtract(stop).TotalSeconds;
                        lstKnitt.Clear();
                    }

                    string str = GetTimeFromSeconds(knittTot + prepTot + stopTot).Replace(':', '.');
                    double.TryParse(cutsTot.ToString(), out var qtyTot);
                    double.TryParse(str, out var knittTimeTot);

                    newRow[_finalTable.Columns.Count - 1] = Convert.ToInt32((Math.Round(qtyTot / shiftHours, 2))).ToString();
                    if (lstAvgKnitt.Count >= 1)
                        newRow[_finalTable.Columns.Count - 2] = GetTimeFromSeconds(lstAvgKnitt.Average());

                    _finalTable.Rows.Add(newRow);
                    newRow = _finalTable.NewRow();

                    stopTot = 0.0;
                    knittTot = 0.0;
                    prepTot = 0.0;
                    cutsTot = 0;
                    hoursWorked = 0;

                    minutesStop = 0.0;
                    minutesKnitt = 0.0;
                    machineCuts = 0;
                    minutesPrep = 0.0;
                    minutesAlf = 0.0;
                    minutesAls = 0.0;
                    minutesAlm = 0.0;

                    macQtyPerHour.Clear();
                    lstAvgKnitt.Clear();

                    lstStop.Clear();
                    lstKnitt.Clear();
                    lstPrep.Clear();
                    lstAlf.Clear();
                    lstAls.Clear();
                    lstAlm.Clear();
                }
               
                if (hour != dbHourx)
                {
                    if (lstStop.Count > 0)
                    {
                        var curTime = dbEventTime;
                        var stop = lstStop.First();
                        minutesStop += curTime.Subtract(stop).TotalSeconds;
                        lstStop.Clear();
                    }
                    if (lstPrep.Count > 0)
                    {
                        var curTime = dbEventTime;
                        var stop = lstPrep.First();
                        minutesPrep += curTime.Subtract(stop).TotalSeconds;
                        lstPrep.Clear();
                    }
                    if (lstKnitt.Count > 0)
                    {
                        var curTime = dbEventTime;
                        var stop = lstKnitt.First();
                        minutesKnitt += curTime.Subtract(stop).TotalSeconds;
                        lstKnitt.Clear();
                    }

                    GetMachineProperties(dbMachine);
                    newRow[0] = dbMachine;
                    newRow[1] = _machineDescrtiption;
                    newRow[2] = _machineLine;
                    newRow[3] = article;
                    newRow[4] = ord;
                    newRow[5] = piecesPerHour * components;
                    newRow[6] = Math.Round(piecesPerHour * components * shiftHours);
                    newRow[7] = phase;
                    newRow[8] = operators;

                    var ix = 0;
                    for (var h = 6; h < 20; h++)
                    {
                        if (hour == h)
                        {
                            hoursWorked++;
                            newRow[17 + ix] = GetTimeFromSeconds(minutesKnitt).ToString();
                            newRow[18 + ix] = GetTimeFromSeconds(minutesPrep).ToString();
                            newRow[19 + ix] = GetTimeFromSeconds(minutesStop + minutesAls + minutesAlm + minutesAlf).ToString();
                            newRow[20 + ix] = machineCuts.ToString();

                            macQtyPerHour.Add(machineCuts);
                            lstAvgKnitt.Add(minutesKnitt);

                            stopTot += (minutesStop + minutesAls + minutesAlm + minutesAlf);
                            knittTot += minutesKnitt;
                            prepTot += minutesPrep;
                            cutsTot += machineCuts;
                        }
                        ix += 4;
                    }

                    newRow[10] = GetTimeFromSeconds(knittTot + prepTot + stopTot).ToString();
                    newRow[12] = GetTimeFromSeconds(knittTot).ToString();
                    newRow[13] = GetTimeFromSeconds(prepTot).ToString();
                    newRow[14] = GetTimeFromSeconds(stopTot).ToString();
                    newRow[15] = cutsTot.ToString();

                    minutesStop = 0.0;
                    minutesKnitt = 0.0;
                    machineCuts = 0;
                    minutesPrep = 0.0;
                    minutesAlf = 0.0;
                    minutesAls = 0.0;
                    minutesAlm = 0.0;

                    lstStop.Clear();
                    lstKnitt.Clear();
                    lstPrep.Clear();
                    lstAlf.Clear();
                    lstAls.Clear();
                    lstAlm.Clear();
                }

                //knitt time
                if (dbCic)
                {
                    lstKnitt.Add(dbEventTime);
                }
                else
                {
                    if (lstKnitt.Count > 0)
                    {
                        var curTime = dbEventTime;
                        var stop = lstKnitt.First();
                        minutesKnitt += curTime.Subtract(stop).TotalSeconds;
                        lstKnitt.Clear();
                    }
                }

                //prep time
                if (!dbTic && !dbCic && dbMvc || dbMlc)
                {
                    lstPrep.Add(dbEventTime);
                }
                else
                {
                    if (lstPrep.Count > 0)
                    {
                        var curTime = dbEventTime;
                        var stop = lstPrep.First();
                        minutesPrep += curTime.Subtract(stop).TotalSeconds;
                        lstPrep.Clear();
                    }
                }
                //stoptime
                if (pauseT.TotalMinutes != 0)
                {
                    int count = items.Count;
                    switch (count)
                    {
                        case 1:
                            {
                                if (!dbCic && !dbTic && !dbMvc && !dbMlc)
                                {
                                    int i = 0;
                                    if (dbEventTime.TimeOfDay < items[i].Istartp.TimeOfDay || dbEventTime.TimeOfDay > items[i].Iendp.TimeOfDay)
                                    {
                                            lstStop.Add(dbEventTime);
                                    }
                                }
                                else
                                {
                                    if (lstStop.Count > 0)
                                    { 
                                        var curTime = dbEventTime;
                                        var stop = lstStop.First();
                                        minutesStop += curTime.Subtract(stop).TotalSeconds;
                                        lstStop.Clear();
                                    }
                                }
                            }
                            break;
                        case 2:
                            {
                                if (!dbCic && !dbTic && !dbMvc && !dbMlc)
                                {
                                    int i = 0;
                                    if (dbEventTime.TimeOfDay < items[i].Istartp.TimeOfDay || dbEventTime.TimeOfDay > items[i].Iendp.TimeOfDay &&
                                 dbEventTime.TimeOfDay < items[i + 1].Istartp.TimeOfDay || dbEventTime.TimeOfDay > items[i + 1].Iendp.TimeOfDay)
                                    {
                                        lstStop.Add(dbEventTime);
                                    }
                                }
                                else
                                {
                                    if (lstStop.Count > 0)
                                    {
                                        var curTime = dbEventTime;
                                        var stop = lstStop.First();
                                        minutesStop += curTime.Subtract(stop).TotalSeconds;
                                        lstStop.Clear();
                                    }
                                }
                            }
                            break;
                        case 3:
                            {
                                if (!dbCic && !dbTic && !dbMvc && !dbMlc)
                                {
                                    int i = 0;
                                    if (dbEventTime.TimeOfDay < items[i].Istartp.TimeOfDay || dbEventTime.TimeOfDay > items[i].Iendp.TimeOfDay &&
                                 dbEventTime.TimeOfDay < items[i + 1].Istartp.TimeOfDay || dbEventTime.TimeOfDay > items[i + 1].Iendp.TimeOfDay &&
                                  dbEventTime.TimeOfDay < items[i + 2].Istartp.TimeOfDay || dbEventTime.TimeOfDay > items[i + 2].Iendp.TimeOfDay)
                                    {
                                        lstStop.Add(dbEventTime);
                                    }
                                }
                                else
                                {
                                    if (lstStop.Count > 0)
                                    {
                                        var curTime = dbEventTime;
                                        var stop = lstStop.First();
                                        minutesStop += curTime.Subtract(stop).TotalSeconds;
                                        lstStop.Clear();
                                    }
                                }
                            }
                            break;
                    }
                }
                else
                {
                    if (!dbCic && !dbTic && !dbMvc && !dbMlc)
                    {
                            lstStop.Add(dbEventTime);
                    }
                    else
                    {
                        if (lstStop.Count > 0)
                        {
                            var curTime = dbEventTime;
                            var stop = lstStop.First();
                            minutesStop += curTime.Subtract(stop).TotalSeconds;
                            lstStop.Clear();
                        }
                    }
                }

                //cuts
                if (dbTic)
                    machineCuts++;

                if (dbAls)
                {
                    lstAls.Add(dbEventTime);
                }
                else
                {
                    if (lstAls.Count > 0)
                    {
                        var curAlsTime = dbEventTime;
                        minutesAls += curAlsTime.Subtract(lstAls.First()).TotalSeconds;
                        lstAls.Clear();
                    }
                }

                if (dbAlm)
                {
                    lstAlm.Add(dbEventTime);
                }
                else
                {
                    if (lstAlm.Count > 0)
                    {
                        var curAlmTime = dbEventTime;
                        minutesAlm += curAlmTime.Subtract(lstAlm.First()).TotalSeconds;
                        lstAlm.Clear();
                    }
                }

                if (dbAlf)
                {
                    lstAlf.Add(dbEventTime);
                }
                else
                {
                    if (lstAlf.Count > 0)
                    {
                        var curAlfTime = dbEventTime;
                        minutesAlf += curAlfTime.Subtract(lstAlf.First()).TotalSeconds;
                        lstAlf.Clear();
                    }
                }
                machine = dbMachine;
                hour = dbHourx;
            }
            _finalTable.Rows.Add(newRow);
        }
        private string GetTimeFromSeconds(double seconds)
        {
            var str = "";

            var ts = TimeSpan.FromSeconds(seconds);
            var h = ts.Hours;
            var m = ts.Minutes;

            return str = ts.ToString(@"hh\:mm");
        }

        int rowIdx;
        private void dgvIntervals_SelectedIndexChange(object sender, EventArgs e)
        {
            if (dgvIntervals.SelectedRows.Count <= 0)
                return;

            rowIdx = dgvIntervals.CurrentCell.RowIndex;
            UpdateGraph(rowIdx, state);
            UpdateGraphTotals(rowIdx, state);

            selectedDgvRow = dgvIntervals.Rows[rowIdx];
        }

        private string state = "all";
        private void UpdateGraphTotals(int rowIndex, string s)
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

            myPane.YAxis.Scale.MajorStep = 100;
            myPane.YAxis.MinorTic.Size = 0;

            myPane.Title.Text = "Total Intervals";
            myPane.YAxis.Title.Text = "Minutes";

            myPane.BarSettings.Type = BarType.Stack;
            myPane.YAxis.Scale.Max = 540;
            myPane.BarSettings.ClusterScaleWidth = 60;
            zedGraphControl2.IsEnableZoom = false;
            zedGraphControl2.IsShowContextMenu = false;

            var lstStops = new PointPairList();
            var lstKnitt = new PointPairList();
            var lstPrep = new PointPairList();
            var lstQty = new PointPairList();

            lstKnitt.Add(0, GetMinutesByTimeFormat(dgvIntervals.Rows[rowIndex].Cells[12].Value.ToString()));
            lstPrep.Add(0, GetMinutesByTimeFormat(dgvIntervals.Rows[rowIndex].Cells[13].Value.ToString()));
            lstStops.Add(0, GetMinutesByTimeFormat(dgvIntervals.Rows[rowIndex].Cells[14].Value.ToString()));
            lstQty.Add(0, Convert.ToInt32(dgvIntervals.Rows[rowIndex].Cells[15].Value.ToString()));

            if (s.Equals("all") || s.Equals("Qty"))
            {
                //double shiftHours = 12.5 -  pauseT.TotalHours;
                double shiftHours = endpC -startpC- pauseT.TotalHours;
                var piecesBorder = new PointPairList();

                double y = 0;
                var art_phase = dgvIntervals.SelectedRows[0].Cells[3].Value.ToString()
                                 + "_" + dgvIntervals.SelectedRows[0].Cells[7].Value.ToString();

                if (_totalPieces.ContainsKey(art_phase))
                {
                    y = Convert.ToDouble(_totalPieces[art_phase] * shiftHours);

                    piecesBorder.Add(myPane.XAxis.Scale.Min, y);
                    piecesBorder.Add(myPane.XAxis.Scale.Max, y);

                    myPane.AddCurve("", piecesBorder, Color.Red, SymbolType.None);
                }
            }
            if (s.Equals("all"))
            {
                myPane.AddBar("Knitt Time", lstKnitt, Color.FromArgb(145, 255, 145)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(145, 255, 145), Color.FromArgb(145, 255, 145));
                myPane.AddBar("Preparation Time", lstPrep, Color.FromArgb(255, 255, 111)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(255, 255, 111), Color.FromArgb(255, 255, 111));
                myPane.AddBar("Stop Time", lstStops, Color.FromArgb(243, 139, 145)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(243, 139, 145), Color.FromArgb(243, 139, 145));
            }
            if (s.Equals("Knitt"))
            {
                myPane.AddBar("Knitt Time", lstKnitt, Color.FromArgb(145, 255, 145)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(145, 255, 145), Color.FromArgb(145, 255, 145));
            }
            if (s.Equals("Prep"))
            {
                myPane.AddBar("Preparation Time", lstPrep, Color.FromArgb(255, 255, 111)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(255, 255, 111), Color.FromArgb(255, 255, 111));
            }
            if (s.Equals("Stop"))
            {
                myPane.AddBar("Stop Time", lstStops, Color.FromArgb(243, 139, 145)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(243, 139, 145), Color.FromArgb(243, 139, 145));
            }
            if (s.Equals("Qty"))
            {
                myPane.YAxis.Title.Text = "Quantity";
                //double shiftHours = 13 - pauseT.TotalHours;
               double shiftHours =endpC-startpC - pauseT.TotalHours;

                //var piecesBorder = new PointPairList();

                //double y = 0;
                var art_phase = dgvIntervals.SelectedRows[0].Cells[3].Value.ToString()
                                 + "_" + dgvIntervals.SelectedRows[0].Cells[7].Value.ToString();

                //if (_totalPieces.ContainsKey(art_phase))
                //{
                //    y = Convert.ToDouble(_totalPieces[art_phase] * shiftHours);

                //    piecesBorder.Add(myPane.XAxis.Scale.Min, y);
                //    piecesBorder.Add(myPane.XAxis.Scale.Max, y);

                //    myPane.AddCurve("", piecesBorder, Color.Red, SymbolType.None);
                //}

                BarItem barItem = myPane.AddBar("Qty", lstQty, Color.AliceBlue);

                if (_totalPieces.ContainsKey(art_phase))
                {
                    var expected_shift_qty = Convert.ToDecimal(_totalPieces[art_phase] * shiftHours);
                    var total_shift_qty = Convert.ToDecimal(dgvIntervals.Rows[rowIndex].Cells[15].Value.ToString());

                    var total_eff = Convert.ToInt32(Math.Round((total_shift_qty / expected_shift_qty), 2) * 100);

                    if (total_eff > 90)
                        barItem.Color = Color.FromArgb(125, 255, 121);
                    if (total_eff >= 70 && total_eff <= 90)
                        barItem.Color = Color.FromArgb(255, 255, 70);
                    if (total_eff < 70)
                        barItem.Color = Color.FromArgb(235, 105, 112);
                }
            }

            foreach (var bar in lstStops)
            {
                var numOfCutsPerHour = dgvIntervals.Rows[rowIndex].Cells[15].Value;
                TextObj barLabel = new TextObj(numOfCutsPerHour.ToString(), bar.X, 495);
                barLabel.FontSpec.Size = 20;
                barLabel.FontSpec.Border.IsVisible = false;
                myPane.GraphObjList.Add(barLabel);
            }

            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));
            zedGraphControl2.AxisChange();
            zedGraphControl2.Refresh();
        }

        private void UpdateGraph(int rowIndex, string s)
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

            myPane.Title.Text = "Intervals Per Hour";
            myPane.BarSettings.Type = BarType.Stack;

            //x axis
            myPane.XAxis.Title.Text = "Hours";
            myPane.XAxis.Title.FontSpec.Size = 20;
            myPane.XAxis.Title.FontSpec.IsBold = false;
            myPane.XAxis.Scale.MajorStep = 1;
            myPane.XAxis.MinorTic.Size = 0;
            myPane.XAxis.Scale.Min = startpC-1;
            myPane.XAxis.Scale.Max = endpC+1;
            myPane.XAxis.MajorTic.IsInside = false;
            myPane.XAxis.MajorTic.IsOutside = true;

            //y axis
            myPane.YAxis.Title.Text = "Minutes";
            myPane.YAxis.Title.FontSpec.Size = 20;
            myPane.YAxis.Title.FontSpec.IsBold = false;
            myPane.YAxis.Scale.Max = 70;
            myPane.YAxis.Scale.MajorStep = 10;
            myPane.YAxis.MinorTic.Size = 0;
            zedGraphControl1.IsEnableZoom = false;
            zedGraphControl1.IsShowContextMenu = false;

            var lstStops = new PointPairList();
            var lstKnitt = new PointPairList();
            var lstPrep = new PointPairList();
            var lstQty = new PointPairList();

            var i = 6;
            for (var c = 20; c <= dgvIntervals.Columns.Count - 5; c += 4)
            {
                lstKnitt.Add(i, GetMinutesByTimeFormat(dgvIntervals.Rows[rowIndex].Cells[c].Value.ToString()));
                i += 1;
            }

            i = 6;
            for (var c = 21; c <= dgvIntervals.Columns.Count - 5; c += 4)
            {
                lstPrep.Add(i, GetMinutesByTimeFormat(dgvIntervals.Rows[rowIndex].Cells[c].Value.ToString()));
                i += 1;
            }

            i = 6;
            for (var c = 22; c <= dgvIntervals.Columns.Count - 5; c += 4)
            {
                lstStops.Add(i, GetMinutesByTimeFormat(dgvIntervals.Rows[rowIndex].Cells[c].Value.ToString()));
                i += 1;
            }

            i = 6;
            for (var c = 23; c <= dgvIntervals.Columns.Count - 5; c += 4)
            {
                int.TryParse(dgvIntervals.Rows[rowIndex].Cells[c].Value.ToString(), out var qty);

                var art_phase = dgvIntervals.SelectedRows[0].Cells[3].Value.ToString()
                                 + "_" + dgvIntervals.SelectedRows[0].Cells[6].Value.ToString();

                int zAxisValue = 0;

                if (_totalPieces.ContainsKey(art_phase))
                {
                    var pieces = Convert.ToDecimal(_totalPieces[art_phase]);
                    var quantity = Convert.ToDecimal(qty);
                    var eff = Convert.ToInt32(Math.Round((quantity / pieces), 2) * 100);

                    if (eff > 90)
                        zAxisValue = 200;
                    if (eff <= 90 && eff >= 70)
                        zAxisValue = 80;
                    if (eff < 70)
                        zAxisValue = -150;
                }
                lstQty.Add(i, qty, zAxisValue);
                i += 1;
            }

            var piecesBorder = new PointPairList();

            double y = 0;
            var article_phase = dgvIntervals.SelectedRows[0].Cells[3].Value.ToString()
                             + "_" + dgvIntervals.SelectedRows[0].Cells[7].Value.ToString();

            if (s.Equals("all") || s.Equals("Qty"))
            {
                if (_totalPieces.ContainsKey(article_phase))
                {
                    y = Convert.ToDouble(_totalPieces[article_phase]);

                    piecesBorder.Add(myPane.XAxis.Scale.Min, y);
                    piecesBorder.Add(myPane.XAxis.Scale.Max, y);

                    myPane.AddCurve("", piecesBorder, Color.Red, SymbolType.None);

                    PointPair curve = piecesBorder.First();
                    TextObj barLabel = new TextObj("Quantity norm per hour", 5.9, curve.Y + 5);
                    barLabel.FontSpec.Size = 18;
                    barLabel.FontSpec.Border.IsVisible = false;
                    myPane.GraphObjList.Add(barLabel);
                }
            }

            if (s.Equals("all"))
            {
                myPane.AddBar("Knitt Time", lstKnitt, Color.FromArgb(145, 255, 145)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(145, 255, 145), Color.FromArgb(145, 255, 145));
                myPane.AddBar("Preparation Time", lstPrep, Color.FromArgb(255, 255, 111)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(255, 255, 111), Color.FromArgb(255, 255, 111));
                myPane.AddBar("Stop Time", lstStops, Color.FromArgb(243, 139, 145)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(243, 139, 145), Color.FromArgb(243, 139, 145));
            }
            if (s.Equals("Knitt"))
            {
                myPane.AddBar("Knitt Time", lstKnitt, Color.FromArgb(145, 255, 145)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(145, 255, 145), Color.FromArgb(145, 255, 145));
            }
            if (s.Equals("Prep"))
            {
                myPane.AddBar("Preparation Time", lstPrep, Color.FromArgb(255, 255, 111)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(255, 255, 111), Color.FromArgb(255, 255, 111));
            }
            if (s.Equals("Stop"))
            {
                myPane.AddBar("Stop Time", lstStops, Color.FromArgb(243, 139, 145)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(243, 139, 145), Color.FromArgb(243, 139, 145));
            }
            if (s.Equals("Qty"))
            {
                myPane.YAxis.Title.Text = "Quantity";

                //var piecesBorder = new PointPairList();

                //double y = 0;
                //var art_phase = dgvIntervals.SelectedRows[0].Cells[3].Value.ToString()
                //                 + "_" + dgvIntervals.SelectedRows[0].Cells[5].Value.ToString();

                //if (_totalPieces.ContainsKey(art_phase))                    
                //{
                //    y = Convert.ToDouble(_totalPieces[art_phase]);

                //    piecesBorder.Add(myPane.XAxis.Scale.Min, y);
                //    piecesBorder.Add(myPane.XAxis.Scale.Max, y);

                //    myPane.AddCurve("", piecesBorder, Color.Red, SymbolType.None);

                //    PointPair curve = piecesBorder.First();
                //    TextObj barLabel = new TextObj("Quantity norm per hour", 5.9, curve.Y + 5);
                //    barLabel.FontSpec.Size = 18;
                //    barLabel.FontSpec.Border.IsVisible = false;
                //    myPane.GraphObjList.Add(barLabel);
                //}

                BarItem barItem = myPane.AddBar("Qty", lstQty, Color.AliceBlue);

                Color[] colors = new Color[] { Color.FromArgb(235, 105, 112),
                                               Color.FromArgb(255, 255, 70),
                                               Color.FromArgb(125, 255, 121) };

                barItem.Bar.Fill = new Fill(colors);
                barItem.Bar.Fill.RangeMax = 90;
                barItem.Bar.Fill.RangeMin = 70;
                barItem.Bar.Fill.Type = FillType.GradientByZ;
            }

            var ct = 23;
            foreach (var bar in lstStops)
            {
                var numOfCutsPerHour = dgvIntervals.Rows[rowIndex].Cells[ct].Value;
                TextObj barLabel = new TextObj(numOfCutsPerHour.ToString(), bar.X, 65);
                barLabel.FontSpec.Size = 20;
                barLabel.FontSpec.Border.IsVisible = false;
                myPane.GraphObjList.Add(barLabel);
                ct += 4;
            }

            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        public int GetMinutesByTimeFormat(string format)
        {
            var mins = 0;

            if (!string.IsNullOrEmpty(format))
            {
                var ts = TimeSpan.Parse(format);
                mins = (Convert.ToInt32(ts.TotalSeconds) / 60);
            }
            return mins;
        }

        #region MegrgedCells
        private void dgvIntervals_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            System.Drawing.Rectangle rtHeader = this.dgvIntervals.DisplayRectangle;
            rtHeader.Height = this.dgvIntervals.ColumnHeadersHeight / 2;
            this.dgvIntervals.Invalidate(rtHeader);
        }

        private void dgvIntervals_Scroll(object sender, ScrollEventArgs e)
        {
            System.Drawing.Rectangle rtHeader = this.dgvIntervals.DisplayRectangle;
            rtHeader.Height = this.dgvIntervals.ColumnHeadersHeight / 2;
            this.dgvIntervals.Invalidate(rtHeader);
        }

        private int start = 17;
        private int end = 69;
        private int startTotals = 12;
        private int endTotals = 16;
        private int rectHoursWidth = 200;
        private void dgvIntervals_Paint(object sender, PaintEventArgs e)
        {
            string[] monthes = { "Total", "6h", "7h", "8h", "9h", "10h", "11h", "12h", "13h", "14h", "15h", "16h", "17h", "18h", "19h" };
            var i = 0;

            for (int j = startTotals; j < endTotals;)
            {
                System.Drawing.Rectangle r1 = this.dgvIntervals.GetCellDisplayRectangle(j, -1, true);
                int w2 = this.dgvIntervals.GetCellDisplayRectangle(j, -1, true).Width;
                r1.X += -1;
                r1.Y += 1;
                r1.Width = rectHoursWidth/*r1.Width + w2 - 2*/;
                r1.Height = r1.Height / 2 - 6;
                e.Graphics.FillRectangle(new SolidBrush(Color.White), r1);
                var p = new Pen(new SolidBrush(Color.Gainsboro), 1);
                e.Graphics.DrawRectangle(p, r1);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(monthes[i],
                this.dgvIntervals.ColumnHeadersDefaultCellStyle.Font,
                new SolidBrush(this.dgvIntervals.ColumnHeadersDefaultCellStyle.ForeColor),
                r1,
                format);
                j += 4;
                i++;
            }

            i = 1;
            for (int j = start; j <= end;)
            {
                System.Drawing.Rectangle r1 = this.dgvIntervals.GetCellDisplayRectangle(j, -1, true);
                int w2 = this.dgvIntervals.GetCellDisplayRectangle(j, -1, true).Width;
                r1.X += -1;
                r1.Y += 1;
                r1.Width = rectHoursWidth/*r1.Width + w2 - 2*/;
                r1.Height = r1.Height / 2 - 6;
                e.Graphics.FillRectangle(new SolidBrush(Color.White), r1);
                var p = new Pen(new SolidBrush(Color.Gainsboro), 1);
                e.Graphics.DrawRectangle(p, r1);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(monthes[i],
                this.dgvIntervals.ColumnHeadersDefaultCellStyle.Font,
                new SolidBrush(this.dgvIntervals.ColumnHeadersDefaultCellStyle.ForeColor),
                r1,
                format);
                j += 4;
                i++;
            }
        }

        private void dgvIntervals_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                System.Drawing.Rectangle r2 = e.CellBounds;
                r2.Y += e.CellBounds.Height / 2;
                r2.Height = e.CellBounds.Height / 2;
                e.PaintBackground(r2, true);
                e.PaintContent(r2);
                e.Handled = true;
            }
            if (e.RowIndex >= -1)
            {
                if (e.ColumnIndex == 9 || e.ColumnIndex == 16 || e.ColumnIndex == 11)
                {
                    var rect = new System.Drawing.Rectangle(e.CellBounds.X - 1, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                    e.Graphics.FillRectangle(new SolidBrush(dgvIntervals.BackgroundColor), rect);
                    e.Handled = true;
                }
            }
        }

        #endregion MergedCells       

        private void AdjustWidthComboBox_DropDown(ComboBox c)
        {
            ComboBox senderComboBox = c;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            System.Drawing.Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (string s in (senderComboBox.Items))
            {
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

        private void DrawButtonInDGVHeader(int colIndex)
        {
            if (bx)
                return;
            ComboBox headerButton = new ComboBox();
            Additional.FillTheFilter(dgvIntervals, headerButton, colIndex);
            AdjustWidthComboBox_DropDown(headerButton);
            System.Drawing.Rectangle rect = dgvIntervals.GetCellDisplayRectangle(colIndex, -1, true);
            headerButton.Size = new Size(17, 10);
            headerButton.Location = new Point(rect.X + (rect.Width - headerButton.Width) - 1, rect.Y + 44);
            dgvIntervals.Controls.Add(headerButton);

            headerButton.SelectedValueChanged += delegate
            {

                if (!string.IsNullOrEmpty(headerButton.Text))
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dgvIntervals.DataSource;
                    bs.Filter = string.Format("CONVERT(" + dgvIntervals.Columns[colIndex].DataPropertyName +
                                              ", System.String) like '%" + headerButton.Text.Replace("'", "''") + "%'");
                    dgvIntervals.DataSource = bs;
                }
                else
                {
                    if (dgvIntervals.DataSource != null)
                        dgvIntervals.DataSource = null;
                    addDataTableColumns();
                }
                bx = true;
            };
        }

        private int knittFilterClicked = 1;
        private void btnKnitt_Click(object sender, EventArgs e)
        {
            if (knittFilterClicked == 1)
            {
                btnPrep.Enabled = false;
                btnStop.Enabled = false;
                btnCuts.Enabled = false;
                for (var i = 12; i <= dgvIntervals.Columns.Count - 1; i++)
                {
                    if (!dgvIntervals.Columns[i].HeaderText.Equals("Knitt Time") && i != 16)
                        dgvIntervals.Columns[i].Visible = false;
                    else
                    {
                        if (i != 16)
                            dgvIntervals.Columns[i].Width = 50;
                        else dgvIntervals.Columns[i].Width = 6;
                    }
                }
                knittFilterClicked++;
                rectHoursWidth = 50;
                btnKnitt.BackColor = SystemColors.Control;
                btnKnitt.Image = Properties.Resources.back;
                btnKnitt.Text = "";
                state = "Knitt";
                UpdateGraph(rowIdx, state);
                UpdateGraphTotals(rowIdx, state);
            }
            else
            {
                RemoveFilters();
                rectHoursWidth = 200;
                knittFilterClicked = 1;
                btnPrep.Enabled = true;
                btnStop.Enabled = true;
                btnCuts.Enabled = true;
                btnKnitt.BackColor = Color.FromArgb(145, 255, 145);
                btnKnitt.Image = null;
                btnKnitt.Text = "Knitt";
                state = "all";
                UpdateGraph(rowIdx, state);
                UpdateGraphTotals(rowIdx, state);
            }
        }

        private int prepFilterClicked = 1;
        private void btnPrep_Click(object sender, EventArgs e)
        {
            if (prepFilterClicked == 1)
            {
                btnKnitt.Enabled = false;
                btnStop.Enabled = false;
                btnCuts.Enabled = false;
                for (var i = 12; i <= dgvIntervals.Columns.Count - 1; i++)
                {
                    if (!dgvIntervals.Columns[i].HeaderText.Equals("Prep Time") && i != 16)
                        dgvIntervals.Columns[i].Visible = false;
                    else
                    {
                        if (i != 16)
                            dgvIntervals.Columns[i].Width = 50;
                        else dgvIntervals.Columns[i].Width = 6;
                    }
                }
                start++;
                end++;
                startTotals++;
                endTotals++;
                rectHoursWidth = 50;
                dgvIntervals.Invalidate();
                prepFilterClicked++;
                btnPrep.BackColor = SystemColors.Control;
                btnPrep.Image = Properties.Resources.back;
                btnPrep.Text = "";
                state = "Prep";
                UpdateGraph(rowIdx, state);
                UpdateGraphTotals(rowIdx, state);
            }
            else
            {
                start = 17;
                end = 69;
                startTotals = 12;
                endTotals = 16;
                rectHoursWidth = 200;
                RemoveFilters();
                prepFilterClicked = 1;
                btnKnitt.Enabled = true;
                btnStop.Enabled = true;
                btnCuts.Enabled = true;
                btnPrep.BackColor = Color.FromArgb(255, 255, 111);
                btnPrep.Image = null;
                btnPrep.Text = "Prep";
                state = "all";
                UpdateGraph(rowIdx, state);
                UpdateGraphTotals(rowIdx, state);
            }
        }

        private int stopFilterClicked = 1;
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (stopFilterClicked == 1)
            {
                btnKnitt.Enabled = false;
                btnPrep.Enabled = false;
                btnCuts.Enabled = false;
                for (var i = 12; i <= dgvIntervals.Columns.Count - 1; i++)
                {
                    if (!dgvIntervals.Columns[i].HeaderText.Equals("Stop Time") && i != 16)
                        dgvIntervals.Columns[i].Visible = false;
                    else
                    {
                        if (i != 16)
                            dgvIntervals.Columns[i].Width = 50;
                        else dgvIntervals.Columns[i].Width = 6;
                    }
                }
                start = 19;
                end = 71;
                startTotals = 14;
                endTotals = 18;
                rectHoursWidth = 50;
                dgvIntervals.Invalidate();
                stopFilterClicked++;
                btnStop.BackColor = SystemColors.Control;
                btnStop.Image = Properties.Resources.back;
                btnStop.Text = "";
                state = "Stop";
                UpdateGraph(rowIdx, state);
                UpdateGraphTotals(rowIdx, state);
            }
            else
            {
                start = 17;
                end = 69;
                startTotals = 12;
                endTotals = 16;
                rectHoursWidth = 200;
                RemoveFilters();
                stopFilterClicked = 1;
                btnKnitt.Enabled = true;
                btnPrep.Enabled = true;
                btnCuts.Enabled = true;
                btnStop.BackColor = Color.FromArgb(243, 139, 145);
                btnStop.Image = null;
                btnStop.Text = "Stop";
                state = "all";
                UpdateGraph(rowIdx, state);
                UpdateGraphTotals(rowIdx, state);
            }
        }

        private int cutsFilterClicked = 1;
        private void btnCuts_Click(object sender, EventArgs e)
        {
            if (cutsFilterClicked == 1)
            {
                btnKnitt.Enabled = false;
                btnPrep.Enabled = false;
                btnStop.Enabled = false;
                for (var i = 12; i <= dgvIntervals.Columns.Count - 1; i++)
                {
                    if (!dgvIntervals.Columns[i].HeaderText.Equals("Qty") && i != 16)
                        dgvIntervals.Columns[i].Visible = false;
                    else
                    {
                        if (i != 16)
                            dgvIntervals.Columns[i].Width = 50;
                        else dgvIntervals.Columns[i].Width = 6;
                    }
                }
                start = 20;
                end = 72;
                startTotals = 15;
                endTotals = 19;
                rectHoursWidth = 50;
                dgvIntervals.Invalidate();
                cutsFilterClicked++;
                btnCuts.BackColor = SystemColors.Control;
                btnCuts.Image = Properties.Resources.back;
                btnCuts.Text = "";
                state = "Qty";
                UpdateGraph(rowIdx, state);
                UpdateGraphTotals(rowIdx, state);
            }
            else
            {
                start = 17;
                end = 69;
                startTotals = 12;
                endTotals = 16;
                rectHoursWidth = 200;
                RemoveFilters();
                cutsFilterClicked = 1;
                btnKnitt.Enabled = true;
                btnPrep.Enabled = true;
                btnStop.Enabled = true;
                btnCuts.BackColor = Color.AliceBlue;
                btnCuts.Image = null;
                btnCuts.Text = "Qty";
                state = "all";
                UpdateGraph(rowIdx, state);
                UpdateGraphTotals(rowIdx, state);
            }
        }

        private void RemoveFilters()
        {
            for (var i = 12; i <= dgvIntervals.Columns.Count - 3; i++)
            {
                if (i != 16)
                {
                    dgvIntervals.Columns[i].Width = 50;
                    dgvIntervals.Columns[i].Visible = true;
                }
                else dgvIntervals.Columns[i].Width = 6;
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

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

                for (int i = 0; i < 8; i++)
                    Header[i] = dt.Columns[i].ColumnName;

                Header[9] = "Total  Work Time";
                Header[11] = "Knitt Total";
                Header[12] = "Prep Total";
                Header[13] = "Stop Total";
                Header[14] = "Qty Total";

                int hours = 6;
                for (var i = 20; i <= columnsCount - 3; i += 4)
                {
                    Header[i] = "Knitt Time / " + hours + "h";
                    hours++;
                }
                hours = 6;
                for (var i = 21; i <= columnsCount - 3; i += 4)
                {
                    Header[i] = "Prep Time / " + hours + "h";
                    hours++;
                }
                hours = 6;
                for (var i = 22; i <= columnsCount - 3; i += 4)
                {
                    Header[i] = "Stop Time / " + hours + "h";
                    hours++;
                }
                hours = 6;
                for (var i = 23; i <= columnsCount - 3; i += 4)
                {
                    Header[i] = "Qty / " + hours + "h";
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
                save_file_dialog.FileName = "Intervals " + DateTime.Now.Date.ToString("dd'-'MM'-'yyyy");
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

        private DataGridViewRow selectedDgvRow = new DataGridViewRow();
        private void dgvIntervals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            QuantityIntervals frmQuantityIntervals = new QuantityIntervals();
            frmQuantityIntervals.LoadData(selectedDgvRow);
            frmQuantityIntervals.ShowDialog();
            frmQuantityIntervals.Dispose();
        }

        private void pbExcel_Click_1(object sender, EventArgs e)
        {
            ExportToExcel(_finalTable);
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
            ControlPaint.DrawBorder(pbExcel.CreateGraphics(), pbPdf.ClientRectangle, Color.Orange, ButtonBorderStyle.Inset);
        }

        private void pbExcel_MouseLeave(object sender, EventArgs e)
        {
            pbExcel.Invalidate();
        }

        private void ExportIntervals_To_PDF()
        {
            System.Drawing.Bitmap screenshot_bmp = PrintScreen();
            iTextSharp.text.Image screenshot_pdf = iTextSharp.text.Image.GetInstance(screenshot_bmp,
                                               System.Drawing.Imaging.ImageFormat.Bmp);

            var save_file_dialog = new SaveFileDialog();
            save_file_dialog.FileName = "Intervals " + DateTime.Now.Date.ToString("dd'-'MM'-'yyyy");
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
            ExportIntervals_To_PDF();
        }
        private void Hidecollumns()
        {
            // textBOre.Text = txtchg;
            Showcollumns();
            //int x = int.Parse(textBOre.Text.ToString()) * 3 - 3;
            int x = Convert.ToInt32(endpC) * 4 - 3;
            int y=58;
            int swx = Convert.ToInt32(startpC);
            switch (swx)
            {
                case 6:
                    y = 58;
                    break;
                case 7:
                    y = 54;
                    break;
                case 8:
                    y = 50;
                    break;
            }
                    for (int hideh = 17; hideh < dgvIntervals.Columns.Count - y; hideh++)
                    {
                        dgvIntervals.Columns[hideh].Visible = false;
                    }
                    for (int hideh = x; hideh < dgvIntervals.Columns.Count - 2; hideh++)
                    {
                        dgvIntervals.Columns[hideh].Visible = false;
                    }
            
        }
        public static void CallHC()
        {
            Intervals f = new Intervals();
            f.Hidecollumns();
        }
        public static void CallSC()
        {
            Intervals f = new Intervals();
            f.Showcollumns();
        }
        private void Showcollumns()
        {
            for (int i = 0; i < dgvIntervals.Columns.Count - 2; i++)
            {
                dgvIntervals.Columns[i].Visible = true;
            }
        }

    }
}