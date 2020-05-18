using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Exacta.DatabaseTableClasses;
using Exacta;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Sinottico : Form
    {
        public static string Mode = string.Empty;

        public Sinottico()
        {
            InitializeComponent();
        }

        private List<Panel> _prod_panels = new List<Panel>();
        private void InstallLines()
        {
            _prod_panels = (from p in this.Controls.OfType<Panel>()
                            where p.Name.StartsWith("panel_")
                            select p).ToList();

            foreach (var panel in _prod_panels)
            {
                var _panel_machines = (from lbl in panel.Controls.OfType<Label>()
                                       where lbl.Name.StartsWith("p")
                                       orderby lbl.Name
                                       select lbl).ToArray();

                var _line_index = int.Parse(panel.Name.Substring(6));
                var _machines = (from m in Tables.TblMachines
                                 where m.Line == "LINEA " + _line_index
                                 orderby Convert.ToInt32(m.Idm)
                                 select Convert.ToInt32(m.Idm)).ToArray();

                var _num_of_machines = _machines.Length;

                if (_num_of_machines >= 1)
                {
                    for (var current_position = 0; current_position < _num_of_machines; current_position++)
                    {
                        if (_machines[current_position] == 0) continue;
                        _panel_machines[current_position].Tag = _machines[current_position];
                    }
                }
            }
        }

        private float MachineEfficiency(int idm, string article, string phase)
        {
            var _quantity = 0;
            var _pieces = 0;

            var _components = 0;
            float _machine_efficiency = 0;
            const double _shift_hours = 7.5;
            var _curr_date = DateTime.Now;

            var _tmp_table = new DataTable();

            var con = new SqlConnection(Exacta.Menu.connectionString);

            SqlCommand cmd = new SqlCommand("sp_efficiency_per_machine", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@idm", SqlDbType.Int).Value = idm;
            cmd.Parameters.Add("@current_date", SqlDbType.NVarChar).Value = Exacta.Menu.dateFrom.ToString("MM/dd/yyyy");//DateTime.Now.ToString("MM/dd/yyyy"); 
            cmd.Parameters.Add("@article", SqlDbType.NVarChar, 50).Value = article;
            cmd.Parameters.Add("@phase", SqlDbType.NVarChar, 50).Value = phase;

            con.Open();
            var dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    int.TryParse(dr[0].ToString(), out var dbQty);
                    int.TryParse(dr[1].ToString(), out var dbPieces);
                    int.TryParse(dr[2].ToString(), out var dbComponents);

                    _quantity += dbQty;
                    _pieces = dbPieces;
                    _components = dbComponents;
                }
            }

            dr.Close();
            con.Close();

            if (_quantity != 0 && _pieces != 0 && _components != 0)
            {
                decimal q = Convert.ToDecimal(_quantity);
                decimal p = Convert.ToDecimal(_pieces * _components * _shift_hours);
                _machine_efficiency = (float)(Math.Round((q / p), 2) * 100);
            }
            else _machine_efficiency = 0;

            return _machine_efficiency;
        }

        private void LineEfficiency(string _line_name)
        {
            var _data = new DataTable();

            var con = new SqlConnection(Exacta.Menu.connectionString);

            SqlCommand cmd = new SqlCommand("sp_getEfficiency", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            //var _curr_date = DateTime.Now;
            cmd.Parameters.Add("@dateFrom", SqlDbType.NVarChar).Value = Exacta.Menu.dateFrom.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("@dateTo", SqlDbType.NVarChar).Value = Exacta.Menu.dateFrom.ToString("MM/dd/yyyy");

            con.Open();
            var dr = cmd.ExecuteReader();

            _data.Load(dr);

            dr.Close();
            con.Close();

            if (_data.Rows.Count <= 0)
            {
                MessageBox.Show(_data.Rows.Count.ToString());
                return;
            }

            const double _shift_hours = 7.5;
            int _quantity = 0;
            int _line_efficiency = 0;

            int.TryParse(_data.Rows[0][0].ToString(), out var idm);
            var article = _data.Rows[0][4].ToString();
            var phase = _data.Rows[0][5].ToString();
            int.TryParse(_data.Rows[0][6].ToString(), out var pieces);
            int.TryParse(_data.Rows[0][7].ToString(), out var components);

            var _line = (from line in _prodLines
                         where line.LineName == _line_name
                         select line).SingleOrDefault();

            var _num_of_machines = _line.GetLineMachines().Count;
            var _position = 0;

            var machineAddition = (from macProp in lstMacProp
                                   where macProp.Idm == _line.GetLineMachines()[_position].ToString()
                                   select macProp).SingleOrDefault();

            var _last_row = _data.Rows.Count - 1;
            foreach (DataRow row in _data.Rows)
            {
                int.TryParse(row[0].ToString(), out var dbIdm);
                bool.TryParse(row[3].ToString(), out var tic);
                var dbArticle = row[4].ToString();
                var dbPhase = row[5].ToString();
                int.TryParse(row[6].ToString(), out var dbPieces);
                int.TryParse(row[7].ToString(), out var dbComponents);

                if (idm != dbIdm || _data.Rows.IndexOf(row) == _last_row)
                {
                    if (_quantity != 0 && pieces != 0 && components != 0)
                    {
                        decimal q = Convert.ToDecimal(_quantity);
                        decimal p = Convert.ToDecimal(pieces * components * _shift_hours);
                        _line_efficiency += Convert.ToInt32(Math.Round((q / p), 2) * 100);
                    }

                    _quantity = 0;

                    if (!_line.GetLineMachines().Contains(dbIdm)) continue;

                    if (_num_of_machines <= _position + 1)
                    {
                        _position++;
                        machineAddition = (from macProp in lstMacProp
                                           where macProp.Idm == _line.GetLineMachines()[_position].ToString()
                                           select macProp).SingleOrDefault();
                    }
                }

                if (!_line.GetLineMachines().Contains(dbIdm)) continue;
                if (machineAddition == null) continue;

                if (dbArticle == machineAddition.Art && dbPhase == machineAddition.Ph && tic) _quantity++;

                idm = dbIdm;
                pieces = dbPieces;
                components = dbComponents;
            }

            var _total_efficiency = 0;
            if (_line_efficiency != 0 || _line.GetLineMachines().Count <= 0)
                _total_efficiency = _line_efficiency / _line.GetLineMachines().Count;

            _line.LineEfficiency = _total_efficiency;
        }

        private GraphicsPath RoundedRectanglePath(Rectangle bounds, int rad)
        {
            var diameter = rad * 2;
            var size = new Size(diameter, diameter);
            var arc = new Rectangle(bounds.Location, size);
            var path = new GraphicsPath();

            if (rad == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();

            return path;
        }

        private List<AGauge> _gauges = new List<AGauge>();
        private List<int[]> _arrayList = new List<int[]>();
        private DataTable _dataTable = new DataTable();
        public List<Label> _machines = new List<Label>();
        private List<int> _currentMachines = new List<int>();
        private List<int[]> _machinesState = new List<int[]>();
        private List<MachineProperties> lstMacProp = new List<MachineProperties>();
        private List<ProductionLines> _prodLines = new List<ProductionLines>();

        public void GetCurrentDataProcedure()
        {
            _dataTable = new DataTable();
            _arrayList = new List<int[]>();

            var query = "[sp_getcurrentdata]";
            using (var conn = new System.Data.SqlClient.SqlConnection(Exacta.Menu.connectionString))
            {
                var cmd = new System.Data.SqlClient.SqlCommand(query, conn);
                cmd.CommandText = query;
                conn.Open();
                var dr = cmd.ExecuteReader();
                _dataTable.Load(dr);
                cmd = null;
                dr.Close();
                conn.Close();
            }

            lstMacProp = new List<MachineProperties>();

            _gauges = new List<AGauge>();
            foreach (var gauge in tableLayoutPanel1.Controls.OfType<AGauge>())
               { _gauges.Add(gauge);
            }


            //_gauges = (from g in this.Controls.OfType<AGauge>()
            //           select g).ToList();
            

            foreach (DataRow row in _dataTable.Rows)
            {
                var idm = int.Parse(row[0].ToString());
                DateTime.TryParse(row[1].ToString(), out var ts);
                var auto = Convert.ToInt16(bool.Parse(row[2].ToString()));
                var als = Convert.ToInt16(bool.Parse(row[3].ToString()));
                var alf = Convert.ToInt16(bool.Parse(row[4].ToString()));
                var alm = Convert.ToInt16(bool.Parse(row[5].ToString()));
                var art = row.ItemArray.GetValue(6).ToString();
                var ph = row.ItemArray.GetValue(7).ToString();
                var tic = Convert.ToInt32(bool.Parse(row[8].ToString()));
                var cic = Convert.ToInt32(bool.Parse(row[9].ToString()));
                var mvc = Convert.ToInt32(bool.Parse(row[10].ToString()));
                var mlc = Convert.ToInt32(bool.Parse(row[11].ToString()));

                lstMacProp.Add(new MachineProperties(idm.ToString(), art, ph));

                //states that defines machine color
                _machinesState.Add(new int[] { idm, tic, cic, mvc, mlc });
                _arrayList.Add(new int[] { idm, auto, als, alf, alm });
                //states that defines machine color

                if (ts.Date != DateTime.Now.Date)
                    _currentMachines.Add(idm);
            }

            _machines = new List<Label>();

            foreach (var panel in _prod_panels)
            {
                ProductionLines newLine = new ProductionLines();

                int _panel_index = int.Parse(panel.Name.Substring(6));
                string name = "Line_" + _panel_index;
                newLine.LineName = name;

                foreach (var machine in (from lbl in panel.Controls.OfType<Label>()
                                         where lbl.Tag != null
                                         select lbl).ToList())
                {
                    int.TryParse(machine.Tag.ToString(), out var idm);


                    if (!string.IsNullOrEmpty(machine.Tag.ToString()))
                    {
                        _machines.Add(machine);
                        newLine.SetLineMachine(idm);
                    }
                }
                _prodLines.Add(newLine);
            }

            foreach (var mac in _machines)
            {
                int.TryParse(mac.Tag.ToString(), out var machineId);

                var actArray = (from list in _arrayList
                                where list[0] == machineId
                                select list).SingleOrDefault();
                var macStateArray = (from mach in _machinesState
                                     where mach[0] == machineId
                                     select mach).SingleOrDefault();

                if (!_currentMachines.Contains(machineId) && actArray != null)
                {
                    mac.BackColor = GetColorByState(machineId, actArray, macStateArray);
                    mac.ForeColor = Color.Black;
                }
                else
                {
                    mac.BackColor = Color.Gray;
                    mac.ForeColor = Color.Black;
                }

                mac.Click += delegate
                {
                    if (mac.Tag == null) return;

                    var testQuery = (from idm in Tables.TblMachines
                                     where mac.Tag.ToString() == idm.Idm
                                     select idm).SingleOrDefault();

                    if (testQuery == null) return;

                    var machineAddition = (from macProp in lstMacProp
                                           where macProp.Idm == mac.Tag.ToString()
                                           select macProp).SingleOrDefault();

                    if (machineAddition == null) return;

                    var _line = (from line in _prodLines
                                 where line.GetLineMachines().Contains(machineId)
                                 select line).SingleOrDefault();

                    string _gauge_index = _line.LineName.Substring(5);

                    var _targeted_gauge = (from g in _gauges
                                           where g.Name.Substring(6) == _gauge_index
                                           select g).SingleOrDefault();

                    var macEff = MachineEfficiency(int.Parse(machineAddition.Idm), machineAddition.Art, machineAddition.Ph);
                    _targeted_gauge.Value = macEff;

                    foreach (AGaugeLabel l in _targeted_gauge.GaugeLabels)
                        l.Text = "Machine nr." + mac.Tag.ToString() + " | " + "Eff." + macEff.ToString() + "%";
                };

                //Show information about machine                 
                mac.MouseHover += (s, ev) =>
                {
                    if (mac.Tag == null) return;

                    var testQuery = (from idm in Tables.TblMachines
                                     where mac.Tag.ToString() == idm.Idm
                                     select idm).SingleOrDefault();

                    if (testQuery == null) return;

                    Machines mach = Exacta.Menu.db.GetTable<Machines>().SingleOrDefault(m => m.Idm.ToString() == mac.Tag.ToString());

                    var machineAddition = (from macProp in lstMacProp
                                           where macProp.Idm == mach.Idm
                                           select macProp).SingleOrDefault();

                    if (machineAddition == null) return;

                    DataTable tblCuts = new DataTable();

                    var con = new SqlConnection(Exacta.Menu.connectionString);
                    SqlCommand cmd = new SqlCommand("sp_getqt", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MachineID", SqlDbType.Int, 50).Value = Int16.Parse(mac.Tag.ToString());
                    cmd.Parameters.Add("@Article", SqlDbType.NVarChar, 50).Value = machineAddition.Art;
                    cmd.Parameters.Add("@Phase", SqlDbType.NVarChar, 50).Value = machineAddition.Ph;
                    cmd.Parameters.Add("@startDate", SqlDbType.NVarChar).Value = Exacta.Menu.dateFrom.ToString("MM/dd/yyyy");
                    cmd.Parameters.Add("@endDate", SqlDbType.NVarChar).Value = Exacta.Menu.dateTo.ToString("MM/dd/yyyy");

                    con.Open();
                    var dr = cmd.ExecuteReader();
                    tblCuts.Load(dr);
                    dr.Close();
                    con.Close();

                    if (tblCuts == null || tblCuts.Rows.Count <= 0) return;

                    int.TryParse(tblCuts.Rows[0][0].ToString(), out var qt);

                    ToolTip toolTip1 = new ToolTip();
                    toolTip1.SetToolTip(mac, "Article:" + machineAddition.Art + "\nPhase: " + machineAddition.Ph
                                        + "\nQt: " + qt);
                    toolTip1.AutoPopDelay = 30000;
                };
            }

        }

        private Color GetColorByState(int machineId, int[] arr, int[] macArr)
        {
            var color = Color.Gainsboro;

            //define machine states
            var lstOfArrs = new List<int[]>();
            var arr1 = new int[] { machineId, 0, 0, 0, 0 };
            var arr2 = new int[] { machineId, 1, 0, 0, 0 };

            lstOfArrs.Add(arr1);
            lstOfArrs.Add(arr2);

            foreach (var array in lstOfArrs)
            {
                if (arr.SequenceEqual(arr1)) color = Color.Yellow;
                if (arr.SequenceEqual(arr2)) color = Color.YellowGreen;
            }
            var stopTimeArr = new int[] { machineId, 0, 0, 0, 0 };
            if (macArr.SequenceEqual(stopTimeArr))
                color = Color.Red;

            return color;
        }

        protected override void OnLoad(EventArgs e)
        {
            InstallLines();
            GetCurrentDataProcedure();
            base.OnLoad(e);
        }

        private void label104_Click_1(object sender, EventArgs e)
        {
            var _clicked_line = (Label)sender;
            var _line = (from l in _prodLines
                         where l.LineName == _clicked_line.Name
                         select l).SingleOrDefault();

            if (_line == null) return;
            if (_line.GetLineMachines().Count() <= 0) return;

            string _gauge_index = _line.LineName.Substring(5);
            var _tageted_gauge = (from g in _gauges
                                  where g.Name.Substring(6) == _gauge_index
                                  select g).SingleOrDefault();

            LineEfficiency(_line.LineName);
            _tageted_gauge.Value = _line.LineEfficiency;

            foreach (AGaugeLabel l in _tageted_gauge.GaugeLabels)
                l.Text = _line.LineName.Replace("_", " ") + " | " + "Eff." + _line.LineEfficiency + "%";
        }

        private void p1_lbl5_Paint(object sender, PaintEventArgs e)
        {
            var lbl = (Label)sender;

            e.Graphics.Clear(Color.Gainsboro);

            var rect = lbl.ClientRectangle;
            rect.Width--;
            rect.Height--;

            var brush = new SolidBrush(lbl.BackColor);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.FillEllipse(brush, rect);
            e.Graphics.DrawEllipse(new Pen(Brushes.Black, 1), rect);
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(rect);

            //Writting machine ID inside ellipse
            if (lbl.Tag != null)
            {
                Font font = new Font("Arial", 8, FontStyle.Bold, GraphicsUnit.Point);

                var mesW = e.Graphics.MeasureString(lbl.Tag.ToString(), font).Width;
                var mesH = e.Graphics.MeasureString(lbl.Tag.ToString(), font).Height;

                e.Graphics.DrawString(lbl.Tag.ToString(), font, Brushes.Black, lbl.Width / 2 - mesW / 2, lbl.Height / 2 - mesH / 2);
            }
        }

        private void p1_lbl1_DoubleClick(object sender, EventArgs e)
        {
            var lbl = (Label)sender;
            int.TryParse(lbl.Tag.ToString(), out var machineId);

            if (machineId == 0) return;

            var f = new Intervals();
            f.SingleSelectedMachine = machineId;
            f.Size = new Size(1350, 700);
            f.ShowDialog();
            f.Dispose();
        }
    }
    public class ProductionLines
    {
        private List<int> _line_machines;

        public ProductionLines()
        {
            _line_machines = new List<int>(10);
            LineName = string.Empty;
            LineEfficiency = 0;
        }

        public string LineName { get; set; }
        public int LineEfficiency { get; set; }

        public List<int> GetLineMachines()
        {
            return _line_machines;
        }
        public void SetLineMachine(int machine)
        {
            _line_machines.Add(machine);
            _line_machines.Sort();
        }
    }
    public class MachineProperties
    {
        public MachineProperties() { }
        public MachineProperties(int c)
        {
            Cut = c;
        }
        public MachineProperties(string idm, string art, string ph)
        {
            Idm = idm;
            Art = art;
            Ph = ph;
        }

        public string Idm { get; set; }
        public string Art { get; set; }
        public string Ph { get; set; }
        public int Cut { get; set; }
    }

}
