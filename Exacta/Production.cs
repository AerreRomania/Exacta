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
using Exacta.DatabaseTableClasses;
using System.Data.Linq;
using ZedGraph;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;

namespace Exacta
{
    public partial class Production : Form
    {
        private const string _PREVIEW_ = "preview";
        private const string _ALL_RECS_ = "allrecs";
        private const string _OF_ARTICLE_ = "ofarticle";
        private string _option;
        private string _machineDescrtiption;
        private string _machineLine;
        //private string _machineOperator;
        private int _sortNumber;
        //public List<ProductionPerHour> listOfProductionPerHour = new List<ProductionPerHour>();

        public static string SelectedMachine { get; set; }
       

        public static void StyleDataGridView(DataGridView myDataGridView)
        {
            myDataGridView.AllowUserToAddRows = false;
            myDataGridView.AllowUserToDeleteRows = false;
            myDataGridView.AllowUserToOrderColumns = false;
            myDataGridView.AllowUserToResizeRows = false;
            myDataGridView.AllowUserToResizeColumns = false;

            myDataGridView.ReadOnly = true; //disallow user to change data

            myDataGridView.BackgroundColor = Color.FromArgb(235, 235, 235);

            myDataGridView.MultiSelect = false;
            myDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            myDataGridView.RowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            myDataGridView.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            myDataGridView.RowHeadersDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            myDataGridView.EnableHeadersVisualStyles = true;
            myDataGridView.BorderStyle = BorderStyle.None;
            myDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            myDataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            myDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //myDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            myDataGridView.DataBindingComplete += delegate
            {
                //disallow manual sorting to follow production life-cycle
                foreach (DataGridViewColumn c in myDataGridView.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                myDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                myDataGridView.ColumnHeadersHeight = 50;

                //sets columns and rows appereance
                myDataGridView.GridColor = Color.Silver;
                myDataGridView.RowTemplate.Height = 25;
                myDataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised; //Sunken
                myDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.Raised; //Sunken
                myDataGridView.EnableHeadersVisualStyles = false;
                myDataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.Gold;
                
                for(int i=0; i<myDataGridView.Columns.Count - 1; i++)
                {
                    if (i == 0 || i == 2 || i == 4 || i == 7 || i == 8 || i == 12 || i == 15 || i == 16 ||
                        i == 19 || i == 21 || i == 23 || i == 25)
                        myDataGridView.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                    else myDataGridView.Columns[i].DefaultCellStyle.BackColor = myDataGridView.BackgroundColor;
                }
                myDataGridView.RowHeadersVisible = false;
                myDataGridView.ColumnHeadersHeight = 30;

                for (var i = 0; i <= myDataGridView.Columns.Count - 1; i++)
                {
                    var c = myDataGridView.Columns[i];
                    c.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8);
                    c.HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 7, FontStyle.Regular);
                    c.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // enable this if you want to auto-resize all cells inside table
                    //AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                }                
            };
        }

        public Production()
        {
            InitializeComponent();
            for (int i = 0; i < dgv_Time.Columns.Count - 1; i++)
            {
                if (i == 0 || i == 2 || i == 4)
                    dgv_Time.Columns[i].DefaultCellStyle.BackColor = Color.Red;
            }
            StyleDataGridView(dgv_Time);
            //PauseTable();
        }             

        private void Production_Load(object sender, EventArgs e)
        {
            _option = "allrecs";
            if (dgv_Time.DataSource != null) dgv_Time.DataSource = null;
            dgv_Time.DataSource = GetData();
            if (dgv_Time.Rows.Count > 0)
                populateGraph();
            ResizeHeaders(dgv_Time);
            dgv_Time.DoubleBuffered(true);

            Additional.FillTheFilter(dgv_Time, cbLine, 2);
            Additional.FillTheFilter(dgv_Time, cbArt, 3);
            Additional.FillTheFilter(dgv_Time, cbPh, 4);
            readf();
        }

        #region production

        public void GetDataPublic()
            {
            _option = "allrecs";
            if (dgv_Time.DataSource != null) dgv_Time.DataSource = null;
            dgv_Time.DataSource = GetData();
            ResizeHeaders(dgv_Time);

            Additional.FillTheFilter(dgv_Time, cbLine, 2);
            Additional.FillTheFilter(dgv_Time, cbArt, 3);
            Additional.FillTheFilter(dgv_Time, cbPh, 4);
        }

        private void ResizeHeaders(DataGridView dgv)
        {
            dgv.Columns[2].HeaderText = "     Line     ";
            dgv.Columns[3].HeaderText = "     Article     ";
            dgv.Columns[4].HeaderText = "    Phase    ";
            dgv.Columns[5].HeaderText = "        Operator        ";
            dgv_Time.Columns["Sorts"].Visible = false;
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
            catch (Exception)
            {
                startpC = 6;
                endpC = 15;
                // MessageBox.Show(e.ToString());
                //MessageBox.Show("Program is not set, default is 6-15.\n To set Program go to Settings/Working time.");
            }
        }
        private DataTable dtPresent = new DataTable();
        private DataTable GetData()
        {
            dtPresent = new DataTable();

            var totalDays = 0.0;
            var _tblOperation = new DataTable();
            List<DateTime> _lstTime = new List<DateTime>();

            _tblOperation.Columns.Add("IDM", typeof(string));
            _tblOperation.Columns.Add("dates", typeof(DateTime));
            _tblOperation.Columns.Add("ts", typeof(DateTime));
            _tblOperation.Columns.Add("auto", typeof(bool));
            _tblOperation.Columns.Add("pw", typeof(bool));
            _tblOperation.Columns.Add("als", typeof(bool));
            _tblOperation.Columns.Add("alf", typeof(bool));
            _tblOperation.Columns.Add("alm", typeof(bool));
            _tblOperation.Columns.Add("name", typeof(string));
            _tblOperation.Columns.Add("operatie", typeof(string));
            _tblOperation.Columns.Add("cic", typeof(bool));
            _tblOperation.Columns.Add("tic", typeof(bool));
            _tblOperation.Columns.Add("mvc", typeof(bool));
            _tblOperation.Columns.Add("mlc", typeof(bool));
            _tblOperation.Columns.Add("hourx", typeof(int));
            _tblOperation.Columns.Add("operator", typeof(string));

            var startHour =6;
            var nextHour = 0;
            var startMachine = "0";
            var firstRead = true;
            var startDate = new DateTime();
            string startName = string.Empty;
            string startOperatie = string.Empty;
            var startTs = new DateTime();
            var startOperator = string.Empty;

            object[] arrOfData = new object[] { };
            object[] arrOfAdditionalData = new object[] { };
            object[] additionArr = new object[] { };
            object[] arrOfStartPointerHour = new object[] { };

            dgv_Time.AllowUserToAddRows = false;
            dgv_Time.ReadOnly = true;
           
            var con = new SqlConnection(Exacta.Menu.connectionString);
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "sp_getdata";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@startDate", SqlDbType.NVarChar).Value = Exacta.Menu.dateFrom.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("@endDate", SqlDbType.NVarChar).Value = Exacta.Menu.dateTo.ToString("MM/dd/yyyy");

            con.Open();
            var dr = cmd.ExecuteReader();

            var newRow = _tblOperation.NewRow();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    newRow = _tblOperation.NewRow();
                    var index = 0;

                    string idm = dr[0].ToString();
                    DateTime.TryParse(dr[1].ToString(), out var dates);
                    DateTime.TryParse(dr[2].ToString(), out var ts);
                    bool.TryParse(dr[3].ToString(), out var auto);
                    bool.TryParse(dr[4].ToString(), out var pw);
                    bool.TryParse(dr[5].ToString(), out var als);
                    bool.TryParse(dr[6].ToString(), out var alf);
                    bool.TryParse(dr[7].ToString(), out var alm);
                    string name = dr[8].ToString();
                    string operatie = dr[9].ToString();
                    bool.TryParse(dr[10].ToString(), out var cic);
                    bool.TryParse(dr[11].ToString(), out var tic);
                    bool.TryParse(dr[12].ToString(), out var mvc);
                    bool.TryParse(dr[13].ToString(), out var mlc);
                    int.TryParse(dr[14].ToString(), out var hourx);
                    var operators = dr[15].ToString();

                    if (firstRead)
                    {
                        startMachine =idm;
                        startDate = dates;
                    }
                    else
                    {
                        if (startMachine != idm | startDate != dates)
                        {
                            startHour = hourx;

                            if (nextHour < endpC+1)
                            {
                                if (arrOfData.Length > 0)
                                {
                                    var illegalTs = new DateTime(startDate.Year, startDate.Month, startDate.Day, nextHour, 0, 0);
                                    arrOfData = new object[] { startMachine, startDate, illegalTs, 1, pw, 0, 0, 0,
                                                               startName, startOperatie, 0, 0, 0, 0, nextHour, startOperator };
                                    newRow = _tblOperation.NewRow();
                                    index = 0;
                                    foreach (var item in arrOfData)
                                    {
                                        newRow[index] = item;
                                        index++;
                                    }
                                    _tblOperation.Rows.Add(newRow);
                                }
                                nextHour = startHour;
                            }
                        }
                    }

                    var fullTs = new DateTime(ts.Year, ts.Month, ts.Day);

                    arrOfData = new object[] { idm, fullTs, ts, auto, pw, als, alf, alm, name, operatie, cic, tic, mvc, mlc, hourx, operators };
                    arrOfAdditionalData = new object[] { };
                    arrOfStartPointerHour = new object[] { };
                    additionArr = new object[] { };

                    var virtTs = new DateTime(fullTs.Year, fullTs.Month, fullTs.Day, nextHour, 0, 0);
                    var lastMinute = new DateTime(fullTs.Year, fullTs.Month, fullTs.Day, nextHour, 59, 59);
                    var newHour = new DateTime(fullTs.Year, fullTs.Month, fullTs.Day, hourx, 0, 0);

                    if (startHour != hourx && ts.Minute > 0)
                    {
                        arrOfStartPointerHour = new object[] { idm, fullTs, virtTs, auto, pw, 0, 0, 0, name, operatie, 0, 0, 0, 0, hourx, operators };
                    }

                    if (startHour != hourx && nextHour != hourx)
                    {
                        arrOfAdditionalData = new object[] { idm, fullTs, virtTs, 0, 0, 0, 0, 0, name, operatie, 0, 0, 0, 0, nextHour, operators };
                        additionArr = new object[] { idm, fullTs, lastMinute, 0, 0, 0, 0, 0, name, operatie, 0, 0, 0, 0, nextHour, operators };
                    }

                    if (arrOfAdditionalData.Length > 0)
                    {
                        foreach (var item in arrOfAdditionalData)
                        {
                            newRow[index] = item;
                            index++;
                        }
                        _tblOperation.Rows.Add(newRow);
                    }
                    if (additionArr.Length > 0)
                    {
                        newRow = _tblOperation.NewRow();
                        index = 0;
                        foreach (var item in additionArr)
                        {
                            newRow[index] = item;
                            index++;
                        }
                        _tblOperation.Rows.Add(newRow);
                    }

                    if (arrOfStartPointerHour.Length > 0)
                    {
                        index = 0;
                        newRow = _tblOperation.NewRow();
                        foreach (var item in arrOfStartPointerHour)
                        {
                            newRow[index] = item;
                            index++;
                        }
                        _tblOperation.Rows.Add(newRow);
                    }

                    newRow = _tblOperation.NewRow();
                    index = 0;
                    foreach (var item in arrOfData)
                    {
                        newRow[index] = item;
                        index++;
                    }

                    _tblOperation.Rows.Add(newRow);

                    nextHour = hourx + 1;
                    startHour = hourx;
                    startMachine = idm;
                    startDate = dates;
                    firstRead = false;
                    startName = name;
                    startOperatie = operatie;
                    startTs = ts;
                    startOperator = operators;
                }
            }
            
            dr.Close();
            con.Close();
            
            totalDays = Exacta.Menu.dateTo.Subtract(Exacta.Menu.dateFrom).TotalDays;
            totalDays = Math.Round(totalDays, 0);

            if (totalDays == 1) totalDays = 2;
            if (totalDays == 0) totalDays = 1;
            

            var minutesPerDay = 0.0;

            // calculation from here
            // ->
            dtPresent.Columns.Add("MachineID");
            //dtPresent.Columns.Add("Date");  
            dtPresent.Columns.Add("Description");
            dtPresent.Columns.Add("Line");
            dtPresent.Columns.Add("Article");
            dtPresent.Columns.Add("Phase");
            dtPresent.Columns.Add("Operator");
            dtPresent.Columns.Add("sep1"); //6
            dtPresent.Columns.Add("Auto Time");
            dtPresent.Columns.Add("Auto[%]");
            dtPresent.Columns.Add("Manual Time");
            dtPresent.Columns.Add("Manual[%]");
            dtPresent.Columns.Add("sep4"); //11
            dtPresent.Columns.Add("Power On Time");
            dtPresent.Columns.Add("Knitt Time");
            dtPresent.Columns.Add("Knitt Time[%]");
            dtPresent.Columns.Add("Stop Time");
            dtPresent.Columns.Add("Stop Time[%]");
            dtPresent.Columns.Add("sep2"); //17
            dtPresent.Columns.Add("ALM TIME");
            dtPresent.Columns.Add("ALM[%]");
            dtPresent.Columns.Add("ALS TIME");
            dtPresent.Columns.Add("ALS[%]");
            dtPresent.Columns.Add("ALF TIME");
            dtPresent.Columns.Add("ALF[%]");
            dtPresent.Columns.Add("sep3"); //24
            dtPresent.Columns.Add("Total Time");
            dtPresent.Columns.Add("Sorts", typeof(int)); //22 on the basis of which we sort                      
               
            if (_tblOperation.Rows.Count == 0)
            {
                dtPresent.Rows.Add(dtPresent.NewRow());

                dgv_Time.DataSource = dtPresent;
                ResizeHeaders(dgv_Time);
                return dtPresent;
            }

            int.TryParse(_tblOperation.Rows[0][0].ToString(), out var catchMachine);
            DateTime.TryParse(_tblOperation.Rows[0][1].ToString(), out var catchDate);
            var machine = catchMachine;
            var dt = catchDate;
            var art = _tblOperation.Rows[0][8].ToString();
            var ph = _tblOperation.Rows[0][9].ToString();
            var artChain = art + "_" + ph;

            List<DateTime> lstAutos = new List<DateTime>();
            List<DateTime> lstManuals = new List<DateTime>();
            var minutesAuto = 0.0;
            var minutesManual = 0.0;

            List<DateTime> lstAlsAuto = new List<DateTime>();
            List<DateTime> lstAlsManual = new List<DateTime>();
            var minutesAlsAuto = 0.0;
            var minutesAlsManual = 0.0;

            List<DateTime> lstAlmAuto = new List<DateTime>();
            List<DateTime> lstAlmManual = new List<DateTime>();
            var minutesAlmAuto = 0.0;
            var minutesAlmManual = 0.0;

            List<DateTime> lstAlfAuto = new List<DateTime>();
            List<DateTime> lstAlfManual = new List<DateTime>();
            var minutesAlfAuto = 0.0;
            var minutesAlfManual = 0.0;

            List<DateTime> lstStop = new List<DateTime>();
            var minutesStop = 0.0;

            List<DateTime> lstKnitt = new List<DateTime>();
            var minutesKnitt = 0.0;
            
            newRow = dtPresent.NewRow();

            var machineOperator = string.Empty;
            var op = string.Empty;
            foreach (DataRow row in _tblOperation.Rows)
            {
                var dbMachine = Convert.ToInt32(row[0]);

                DateTime.TryParse(row[1].ToString(), out var dbDate);   //last readed date
                DateTime.TryParse(row[2].ToString(), out var dbTime);
                bool.TryParse(row[3].ToString(), out var dbAuto);

                //bool.TryParse(row[4].ToString(), out var dbPower);
                bool.TryParse(row[5].ToString(), out var dbAls);
                bool.TryParse(row[6].ToString(), out var dbAlf);
                bool.TryParse(row[7].ToString(), out var dbAlm);

                var article = row[8].ToString();
                var phase = row[9].ToString();

                //alarms for stop time
                bool.TryParse(row[10].ToString(), out var dbCic);
                bool.TryParse(row[11].ToString(), out var dbTic);
                bool.TryParse(row[12].ToString(), out var dbMvc);
                bool.TryParse(row[13].ToString(), out var dbMlc);

                var dbChain = article + "_" + phase;

                machineOperator = row[15].ToString();

                if (dt != dbDate)
                {
                    if (lstAutos.Count > 0)
                    {
                        var auto = lstAutos.First();
                        var autoLast = lstAutos.Last();
                        minutesAuto += autoLast.Subtract(auto).TotalSeconds;
                        lstAutos.Clear();
                    }

                    if (lstManuals.Count > 0)
                    {
                        var manual = lstManuals.First();
                        var manualLast = lstManuals.Last();
                        minutesManual += manualLast.Subtract(manual).TotalSeconds;
                        lstManuals.Clear();
                    }

                    if (lstStop.Count > 0)
                    {
                        var auto = lstStop.First();
                        var autoLast = lstStop.Last();
                        minutesStop += autoLast.Subtract(auto).TotalSeconds;
                        lstStop.Clear();
                    }

                    if (lstKnitt.Count > 0)
                    {
                        var auto = lstKnitt.First();
                        var autoLast = lstKnitt.Last();
                        minutesKnitt += autoLast.Subtract(auto).TotalSeconds;
                        lstKnitt.Clear();
                    }
                }

                if (_option == _ALL_RECS_ && machine != dbMachine
                    || _option == _OF_ARTICLE_ && artChain != dbChain)
                {
                    if (lstAutos.Count > 0)
                    {
                        var auto = lstAutos.First();
                        var autoLast = lstAutos.Last();
                        minutesAuto += autoLast.Subtract(auto).TotalSeconds;
                        lstAutos.Clear();
                    }
                    if (lstManuals.Count > 0)
                    {
                        var manual = lstManuals.First();
                        var manualLast = lstManuals.Last();
                        minutesManual += manualLast.Subtract(manual).TotalSeconds;
                        lstManuals.Clear();
                    }
                    if (lstStop.Count > 0)
                    {
                        var auto = lstStop.First();
                        var autoLast = lstStop.Last();
                        minutesStop += autoLast.Subtract(auto).TotalSeconds;
                        lstStop.Clear();
                    }
                    if (lstKnitt.Count > 0)
                    {
                        var auto = lstKnitt.First();
                        var autoLast = lstKnitt.Last();
                        minutesKnitt += autoLast.Subtract(auto).TotalSeconds;
                        lstKnitt.Clear();
                    }   
                    newRow = dtPresent.NewRow();
                    GetMachineProperties(machine.ToString());
                    minutesPerDay = minutesAuto + minutesManual;

                    newRow[0] = machine.ToString();
                    newRow[1] = _machineDescrtiption;
                    newRow[2] = _machineLine;
                    newRow[3] = art;
                    newRow[4] = ph;
                    newRow[5] = op;
                    newRow[7] = GetTime(minutesAuto - minutesAlsAuto - minutesAlmAuto - minutesAlfAuto);
                    newRow[8] = Math.Round(((minutesAuto - minutesAlsAuto - minutesAlmAuto - minutesAlfAuto) / minutesPerDay) * 100, 2).ToString() + " %";
                    newRow[9] = GetTime(minutesManual - minutesAlsManual - minutesAlmManual - minutesAlfManual);
                    newRow[10] = Math.Round(((minutesManual - minutesAlsManual - minutesAlmManual - minutesAlfManual) / minutesPerDay) * 100, 2).ToString() + " %";
                    newRow[12] = GetTime(minutesStop  + minutesKnitt + minutesAlfAuto + minutesAlfManual + minutesAlmAuto + minutesAlmManual + minutesAlsAuto + minutesAlsManual);
                    newRow[13] = GetTime(minutesKnitt);
                    newRow[14] = Math.Round((minutesKnitt / minutesPerDay) * 100, 2).ToString() + " %";
                    newRow[15] = GetTime(minutesStop);
                    newRow[16] = Math.Round(((minutesStop ) / minutesPerDay) * 100, 2).ToString() + " %";
                    newRow[18] = GetTime(minutesAlsManual + minutesAlsAuto);
                    newRow[19] = Math.Round(((minutesAlsAuto + minutesAlsManual) / minutesPerDay) * 100, 2).ToString() + " %";
                    newRow[20] = GetTime(minutesAlmManual + minutesAlmAuto);
                    newRow[21] = Math.Round(((minutesAlmAuto + minutesAlmManual) / minutesPerDay) * 100, 2).ToString() + " %";
                    newRow[22] = GetTime(minutesAlfManual + minutesAlfAuto);
                    newRow[23] = Math.Round(((minutesAlfAuto + minutesAlfManual) / minutesPerDay) * 100, 2).ToString() + " %";
                    newRow[25] = GetTime(minutesAuto + minutesManual);
                    newRow[26] = _sortNumber;

                    dtPresent.Rows.Add(newRow);
 
                    minutesAuto = 0.0;
                    minutesManual = 0.0;
                    minutesAlsAuto = 0.0;
                    minutesAlsManual = 0.0;
                    minutesAlmAuto = 0.0;
                    minutesAlmManual = 0.0;
                    minutesAlfAuto = 0.0;
                    minutesAlfManual = 0.0;
                    minutesStop = 0.0;
                    minutesKnitt = 0.0;

                    lstAutos.Clear();
                    lstManuals.Clear();
                    lstAlsAuto.Clear();
                    lstAlmAuto.Clear();
                    lstAlfAuto.Clear();
                    lstAlsManual.Clear();
                    lstAlmManual.Clear();
                    lstAlfManual.Clear();
                    lstStop.Clear();
                    lstKnitt.Clear();
                }             

                //knitt time
                if (dbCic)
                {
                    lstKnitt.Add(dbTime);
                }
                else
                {
                    if (lstKnitt.Count > 0)
                    {
                        var curTime = dbTime;
                        var stop = lstKnitt.First();
                        minutesKnitt += curTime.Subtract(stop).TotalSeconds;
                        lstKnitt.Clear();
                    }
                }

                //stop time
                if (!dbCic && !dbTic && !dbMvc && !dbMlc)
                {
                    lstStop.Add(dbTime);
                }
                else
                {
                    if (lstStop.Count > 0)
                    {
                        var curTime = dbTime;
                        var stop = lstStop.First();
                        minutesStop += curTime.Subtract(stop).TotalSeconds;
                        lstStop.Clear();
                    }
                }
                if (!dbAuto) //manual
                {
                    if (!dbCic && !dbTic && !dbMvc && !dbMlc)
                    {
                        lstStop.Add(dbTime);
                    }
                    else
                    {
                        lstManuals.Add(dbTime);
                        var curTime = dbTime;

                        if (lstAutos.Count > 0)
                        {
                            var auto = lstAutos.First();
                            minutesAuto += curTime.Subtract(auto).TotalSeconds;
                            lstAutos.Clear();
                        }
                        if (lstStop.Count > 0)
                        {
                            //var curTime = dbTime;
                            var stop = lstStop.First();
                            minutesStop += curTime.Subtract(stop).TotalSeconds;
                            lstStop.Clear();
                        }
                    }
                }
                else
                {
                    if (!dbCic && !dbTic && !dbMvc && !dbMlc)
                    {
                        lstStop.Add(dbTime);
                    }
                    else
                    {
                        lstAutos.Add(dbTime);
                        var curTime = dbTime;

                        if (lstManuals.Count > 0)
                        {
                            var manual = lstManuals.First();
                            minutesManual += curTime.Subtract(manual).TotalSeconds;
                            lstManuals.Clear();
                        }
                        if (lstStop.Count > 0)
                        {
                            //var curTime = dbTime;
                            var stop = lstStop.First();
                            minutesStop += curTime.Subtract(stop).TotalSeconds;
                            lstStop.Clear();
                        }
                    }
                }

                if (dbAls)
                {
                    lstAlsManual.Add(dbTime);
                }
                else
                {
                    if (lstAlsManual.Count > 0)
                    {
                        var curAlsTime = dbTime;
                        minutesAlsManual += curAlsTime.Subtract(lstAlsManual.First()).TotalSeconds;
                        lstAlsManual.Clear();
                    }
                }
                if (dbAlm)
                {
                    lstAlmManual.Add(dbTime);
                }
                else
                {
                    if (lstAlmManual.Count > 0)
                    {
                        var curAlmTime = dbTime;
                        minutesAlmManual += curAlmTime.Subtract(lstAlmManual.First()).TotalSeconds;
                        lstAlmManual.Clear();
                    }
                }
                if (dbAlf)
                {
                    lstAlfManual.Add(dbTime);
                }
                else
                {
                    if (lstAlfManual.Count > 0)
                    {
                        var curAlfTime = dbTime;
                        minutesAlfManual += curAlfTime.Subtract(lstAlfManual.First()).TotalSeconds;
                        lstAlfManual.Clear();
                    }
                }
                dt = dbDate;
                machine = dbMachine;
                art = article;
                ph = phase;
                artChain = dbChain;
                op = machineOperator;
            }

            if (lstAutos.Count > 0)
            {
                var auto = lstAutos.First();
                var autoLast = lstAutos.Last();
                minutesAuto += autoLast.Subtract(auto).TotalSeconds;
                lstAutos.Clear();
            }

            if (lstManuals.Count > 0)
            {
                var manual = lstManuals.First();
                var manualLast = lstManuals.Last();
                minutesManual += manualLast.Subtract(manual).TotalSeconds;
                lstManuals.Clear();
            }

            newRow = dtPresent.NewRow();

            GetMachineProperties(machine.ToString());

            minutesPerDay = minutesAuto + minutesManual;

            newRow[0] = machine.ToString();
            newRow[1] = _machineDescrtiption;
            newRow[2] = _machineLine;
            newRow[3] = art;
            newRow[4] = ph;
            newRow[5] = machineOperator;
            newRow[7] = GetTime(minutesAuto - minutesAlsAuto - minutesAlmAuto - minutesAlfAuto);
            newRow[8] = Math.Round(((minutesAuto - minutesAlsAuto - minutesAlmAuto - minutesAlfAuto) / minutesPerDay) * 100, 2).ToString() + " %";
            newRow[9] = GetTime(minutesManual - minutesAlsManual - minutesAlmManual - minutesAlfManual);
            newRow[10] = Math.Round(((minutesManual - minutesAlsManual - minutesAlmManual - minutesAlfManual) / minutesPerDay) * 100, 2).ToString() + " %";
            newRow[12] = GetTime(minutesKnitt + minutesStop  + minutesAlfAuto + minutesAlfManual + minutesAlmAuto + minutesAlmManual + minutesAlsAuto + minutesAlsManual);
            newRow[13] = GetTime(minutesKnitt);
            newRow[14] = Math.Round(((minutesKnitt) / minutesPerDay) * 100, 2).ToString() + " %";
            newRow[15] = GetTime(minutesStop);
            newRow[16] = Math.Round(((minutesStop) / minutesPerDay) * 100, 2).ToString() + " %";
            newRow[18] = GetTime(minutesAlsManual + minutesAlsAuto);
            newRow[19] = Math.Round(((minutesAlsAuto + minutesAlsManual) / minutesPerDay) * 100, 2).ToString() + " %";
            newRow[20] = GetTime(minutesAlmManual + minutesAlmAuto);
            newRow[21] = Math.Round(((minutesAlmAuto + minutesAlmManual) / minutesPerDay) * 100, 2).ToString() + " %";
            newRow[22] = GetTime(minutesAlfManual + minutesAlfAuto);
            newRow[23] = Math.Round(((minutesAlfAuto + minutesAlfManual) / minutesPerDay) * 100, 2).ToString() + " %";
            newRow[25] = GetTime(minutesAuto + minutesManual);
            newRow[26] = _sortNumber;

            dtPresent.Rows.Add(newRow);

            minutesAuto = 0.0;
            minutesManual = 0.0;
            minutesAlsAuto = 0.0;
            minutesAlsManual = 0.0;
            minutesAlmAuto = 0.0;
            minutesAlmManual = 0.0;
            minutesAlfAuto = 0.0;
            minutesAlfManual = 0.0;
            minutesStop = 0.0;
            minutesKnitt = 0.0;

            lstAutos.Clear();
            lstManuals.Clear();
            lstAlsAuto.Clear();
            lstAlmAuto.Clear();
            lstAlfAuto.Clear();
            lstAlsManual.Clear();
            lstAlmManual.Clear();
            lstAlfManual.Clear();
            lstStop.Clear();
            lstKnitt.Clear();
            return dtPresent;          
        }
        public TimeSpan pauseT;
        public DateTime startp;
        public DateTime endp;
        public void PauseTable()
        {
            pauseT = TimeSpan.Zero;
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
            }
        }
        private void btn_AllRecords_Click(object sender, EventArgs e)
        {
            LoadingInfo.InfoText = "Loading production...";
            LoadingInfo.ShowLoading();

            _option = "allrecs";
            if (dgv_Time.DataSource != null) dgv_Time.DataSource = null;
            dgv_Time.DataSource = GetData();
            ResizeHeaders(dgv_Time);
            Additional.FillTheFilter(dgv_Time, cbLine, 2);
            Additional.FillTheFilter(dgv_Time, cbArt, 3);
            Additional.FillTheFilter(dgv_Time, cbPh, 4);
            //Exacta.Menu._pbReload.Enabled = true;
            populateGraph();

            LoadingInfo.CloseLoading();
        }

        private void btn_ByArticle_Click(object sender, EventArgs e)
        {
            LoadingInfo.InfoText = "Loading production...";
            LoadingInfo.ShowLoading();

            _option = "ofarticle";
            if (dgv_Time.DataSource != null) dgv_Time.DataSource = null;
            dgv_Time.DataSource = GetData();
            ResizeHeaders(dgv_Time);

            LoadingInfo.CloseLoading();
        }

        private string GetTime(double seconds)
        {
            var str = "";

            var ts = TimeSpan.FromSeconds(seconds);
            var h = ts.TotalHours;
            var m = ts.Minutes;

            return str = ts.ToString(@"hh\:mm", CultureInfo.InvariantCulture);
        }

        private DateTime GetDate(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }      
        
        #endregion
        
        private void button1_Click(object sender, EventArgs e)
            {
            if (dgv_Time.Rows.Count <= 0 || dgv_Time.DataSource == null)                
                return;                
            
            if (dgv_Time.CurrentCell == null)                
                dgv_Time.CurrentCell = dgv_Time.Rows[0].Cells[1];                

            SelectedMachine = dgv_Time.Rows[dgv_Time.CurrentRow.Index].Cells[0].Value.ToString();

            if (!string.IsNullOrEmpty(SelectedMachine))
            {
                var frmCuts = new Cuts();
                frmCuts.ShowDialog();
                frmCuts.Dispose();
            }
            else MessageBox.Show("No selected machine.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            }
        
        private string GetMachineProperties(string machineId)
            {
            _sortNumber = 999;
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

                    int.TryParse(sb.ToString(), out var dig);
                    _sortNumber = dig;
                }                
                //_machineOperator = mac.Operator;
                }

            return machineDesc;
            }

        private void dgv_Time_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
            {
            if (dgv_Time.Columns.Count < 2) return;
            dgv_Time.Columns[1].Width = 100;
            dgv_Time.Columns[2].Width = 100;

            dgv_Time.Columns["sep1"].HeaderText = "";
            dgv_Time.Columns["sep2"].HeaderText = "";
            dgv_Time.Columns["sep3"].HeaderText = "";
            dgv_Time.Columns["sep4"].HeaderText = "";
        }

        private void dgv_Time_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= -1)
            {
                if (e.ColumnIndex == 6 || e.ColumnIndex==11 || e.ColumnIndex == 17 || e.ColumnIndex == 24)
                {
                    var rect = new System.Drawing.Rectangle(e.CellBounds.X + 1, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
                    e.Graphics.FillRectangle(new SolidBrush(dgv_Time.BackgroundColor), rect);
                    e.Handled = true;
                }
            }
        }        

        private void cbArt_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbArt.Text != String.Empty)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dgv_Time.DataSource;
                bs.Filter = string.Format("CONVERT(" + dgv_Time.Columns[3].DataPropertyName + ", System.String) like '%" + cbArt.Text.Replace("'", "''") + "%'");
                dgv_Time.DataSource = bs;
            }
            else
            {
                if (dgv_Time.DataSource != null) dgv_Time.DataSource = null;
                dgv_Time.DataSource = GetData();
                ResizeHeaders(dgv_Time);
            }
        }

        private void cbPh_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbPh.Text != String.Empty)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dgv_Time.DataSource;
                bs.Filter = string.Format("CONVERT(" + dgv_Time.Columns[4].DataPropertyName + ", System.String) like '%" + cbPh.Text.Replace("'", "''") + "%'");
                dgv_Time.DataSource = bs;
            }
            else
            {
                if (dgv_Time.DataSource != null) dgv_Time.DataSource = null;
                dgv_Time.DataSource = GetData();
                ResizeHeaders(dgv_Time);
            }
        }

        private void cbLine_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cbLine.Text))
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = dgv_Time.DataSource;
                bs.Filter = string.Format("CONVERT(" + dgv_Time.Columns[2].DataPropertyName + ", System.String) like '%" + cbLine.Text.Replace("'", "''") + "%'");
                dgv_Time.DataSource = bs;
                populateGraph();
            }
            else
            {
                if (dgv_Time.DataSource != null)
                    dgv_Time.DataSource = null;

                dgv_Time.DataSource = GetData();
                ResizeHeaders(dgv_Time);
                populateGraph();
            }
        }              

        private void btnSortByLine_Click(object sender, EventArgs e)
        {
            if (dgv_Time.Rows.Count == 0)
                return;

            dgv_Time.Sort(dgv_Time.Columns["Sorts"], ListSortDirection.Ascending);            
        }

        public void RefreshDgv()
        {           
            if (dgv_Time.DataSource != null)                
             dgv_Time.DataSource = null;   
            
            dgv_Time.DataSource = GetData();
            ResizeHeaders(dgv_Time);
            Additional.FillTheFilter(dgv_Time, cbLine, 2);
            Additional.FillTheFilter(dgv_Time, cbArt, 3);
            Additional.FillTheFilter(dgv_Time, cbPh, 4);
            dgv_Time.Refresh();            
        }
        public int GetHoursByTimeFormat(string format)
        {
            var hours = 0;

            if (!string.IsNullOrEmpty(format))
            {
                var ts = TimeSpan.Parse(format);
                hours = (Convert.ToInt32(ts.TotalSeconds) / 480);
            }
            return hours;
        }
        private void populateGraph()
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

            myPane.Title.Text = string.Empty;
            myPane.BarSettings.Type = BarType.Stack;

            //x axis
            myPane.XAxis.Title.Text = "Machine ID";
            myPane.XAxis.Title.FontSpec.Size = 20;
            myPane.XAxis.Title.FontSpec.IsBold = false;
            myPane.XAxis.Scale.MajorStep = 1;
            myPane.XAxis.MinorTic.Size = 0;
            //myPane.XAxis.Scale.Min = 5;
            myPane.XAxis.Scale.Max = 26;
            myPane.XAxis.MajorTic.IsInside = false;
            myPane.XAxis.MajorTic.IsOutside = true;

            //y axis
            myPane.YAxis.Title.Text = "Minutes";
            myPane.YAxis.Title.FontSpec.Size = 20;
            myPane.YAxis.Title.FontSpec.IsBold = false;
            myPane.YAxis.Scale.Max = 90;
            myPane.YAxis.Scale.MajorStep = 10;
            myPane.YAxis.MinorTic.Size = 0;
            zedGraphControl1.IsEnableZoom = false;
            zedGraphControl1.IsShowContextMenu = false;

            var autoTime = new PointPairList();
            var manualTime = new PointPairList();
            var stopTime = new PointPairList();

            //var c = 1;
            for (var i = 0; i < dgv_Time.Rows.Count; i++)
            {
                int.TryParse(dgv_Time.Rows[i].Cells[0].Value.ToString(), out var position);
                autoTime.Add(position, GetHoursByTimeFormat(dgv_Time.Rows[i].Cells[7].Value.ToString()));
                //c+=1;
            }
            //c = 1;
            for (var i = 0; i < dgv_Time.Rows.Count; i++)
            {
                int.TryParse(dgv_Time.Rows[i].Cells[0].Value.ToString(), out var position);
                manualTime.Add(position, GetHoursByTimeFormat(dgv_Time.Rows[i].Cells[9].Value.ToString()));
                //c += 1;
            }
            for (var i = 0; i < dgv_Time.Rows.Count; i++)
            {
                int.TryParse(dgv_Time.Rows[i].Cells[0].Value.ToString(), out var position);
                stopTime.Add(position, GetHoursByTimeFormat(dgv_Time.Rows[i].Cells[15].Value.ToString()));
                //c += 1;
            }

            myPane.AddBar("Auto Time", autoTime, Color.FromArgb(145, 255, 145)).Bar.Fill =
                    new Fill(Color.Gainsboro, Color.FromArgb(145, 255, 145), Color.FromArgb(145, 255, 145));
            myPane.AddBar("Manual Time", manualTime, Color.FromArgb(255, 255, 111)).Bar.Fill =
                new Fill(Color.Gainsboro, Color.FromArgb(255, 255, 111), Color.FromArgb(255, 255, 111));
            myPane.AddBar("Stop Time", stopTime, Color.FromArgb(243, 139, 145)).Bar.Fill =
                new Fill(Color.Gainsboro, Color.FromArgb(243, 139, 145), Color.FromArgb(243, 139, 145));

            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();             
        }

        private void ExportProduction_To_PDF()
        {
            System.Drawing.Bitmap screenshot_bmp = PrintScreen();
            iTextSharp.text.Image screenshot_pdf = iTextSharp.text.Image.GetInstance(screenshot_bmp,
                                               System.Drawing.Imaging.ImageFormat.Jpeg);

            var save_file_dialog = new SaveFileDialog();
            save_file_dialog.FileName = "Production " + DateTime.Now.Date.ToShortDateString();
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
            ExportProduction_To_PDF();
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

                object[] Header = new object[columnsCount - 1];

                for (int i = 0; i <= columnsCount - 2; i++)
                    Header[i] = dt.Columns[i].ColumnName;

                Header[6] = string.Empty;
                Header[11] = string.Empty;
                Header[17] = string.Empty;
                Header[24] = string.Empty;
                
                Microsoft.Office.Interop.Excel.Range HeaderRange = osheet.get_Range((Microsoft.Office.Interop.Excel.Range)
                                                                   (osheet.Cells[1, 1]), (Microsoft.Office.Interop.Excel.Range)
                                                                   (osheet.Cells[1, columnsCount - 1]));
                HeaderRange.Value = Header;
                HeaderRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                HeaderRange.Font.Bold = true;

                int rowsCount = dt.Rows.Count;
                object[,] Cells = new object[rowsCount, columnsCount - 1];

                for (int j = 0; j < rowsCount; j++)
                    for (int i = 0; i <= columnsCount - 2; i++)
                        Cells[j, i] = dt.Rows[j][i];

                osheet.get_Range((Microsoft.Office.Interop.Excel.Range)(osheet.Cells[2, 1]),
                                (Microsoft.Office.Interop.Excel.Range)
                                (osheet.Cells[rowsCount + 1, columnsCount - 1])).Value = Cells;
                oexcel.Columns.AutoFit();

                string filePath = string.Empty;

                var save_file_dialog = new SaveFileDialog();
                save_file_dialog.FileName = "Production " + DateTime.Now.Date.ToShortDateString();
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

        private void pbExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel(dtPresent);
        }
    }
}
