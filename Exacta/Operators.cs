using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Exacta.DatabaseTableClasses;

namespace Exacta
{
    public partial class Operators : Form
    {
        private DataTable _operators = new DataTable();

        public Operators()
        {
            InitializeComponent();
        }        

        private void GetData()
        {
            _operators = new DataTable();

            _operators.Columns.Add("Code", typeof(int));
            _operators.Columns.Add("Name");
            _operators.Columns.Add("Surname");
            _operators.Columns.Add("Nr Tel");
            _operators.Columns.Add("Address");
            _operators.Columns.Add("Line");

            var con = new SqlConnection(Exacta.Menu.connectionString);
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from Operators";
            cmd.CommandType = CommandType.Text;

            con.Open();
            var dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    var newRow = _operators.NewRow();

                    int.TryParse(dr[0].ToString(), out var operatorId);
                    string name = dr[1].ToString();
                    string surname = dr[2].ToString();
                    string telephone = dr[3].ToString();
                    string address = dr[4].ToString();
                    string line = dr[5].ToString();

                    newRow[0] = operatorId;
                    newRow[1] = name;
                    newRow[2] = surname;
                    newRow[3] = telephone;
                    newRow[4] = address;
                    newRow[5] = line;

                    _operators.Rows.Add(newRow);
                }
            }

            PopulateLineData();

            dr.Close();
            con.Close();

            dgvOperators.DataSource = _operators;
            dgvOperators.Columns[0].Width = 60;
        }

        private void EnableOperatorsFields(bool enable)
        {
            txtName.Enabled = enable;
            txtSurname.Enabled = enable;
            txtAddress.Enabled = enable;
            txtTel.Enabled = enable;
            cbLine.Enabled = enable;
        }

        private void PopulateLineData()
        {
            var lineGroup = (from line in Tables.TblLines
                                select line).ToList();

            cbLine.Items.Clear();
            var i = 0;
            foreach (var l in lineGroup)
            {
                cbLine.Items.Insert(i++, l.LineName);
            }
            if (cbLine.Items.Count != 0)
            {
                cbLine.SelectedIndex = 0;
            }
            else 
            {
                MessageBox.Show("Please define Lines first \n(Go to Settings, Lines tab)", "Atention! No Lines!");
                this.Close();
            }
        }

        private void btnAddOperator_Click(object sender, EventArgs e)
        {
            _isNewOperator = true;

            txtName.Text = string.Empty;
            txtName.Focus();
            txtSurname.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtAddress.Text = string.Empty;
            cbLine.SelectedIndex = 0;            
            
            btnSaveOperator.Enabled = true;
            btnCancelOperator.Enabled = true;
            btnEditOperator.Enabled = false;
            btnDeleteOperator.Enabled = false;
            btnAddOperator.Enabled = false;
            EnableOperatorsFields(true);
        }

        private void btnEditOperator_Click(object sender, EventArgs e)
        {
            if (dgvOperators.SelectedRows.Count <= 0 ||
                dgvOperators.DataSource == null)
                return;

            btnAddOperator.Enabled = false;
            btnEditOperator.Enabled = false;
            btnDeleteOperator.Enabled = false;
            btnSaveOperator.Enabled = true;
            btnCancelOperator.Enabled = true;
            EnableOperatorsFields(true);
        }

        private void btnCancelOperator_Click(object sender, EventArgs e)
        {
            if (dgvOperators.SelectedRows.Count <= 0 ||
                dgvOperators.DataSource == null)
            {
                btnSaveOperator.Enabled = false;
                btnCancelOperator.Enabled = false;
                btnAddOperator.Enabled = true;
                btnEditOperator.Enabled = true;
                btnDeleteOperator.Enabled = true;
            }
            else
            {
                txtName.Text = dgvOperators.SelectedRows[0].Cells[1].Value.ToString();
                txtSurname.Text = dgvOperators.SelectedRows[0].Cells[2].Value.ToString();
                txtTel.Text = dgvOperators.SelectedRows[0].Cells[3].Value.ToString();
                txtAddress.Text = dgvOperators.SelectedRows[0].Cells[4].Value.ToString();
                cbLine.Text = dgvOperators.SelectedRows[0].Cells[5].Value.ToString();

                btnSaveOperator.Enabled = false;
                btnCancelOperator.Enabled = false;
                btnAddOperator.Enabled = true;
                btnEditOperator.Enabled = true;
                btnDeleteOperator.Enabled = true;
            }
            EnableOperatorsFields(false);
        }

        private bool _isNewOperator;
        private void btnSaveOperator_Click(object sender, EventArgs e)
        { 
                if (_isNewOperator)
                {
                    var newOperator = new Operator();

                    if (txtName.Text == string.Empty || txtSurname.Text == string.Empty
                       || txtTel.Text == string.Empty || txtAddress.Text == string.Empty
                       || cbLine.Text == string.Empty)
                    {
                        MessageBox.Show("Fields can't be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    newOperator.Name = txtName.Text;
                    newOperator.Surname = txtSurname.Text;
                    newOperator.Telephone = txtTel.Text;
                    newOperator.Address = txtAddress.Text;
                    newOperator.Line = cbLine.Text;

                    Tables.TblOperator.InsertOnSubmit(newOperator);
                    Exacta.Menu.db.SubmitChanges();

                    _isNewOperator = false;
                }
                else
                {
                    var dr = MessageBox.Show("Do you want to update operator?", "Operator update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.No)
                        return;

                    var updatingOperator = (from oper in Tables.TblOperator
                                            where oper.Id == Convert.ToInt32(dgvOperators.SelectedRows[0].Cells[0].Value.ToString())
                                            select oper).SingleOrDefault();

                    if (txtName.Text == string.Empty || txtSurname.Text == string.Empty
                       || txtTel.Text == string.Empty || txtAddress.Text == string.Empty
                       || cbLine.Text == string.Empty)
                    {
                        MessageBox.Show("Fields can't be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    updatingOperator.Name = txtName.Text;
                    updatingOperator.Surname = txtSurname.Text;
                    updatingOperator.Telephone = txtTel.Text;
                    updatingOperator.Address = txtAddress.Text;
                    updatingOperator.Line = cbLine.Text;

                    Exacta.Menu.db.SubmitChanges();
                }
                GetData();
                btnSaveOperator.Enabled = false;
                btnCancelOperator.Enabled = false;
                btnAddOperator.Enabled = true;
                btnEditOperator.Enabled = true;
                btnDeleteOperator.Enabled = true;
                EnableOperatorsFields(false);
        }

        private void Operators_Load(object sender, EventArgs e)
        {
            dgvOperators.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn c in dgvOperators.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvOperators.RowHeadersVisible = false;
                }
                dgvOperators.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                dgvOperators.GridColor = Color.Silver;
                dgvOperators.RowTemplate.Height = 25;
                dgvOperators.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised; //Sunken
                dgvOperators.CellBorderStyle = DataGridViewCellBorderStyle.Raised; //Sunken
                dgvOperators.EnableHeadersVisualStyles = false;
                dgvOperators.ColumnHeadersDefaultCellStyle.BackColor = Color.Gold;
                dgvOperators.DefaultCellStyle.BackColor = dgvOperators.BackgroundColor;
                dgvOperators.RowHeadersVisible = false;

                for (var i = 0; i <= dgvOperators.Columns.Count - 1; i++)
                {
                    var c = dgvOperators.Columns[i];
                    c.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
                    c.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 7, FontStyle.Regular);
                }
            };
            dgvOperators.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOperators.AllowUserToAddRows = false;
            dgvOperators.AllowUserToDeleteRows = false;
            dgvOperators.AllowUserToResizeColumns = false;
            dgvOperators.AllowUserToResizeRows = false;
            dgvOperators.AllowUserToOrderColumns = false;
            dgvOperators.ReadOnly = true;
            dgvOperators.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOperators.MultiSelect = false;
            dgvOperators.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            //dgv_Cuts.ColumnHeadersHeight = this.dgv_Cuts.ColumnHeadersHeight + 5;
            dgvOperators.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            dgvOperators.BackgroundColor = Color.FromArgb(235, 235, 235);
            dgvOperators.RowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvOperators.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgvOperators.RowHeadersDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvOperators.EnableHeadersVisualStyles = true;
            dgvOperators.BorderStyle = BorderStyle.None;
            dgvOperators.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvOperators.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOperators.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            GetData();
            dgvOperators.DoubleBuffered(true);
            EnableOperatorsFields(false);

            btnSaveOperator.Enabled = false;
            btnCancelOperator.Enabled = false;
            btnAddOperator.Enabled = true;
            btnEditOperator.Enabled = true;
            btnDeleteOperator.Enabled = true;
           //Exacta.Menu.Expiration();
        }

        private void dgvOperators_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOperators.SelectedRows.Count <= 0)
                return;

            txtName.Text = dgvOperators.SelectedRows[0].Cells[1].Value.ToString();
            txtSurname.Text = dgvOperators.SelectedRows[0].Cells[2].Value.ToString();
            txtTel.Text = dgvOperators.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = dgvOperators.SelectedRows[0].Cells[4].Value.ToString();
            cbLine.Text = dgvOperators.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void btnDeleteOperator_Click(object sender, EventArgs e)
        {
            if (dgvOperators.DataSource == null ||
                dgvOperators.Rows.Count <= 0)
                return;
            
            var dr = MessageBox.Show("Are you sure you want to delete operator ?", "Operator", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
                return;

            var removedOperator = (from oper in Tables.TblOperator
                         where oper.Id == Convert.ToInt32(dgvOperators.SelectedRows[0].Cells[0].Value.ToString())
                         select oper).SingleOrDefault();

            Tables.TblOperator.DeleteOnSubmit(removedOperator);
            Exacta.Menu.db.SubmitChanges();

            GetData();
            if (dgvOperators.Rows.Count <= 0)
            {
                txtName.Text = string.Empty;
                txtSurname.Text = string.Empty;
                txtTel.Text = string.Empty;
                txtAddress.Text = string.Empty;
                cbLine.SelectedIndex = 0;
            }
        }
    }
}

