using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Exacta.DatabaseTableClasses;

namespace Exacta
{
    public partial class Orders : Form
    {
        private DataTable _orders = new DataTable();

        public Orders()
        {
            InitializeComponent();
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            dgvOrders.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn c in dgvOrders.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvOrders.RowHeadersVisible = false;
                }
                dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                dgvOrders.GridColor = Color.Silver;
                dgvOrders.RowTemplate.Height = 25;
                dgvOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised; //Sunken
                dgvOrders.CellBorderStyle = DataGridViewCellBorderStyle.Raised; //Sunken
                dgvOrders.EnableHeadersVisualStyles = false;
                dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.Gold;
                dgvOrders.DefaultCellStyle.BackColor = dgvOrders.BackgroundColor;
                dgvOrders.RowHeadersVisible = false;

                for (var i = 0; i <= dgvOrders.Columns.Count - 1; i++)
                {
                    var c = dgvOrders.Columns[i];
                    c.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8);
                    c.HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 7, FontStyle.Regular);
                }
            };
            dgvOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.AllowUserToAddRows = false;
            dgvOrders.AllowUserToDeleteRows = false;
            dgvOrders.AllowUserToResizeColumns = false;
            dgvOrders.AllowUserToResizeRows = false;
            dgvOrders.AllowUserToOrderColumns = false;
            dgvOrders.ReadOnly = true;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.MultiSelect = false;
            dgvOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            //dgv_Cuts.ColumnHeadersHeight = this.dgv_Cuts.ColumnHeadersHeight + 5;
            dgvOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            dgvOrders.BackgroundColor = Color.FromArgb(235, 235, 235);
            dgvOrders.RowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvOrders.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dgvOrders.RowHeadersDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dgvOrders.EnableHeadersVisualStyles = true;
            dgvOrders.BorderStyle = BorderStyle.None;
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            GetData();
            dgvOrders.DoubleBuffered(true);
            EnableOrdersFields(false);

            btnSaveOrder.Enabled = false;
            btnCancelOrder.Enabled = false;
            btnAddOrder.Enabled = true;
            btnEditOrder.Enabled = true;
            btnDeleteOrder.Enabled = true;
         //  Exacta.Menu.Expiration();
        }

        private void GetData()
        {
            _orders = new DataTable();

            _orders.Columns.Add("Id", typeof(int));
            _orders.Columns.Add("Nr Crt", typeof(int));
            _orders.Columns.Add("Nr Order", typeof(int));
            _orders.Columns.Add("Client");
            _orders.Columns.Add("Article");
            _orders.Columns.Add("Date Arrival");

            var con = new SqlConnection(Exacta.Menu.connectionString);
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from Orders";
            cmd.CommandType = CommandType.Text;
            
            con.Open();
            var dr = cmd.ExecuteReader();

            var nrCrt = 0;

            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    var newRow = _orders.NewRow();

                    nrCrt++;
                    int.TryParse(dr[0].ToString(), out var id);
                    int.TryParse(dr[1].ToString(), out var orderNumber);
                    string client = dr[2].ToString();
                    string article = dr[3].ToString();
                    DateTime.TryParse(dr[4].ToString(), out var dbDateTimeArrival);
                    var dateArrival = dbDateTimeArrival.ToShortDateString();

                    newRow[0] = id;
                    newRow[1] = nrCrt;
                    newRow[2] = orderNumber;
                    newRow[3] = client;
                    newRow[4] = article;
                    newRow[5] = dateArrival;

                    _orders.Rows.Add(newRow);
                }
            }
            dr.Close();
            con.Close();

            PopulateClientData();
            PopulateArticleData();

            dgvOrders.DataSource = _orders;
            dgvOrders.Columns[0].Visible = false;
            dgvOrders.Columns[1].Width = 60;
            dgvOrders.Columns[2].Width = 60;
            dgvOrders.Columns[3].Width = 120;
            dgvOrders.Columns[4].Width = 120;
            dgvOrders.Columns[5].Width = 130;
        }

        private void EnableOrdersFields(bool enable)
        {
            txtNrOrder.Enabled = enable;
            cbClient.Enabled = enable;
            cbArticle.Enabled = enable;
            dtpDateArrival.Enabled = enable;
        }

        private void PopulateClientData()
        {
            var clientGroup = (from client in Tables.TblClients
                        select client).ToList();

            cbClient.Items.Clear();
            var i = 0;
            foreach (var c in clientGroup)
            {
                cbClient.Items.Insert(i++, c.Name);
            }
            if (cbClient.Items.Count != 0)
            {
                cbClient.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Please define Clients first! \n(Go to Settings, Clients tab)", "Atention! No Clients found!");
                this.Close();
            }

        }

        private void PopulateArticleData()
        {
            var articleGroup = (from article in Tables.TblArticole
                               select article).ToList();

            cbArticle.Items.Clear();
            var i = 0;
            foreach (var a in articleGroup)
            {
                cbArticle.Items.Insert(i++, a.Articol);
            }
            if (cbArticle.Items.Count != 0)
            {
                cbArticle.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Please define Articles first \n(Go to Articles)", "Atention! No Articles found!");
                this.Close();
            }
        }
        private void btnAddOrder_Click(object sender, EventArgs e)
        { 
            _isNewOrder = true;
            txtNrOrder.Text = string.Empty;
            cbArticle.Text = string.Empty;
            cbClient.Text = string.Empty;
            txtNrOrder.Focus();
            dtpDateArrival.Value = DateTime.Now;
            btnSaveOrder.Enabled = true;
            btnCancelOrder.Enabled = true;
            btnEditOrder.Enabled = false;
            btnDeleteOrder.Enabled = false;
            btnAddOrder.Enabled = false;
            EnableOrdersFields(true);
        }

        private bool _isNewOrder = false;
        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            if (_isNewOrder)
            {
                    var order = new Order();
                    
                    if(cbClient.Text == string.Empty || cbArticle.Text == string.Empty
                    || txtNrOrder.Text == string.Empty)
                    {
                    MessageBox.Show("Fields can't be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    order.OrderNumber = int.Parse(txtNrOrder.Text);
                    order.DateArrival = dtpDateArrival.Value;
                    order.Client = cbClient.Text;
                    order.Article = cbArticle.Text;

                Tables.TblOrder.InsertOnSubmit(order);
                    Exacta.Menu.db.SubmitChanges();
                    
                    _isNewOrder = false;
            }
            else
            {
                var dr = MessageBox.Show("Do you want to update order " + dgvOrders.SelectedRows[0].Cells[2].Value.ToString()
                    + "?", "Order update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.No)
                    return;
                
                    var order = (from ord in Tables.TblOrder
                                where ord.Id == Convert.ToInt32(dgvOrders.SelectedRows[0].Cells[0].Value.ToString())
                                select ord).SingleOrDefault();

                if (cbClient.Text == string.Empty || cbArticle.Text == string.Empty
                    || txtNrOrder.Text == string.Empty)
                {
                    MessageBox.Show("Fields can't be empty!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                order.OrderNumber = int.Parse(txtNrOrder.Text);
                order.DateArrival = dtpDateArrival.Value;
                order.Client = cbClient.Text;
                order.Article = cbArticle.Text;

                Exacta.Menu.db.SubmitChanges();
            }
            GetData();
            btnSaveOrder.Enabled = false;
            btnCancelOrder.Enabled = false;
            btnAddOrder.Enabled = true;
            btnEditOrder.Enabled = true;
            btnDeleteOrder.Enabled = true;
            EnableOrdersFields(false);
        }

        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count <= 0 ||
                dgvOrders.DataSource == null)
                return;

            btnAddOrder.Enabled = false;
            btnEditOrder.Enabled = false;
            btnDeleteOrder.Enabled = false;
            btnSaveOrder.Enabled = true;
            btnCancelOrder.Enabled = true;
            EnableOrdersFields(true);
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count <= 0 ||
                dgvOrders.DataSource == null)
            {
                btnSaveOrder.Enabled = false;
                btnCancelOrder.Enabled = false;
                btnAddOrder.Enabled = true;
                btnEditOrder.Enabled = true;
                btnDeleteOrder.Enabled = true;
            }
            else
            {
                txtNrOrder.Text = dgvOrders.SelectedRows[0].Cells[2].Value.ToString();
                cbClient.Text = dgvOrders.SelectedRows[0].Cells[3].Value.ToString();
                cbArticle.Text = dgvOrders.SelectedRows[0].Cells[4].Value.ToString();
                dtpDateArrival.Value = Convert.ToDateTime(dgvOrders.SelectedRows[0].Cells[5].Value.ToString());

                btnSaveOrder.Enabled = false;
                btnCancelOrder.Enabled = false;
                btnAddOrder.Enabled = true;
                btnEditOrder.Enabled = true;
                btnDeleteOrder.Enabled = true;
            }
            EnableOrdersFields(false);
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count <= 0)
                return;

            txtNrOrder.Text = dgvOrders.SelectedRows[0].Cells[2].Value.ToString();
            cbClient.Text = dgvOrders.SelectedRows[0].Cells[3].Value.ToString();
            cbArticle.Text = dgvOrders.SelectedRows[0].Cells[4].Value.ToString();
            dtpDateArrival.Value = Convert.ToDateTime(dgvOrders.SelectedRows[0].Cells[5].Value.ToString());
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.DataSource == null ||
                dgvOrders.Rows.Count <= 0)
                return;
            
            var dr = MessageBox.Show("Are you sure you want to delete order " + dgvOrders.SelectedRows[0].Cells[2].Value.ToString()
                + "?", "Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
                return;

            var order = (from ord in Tables.TblOrder
                        where ord.Id == Convert.ToInt32(dgvOrders.SelectedRows[0].Cells[0].Value.ToString())
                        select ord).SingleOrDefault();

            Tables.TblOrder.DeleteOnSubmit(order);

            Exacta.Menu.db.SubmitChanges();
            cbArticle.Text = string.Empty;
            txtNrOrder.Text = string.Empty;
            cbClient.Text = string.Empty;
            dtpDateArrival.Value = DateTime.Now;

            GetData();
        }
    }
}
