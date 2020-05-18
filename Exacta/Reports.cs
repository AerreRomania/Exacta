using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Exacta
{
    public partial class Reports : Form
    {
        List<CheckBox> _modes = new List<CheckBox>();
        DataTable _table_reports = new DataTable();
        System.Drawing.Rectangle _headerCell = new System.Drawing.Rectangle();
        List<ComboBox> _filters = new List<ComboBox>();
        DataTable tbl_rep_operators = new DataTable();

        public Reports()
        {
            InitializeComponent();

            dataGridView1.DataBindingComplete += delegate
            {
                foreach (DataGridViewColumn c in dataGridView1.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dataGridView1.RowHeadersVisible = false;
                }
                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            };

            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView1.ColumnHeadersHeight = this.dataGridView1.ColumnHeadersHeight * 2 + 20;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.BackgroundColor = Color.FromArgb(235, 235, 235);
            dataGridView1.RowsDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dataGridView1.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.SteelBlue;
            dataGridView1.EnableHeadersVisualStyles = true;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            AddDataTableColumns();
        }

        //private void ReadModes()
        //{
        //    bool opener = false;
        //    bool noChecks = true;
        //    var columnName = string.Empty;

        //    foreach (var mode in _modes)
        //    {
        //        if (!mode.Checked)
        //        {
        //            columnName = mode.Name.Remove(0, 2);
        //            dataGridView1.Columns[columnName].Visible = false;
        //            continue;
        //        }
        //        else
        //        {
        //            noChecks = false;
        //            if (opener)
        //            {
        //                for (var position = 6; position < dataGridView1.Columns.Count; position++)
        //                    dataGridView1.Columns[position].Visible = true;
        //            }
        //            else
        //            {
        //                //for (var position = 6; position < dataGridView1.Columns.Count; position++)
        //                dataGridView1.Columns[columnName].Visible = false;
        //            }

        //            columnName = mode.Name.Remove(0, 2);
        //            dataGridView1.Columns[columnName].Visible = true;
        //        }
        //    }

        //    if (noChecks)
        //        foreach (DataGridViewColumn column in dataGridView1.Columns)
        //            column.Visible = false;
        //}

        private void AddDataTableColumns()
        {
            _table_reports = new DataTable();
            _table_reports.Columns.Add("Line");
            _table_reports.Columns.Add("Machine");
            _table_reports.Columns.Add("Operator");
            _table_reports.Columns.Add("Order");
            _table_reports.Columns.Add("Article");
            _table_reports.Columns.Add("Phase");
            _table_reports.Columns.Add("CapiH");
            _table_reports.Columns.Add("WorkedHours");
            _table_reports.Columns.Add("TargetQty");
            _table_reports.Columns.Add("QtyProd");
            _table_reports.Columns.Add("Diff");
            _table_reports.Columns.Add("OperatorEff");

            GetData();
            //ReadModes();
        }
        private void AddDataTableColumnsOp()
        {
            tbl_rep_operators = new DataTable();
            tbl_rep_operators.Columns.Add("Operator");//0
            tbl_rep_operators.Columns.Add("Line");
            tbl_rep_operators.Columns.Add("Machine");
            tbl_rep_operators.Columns.Add("Order");
            tbl_rep_operators.Columns.Add("Article");//4
            tbl_rep_operators.Columns.Add("Phase");
            tbl_rep_operators.Columns.Add("CapiH");
            tbl_rep_operators.Columns.Add("Hours");
            tbl_rep_operators.Columns.Add("QtyProd");
            tbl_rep_operators.Columns.Add("OperatorEff");

            OperatorEff();
            //ReadModes();
        }

        private void GetData()
        {
            var _tmpTable = new DataTable();

            var con = new SqlConnection(Exacta.Menu.connectionString);
            var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "spReportsData";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@datefrom", SqlDbType.NVarChar).Value = Exacta.Menu.dateFrom.ToString("MM/dd/yyyy");
            cmd.Parameters.Add("@dateTo", SqlDbType.NVarChar).Value = Exacta.Menu.dateTo.ToString("MM/dd/yyyy");

            con.Open();
            var dr = cmd.ExecuteReader();
            _tmpTable.Load(dr);
            dr.Close();
            con.Close();

            if (_tmpTable.Rows.Count <= 0) return;

            var startLine = _tmpTable.Rows[0][6].ToString();
            var startIdm = _tmpTable.Rows[0][0].ToString();
            var startArticle = _tmpTable.Rows[0][4].ToString();
            var startPhase = _tmpTable.Rows[0][5].ToString();
            int.TryParse(_tmpTable.Rows[0][9].ToString(), out var startOrder);
            float.TryParse(_tmpTable.Rows[0][8].ToString(), out var startPieces);
            int.TryParse(_tmpTable.Rows[0][1].ToString(), out var startHours);
            var startOperators = _tmpTable.Rows[0][7].ToString();
            int cuts = 0;

            var hoursWorked = 0;
            var newRow = _table_reports.NewRow();

            foreach (DataRow row in _tmpTable.Rows)
            {
                string idm = row[0].ToString();//1
                int.TryParse(row[1].ToString(), out var hours);
                bool.TryParse(row[3].ToString(), out var tic);
                string article = row[4].ToString();//3
                string phs = row[5].ToString();
                string line = row[6].ToString();
                string operators = row[7].ToString();//2
                float.TryParse(row[8].ToString(), out var pieces);
                int.TryParse(row[9].ToString(), out var ord);

                if (startHours != hours) hoursWorked++;
                if (line != startLine || phs != startPhase || article != startArticle ||
                operators != startOperators || idm != startIdm)
                {
                    if (idm != startIdm) hoursWorked--;
                    newRow[0] = startLine;
                    newRow[1] = startIdm;
                    newRow[2] = startOperators;
                    newRow[3] = ord.ToString();
                    newRow[4] = startArticle;
                    newRow[5] = startPhase;
                    newRow[6] = startPieces.ToString();
                    newRow[7] = hoursWorked.ToString();
                    newRow[8] = hoursWorked * startPieces;
                    newRow[9] = cuts.ToString();
                    if ((hoursWorked * startPieces - cuts) > 0)
                        newRow[10] = (hoursWorked * startPieces - cuts) * -1;
                    else newRow[10] = "+" + ((hoursWorked * startPieces - cuts) * -1).ToString();
                    newRow[11] = Math.Round(cuts / (hoursWorked * startPieces) * 100, 2).ToString() + "%";

                    if (hoursWorked > 0) _table_reports.Rows.Add(newRow);
                    newRow = _table_reports.NewRow();

                    cuts = 0;
                    hoursWorked = 0;
                }
                if (tic) cuts++;
                startLine = line;
                startPhase = phs;
                startArticle = article;
                startOperators = operators;
                startIdm = idm;
                startPieces = pieces;
                startOrder = ord;
                startHours = hours;
            }
            newRow[0] = startLine;
            newRow[1] = startIdm;
            newRow[2] = startOperators;
            newRow[3] = startOrder.ToString();
            newRow[4] = startArticle;
            newRow[5] = startPhase;
            newRow[6] = startPieces.ToString();
            newRow[7] = hoursWorked.ToString();
            newRow[8] = hoursWorked * startPieces;
            newRow[9] = cuts.ToString();
            if ((hoursWorked * startPieces - cuts) > 0)
                newRow[10] = (hoursWorked * startPieces - cuts) * -1;
            else newRow[10] = "+" + ((hoursWorked * startPieces - cuts) * -1).ToString();
            newRow[11] = Math.Round(cuts / (hoursWorked * startPieces) * 100, 2).ToString() + "%";
            cuts = 0;
            if (hoursWorked > 0) _table_reports.Rows.Add(newRow);
            dataGridView1.DataSource = _table_reports;

            for (int i = 0; i <= 11; i++)
            {
                if (i == 11)
                {
                    dataGridView1.Columns[i].Width = 90;
                }
                if (i == 0 || i == 2 || i == 4)
                {
                    dataGridView1.Columns[i].Width = 120;
                }
                if (i == 3 || i == 5)
                {
                    dataGridView1.Columns[i].Width = 100;
                }
                if (i == 1 || i >= 6 && i <= 10)
                {
                    dataGridView1.Columns[i].Width = 60;
                    if (i == 7)
                        dataGridView1.Columns[i].Width = 90;
                }
                //dataGridView1.Columns[i].Visible = false; 
            }
        }
       
        public void OperatorEff()
        { 
            decimal eff = 0;
            decimal totaleff =0;
            decimal h=0;
            int c = 0;
            decimal starteff = 0;
            dataGridView1.Sort(dataGridView1.Columns["Operator"], ListSortDirection.Descending);
            var startOp = dataGridView1.Rows[0].Cells["Operator"].Value.ToString();
            decimal.TryParse(dataGridView1.Rows[0].Cells["OperatorEff"].Value.ToString().Trim('%'), out starteff);
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                var op = r.Cells["Operator"].Value.ToString();
                decimal.TryParse(r.Cells["OperatorEff"].Value.ToString().Trim('%'),out eff);
                decimal.TryParse(r.Cells["WorkedHours"].Value.ToString(), out h);
                if (startOp != op)
                { 
                    if (c > 1)
                    {
                        var newRow1 = tbl_rep_operators.NewRow();
                        for (int i = 0; i <= tbl_rep_operators.Columns.Count - 2; i++)
                        {
                            newRow1[i] = "";
                        }
                        newRow1[8] = "Total Eff:";
                        newRow1[9] = totaleff + "%";
                        tbl_rep_operators.Rows.Add(newRow1);
                        totaleff = 0;
                        startOp = op;
                        starteff = eff;
                    }
                else 
                    {
                        var newRow1 = tbl_rep_operators.NewRow();
                        for (int i = 0; i <= tbl_rep_operators.Columns.Count - 2; i++)
                        {
                            newRow1[i] = "";
                        }
                        newRow1[8] = "Total Eff:";
                        newRow1[9] = starteff + "%";
                        tbl_rep_operators.Rows.Add(newRow1);
                        totaleff = 0;
                        startOp = op;
                        starteff = eff;
                    }
                    c = 0;
                }
                    if (startOp == op) c++;
                    var newRow = tbl_rep_operators.NewRow();
                    newRow[0] = r.Cells[2].Value.ToString();
                    newRow[1] = r.Cells[0].Value.ToString();
                    newRow[2] = r.Cells[1].Value.ToString();
                    newRow[3] = r.Cells[3].Value.ToString();
                    newRow[4] = r.Cells[4].Value.ToString();
                    newRow[5] = r.Cells[5].Value.ToString();
                    newRow[6] = r.Cells[6].Value.ToString();
                    newRow[7] = r.Cells[7].Value.ToString();
                    newRow[8] = r.Cells[9].Value.ToString();
                    newRow[9] = r.Cells[11].Value.ToString();
                    tbl_rep_operators.Rows.Add(newRow);
                    totaleff += Math.Round(eff / h, 2);
            }
            var newRow2 = tbl_rep_operators.NewRow();
            for (int i = 0; i <= tbl_rep_operators.Columns.Count - 2; i++)
            {
                newRow2[i] = "";
            }
            newRow2[8] = "Total Eff:";
            newRow2[9] = totaleff + "%";
            tbl_rep_operators.Rows.Add(newRow2);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = tbl_rep_operators;
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (i == 0 || i == 4 || i == 8 || i == 9)
                {
                    dataGridView1.Columns[i].Width = 100;
                    dataGridView1.Columns[0].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point);
                }
                else dataGridView1.Columns[i].Width = 60;
            }

            string txt1 = "";
            string txt2 = "Total Eff:";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                txt1 = dataGridView1.Rows[i].Cells[8].Value.ToString();
                if (txt1 == txt2)
                {
                    dataGridView1.Rows[i].Cells[8].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point);
                    dataGridView1.Rows[i].Cells[9].Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point);

                }
            }
        }
        
        private void CreateHeaderFilters(DataGridView dgv)
        {
            if (_filters.Count >= 1)
                foreach (var filter in _filters)
                {
                    var cb = (from c in dgv.Controls.OfType<ComboBox>()
                              where c.Name == filter.Name
                              select c).SingleOrDefault();
                    dgv.Controls.Remove(cb);
                }

            var startIdx = 0;
            foreach (DataGridViewColumn visColumn in dgv.Columns)
            {
                if (visColumn.Visible == true)
                {
                    startIdx = visColumn.Index;
                    break;
                }
            }

            _headerCell = dgv.GetCellDisplayRectangle(startIdx, -1, true);

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible == true)
                {
                    ComboBox cb = new ComboBox();
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                    cb.Name = "cb_" + col.Name;
                    dgv.Controls.Add(cb);
                    cb.Location = new Point(_headerCell.Location.X, _headerCell.Location.Y + _headerCell.Height - cb.Height - 1);
                    cb.Size = new Size(col.Width - 1, 10);
                    _headerCell.X += col.Width;

                    _filters.Add(cb);
                    Additional.FillTheFilter(dgv, cb, col.Index);

                    cb.SelectedIndexChanged += delegate
                    {
                        if (!string.IsNullOrEmpty(cb.Text))
                        {
                            BindingSource bs = new BindingSource();
                            bs.DataSource = dgv.DataSource;
                            bs.Filter = string.Format("CONVERT(" + dgv.Columns[col.Name].DataPropertyName +
                                                      ", System.String) like '" + cb.Text.Replace("'", "''") + "'");
                            dgv.DataSource = bs;
                        }
                        else
                        {
                            if (dgv.DataSource != null)
                                dgv.DataSource = null;

                            AddDataTableColumns();
                            CreateHeaderFilters(dataGridView1);
                        }
                    };
                }
            }
        }

        private void ExportToExcel(DataTable dt)
        {
            Microsoft.Office.Interop.Excel._Application excl = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excl.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            excl.Visible = false;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Exacta Report";

            var currentPosition = 1;
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                if (dataGridView1.Columns[i - 1].Visible)
                {
                    worksheet.Cells[1, currentPosition] = dataGridView1.Columns[i - 1].HeaderText;
                    currentPosition++;
                }
            }

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                currentPosition = 0;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (dataGridView1.Columns[j].Visible)
                    {
                        worksheet.Cells[i + 2, currentPosition + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        currentPosition++;
                    }
                }
            }

            excl.Columns.AutoFit();

            string filePath = string.Empty;

            var save_file_dialog = new SaveFileDialog();
            //save_file_dialog.FileName = "Reports " + DateTime.Now.Date.ToShortDateString();
            save_file_dialog.FileName = "Reports " + DateTime.Now.Date.ToString("dd'-'MM'-'yyyy");
            save_file_dialog.DefaultExt = ".xlsx";
            save_file_dialog.Filter = "Excel File|*.xlsx|All Files|*.*";

            if (save_file_dialog.ShowDialog() == DialogResult.OK)
            {
                filePath = save_file_dialog.FileName;
                excl.ActiveWorkbook.SaveCopyAs(filePath);
                excl.ActiveWorkbook.Saved = true;
                excl.Quit();
            }
            save_file_dialog.Dispose();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            
            foreach (CheckBox c in tableLayoutPanel2.Controls.OfType<CheckBox>())
                c.Checked = true;

            AddDataTableColumns();

            foreach (CheckBox cb in tableLayoutPanel2.Controls.OfType<CheckBox>())
                _modes.Add(cb);

            dataGridView1.DoubleBuffered(true);

            

        }
        private void ExportReports_To_PDF()
        {
            System.Drawing.Bitmap screenshot_bmp = PrintScreen();
            iTextSharp.text.Image screenshot_pdf = iTextSharp.text.Image.GetInstance(screenshot_bmp,
                                               System.Drawing.Imaging.ImageFormat.Bmp);

            var save_file_dialog = new SaveFileDialog();
            save_file_dialog.FileName = "Reports " + DateTime.Now.Date.ToString("dd'-'MM'-'yyyy");
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

        private void pbExcel_MouseEnter(object sender, EventArgs e)
        {
            ControlPaint.DrawBorder(pictureBox1.CreateGraphics(), pictureBox1.ClientRectangle, Color.Orange, ButtonBorderStyle.Inset);
        }

        private void pbExcel_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void pbPdf_MouseEnter(object sender, EventArgs e)
        {
            ControlPaint.DrawBorder(pictureBox2.CreateGraphics(), pictureBox2.ClientRectangle, Color.Orange, ButtonBorderStyle.Inset);
        }

        private void pbPdf_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ExportToExcel(_table_reports);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ExportReports_To_PDF();
        }

        private void Modes_CheckChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            var name = cb.Name.Remove(0, 2);

            if (cb.Checked)
            {
                for (var position = 6; position < dataGridView1.Columns.Count; position++)
                {
                    dataGridView1.Columns[position].Visible = true;
                }
                   dataGridView1.Columns[name].Visible = true;
                
            }
            else
                dataGridView1.Columns[name].Visible = false;

            bool noChecks = true;
            foreach (var mode in _modes)
            {
                if (mode.Checked)
                {
                    noChecks = false;
                    break;
                }
            }

            if (noChecks)
                for (var position = 6; position < dataGridView1.Columns.Count; position++)
                    dataGridView1.Columns[position].Visible = true;

            CreateHeaderFilters(dataGridView1);
        }
        int btnclick = 1;
        private void button1_Click(object sender, EventArgs e)
        {   if (btnclick == 1)
            {
                //dataGridView1.DataSource = null;
                AddDataTableColumnsOp();
                //dataGridView1.DataSource = tbl_rep_operators;
                CreateHeaderFilters(dataGridView1);
                btnclick++;
                button1.Text = "Go Back";
;            }
            else
            {
                LoadingInfo.InfoText = "Loading reports...";
                LoadingInfo.ShowLoading();
                dataGridView1.DataSource = null;
                AddDataTableColumns();
               // dataGridView1.DataSource = _table_reports;
                CreateHeaderFilters(dataGridView1);
                btnclick = 1;
                button1.Text = "Report by Operator";
                LoadingInfo.CloseLoading();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Sort(dataGridView1.Columns["Operator"], ListSortDirection.Ascending);
        }
    }

}
