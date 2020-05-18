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

namespace Exacta
{
    public partial class Cuts : Form
    {
        public Cuts()
        {
            InitializeComponent();
        }

        private void Cuts_Load(object sender, EventArgs e)
        {
            dgv_Cuts.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn c in dgv_Cuts.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgv_Cuts.RowHeadersVisible = false;
                }
                dgv_Cuts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                dgv_Cuts.GridColor = Color.Silver;
                dgv_Cuts.RowTemplate.Height = 25;
                dgv_Cuts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised; //Sunken
                dgv_Cuts.CellBorderStyle = DataGridViewCellBorderStyle.Raised; //Sunken
                dgv_Cuts.EnableHeadersVisualStyles = false;
                dgv_Cuts.ColumnHeadersDefaultCellStyle.BackColor = Color.Gold;
                dgv_Cuts.DefaultCellStyle.BackColor = dgv_Cuts.BackgroundColor;
                dgv_Cuts.RowHeadersVisible = false;

                for (var i = 0; i <= dgv_Cuts.Columns.Count - 1; i++)
                {
                    var c = dgv_Cuts.Columns[i];
                    c.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
                    c.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 7, FontStyle.Regular);
                }
            };
            dgv_Cuts.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_Cuts.AllowUserToAddRows = false;
            dgv_Cuts.AllowUserToDeleteRows = false;
            dgv_Cuts.AllowUserToResizeColumns = false;
            dgv_Cuts.AllowUserToResizeRows = false;
            dgv_Cuts.AllowUserToOrderColumns = false;
            dgv_Cuts.ReadOnly = true;
            dgv_Cuts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Cuts.MultiSelect = false;
            dgv_Cuts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            //dgv_Cuts.ColumnHeadersHeight = this.dgv_Cuts.ColumnHeadersHeight + 5;
            dgv_Cuts.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            dgv_Cuts.BackgroundColor = Color.FromArgb(235, 235, 235);
            dgv_Cuts.RowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgv_Cuts.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgv_Cuts.RowHeadersDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgv_Cuts.EnableHeadersVisualStyles = true;
            dgv_Cuts.BorderStyle = BorderStyle.None;
            dgv_Cuts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_Cuts.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv_Cuts.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            GetData();
           // Exacta.Menu.Expiration();
        }
        
        private void GetData()
        {
            var _tblOperation = new DataTable();

            var con = new SqlConnection(Exacta.Menu.connectionString);
            SqlCommand cmd = new SqlCommand("sp_getmachinecuts", con);
            cmd.CommandType = CommandType.StoredProcedure;

            var dateFrom = new DateTime(Exacta.Menu.dateFrom.Year, Exacta.Menu.dateFrom.Month, Exacta.Menu.dateFrom.Day);
            var dateTo = new DateTime(Exacta.Menu.dateTo.Year, Exacta.Menu.dateTo.Month, Exacta.Menu.dateTo.Day);

            cmd.Parameters.Add("@Machine", SqlDbType.Int, 50).Value = Int16.Parse(Production.SelectedMachine);
            cmd.Parameters.Add("@dateFrom", SqlDbType.Date).Value = dateFrom;
            cmd.Parameters.Add("@dateTo", SqlDbType.Date).Value = dateTo;

            con.Open();
            var dr = cmd.ExecuteReader();
            _tblOperation.Load(dr);
            dr.Close();
            con.Close();

            dgv_Cuts.DataSource = _tblOperation;

            for (var i = 0; i <= dgv_Cuts.Columns.Count - 1; i++)
                dgv_Cuts.Columns[i].Width = 135;

            dgv_Cuts.Columns[1].HeaderText = "Phase";
        }

        }
    }
